using BLL;
using BLL.DTO;
using BLL.RepositoryServices.Implementations.RepositoryServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Windows;

namespace WPF
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        private readonly IServiceProvider _serviceProvider;

        public MainWindow()
        {
            InitializeComponent();
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddBLLServices();

            _serviceProvider = serviceCollection.BuildServiceProvider();
        }

        private async void Fill()
        {
            UserRepositoryService? service = _serviceProvider.GetService<UserRepositoryService>();

            var response = await service.GetAllAsync();

            if (response.StatusCode.StatusCode == 200)
            {
                List<UserDTO> users = response.Data;


                userGrid.ItemsSource = users;
            }
        }

        private void MenuItem_Click(object sender, RoutedEventArgs e)
        {
            Fill();
            AddMenu.IsEnabled = true;
            EditMenu.IsEnabled = true;
            DeleteMenu.IsEnabled = true;
        }

        private void AddMenu_Click(object sender, RoutedEventArgs e)
        {
            List<User> users = (List<User>)userGrid.ItemsSource;
            UserRepositoryService? service = _serviceProvider.GetService<UserRepositoryService>();

            users.ForEach(async user => await service.UpdateAsync(user));
        }

        private async void EditMenu_Click(object sender, RoutedEventArgs e)
        {
            User user = (User)userGrid.SelectedItem;
            UserRepositoryService? service = _serviceProvider.GetService<UserRepositoryService>();

            await service.UpdateAsync(user);
        }

        private async void DeleteMenu_Click(object sender, RoutedEventArgs e)
        {
            User user = (User)userGrid.SelectedItem;
            UserRepositoryService? service = _serviceProvider.GetService<UserRepositoryService>();

            await service.DeleteAsync(user);

            var response = await service.GetAllAsync();
            List<User> users = response.Data;
            userGrid.ItemsSource = users;
        }
    }
}
