using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.ViewModels
{
    class MainViewModel : ObservableObject
    {
        const string Default = "Wybierz plik...";

        #region FileName Propertty
        private string _fileName;
        public string FileName
        {
            get
            {
                if (string.IsNullOrEmpty(_fileName))
                {
                    return Default;
                }
                return _fileName;
            }
            set
            {
                _fileName = value;
            }
        }
        #endregion

        public Command CMD { get; private set; }

        public MainViewModel()
        {
            CMD = new Command((s) =>
            {
                Console.WriteLine(s);
            },
            (s) =>
            {
                if ((string)s == Default)
                    return false;
                return true;
            });
        }
    }
}
