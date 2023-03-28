﻿using MvvmHelpers;
using SafeIn_Mobile.Services;
using SafeIn_Mobile.Services.Navigation;
using SafeIn_Mobile.Views;
using Splat;

namespace SafeIn_Mobile.ViewModels
{
    class LoadingViewModel : BaseViewModel
    {
        private readonly IRoutingService _routingService;
        private readonly IUserService _userService;
        private readonly ILoginService _loginService;

        public LoadingViewModel(IRoutingService routingService = null, IUserService userService = null, ILoginService loginService = null)
        {
            _routingService = routingService ?? Locator.Current.GetService<IRoutingService>();
            _userService = userService ?? Locator.Current.GetService<IUserService>();
            _loginService = loginService ?? Locator.Current.GetService<ILoginService>();
        }

        // Called by the views OnAppearing method
        public async void Init()
        {
            var accessCheckResult = await _loginService.AccessCheckAsync();
            if (accessCheckResult.Success)
            {
                App.IsLoggedIn= true;
                await _routingService.NavigateTo("///main");
            }
            else
            {
                App.IsLoggedIn = false;
                await _routingService.NavigateTo($"///{nameof(LoginPage)}");
            }
        }
    }
}