using MahApps.Metro;
using MahApps.Metro.Controls;


namespace FlashCards.Views
{
    /// <summary>
    /// Logika interakcji dla klasy Warning.xaml
    /// </summary>
    public partial class Warning : MetroWindow
    {
        public Warning(string content)
        {
            InitializeComponent();
            ThemeManager.ChangeAppStyle(
                this,
                ThemeManager.GetAccent("Red"),
                ThemeManager.GetAppTheme("BaseLight")
            );
            Content = content;
        }
    }
}
