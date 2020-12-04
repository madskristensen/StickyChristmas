using System.ComponentModel;

namespace StickyChristmas
{
    internal class GeneralOptions : BaseOptionModel<GeneralOptions>
    {
        [Category("General")]
        [DisplayName("Enabled")]
        [Description("Specifies whether the keyboard should be sticky.")]
        [DefaultValue(true)]
        public bool Enabled { get; set; } = true;
    }
}
