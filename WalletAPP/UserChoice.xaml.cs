using System;
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
            lista.Items.Clear();
            using var db = new Wallet();
            IQueryable<User> users = db.Users;
            foreach (var user in users)
                lista.Items.Add($"{user.Id} - {user.Nickname} - {user.Money}PLN");
        }

        private void lista_SelectionChanged(object sender, System.Windows.Controls.SelectionChangedEventArgs e)
        {
            string[] curItem = lista.SelectedItem.ToString().Split('-');
            currUser.Content = curItem[1];
            GLOBALS.CurrentUserID = long.Parse(curItem[0]);
            GLOBALS.CurrentUserName = curItem[1];
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
        public UserChoice()
        {
            InitializeComponent();
            UpdateUserList();
        }
    }
}
