﻿using FlashCards.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

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

        public Command LoginCommand { get; private set; }

        public LoginViewModel()
        {
            Database.Instance.Connect();
            LoginCommand = new Command(Login, IsFieldsNotEmpty);
        }

        private void Login(object obj)
        {
            var gateway = new UsersGateway();
            if (gateway.IsValid(_nick, _password, Database.Instance))
                Visible = false;
            else
                MessageBox.Show("Niepoprawna nazwa użytkownika lub hasło.");
        }

        private bool IsFieldsNotEmpty(object obj)
        {
            if (string.IsNullOrEmpty(_nick) || string.IsNullOrEmpty(_password))
                return false;
            return true;
        }
    }
}