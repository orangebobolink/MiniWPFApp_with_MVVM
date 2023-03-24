using AutoMapper;
using BLL;
using BLL.DTO;
using BLL.RepositoryServices.Implementations.RepositoryServices;
using DAL.Domain.Models;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic.ApplicationServices;
using System;
using System.Collections.Generic;
using System.Windows;
using WPF.ViewModels;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            DataContext = new UserViewModel();
        }

        private void MenuItem_Click_1(object sender, RoutedEventArgs e)
        {
            AddMenu.IsEnabled = true;
            DeleteMenu.IsEnabled = true;
        }
    }
}
