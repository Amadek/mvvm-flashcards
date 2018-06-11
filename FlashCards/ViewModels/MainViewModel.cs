
namespace FlashCards.ViewModels
{
    public class MainViewModel : BaseViewModel
    {
        public FlashCardViewModel FlashCardVM { get; private set; }
        public LoginViewModel LoginVM { get; private set; }

        public MainViewModel()
        {
            FlashCardVM = new FlashCardViewModel();
            LoginVM = new LoginViewModel();
        }
    }
}
