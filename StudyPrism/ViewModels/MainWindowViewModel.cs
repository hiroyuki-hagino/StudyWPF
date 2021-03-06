﻿using Prism.Mvvm;

namespace StudyPrism.ViewModels
{
    public class MainWindowViewModel : BindableBase
    {
        private string _title = "Prism Application";
        public string Title
        {
            get { return _title; }
            set { SetProperty(ref _title, value); }
        }

        /// <summary>
        /// MainWindow ViewModel
        /// </summary>
        public MainWindowViewModel()
        {

        }
    }
}
