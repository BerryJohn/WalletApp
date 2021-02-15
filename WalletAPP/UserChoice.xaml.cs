﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WalletDB.ScaffoldModels;
using Microsoft.EntityFrameworkCore;
using WalletGlobal;

namespace WalletAPP
{
    /// <summary>
    /// Interaction logic for UserChoice.xaml
    /// </summary>
    public partial class UserChoice : Page
    {
        private void UpdateUserList()
        {
            userList.Items.Clear();
            using var db = new Wallet();
            IQueryable<User> users = db.Users;
            foreach (var user in users)
                userList.Items.Add($"{user.Id}-{user.Nickname}");
        }

        private void addUserButton(object sender, RoutedEventArgs e)
        {
            try
            {
                if(newUserNick.Text.Trim() != "")
                {
                    using var db = new Wallet();
                    var newUser = new User();
                    newUser.Nickname = newUserNick.Text.Trim();
                    newUser.Money = 0;
                    db.Users.Add(newUser);
                    int succes = db.SaveChanges();
                    if (!string.IsNullOrEmpty(userErrorLabel.Content.ToString()))
                        userErrorLabel.Content = "";
                    UpdateUserList();
                }
            }
            catch(Exception ex)
            {
                if(ex is Microsoft.EntityFrameworkCore.DbUpdateException)
                {
                    userErrorLabel.Content = "Name is already used!";
                }
            }

        }
        public UserChoice()
        {
            InitializeComponent();
            UpdateUserList();
        }

        private void removeUser_Click(object sender, RoutedEventArgs e)
        {
            if (userList.SelectedItem is not null)
            {
                using var db = new Wallet();
                string selected = userList.SelectedItem.ToString().Split('-')[1];
                IQueryable<User> userToRemove = db.Users.Where(el => el.Nickname == selected);
                db.Users.RemoveRange(userToRemove);
                int result = db.SaveChanges();
                UpdateUserList();
            }
        }

        private void userList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if(userList.SelectedItem is not null)
            {
            string[] curItem = userList.SelectedItem.ToString().Split('-');
            currUser.Content = curItem[1];
            GLOBALS.CurrentUserID = long.Parse(curItem[0]);
            GLOBALS.CurrentUserName = curItem[1];
            }
            else
            {
            currUser.Content = "";
            GLOBALS.CurrentUserID = 0;
            GLOBALS.CurrentUserName = "";
            }

        }
    }
}
