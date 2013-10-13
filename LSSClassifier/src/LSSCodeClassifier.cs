using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text;
using System.Text.RegularExpressions;

namespace FourWalledCubicle.LSSClassifier
{
    internal sealed class LSSCodeClassifier : IClassifier
    {
        private ITextBuffer mTextBuffer;
        private IClassificationTypeRegistryService mClassificationTypeRegistry;

        private readonly List<ClassificationSpan> classifications = new List<ClassificationSpan>();

        private static readonly Dictionary<LSSParser.LSSLineTypes, string> mClassifierTypeNames = new Dictionary<LSSParser.LSSLineTypes, string>() {
            { LSSParser.LSSLineTypes.SYMBOL_DEF, "lss.symboldef" },
            { LSSParser.LSSLineTypes.ADDRESS, "lss.address" },
            { LSSParser.LSSLineTypes.ENCODING, "lss.encoding" },
            { LSSParser.LSSLineTypes.ASM, "lss.instruction" },
            { LSSParser.LSSLineTypes.COMMENT, "lss.comment" },
            { LSSParser.LSSLineTypes.SOURCE_FRAGMENT, "lss.srccode" },
        };

        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

        public LSSCodeClassifier(ITextBuffer buffer, IClassificationTypeRegistryService classifierTypeRegistry)
        {
            mTextBuffer = buffer;
            mClassificationTypeRegistry = classifierTypeRegistry;
        }

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            classifications.Clear();

            if (span.Length == 0)
                return classifications;

            ITextSnapshotLine line = span.Start.GetContainingLine();

            foreach (Tuple<LSSParser.LSSLineTypes, SnapshotSpan> segment in LSSParser.Parse(line))
            {
                IClassificationType classificationType = mClassificationTypeRegistry.GetClassificationType(mClassifierTypeNames[segment.Item1]);
                classifications.Add(new ClassificationSpan(segment.Item2, classificationType));
            }

            return classifications;
        }
    }
}
