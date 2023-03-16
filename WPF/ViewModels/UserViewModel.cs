using AutoMapper;
using BLL;
using BLL.DTO;
using BLL.RepositoryServices.Implementations.RepositoryServices;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Threading;
using System.Threading.Tasks;
using WPF.Models;

namespace WPF.ViewModels
{
    internal class UserViewModel : INotifyPropertyChanged
    {
        private readonly IServiceProvider _serviceProvider;
        private readonly IMapper _mapper;
        public ObservableCollection<UserDTO> UsersDTO { get; set; }

        public UserViewModel()
        {
            UsersDTO = new ObservableCollection<UserDTO>(new List<UserDTO>());

            var serviceCollection = new ServiceCollection();
            serviceCollection.AddBLLServices();

            _serviceProvider = serviceCollection.BuildServiceProvider();
            _mapper = _serviceProvider.GetRequiredService<IMapper>();
        }

        private UserDTO _selectedUserDTO;
        public UserDTO SelectedUser
        {
            get { return _selectedUserDTO; }
            set
            {
                _selectedUserDTO = value;
                OnPropertyChanged("SelectedUser");
            }
        }

        private string _findAgeText;
        public string FindAgeText
        {
            get { return _findAgeText; }
            set
            {
                _findAgeText = value;
                OnPropertyChanged("SelectedPhone");
            }
        }

        private string _findAnimalText;
        public string FindAnimalText
        {
            get { return _findAnimalText; }
            set
            {
                _findAnimalText = value;
                OnPropertyChanged("SelectedPhone");
            }
        }

        private RelayCommand _fillCommand;
        public RelayCommand FillCommand
        {
            get
            {
                return _fillCommand ??
                  (_fillCommand = new RelayCommand(async obj =>
                  {
                      var allUsers = await GetAllUsersAsync();

                      UsersDTO.Clear();
                      allUsers.ForEach(user => UsersDTO.Add(user));
                  }));
            }
        }

        private RelayCommand _removeCommand;
        public RelayCommand RemoveCommand
        {
            get
            {
                return _removeCommand ??
                  (_removeCommand = new RelayCommand(obj =>
                  {
                      UserDTO phone = (UserDTO)obj;
                      if (phone != null)
                      {
                          UsersDTO.Remove(phone);
                          //var service = _serviceProvider.GetService<UserRepositoryService>();

                          //var f = service.DeleteAsync(phone)?.Result;
                      }
                  },
                 (obj) => UsersDTO.Count > 0));
            }
        }

        private RelayCommand _findAgeCommand;
        public RelayCommand FindAgeCommand
        {
            get
            {
                return _findAgeCommand ??
                  (_findAgeCommand = new RelayCommand(async obj =>
                  {
                      var age = int.Parse(_findAgeText);
                      var allUsers = await GetAllUsersAsync();

                      if (age != null)
                          allUsers = allUsers.Where(user => user.Age == age)
                                              .ToList();

                      UsersDTO.Clear();
                      allUsers.ForEach(user => UsersDTO.Add(user));
                  }));
            }
        }

        private RelayCommand _findAnimalCommand;
        public RelayCommand FindAnimalCommand
        {
            get
            {
                return _findAnimalCommand ??
                  (_findAnimalCommand = new RelayCommand(async obj =>
                  {
                      var animal = _findAnimalText;
                      var allUsers = await GetAllUsersAsync();

                      if (animal != null)
                          allUsers = allUsers.Where(user => user.Trophy.Animal.Type.Name == animal)
                                              .ToList();

                      UsersDTO.Clear();
                      allUsers.ForEach(user => UsersDTO.Add(user));
                  }));
            }
        }

        private RelayCommand _recordingCommand;
        public RelayCommand RecordingCommand
        {
            get
            {
                return _recordingCommand ??
                  (_recordingCommand = new RelayCommand(async obj =>
                  {
                      var allUser = await GetAllUsersAsync();

                      var service = _serviceProvider.GetService<UserRepositoryService>();

                      allUser.ForEach(async user => await service.DeleteAsync(user));

                      Thread.Sleep(1000);

                      UsersDTO.ToList().ForEach(async user => await service.CreateAsync(user));
                  }));
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        public void OnPropertyChanged([CallerMemberName] string prop = "")
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(prop));
        }

        private async Task<List<UserDTO>> GetAllUsersAsync()
        {
            var service = _serviceProvider.GetService<UserRepositoryService>();

            var response = await service.GetAllAsync();

            if (response.StatusCode.StatusCode == 200)
                return response.Data;
            return new List<UserDTO>();
        }
    }
}