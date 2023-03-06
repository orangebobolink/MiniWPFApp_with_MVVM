using AutoMapper;
using BLL;
using BLL.DTO;
using BLL.RepositoryServices.Implementations.RepositoryServices;
using DAL.Domain.Models;
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
        private readonly IMapper _mapper;

        public MainWindow()
        {
            InitializeComponent();
            var serviceCollection = new ServiceCollection();
            serviceCollection.AddBLLServices();

            _serviceProvider = serviceCollection.BuildServiceProvider();
            _mapper = _serviceProvider.GetRequiredService<IMapper>(); // тута
        }

        private async void Fill()
        {
            UserRepositoryService? service = _serviceProvider.GetService<UserRepositoryService>();

            var response = await service.GetAllAsync();

            if (response.StatusCode.StatusCode == 200)
            {
                var users = _mapper.Map<List<UserDTO>>(response.Data);

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
            List<UserDTO> users = (List<UserDTO>)userGrid.ItemsSource;
            UserRepositoryService? service = _serviceProvider.GetService<UserRepositoryService>();

            users.ForEach(async user => await service.UpdateAsync(user)); // TODO: тут шото не то
        }

        private async void EditMenu_Click(object sender, RoutedEventArgs e)
        {
            UserDTO user = (UserDTO)userGrid.SelectedItem;
            UserRepositoryService? service = _serviceProvider.GetService<UserRepositoryService>();

            await service.UpdateAsync(user);
        }

        private async void DeleteMenu_Click(object sender, RoutedEventArgs e)
        {
            UserDTO user = (UserDTO)userGrid.SelectedItem;
            UserRepositoryService? service = _serviceProvider.GetService<UserRepositoryService>();

            await service.DeleteAsync(user); // тута

            var response = await service.GetAllAsync();
            List<UserDTO> users = _mapper.Map<List<UserDTO>>(response.Data);
            userGrid.ItemsSource = users;
        }
    }
}
