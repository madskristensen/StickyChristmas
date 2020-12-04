using System;
using System.Runtime.InteropServices;
using Microsoft.VisualStudio.Shell;

namespace StickyChristmas
{
    [PackageRegistration(UseManagedResourcesOnly = true, AllowsBackgroundLoading = true)]
    [Guid("a0a78129-da4c-41a8-817e-a636a4ae04fc")]
    [ProvideOptionPage(typeof(DialogPageProvider.General), "Extensions", Vsix.Name, 0, 0, true, 0, ProvidesLocalizedCategoryName = false)]
    [ProvideProfile(typeof(DialogPageProvider.General), "Extensions", Vsix.Name, 0, 0, true)]
    public sealed class StickyChristmasPackage : AsyncPackage
    {
    }
}
