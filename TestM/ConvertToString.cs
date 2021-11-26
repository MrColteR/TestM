using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TestM
{
    internal class ConvertToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TypeQuestion typeQuestion)
            {
                switch (typeQuestion)
                {
                    case TypeQuestion.legalBases:
                        return "Правовые основания";
                    case TypeQuestion.safety:
                        return "Меры безопастности";
                    case TypeQuestion.ttx:
                        return "ТТХ";
                    case TypeQuestion.command:
                        return "Команды";
                    case TypeQuestion.delays:
                        return "Задержки";
                    default:
                        break;
                }
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}
