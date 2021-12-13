using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace TestM.Enums
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
            if (value is RightAnswers rightAnswers)
            {
                switch (rightAnswers)
                {
                    case RightAnswers.A:
                        return "А";
                    case RightAnswers.B:
                        return "Б";
                    case RightAnswers.C:
                        return "В";
                    case RightAnswers.D:
                        return "Г";
                    default:
                        break;
                }
            }
            return value;
        }
        public object Convert(object value)
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
            if (value is RightAnswers rightAnswers)
            {
                switch (rightAnswers)
                {
                    case RightAnswers.A:
                        return "А";
                    case RightAnswers.B:
                        return "Б";
                    case RightAnswers.C:
                        return "В";
                    case RightAnswers.D:
                        return "Г";
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
        public object ConvertBack(object value)
        {
            if (value is string typeQuestion)
            {
                switch (typeQuestion)
                {
                    case "Правовые основания":
                        return TypeQuestions.legalBases;
                    case "Меры безопастности":
                        return TypeQuestions.safety;
                    case "ТТХ":
                        return TypeQuestions.ttx;
                    case "Команды":
                        return TypeQuestions.command;
                    case "Задержки":
                        return TypeQuestions.delays;
                    default:
                        break;
                }
            }
            if (value is string rightAnswers)
            {
                switch (rightAnswers)
                {
                    case "А":
                        return RightAnswers.A;
                    case "Б":
                        return RightAnswers.B;
                    case "В":
                        return RightAnswers.C;
                    case "Г":
                        return RightAnswers.D;
                    default:
                        break;
                }
            }
            return value;
        }
    }
}
