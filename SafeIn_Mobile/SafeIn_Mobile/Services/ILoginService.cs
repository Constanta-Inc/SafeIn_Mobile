﻿using System.Threading.Tasks;
using SafeIn_Mobile.Models.Auth;

namespace SafeIn_Mobile.Services
{
    public interface ILoginService
    {

        void Logout();
        Task<RefreshTokenResult> RefreshTokensAsync();
        Task<LoginResult> LoginAsync(string email, string password);
        Task<bool> AuthCheck();
    }
}