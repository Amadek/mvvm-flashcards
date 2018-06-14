using MahApps.Metro;
using MahApps.Metro.Controls;

namespace FlashCards.Views
{
    /// <summary>
    /// Logika interakcji dla klasy Success.xaml
    /// </summary>
    public partial class Success : MetroWindow
    {
        public Success(string content)
        {
            InitializeComponent();
            ThemeManager.ChangeAppStyle(
                this,
                ThemeManager.GetAccent("Green"),
                ThemeManager.GetAppTheme("BaseLight")
            );
            Content = content;
        }
    }
}
