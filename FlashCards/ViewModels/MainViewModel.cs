using FlashCards.Models;
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
        
        private DataModel _dataModel;

        public Command FileCommand { get; private set; }
        public Command NextCommand { get; private set; }
        public Command ShowCommand { get; private set; }
        public Command DontKnowCommand { get; private set; }

        private bool _isFlashCardsEmpty(object obj)
        {
            if (_dataModel.FlashCards.Count == 0)
                return false;
            return true;
        }

        public MainViewModel()
        {
            _dataModel = new DataModel();

            FlierKey = _dataModel.FlashCards[0][0];

            #region FileCommand Initialization
            FileCommand = new Command(
                obj =>
                {
                    try
                    {
                        _dataModel = new DataModel(obj.ToString());
                    }
                    catch (InvalidDataException e)
                    {
                        MessageBox.Show(e.Message);
                    }
                },
                obj =>
                {
                    if (obj.ToString() == Default["FileName"])
                    {
                        return false;
                    }
                    return true;
                }
            );
            #endregion

            #region NextCommand Initialization
            NextCommand = new Command(
                obj =>
                {
                    _dataModel.FlashCards.RemoveAt(0);

                    if (_dataModel.FlashCards.Count == 0)
                    {
                        FlierKey = "To już wszystkie!";
                    }
                    else
                    {
                        FlierKey = _dataModel.FlashCards[0][0];
                    }
                },
                _isFlashCardsEmpty
            );
            #endregion

            #region ShowCommand Initialization
            ShowCommand = new Command(obj =>
            {
                FlierKey = _dataModel.FlashCards[0][1];

            }, _isFlashCardsEmpty);
            #endregion

            #region DontKnowCommand Initialization
            DontKnowCommand = new Command(obj =>
                {
                    _dataModel.FlashCards.Add(_dataModel.FlashCards[0]);
                    _dataModel.FlashCards.RemoveAt(0);
                    FlierKey = _dataModel.FlashCards[0][0];

                }, _isFlashCardsEmpty); 
            #endregion
        }
    }
}
