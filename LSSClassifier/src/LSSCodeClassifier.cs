using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text;
using System.Text.RegularExpressions;

namespace FourWalledCubicle.LSSClassifier
{
    class LSSCodeClassifier : IClassifier
    {
        private ITextBuffer mTextBuffer;
        private IClassificationTypeRegistryService mClassificationTypeRegistry;
        private List<ClassificationSpan> classifications = new List<ClassificationSpan>();

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

            mTextBuffer.Changed += OnBufferChanged;
        }

        void OnBufferChanged(object sender, TextContentChangedEventArgs e)
        {
            if (e.After != mTextBuffer.CurrentSnapshot)
                return;

            foreach (ITextChange change in e.Changes)
            {
                if (ClassificationChanged != null)
                    ClassificationChanged(this, new ClassificationChangedEventArgs(new SnapshotSpan(e.After, change.NewSpan)));
            }
        }

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            List<ClassificationSpan> classifications = new List<ClassificationSpan>();

            LSSParser lssParser = new LSSParser();

            foreach (Tuple<LSSParser.LSSLineTypes, SnapshotSpan> segment in lssParser.Parse(span))
            {
                IClassificationType classificationType = mClassificationTypeRegistry.GetClassificationType(mClassifierTypeNames[segment.Item1]);
                classifications.Add(new ClassificationSpan(segment.Item2, classificationType));
            }

            return classifications;
        }
    }
}
