﻿namespace StickyChristmas
{
    /// <summary>
    /// A provider for custom <see cref="DialogPage" /> implementations.
    /// </summary>
    internal class DialogPageProvider
    {
        public class General : BaseOptionPage<GeneralOptions> { }
        public class Foo : BaseOptionPage<GeneralOptions> { }
    }
}
