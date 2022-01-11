using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using System.Windows;
using TestM.Data;
using TestM.Properties;

namespace TestM
{
    /// <summary>
    /// Логика взаимодействия для App.xaml
    /// </summary>
    public partial class App : Application
    {
        private static readonly string path = Directory.GetCurrentDirectory();
        private readonly string fileInfo = path.Substring(0, path.IndexOf("bin")) + "Info.json";

        private JsonFileService service;
        private ResourceDictionary ThemeDictonary => Resources.MergedDictionaries[0];
        public App()
        {
            InitializeComponent();

            service = new JsonFileService();

            switch (service.OpenStyleApp(fileInfo))
            {
                case "Темная":
                    ChangeTheme(new Uri("/Styles/DarkStyle.xaml", UriKind.RelativeOrAbsolute));
                    break;
                case "Светлая":
                    ChangeTheme(new Uri("/Styles/DefaultStyle.xaml", UriKind.RelativeOrAbsolute));
                    break;
            }
        }

        public void ChangeTheme(Uri uri)
        {
            ThemeDictonary.MergedDictionaries.Clear();
            ThemeDictonary.MergedDictionaries.Add(new ResourceDictionary() { Source = uri });
        }
    }
}
