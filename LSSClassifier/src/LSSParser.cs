using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Text;
using System.Text.RegularExpressions;

namespace FourWalledCubicle.LSSClassifier
{
    class LSSParser
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

        private readonly Regex mSymbolDefRegex = new Regex(@"^[a-f0-9]* <[^>]*>:", RegexOptions.Compiled);
        private readonly Regex mASMLineRegex = new Regex(@"^\s*[a-f0-9]*:\s*", RegexOptions.Compiled);

        public IEnumerable<Tuple<LSSLineTypes, SnapshotSpan>> Parse(SnapshotSpan span)
        {
            foreach (var line in span.Snapshot.Lines)
            {
                string text = line.GetText();

                if (mSymbolDefRegex.Match(text).Success)
                {
                    yield return new Tuple<LSSLineTypes, SnapshotSpan>(
                        LSSLineTypes.SYMBOL_DEF, new SnapshotSpan(span.Snapshot, line.Start, line.Length));
                }
                else if (mASMLineRegex.Match(text).Success)
                {
                    string[] codeSections = text.Split('\t');
                    LSSLineTypes? currentType = null;
                    int pos = line.Start;

                    for (int i = 0; i < codeSections.Length; i++)
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
                                if (codeSections[i][0] == ';')
                                    currentType = LSSLineTypes.COMMENT;
                                break;

                            case LSSLineTypes.COMMENT:
                                break;
                        }

                        yield return new Tuple<LSSLineTypes, SnapshotSpan>(
                            currentType.Value, new SnapshotSpan(span.Snapshot, pos, codeSections[i].Length));

                        pos += codeSections[i].Length + 1;
                    }
                }
                else
                {
                    yield return new Tuple<LSSLineTypes, SnapshotSpan>(
                        LSSLineTypes.SOURCE_FRAGMENT, new SnapshotSpan(span.Snapshot, line.Start, line.Length));
                }
            }
        }
    }
}
