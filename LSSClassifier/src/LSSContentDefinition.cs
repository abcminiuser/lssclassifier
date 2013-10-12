using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.VisualStudio.Utilities;
using System.ComponentModel.Composition;

namespace FourWalledCubicle.LSSClassifier
{
    internal static class LSSContentDefinition
    {
        [Export]
        [Name("lss")]
        [BaseDefinition("code")]
        internal static ContentTypeDefinition lssContentTypeDefinition;
        
        [Export]
        [ContentType("lss")]
        [FileExtension(".lss")]
        internal static FileExtensionToContentTypeDefinition lssContentTypeDefinitionFileExtension;
    }
}
