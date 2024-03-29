﻿using System;
using SafeIn_Mobile.Services.Navigation;
using SafeIn_Mobile.Services;
using Splat;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;
using SafeIn_Mobile.ViewModels;
using System.Net.Http;
using Xamarin.Essentials;
using SafeIn_Mobile.Views;
using SafeIn_Mobile.Helpers;

namespace SafeIn_Mobile
{
    public partial class App : Application
    {
       
        private readonly string BaseUrl = Constants.AzureConnectionUrl;
        // only local env only
        //private readonly string BaseUrl = DeviceInfo.Platform == DevicePlatform.Android ?
        //Constants.LocalAndroidConnectionUrl : Constants.LocalIosConnectionUrl;
        public static bool IsLoggedIn { get; set; }

        public App()
        {

            InitializeDi();
            InitializeComponent();

            MainPage = new AppShell();
        }
        private void InitializeDi()
        {
            // Services
            Locator.CurrentMutable.RegisterLazySingleton<IRoutingService>(() => new ShellRoutingService());
            Locator.CurrentMutable.RegisterLazySingleton<IUserService>(() => new UserService());
            Locator.CurrentMutable.RegisterLazySingleton<ILoginService>(() => new LoginService());
            Locator.CurrentMutable.RegisterLazySingleton<ILoginService>(() => new LoginService());


            // ViewModels
            Locator.CurrentMutable.Register(() => new LoadingViewModel());
            Locator.CurrentMutable.Register(() => new SettingsViewModel());
            Locator.CurrentMutable.Register(() => new LoginViewModel());

            // Connection  
            Locator.CurrentMutable.RegisterLazySingleton(() => new HttpClient()
            {
                BaseAddress = new Uri(BaseUrl)
            });
            // only local env
            //Locator.CurrentMutable.RegisterLazySingleton(() => new HttpClient(GetInsecureHandler())
            //{
            //    BaseAddress = new Uri(BaseUrl)
            //});
        }
        protected override void OnStart()
        {
        }

        protected override void OnSleep()
        {
        }

        protected override void OnResume()
        {
        }

        // only in debug to ignore SSL
        public HttpClientHandler GetInsecureHandler()
        {
            HttpClientHandler handler = new HttpClientHandler();
            handler.ServerCertificateCustomValidationCallback = (message, cert, chain, errors) =>
            {
                if (cert.Issuer.Equals("CN=localhost"))
                    return true;
                return errors == System.Net.Security.SslPolicyErrors.None;
            };
            return handler;
        }

    }
}
