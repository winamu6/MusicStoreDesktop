using SkufMusic.Core.Services.UserServices.UserServicesInterfaces;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows;
using SkufMusic.App.Helpers;

namespace SkufMusic.App.ViewModel
{
    public class LoginViewModel : INotifyPropertyChanged
    {
        private readonly IUserService _userService;
        private Window _window;

        public string Username { get; set; }
        public string ErrorMessage { get; set; }

        public ICommand LoginCommand { get; }
        public ICommand RegisterCommand { get; }

        public LoginViewModel(IUserService userService, Window window)
        {
            _userService = userService;
            _window = window;

            LoginCommand = new RelayCommand(ExecuteLogin);
            RegisterCommand = new RelayCommand(ExecuteRegister);
        }

        public async void ExecuteLogin(object passwordBoxObj)
        {
            var passwordBox = passwordBoxObj as PasswordBox;
            var password = passwordBox?.Password ?? "";

            var user = await _userService.AuthenticateAsync(Username, password);
            if (user == null)
            {
                ErrorMessage = "Неверный логин или пароль.";
                OnPropertyChanged(nameof(ErrorMessage));
                return;
            }

            var mainWindow = new MainWindow();
            mainWindow.Show();
            _window.Close();
        }

        public async void ExecuteRegister(object passwordBoxObj)
        {
            var passwordBox = passwordBoxObj as PasswordBox;
            var password = passwordBox?.Password ?? "";

            try
            {
                var user = await _userService.RegisterAsync(Username, password);
                ErrorMessage = "Регистрация прошла успешно. Войдите в систему.";
                OnPropertyChanged(nameof(ErrorMessage));
            }
            catch (Exception ex)
            {
                ErrorMessage = ex.Message;
                OnPropertyChanged(nameof(ErrorMessage));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;
        protected void OnPropertyChanged(string name) => PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(name));
    }

}
