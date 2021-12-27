﻿using System.Collections.ObjectModel;
using System.IO;
using TestM.Command;
using TestM.Data;
using TestM.Models;
using TestM.ViewModels.Base;

namespace TestM.ViewModels
{
    public class PageTestViewModel : ViewModel
    {
        private static string path = Directory.GetCurrentDirectory();
        private readonly string fileActualQuestion = path.Substring(0, path.IndexOf("bin")) + "ActualQuestion.json";
        private readonly string fileIndex = path.Substring(0, path.IndexOf("bin")) + "Index.json";

        private ObservableCollection<QuestionModel> questions;
        private ActualQuestion actualQuestionModel;
        JsonFileService service;

        #region Commands
        private RelayCommand firstAnswer;
        public RelayCommand FirstAnswer
        {
            get
            {
                return firstAnswer ?? (firstAnswer = new RelayCommand(obj => 
                {
                    RightAnswer = "А";
                }));
            }
        }
        private RelayCommand secondAnswer;
        public RelayCommand SecondAnswer
        {
            get
            {
                return secondAnswer ?? (secondAnswer = new RelayCommand(obj => 
                {
                    RightAnswer = "Б";
                }));
            }
        }
        private RelayCommand thirdAnswer;
        public RelayCommand ThirdAnswer
        {
            get
            {
                return thirdAnswer ?? (thirdAnswer = new RelayCommand(obj =>
                {
                    RightAnswer = "В";
                }));
            }
        }
        #endregion
        #region Property
        //private string answerFirst;
        //private string answerSecond;
        //private string answerThird; 
        private string rightAnswer;
        public string QuestionFirst => questions[actualQuestionModel.Index].Question;
        public string AnswerFirst
        {
            get => questions[actualQuestionModel.Index].AnswerA;
            //set 
            //{
            //    answerFirst = value; 
            //    OnPropertyChanged(nameof(AnswerFirst));
            //}
        }
        public string AnswerSecond
        {
            get => questions[actualQuestionModel.Index].AnswerB;
            //set
            //{
            //    answerSecond = value;
            //    OnPropertyChanged(nameof(AnswerSecond)); 
            //}
        }
        public string AnswerThird 
        {
            get => questions[actualQuestionModel.Index].AnswerC;
            //set
            //{
            //    answerThird = value;
            //    OnPropertyChanged(nameof(AnswerThird));
            //}
        }
        public string RightAnswer
        {
            get => rightAnswer; 
            set
            {
                rightAnswer = value;
                OnPropertyChanged(nameof(RightAnswer));
            }
        }
        #endregion
        public PageTestViewModel()
        {
            service = new JsonFileService();
            questions = service.Open(fileActualQuestion);
            actualQuestionModel = service.OpenIndex(fileIndex);
            service.SaveIndex(fileIndex);
        }
    }
}
