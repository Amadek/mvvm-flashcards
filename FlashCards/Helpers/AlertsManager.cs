using FlashCards.Views;
using MahApps.Metro.Controls;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FlashCards.Helpers
{
    class AlertsManager
    {
        public void Show(string text, Alerts type)
        {
            MetroWindow alert = new MetroWindow();
            switch (type)
            {
                case Alerts.Success:
                    alert = new Success(text);
                    break;
                case Alerts.Warning:
                    alert = new Warning(text);
                    break;
            }
            alert.Show();
        }
    }
}
