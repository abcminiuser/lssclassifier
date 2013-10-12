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
            SOURCE_CODE,
            UNKNOWN
        };

        private readonly Regex mSymbolDefRegex = new Regex(@"^[a-f0-9]* <[^>]*>:", RegexOptions.Compiled);
        private readonly Regex mASMLineRegex = new Regex(@"^\s*[a-f0-9]*:\s*", RegexOptions.Compiled);

        public IEnumerable<Tuple<LSSLineTypes, SnapshotSpan>> Parse(SnapshotSpan span)
        {
            foreach (var line in span.Snapshot.Lines)
            {
                LSSLineTypes parsedLineType = ClassifyLine(line.GetText());

                if (parsedLineType != LSSLineTypes.UNKNOWN)
                    yield return new Tuple<LSSLineTypes, SnapshotSpan>(parsedLineType, new SnapshotSpan(span.Snapshot, line.Start, line.Length));
            }
        }

        private LSSLineTypes ClassifyLine(string text)
        {
            if (mSymbolDefRegex.Match(text).Success)
                return LSSLineTypes.SYMBOL_DEF;
            else if (!mASMLineRegex.Match(text).Success)
                return LSSLineTypes.SOURCE_CODE;
            else
                return LSSLineTypes.UNKNOWN;
        }
    }
}
