using FlashCards.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.IO;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace FlashCards.ViewModels
{
    public class FlashCardViewModel : BaseViewModel
    {
        private User _user = null;

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

        #region CategoryName Property
        private string _categoryName;
        public string CategoryName
        {
            get
            {
                if (string.IsNullOrEmpty(_categoryName))
                    return "";
                return _categoryName;
            }
            set
            {
                if (value != _categoryName)
                {
                    _categoryName = value;
                    OnPropertyChanged("CategoryName");
                }
            }
        } 
        #endregion

        public ObservableCollection<string> UnitsBox { get; set; }

        public Command FileCommand { get; private set; }
        public Command NextCommand { get; private set; }
        public Command ShowCommand { get; private set; }
        public Command DontKnowCommand { get; private set; }
        public Command SaveCommand { get; private set; }
        public Command ShuffleCommand { get; private set; }
        public Command SendCommand { get; private set; }
        public Command LoadCommand { get; private set; }

        public void LoadUser(object sender, LoginEventArgs e)
        {
            _user = e.User;
            _user.LoadCardsLocal();
            FlierKey = _user.Cards[0][0];

            var gateway = new CardsGateway();
            var units = gateway.GetUnits(_user.ID, Database.Instance);
            foreach (var item in units)
            {
                UnitsBox.Add(item);
            }
        }


        public FlashCardViewModel()
        {
            UnitsBox = new ObservableCollection<string>();

            NextCommand = new Command(Next, IsNotCardsEmpty);
            ShowCommand = new Command(Show, IsNotCardsEmpty);
            DontKnowCommand = new Command(DontKnow, IsNotCardsEmpty);
            ShuffleCommand = new Command(Shufle, IsNotCardsEmpty);
            LoadCommand = new Command(Load, IsSelected);
            FileCommand = new Command(LoadLocal, IsLogged);
            SaveCommand = new Command(Save, IsNotCardsEmpty);
            SendCommand = new Command(Send, IsCatNotNullAndUnique);
        }

        private bool IsLogged(object obj)
        {
            return _user != null;
        }

        private bool IsNotCardsEmpty(object obj)
        {
            return IsLogged(obj) && !_user.IsCardsEmpty();
        }
        
        private bool IsSelected(object obj)
        {
            return IsLogged(obj) && obj != null;
        }

        private void Next(object obj)
        {
            _user.Cards.RemoveAt(0);
            
            if (_user.Cards.Count == 0)
            {
                FlierKey = "To już wszystkie!";
            }
            else
            {
                FlierKey = _user.Cards[0][0];
            }
        }

        private void Show(object obj)
        {
            FlierKey = _user.Cards[0][1];
        }

        private void DontKnow(object obj)
        {
            _user.MoveCardToEnd();
            FlierKey = _user.Cards[0][0];
        }

        private void Shufle(object obj)
        {
            _user.ShuffleCards();
            FlierKey = _user.Cards[0][0];
        }

        private void Load(object obj)
        {
            _user.LoadCardsDB(obj as string);
            FlierKey = _user.Cards[0][0];
            MessageBox.Show("Załadowano");
        }

        private void LoadLocal(object obj)
        {
            OpenFileDialog fileDialog = new OpenFileDialog();
            fileDialog.Filter = "Text files (*.txt)|*.txt|All files (*.*)|*.*";

            if (fileDialog.ShowDialog() == true)
            {
                try
                {
                    _user.LoadCardsLocal(fileDialog.FileName);
                    FlierKey = _user.Cards[0][0];
                }
                catch (InvalidDataException e)
                {
                    MessageBox.Show(e.Message);
                }
            }
        }

        private void Save(object obj)
        {
            _user.Save();
        }

        private void Send(object obj)
        {
            _user.Send(CategoryName);
            MessageBox.Show("Wysłano");
        }

        private bool IsCatNotNullAndUnique(object obj)
        {
            if (string.IsNullOrEmpty(CategoryName))
                return false;

            foreach (var item in UnitsBox)
            {
                if (CategoryName == item)
                    return false;
            }

            return true;
        }
    }
}
