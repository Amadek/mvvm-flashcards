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
        public FlashCardViewModel FlashCardVM { get; private set; }

        public MainViewModel()
        {
            FlashCardVM = new FlashCardViewModel();
        }
    }
}
