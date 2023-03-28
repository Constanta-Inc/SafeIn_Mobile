﻿using System;
using System.Threading.Tasks;
using MvvmHelpers;
using MvvmHelpers.Commands;
using SafeIn_Mobile.Helpers;
using SafeIn_Mobile.Services;
using SafeIn_Mobile.Services.Navigation;
using SafeIn_Mobile.Views;
using Splat;
using Xamarin.Forms;

namespace SafeIn_Mobile.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {

        public string Email { get => email; set => SetProperty(ref email, value);
        }
        public string Password { get => password; set => SetProperty(ref password, value); }
        public string EmailMessage { get => emailMessage; set => SetProperty(ref emailMessage, value); }
        public string PasswordMessage { get => passwordMessage; set => SetProperty(ref passwordMessage, value); }
        public string CredentialsNotValid { get => credentialsNotValid; set => SetProperty(ref credentialsNotValid, value); }
        public AsyncCommand LoginCommand { get; set; }
        private readonly IRoutingService _navigationService;
        private readonly IUserService _userService;
        private readonly ILoginService _loginService;
        private string email;
        private string password;
        private string emailMessage;
        private string passwordMessage;
        private string credentialsNotValid;

        public LoginViewModel(IRoutingService navigationService = null, ILoginService loginService = null)
        {
            _navigationService = navigationService ?? Locator.Current.GetService<IRoutingService>();
            _loginService = loginService ?? Locator.Current.GetService<ILoginService>();
            Title = "Login Page";

            //delete
            Email = "tomas@lnu.edu";
            Password = "Tomas-123";

            OnPropertyChanged();
            LoginCommand = new AsyncCommand(Login);
        }

        async Task Login()
        {
            // validate data
            EmailMessage = "";
            PasswordMessage= "";
            var email_correct = await UserValidation.EmailValidAsync(Email);
            var password_correct = await UserValidation.PasswordValidAsync(Password);
            if (!email_correct)
            {
                EmailMessage = Constants.EmailNotValidMessage;
            }
            if (!password_correct)
            {
                PasswordMessage = Constants.PasswordEmptyMessage;
            }

            // cansle if credential not valid
            if (!email_correct || !password_correct)
            {
                return;
            }

            // login user and put refresh and access tokens into SecureStorage
            var loginResult = await _loginService.LoginAsync(Email, Password);
            if (!loginResult.Success)
            {
                // handle if login was unsuccessful.NavigationStack.Countl
                CredentialsNotValid = AuthErrorMessages.UncorrectCredentials;
                return;
            }
            App.IsLoggedIn = true;
            await _navigationService.NavigateTo($"///main/{nameof(UserPage)}");
        }
    }
}