﻿using System.Collections.ObjectModel;
using System.IO;
using TestM.Command;
using TestM.Data.Base;
using TestM.Models;
using TestM.ViewModels.Base;
using TestM.Views;

namespace TestM.ViewModels
{
    public class QuestionWindowViewModel : ViewModel
    {
        private static string path = Directory.GetCurrentDirectory();
        public readonly string fileName = path.Substring(0, path.IndexOf("bin")) + "Data.json";
        IFileService fileService;
        #region Commands
        private RelayCommand addQuestion;
        public RelayCommand AddQuestion
        {
            get
            {
                return addQuestion ?? (addQuestion = new RelayCommand(obj =>
                {
                    var window = new AddQuestionWindow(obj as QuestionWindowViewModel);
                    window.ShowDialog();
                }));
            }
        }
        private RelayCommand updateQuestion;
        public RelayCommand UpdateQuestion 
        {
            get
            {
                return updateQuestion ?? (updateQuestion = new RelayCommand(obj => 
                {
                    var window = new UpdateQuestionWindow(SelectedItem);
                    window.ShowDialog();
                }));
            }
        }
        private RelayCommand deleteQustion;
        public RelayCommand DeleteQuestion 
        {
            get
            {
                return deleteQustion ?? (deleteQustion = new RelayCommand(obj =>
                {
                    var index = ItemsSource.IndexOf(SelectedItem);
                    if (SelectedItem != null)
                    {
                        ItemsSource.Remove(SelectedItem);
                        if (index == ItemsSource.Count)
                        {
                            SelectedIndex = index - 1;
                        }
                        else
                        {
                            SelectedIndex = index;
                        }
                    }
                }, (obj) => ItemsSource.Count > 0));
            }
        }
        private RelayCommand saveFile;
        public RelayCommand SaveFile
        {
            get
            {
                return saveFile ?? (saveFile = new RelayCommand(obj =>
                {
                    fileService.Save(fileName, ItemsSource);
                }));
            }
        }
        private RelayCommand closeQuestionWindow;
        public RelayCommand CloseQuestionWindow
        {
            get
            {
                return closeQuestionWindow ?? (closeQuestionWindow = new RelayCommand(obj =>
                {
                    QuestionWindow wnd = obj as QuestionWindow;
                    wnd.Close();
                }));
            }
        }
        #endregion
        #region Property
        private QuestionModel selectedItem;
        private int selectedIndex;
        public QuestionModel SelectedItem
        {
            get { return selectedItem; }
            set
            {
                selectedItem = value;
                OnPropertyChanged(nameof(SelectedItem));
            }
        }
        public int SelectedIndex
        {
            get { return selectedIndex; }
            set
            {
                selectedIndex = value;
                OnPropertyChanged(nameof(SelectedIndex));
            }
        }
        public ObservableCollection<QuestionModel> ItemsSource { get; set; }
        #endregion
        public QuestionWindowViewModel(IFileService fileService)
        {
            QuestionModel questionModel = new QuestionModel();
            this.fileService = fileService;
            ItemsSource = fileService.Open(fileName);
            QuestionDataGridViewModel question = new QuestionDataGridViewModel(ItemsSource, questionModel);
        }
    }
}
