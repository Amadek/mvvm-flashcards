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
    class MainViewModel : ObservableObject
    {
        public readonly Dictionary<string, string> Default = new Dictionary<string, string>()
        {
            { "FileName", "Wybierz plik..." },
            { "FlierKey", "Tutaj będą wyświetlać się twoje fiszki." }
        };

        #region FileName Property
        private string _fileName;
        public string FileName
        {
            get
            {
                if (string.IsNullOrEmpty(_fileName))
                {
                    return Default["FileName"];
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
                    return Default["FlierKey"];
                return _flierKey;
            }
            set
            {
                _flierKey = value;
                OnPropertyChanged("FlierKey");
            }
        } 
        #endregion
        
        private FileManager _dataModel;

        public Command FileCommand { get; private set; }
        public Command NextCommand { get; private set; }
        public Command ShowCommand { get; private set; }
        public Command DontKnowCommand { get; private set; }
        public Command SaveCommand { get; private set; }
        public Command ShuffleCommand { get; private set; }
        public Command SendCommand { get; private set; }

        //private bool _isFlashCardsEmpty(object obj)
        //{
        //    if (_dataModel.FlashCards.Count == 0)
        //        return false;
        //    return true;
        //}

        public MainViewModel()
        {
            NextCommand = new Command(obj =>
            {
                DBManager.GetInstance();
                User.LoadUser("Amadek");
                MessageBox.Show(User.GetInstance().ID.ToString());
            });
            //_dataModel = new FileManager();

            //FlierKey = _dataModel.FlashCards[0][0];

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

            //#region NextCommand Initialization
            //NextCommand = new Command(obj =>
            //{
            //    _dataModel.FlashCards.RemoveAt(0);

            //    if (_dataModel.FlashCards.Count == 0)
            //    {
            //        FlierKey = "To już wszystkie!";
            //    }
            //    else
            //    {
            //        FlierKey = _dataModel.FlashCards[0][0];
            //    }
            //}, _isFlashCardsEmpty);
            //#endregion

            //#region ShowCommand Initialization
            //ShowCommand = new Command(obj =>
            //{
            //    // Show value of key.
            //    FlierKey = _dataModel.FlashCards[0][1];

            //}, _isFlashCardsEmpty);
            //#endregion

            //#region DontKnowCommand Initialization
            //DontKnowCommand = new Command(obj =>
            //{
            //    _dataModel.FlashCards.Add(_dataModel.FlashCards[0]);
            //    _dataModel.FlashCards.RemoveAt(0);
            //    FlierKey = _dataModel.FlashCards[0][0];

            //}, _isFlashCardsEmpty);
            //#endregion

            //#region SaveCommand Initialization
            //SaveCommand = new Command(obj =>
            //{
            //    _dataModel.Save();
            //});
            //#endregion

            //#region ShuffleCommand Initialization
            //ShuffleCommand = new Command(obj =>
            //{
            //    _dataModel.ShuffleCards();
            //    FlierKey = _dataModel.FlashCards[0][0];
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
        }
    }
}
