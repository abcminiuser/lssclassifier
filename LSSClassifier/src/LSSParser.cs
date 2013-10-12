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
                    yield return new Tuple<LSSLineTypes, SnapshotSpan>(LSSLineTypes.SYMBOL_DEF, new SnapshotSpan(span.Snapshot, line.Start, line.Length));
                }
                else if (mASMLineRegex.Match(text).Success)
                {
                    int commentStart = text.LastIndexOf(';');

                    if (commentStart > 0)
                        yield return new Tuple<LSSLineTypes, SnapshotSpan>(LSSLineTypes.COMMENT, new SnapshotSpan(span.Snapshot, line.Start + commentStart, line.Length - commentStart));                    
                }
                else
                {
                    yield return new Tuple<LSSLineTypes, SnapshotSpan>(LSSLineTypes.SOURCE_FRAGMENT, new SnapshotSpan(span.Snapshot, line.Start, line.Length));
                }
            }
        }
    }
}
