using System;
using System.Windows.Markup;

namespace TestM.Command
{
    public class EnumBindingSource : MarkupExtension
    {
        public Type EnumType { get; private set; }
        public EnumBindingSource(Type enumType)
        {
            if (enumType is null || !enumType.IsEnum)
            {
                throw new Exception("EnumType is null or not Enum");
            }
            EnumType = enumType;
        }
        public override object ProvideValue(IServiceProvider serviceProvider)
        {
            return Enum.GetValues(EnumType);
        }
    }
}
