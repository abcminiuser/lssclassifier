using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Text;

namespace FourWalledCubicle.LSSClassifier
{
    class LSSCodeClassifier : IClassifier
    {
        private ITextBuffer mTextBuffer;
        private IClassificationTypeRegistryService mClassificationTypeRegistry;

        public event EventHandler<ClassificationChangedEventArgs> ClassificationChanged;

        public LSSCodeClassifier(ITextBuffer buffer, IClassificationTypeRegistryService classifierTypeRegistry)
        {
            mTextBuffer = buffer;
            mClassificationTypeRegistry = classifierTypeRegistry;

            mTextBuffer.Changed += OnBufferChanged;
        }

        void OnBufferChanged(object sender, TextContentChangedEventArgs e)
        {

        }

        public IList<ClassificationSpan> GetClassificationSpans(SnapshotSpan span)
        {
            IList<ClassificationSpan> spans = new List<ClassificationSpan>();

            var sss = new SnapshotSpan(mTextBuffer.CurrentSnapshot, 0, mTextBuffer.CurrentSnapshot.Length);
            spans.Add(new ClassificationSpan(sss, mClassificationTypeRegistry.GetClassificationType("lss.codestart")));

            return spans;
        }
    }
}
