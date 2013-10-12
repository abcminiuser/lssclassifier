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
    [ClassificationType(ClassificationTypeNames = "lss.symboldef")]
    [Name("lss.symboldef")]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class LSSSymbolDefinitionFormat : ClassificationFormatDefinition
    {
        public LSSSymbolDefinitionFormat()
        {
            this.DisplayName = "LSS Symbol Definition";
            this.IsBold = true;
            this.ForegroundColor = Colors.Brown;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "lss.srccode")]
    [Name("lss.srccode")]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class LSSSourceCodeDefinitionFormat : ClassificationFormatDefinition
    {
        public LSSSourceCodeDefinitionFormat()
        {
            this.DisplayName = "LSS Source Code Section";
            this.IsBold = true;
            this.ForegroundColor = Colors.Gray;
        }
    }

    internal static class LSSClassificationType
    {
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("lss.symboldef")]
        internal static ClassificationTypeDefinition LSSSymbolDefinition;

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("lss.srccode")]
        internal static ClassificationTypeDefinition LSSSourceCodeDefinition;
    }
}
