using System;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Text;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

namespace FourWalledCubicle.LSSClassifier
{
    [Export(typeof(IClassifierProvider))]
    [ContentType("lss")]
    internal class LSSClassifierProvider : IClassifierProvider
    {
        [Import]
        internal IClassificationTypeRegistryService mClassificationRegistry { get; set; }

        public IClassifier GetClassifier(ITextBuffer buffer)
        {
            Func<IClassifier> classifierFunc =
                () => new LSSCodeClassifier(buffer, mClassificationRegistry) as IClassifier;
            return buffer.Properties.GetOrCreateSingletonProperty<IClassifier>(classifierFunc);
        }
    }
}
