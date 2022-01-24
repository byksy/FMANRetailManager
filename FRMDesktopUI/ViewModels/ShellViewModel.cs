using Caliburn.Micro;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FRMDesktopUI.ViewModels
{
    public class ShellViewModel : Conductor<object>
    {
        private LoginViewModel _loginVM;
        public ShellViewModel(LoginViewModel loginVM)
        {
            _loginVM = loginVM; //Constructor injection => To pass in a new instance of login VM and activating it immediately after storing it so I can use it later
            ActivateItem(_loginVM); // Activate the login view on the shell view model
        }
    }
}
