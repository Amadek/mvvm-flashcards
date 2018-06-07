using FlashCards.Models;
using Microsoft.Win32;
using MySql.Data.MySqlClient;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FlashCards.ViewModels
{
    class MainViewModel : BaseViewModel
    {
        #region FileName Property
        private string _fileName;
        public string FileName
        {
            get
            {
                if (string.IsNullOrEmpty(_fileName))
                {
                    return "Wybierz plik...";
                }
                return _fileName;
            }
            set
            {
                _fileName = value;
            }
        }
        #endregion

        #region FlierKey Property
        private string _flierKey;
        public string FlierKey
        {
            get
            {
                if (string.IsNullOrEmpty(_flierKey))
                    return "Tutaj będą wyświetlać się twoje fiszki.";
                return _flierKey;
            }
            set
            {
                _flierKey = value;
                OnPropertyChanged("FlierKey");
            }
        } 
        #endregion
        
        public Command FileCommand { get; private set; }
        public Command NextCommand { get; private set; }
        public Command ShowCommand { get; private set; }
        public Command DontKnowCommand { get; private set; }
        public Command SaveCommand { get; private set; }
        public Command ShuffleCommand { get; private set; }
        public Command SendCommand { get; private set; }
        
        public MainViewModel()
        {
            DBManager.GetInstance();
            User.LoadUser("TestUser");
            User.GetInstance().LoadCardsDB("Testowy Rozdział");
            FlierKey = User.GetInstance().Cards[0][0];

            NextCommand = new Command(Next, IsNotCardsEmpty);

            ShowCommand = new Command(Show, IsNotCardsEmpty);

            DontKnowCommand = new Command(DontKnow, IsNotCardsEmpty);

            ShuffleCommand = new Command(Shufle, IsNotCardsEmpty);

            #region TO REFACTOR
            //#region FileCommand Initialization
            //FileCommand = new Command(obj =>
            //{
            //    OpenFileDialog fileDialog = new OpenFileDialog();
            //    fileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            //    if (fileDialog.ShowDialog() == true)
            //    {
            //        try
            //        {
            //            _dataModel = new FileManager(fileDialog.FileName);
            //            FlierKey = _dataModel.FlashCards[0][0];
            //        }
            //        catch (InvalidDataException e)
            //        {
            //            MessageBox.Show(e.Message);
            //        }
            //    }
            //});
            //#endregion

            //#region SaveCommand Initialization
            //SaveCommand = new Command(obj =>
            //{
            //    _dataModel.Save();
            //});
            //#endregion

            //#region SendCommand Initalization
            //SendCommand = new Command(obj =>
            //{
            //    try
            //    {
            //        _dataModel.Send();
            //        MessageBox.Show("Wysłano.");
            //    }
            //    catch (MySqlException)
            //    {
            //        MessageBox.Show("Problem z połączeniem się z bazą.");
            //    }

            //}); 
            //#endregion
            #endregion
        }

        private bool IsNotCardsEmpty(object obj)
        {
            return !User.GetInstance().IsCardsEmpty();
        }

        private void Next(object obj)
        {
            User.GetInstance().Cards.RemoveAt(0);

            if (User.GetInstance().Cards.Count == 0)
            {
                FlierKey = "To już wszystkie!";
            }
            else
            {
                FlierKey = User.GetInstance().Cards[0][0];
            }
        }

        private void Show(object obj)
        {
            FlierKey = User.GetInstance().Cards[0][1];
        }

        private void DontKnow(object obj)
        {
            User.GetInstance().MoveCardToEnd();
            FlierKey = User.GetInstance().Cards[0][0];
        }

        private void Shufle(object obj)
        {
            User.GetInstance().ShuffleCards();
            FlierKey = User.GetInstance().Cards[0][0];
        }
    }
}
