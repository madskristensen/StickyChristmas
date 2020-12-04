using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace StickyChristmas
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid("a0a78129-da4c-41a8-817e-a636a4ae04fc")]
    public sealed class StickyChristmasPackage : AsyncPackage
    {
    }
}
