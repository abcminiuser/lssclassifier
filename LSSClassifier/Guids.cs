// Guids.cs
// MUST match guids.h
using System;

namespace FourWalledCubicle.LSSClassifier
{
    static class GuidList
    {
        public const string guidLSSClassifierPkgString = "69615ee2-f01c-4c5d-95cd-96dfe549a504";
        public const string guidLSSClassifierCmdSetString = "54abb847-5754-46ff-827f-6f05bc6f0e1c";

        public static readonly Guid guidLSSClassifierCmdSet = new Guid(guidLSSClassifierCmdSetString);
    };
}