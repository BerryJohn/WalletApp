using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;
using WalletDB.ScaffoldModels;
using Microsoft.EntityFrameworkCore;
using System.Linq;

namespace WalletAPP
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void btn_Click(object sender, RoutedEventArgs e)
        {
            using var db = new Wallet();
            IQueryable<User> users = db.Users.Where(u => u.Id == 1);
            foreach (var user in users)
                tekst.Content = user.Nickname;
        }
    }
}
