using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using WalletDB.ScaffoldModels;
using Microsoft.EntityFrameworkCore;
using WalletGlobal;
using System.Windows.Media;

namespace WalletAPP
{
    /// <summary>
    /// Logical interprattion for main  page of application - user choice
    /// </summary>
    public partial class UserChoice : Page
    {
        public UserChoice()
        {
            InitializeComponent();
            UpdateUserList();
        }

        /// <summary>
        /// Function responsible for updating balance for given user
        /// </summary>
        public void UpdateBalance()
        {
            if(!string.IsNullOrEmpty(GLOBALS.CurrentUserName))
            {
                long totalOutgoing = 0;
                long totalIncome = 0;
                using var db = new Wallet();

                IQueryable<Outgoing> outgoin = db.Outgoings.Where(el => el.UserId == GLOBALS.CurrentUserID);
                foreach (var el in outgoin)
                    totalOutgoing += el.Outcome;
                IQueryable<Incom> Incoms = db.Incoms.Where(el => el.UserId == GLOBALS.CurrentUserID);
                foreach (var el in Incoms)
                    totalIncome += el.Income;

                long balance = totalIncome - totalOutgoing;
                userBalance.Content = $"{balance}PLN";
                Color color;
                if(balance >= 0)
                    color = (Color)ColorConverter.ConvertFromString("#FF548D33");
                else
                    color =  (Color)ColorConverter.ConvertFromString("#FFA22F2F");
                userBalance.Foreground = new System.Windows.Media.SolidColorBrush(color);
            }
            else
                userBalance.Content = $"";
        }

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
                if (newUserNick.Text.Trim() != "")
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
                    newUserNick.Text = "";
                }
            }
            catch (Exception ex)
            {
                if (ex is Microsoft.EntityFrameworkCore.DbUpdateException)
                    userErrorLabel.Content = "Name is already used!";
            }

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
                UpdateBalance();
            }
        }

        private void userList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {

            if (userList.SelectedItem is not null)
            {
                string[] curItem = userList.SelectedItem.ToString().Split('-');
                currUser.Content = curItem[1];
                GLOBALS.CurrentUserID = long.Parse(curItem[0]);
                GLOBALS.CurrentUserName = curItem[1];
                UpdateBalance();
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
