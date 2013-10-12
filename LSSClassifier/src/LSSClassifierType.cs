using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Text.Classification;
using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Utilities;
using System.Windows.Media;

namespace FourWalledCubicle.LSSClassifier
{
    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "lss.codestart")]
    [Name("lss.codestart")]
    [DisplayName("LSS File")]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class LSSCodeStartFormat : ClassificationFormatDefinition
    {
        public LSSCodeStartFormat()
        {
            this.IsBold = true;
            this.BackgroundColor = Colors.Brown;
        }
    }

    internal static class LSSClassificationType
    {
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("lss.codestart")]
        internal static ClassificationTypeDefinition LSSCodeStartDefinition;
    }
}
