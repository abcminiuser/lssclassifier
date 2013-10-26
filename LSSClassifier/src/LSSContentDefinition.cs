using System.ComponentModel.Composition;
using Microsoft.VisualStudio.Utilities;

namespace FourWalledCubicle.LSSClassifier
{
    internal static class LSSContentDefinition
    {
        [Export]
        [Name("lss")]
        [BaseDefinition("code")]
        internal static ContentTypeDefinition lssContentTypeDefinition { get; set; }
        
        [Export]
        [ContentType("lss")]
        [FileExtension(".lss")]
        internal static FileExtensionToContentTypeDefinition lssContentTypeDefinitionFileExtension { get; set; }
    }
}
