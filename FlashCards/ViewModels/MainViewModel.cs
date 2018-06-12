
using FlashCards.Models;

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

            LoginVM.UserLogged += FlashCardVM.LoadUser;
        }
    }
}
