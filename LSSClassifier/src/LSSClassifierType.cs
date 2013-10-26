using System.ComponentModel.Composition;
using System.Windows.Media;
using Microsoft.VisualStudio.Text.Classification;
using Microsoft.VisualStudio.Utilities;

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
            this.DisplayName = "LSS Source Code Fragment";
            this.ForegroundColor = Colors.Olive;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "lss.address")]
    [Name("lss.address")]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class LSSAddressDefinitionFormat : ClassificationFormatDefinition
    {
        public LSSAddressDefinitionFormat()
        {
            this.DisplayName = "LSS ASM Address";
            this.ForegroundColor = Colors.Purple;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "lss.encoding")]
    [Name("lss.encoding")]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class LSSEncodingDefinitionFormat : ClassificationFormatDefinition
    {
        public LSSEncodingDefinitionFormat()
        {
            this.DisplayName = "LSS ASM Encoding";
            this.ForegroundColor = Colors.Black;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "lss.instruction")]
    [Name("lss.instruction")]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class LSSInstructionDefinitionFormat : ClassificationFormatDefinition
    {
        public LSSInstructionDefinitionFormat()
        {
            this.DisplayName = "LSS ASM Instruction";
            this.ForegroundColor = Colors.Blue;
        }
    }

    [Export(typeof(EditorFormatDefinition))]
    [ClassificationType(ClassificationTypeNames = "lss.comment")]
    [Name("lss.comment")]
    [UserVisible(true)]
    [Order(After = Priority.Default)]
    internal sealed class LSSCommentDefinitionFormat : ClassificationFormatDefinition
    {
        public LSSCommentDefinitionFormat()
        {
            this.DisplayName = "LSS ASM Comment";
            this.ForegroundColor = Colors.Green;
        }
    }

    internal static class LSSClassificationType
    {
        [Export(typeof(ClassificationTypeDefinition))]
        [Name("lss.symboldef")]
        internal static ClassificationTypeDefinition LSSSymbolDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("lss.srccode")]
        internal static ClassificationTypeDefinition LSSSourceCodeDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("lss.address")]
        internal static ClassificationTypeDefinition LSSAddressDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("lss.encoding")]
        internal static ClassificationTypeDefinition LSSEncodingDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("lss.instruction")]
        internal static ClassificationTypeDefinition LSSInstructionDefinition { get; set; }

        [Export(typeof(ClassificationTypeDefinition))]
        [Name("lss.comment")]
        internal static ClassificationTypeDefinition LSSCommentDefinition { get; set; }
    }
}
