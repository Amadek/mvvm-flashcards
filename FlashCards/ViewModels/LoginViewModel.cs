﻿using FlashCards.Models;
using System;
using FlashCards.Helpers;

namespace FlashCards.ViewModels
{
    public class LoginViewModel : BaseViewModel
    {
        #region Nick
        private string _nick;
        public string Nick
        {
            get
            {
                if (string.IsNullOrEmpty(_nick))
                    return "";
                return _nick;
            }
            set
            {
                if (value != _nick)
                {
                    _nick = value;
                    OnPropertyChanged("Login");
                }
            }
        }
        #endregion

        #region Password
        private string _password;
        public string Password
        {
            get
            {
                if (string.IsNullOrEmpty(_password))
                    return "";
                return _password;
            }
            set
            {
                if (value != _password)
                {
                    _password = value;
                    OnPropertyChanged("Password");
                }
            }
        }
        #endregion

        #region Visible
        private bool _visible = true;
        public bool Visible
        {
            get
            {
                return _visible;
            }
            set
            {
                _visible = value;
                OnPropertyChanged("Visible");
            }
        }
        #endregion

        #region UserLogged event
        public event EventHandler<LoginEventArgs> UserLogged;

        protected virtual void OnUserLogged(User user)
        {
            if (UserLogged != null)
                UserLogged.Invoke(this, new LoginEventArgs() { User = user });
        }
        #endregion

        public Command LoginCommand { get; private set; }

        public LoginViewModel()
        {
            LoginCommand = new Command(Login, IsFieldsNotEmpty);
        }

        private void Login(object obj)
        {
            var manager = new AlertsManager();
            try
            {
                Database.Instance.Connect();
                var gateway = new UsersGateway();

                if (gateway.IsValid(_nick, _password, Database.Instance))
                {
                    var user = new User(_nick);
                    OnUserLogged(user);
                    Visible = false;
                }
                else
                {
                    manager.Show("Niepoprawna nazwa użytkownika lub hasło.", Alerts.Warning);
                }
            }
            catch (Exception)
            {
                manager.Show("Problem z połączeniem z internetem.", Alerts.Warning);
            }
        }

        private bool IsFieldsNotEmpty(object obj)
        {
            if (string.IsNullOrEmpty(_nick) || string.IsNullOrEmpty(_password))
                return false;
            return true;
        }
    }
}
