﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WalletDB.ScaffoldModels;
using Microsoft.EntityFrameworkCore;

namespace WalletAPP
{
    public partial class MainWindow : Window
    {
        private void UpdateUserList()
        {
            lista.Items.Clear();
            using var db = new Wallet();
            IQueryable<User> users = db.Users;
            foreach (var user in users)
                lista.Items.Add($"{user.Nickname}-{user.Money}PLN");
        }
        public MainWindow()
        {
            InitializeComponent();
            UpdateUserList();
        }



        private void lista_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string[] curItem = lista.SelectedItem.ToString().Split('-');
            currUser.Content = curItem[0];
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            using var db = new Wallet();
            
            var newUser = new User();
            newUser.Nickname = new_user_nick.Text;
            newUser.Money = 0;
            db.Users.Add(newUser);
            int succes = db.SaveChanges();
            UpdateUserList();
        }
    }
}
