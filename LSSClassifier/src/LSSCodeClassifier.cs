using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;

namespace FourWalledCubicle.LSSClassifier
{
    internal sealed class LSSCodeClassifier : IClassifier
    {
        private static readonly Dictionary<LSSParser.LSSLineTypes, string> mClassifierTypeNames = new Dictionary<LSSParser.LSSLineTypes, string>() {
            { LSSParser.LSSLineTypes.SYMBOL_DEF, "lss.symboldef" },
            { LSSParser.LSSLineTypes.ADDRESS, "lss.address" },
            { LSSParser.LSSLineTypes.ENCODING, "lss.encoding" },
            { LSSParser.LSSLineTypes.ASM, "lss.instruction" },
            { LSSParser.LSSLineTypes.COMMENT, "lss.comment" },
            { LSSParser.LSSLineTypes.SOURCE_FRAGMENT, "lss.srccode" },
        };

        private readonly ITextBuffer mTextBuffer;
        private readonly IClassificationTypeRegistryService mClassificationTypeRegistry;
        private readonly List<ClassificationSpan> classifications = new List<ClassificationSpan>();

#pragma warning disable 0067
        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;
#pragma warning restore 0067

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
