using System;
using System.Globalization;
using System.Windows.Data;
using TestM.Data;

namespace TestM.Command
{
    public class ConvertToString : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is TypeEnum type)
            {
                switch (type)
                {
                    case TypeEnum.firstType:
                        return "1";
                    case TypeEnum.secondType:
                        return "2";
                    case TypeEnum.thirdType:
                        return "3";
                    case TypeEnum.fourthType:
                        return "4";
                    case TypeEnum.fifthType:
                        return "5";
                    default:
                        break;
                }
            }

            if (value is AnswerEnum answer)
            {
                switch (answer)
                {
                    case AnswerEnum.firstAnswer:
                        return "А";
                    case AnswerEnum.secondAnswer:
                        return "Б";
                    case AnswerEnum.thirdAnswer:
                        return "В";
                    default:
                        break;
                }
            }

            if (value is StyleEnum style)
            {
                switch (style)
                {
                    case StyleEnum.lightStyle:
                        return "Светлая";
                    case StyleEnum.darkStyle:
                        return "Темная";
                    default:
                        break;
                }
            }
            return value;
        }
        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is string type)
            {
                switch (type)
                {
                    case "1":
                        return TypeEnum.firstType;
                    case "2":
                        return TypeEnum.secondType;
                    case "3":
                        return TypeEnum.thirdType;
                    case "4":
                        return TypeEnum.fourthType;
                    case "5":
                        return TypeEnum.fifthType;
                    default:
                        break;
                }
            }

            if (value is string answer)
            {
                switch (answer)
                {
                    case "А":
                        return AnswerEnum.firstAnswer;
                    case "Б":
                        return AnswerEnum.secondAnswer;
                    case "В":
                        return AnswerEnum.thirdAnswer;
                    default:
                        break;
                }
            }

            if (value is string style)
            {
                switch (style)
                {
                    case "Светлая":
                        return StyleEnum.lightStyle;
                    case "Темная":
                        return StyleEnum.darkStyle;
                    default:
                        break;
                }
            }
            return value;
        }
    }
}
