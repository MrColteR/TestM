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
            if (value is TypeQuestions typeQuestion)
            {
                switch (typeQuestion)
                {
                    case TypeQuestions.legalBases:
                        return "Правовые основания";
                    case TypeQuestions.safety:
                        return "Меры безопастности";
                    case TypeQuestions.ttx:
                        return "ТТХ";
                    case TypeQuestions.command:
                        return "Команды";
                    case TypeQuestions.delays:
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
