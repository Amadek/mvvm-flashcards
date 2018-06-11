using FlashCards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.ViewModels
{
    public class LoginEventArgs : EventArgs
    {
        public User User { get; set; }
    }
}
