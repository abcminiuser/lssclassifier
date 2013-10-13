using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Text;
using System.Text.RegularExpressions;

namespace FourWalledCubicle.LSSClassifier
{
    internal abstract class LSSParser
    {
        public enum LSSLineTypes
        {
            SYMBOL_DEF,
            ADDRESS,
            ENCODING,
            ASM,
            COMMENT,
            SOURCE_FRAGMENT
        };

        private static readonly Regex mSymbolDefRegex = new Regex(@"^[a-f0-9]* <[^>]*>:", RegexOptions.Compiled);
        private static readonly Regex mASMLineRegex = new Regex(@"^\s*[a-f0-9]*:\s*", RegexOptions.Compiled);

        public static IEnumerable<Tuple<LSSLineTypes, SnapshotSpan>> Parse(ITextSnapshotLine line)
        {
            string text = line.GetText();

            if (mSymbolDefRegex.Match(text).Success)
            {
                yield return new Tuple<LSSLineTypes, SnapshotSpan>(
                    LSSLineTypes.SYMBOL_DEF, new SnapshotSpan(line.Snapshot, line.Start, line.Length));
            }
            else if (mASMLineRegex.Match(text).Success)
            {
                string[] codeSections = text.Split('\t');
                LSSLineTypes? currentType = null;
                int pos = line.Start;

                foreach (string currSection in codeSections)
                {
                    switch (currentType)
                    {
                        case null:
                            currentType = LSSLineTypes.ADDRESS;
                            break;

                        case LSSLineTypes.ADDRESS:
                            currentType = LSSLineTypes.ENCODING;
                            break;

                        case LSSLineTypes.ENCODING:
                            currentType = LSSLineTypes.ASM;
                            break;

                        case LSSLineTypes.ASM:
                            if (currSection.StartsWith(";"))
                                currentType = LSSLineTypes.COMMENT;
                            break;

                        case LSSLineTypes.COMMENT:
                            break;
                    }

                    yield return new Tuple<LSSLineTypes, SnapshotSpan>(
                        currentType.Value, new SnapshotSpan(line.Snapshot, pos, currSection.Length));

                    pos += currSection.Length + 1;
                }
            }
            else
            {
                yield return new Tuple<LSSLineTypes, SnapshotSpan>(
                    LSSLineTypes.SOURCE_FRAGMENT, new SnapshotSpan(line.Snapshot, line.Start, line.Length));
            }
        }
    }
}
