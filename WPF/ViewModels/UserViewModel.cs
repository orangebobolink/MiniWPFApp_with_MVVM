using AutoMapper;
using BLL;
using BLL.DTO;
using BLL.RepositoryServices.Implementations.RepositoryServices;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.VisualBasic;
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

        private string _firstName;
        public string FirstName
        {
            get { return _firstName; }
            set
            {
                _firstName = value;
                OnPropertyChanged("SelectedPhone");
            }
        }

        private string _lastName;
        public string LastName
        {
            get { return _lastName; }
            set
            {
                _lastName = value;
                OnPropertyChanged("SelectedPhone");
            }
        }

        private string _middleName;
        public string MiddleName
        {
            get { return _middleName; }
            set
            {
                _middleName = value;
                OnPropertyChanged("SelectedPhone");
            }
        }

        private string _age;
        public string Age
        {
            get { return _age; }
            set
            {
                _age = value;
                OnPropertyChanged("SelectedPhone");
            }
        }

        private string _typeAnimal;
        public string TypeAnimal
        {
            get { return _typeAnimal; }
            set
            {
                _typeAnimal = value;
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
                      var allTypeAnimal = await GetAllTypeAnimalAsync();

                      var allAnimal = await GetAllAnimalAsync();
                      allAnimal.ForEach(a => a.Type = allTypeAnimal[a.TypeId - 1]);

                      var allTrophy = await GetAllTrophyAsync();
                      allTrophy.ForEach(t => t.Animal = allAnimal[t.AnimalId - 1]);

                      var allUsers = await GetAllUsersAsync();
                      allUsers.ForEach(u => u.Trophy = allTrophy[u.TrophyId - 1]);

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
                      UserDTO user = (UserDTO)obj;
                      if (user != null)
                      {
                          UsersDTO.Remove(user);
                          var service = _serviceProvider.GetService<UserRepositoryService>();
                          user.Trophy = null;

                          var f = service.DeleteAsync(user)?.Result;
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
                      try
                      {
                          var animal = _findAnimalText;
                          var allUsers = await GetAllUsersAsync();

                          if (animal != null)
                              allUsers = allUsers.Where(user => user.Trophy.Animal.Type.Name == animal)
                                                  .ToList();

                          UsersDTO.Clear();
                          allUsers.ForEach(user => UsersDTO.Add(user));
                      }
                      catch { }
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

        private RelayCommand _updateCommand;
        public RelayCommand UpdateCommand
        {
            get
            {
                return _updateCommand ??
                  (_updateCommand = new RelayCommand(async obj =>
                  {
                      UserDTO user = (UserDTO)obj;
                      if (user != null)
                      {
                          user.Trophy = null;
                          var service = _serviceProvider.GetService<UserRepositoryService>();

                          var f = service.UpdateAsync(user)?.Result;
                      }
                  }));
            }
        }

        private RelayCommand _createAddWindowCommand;
        public RelayCommand CreateAddWindowCommand
        {
            get
            {
                return _createAddWindowCommand ??
                  (_createAddWindowCommand = new RelayCommand(async obj =>
                  {
                      var window = new Window1();
                      window.ShowDialog();
                  }));
            }
        }

        private RelayCommand _addCommand;
        public RelayCommand AddCommand
        {
            get
            {
                return _addCommand ??
                  (_addCommand = new RelayCommand(async obj =>
                  {
                      try
                      {
                          var allTypes = await GetAllTypeAnimalAsync();

                          _typeAnimal = _typeAnimal.Split(": ")[1];

                          TypeAnimalDTO? typeAnimal = allTypes
                            .FirstOrDefault(type => type.Name == _typeAnimal);

                          var allAnimal = await GetAllAnimalAsync();
                          AnimalDTO? animal = allAnimal.FirstOrDefault(animal => animal.TypeId == typeAnimal.Id);

                          TrophyDTO? trophy = (await GetAllTrophyAsync())
                          .FirstOrDefault(trophy => trophy.AnimalId == animal.Id);

                          var user = new UserDTO()
                          {
                              FirstName = _firstName,
                              LastName = _lastName,
                              MiddleName = _middleName,
                              TrophyId = trophy.Id,
                              Age = int.Parse(_age)
                          };

                          var service = _serviceProvider.GetService<UserRepositoryService>();

                          await service.CreateAsync(user);
                      }
                      catch { }
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

        private async Task<List<TypeAnimalDTO>> GetAllTypeAnimalAsync()
        {
            var service = _serviceProvider.GetService<TypeAnimalRepositoryService>();

            var response = await service.GetAllAsync();

            if (response.StatusCode.StatusCode == 200)
                return response.Data;
            return new List<TypeAnimalDTO>();
        }

        private async Task<List<TrophyDTO>> GetAllTrophyAsync()
        {
            var service = _serviceProvider.GetService<TrophyRepositoryService>();

            var response = await service.GetAllAsync();

            if (response.StatusCode.StatusCode == 200)
                return response.Data;
            return new List<TrophyDTO>();
        }

        private async Task<List<AnimalDTO>> GetAllAnimalAsync()
        {
            var service = _serviceProvider.GetService<AnimalRepositoryService>();

            var response = await service.GetAllAsync();

            if (response.StatusCode.StatusCode == 200)
                return response.Data;
            return new List<AnimalDTO>();
        }
    }
}