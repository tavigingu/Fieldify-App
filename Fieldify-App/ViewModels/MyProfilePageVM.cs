using Fieldify_App.Commands;
using Fieldify_App.Data;
using Fieldify_App.Models;
using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using System.Windows.Media.Imaging;

namespace Fieldify_App.ViewModels
{
    public class MyProfilePageVM : ViewModelBase
    {

        public MyProfilePageVM(User user)
        {
            LoadUser(user);

            SaveCommand = new RelayCommand<object>(_ =>SaveChanges());
            UploadProfilePictureCommand = new RelayCommand<object>(_ => UploadProfileImage());
        }

        public MyProfilePageVM()
        {

        }
        
        private User _currentUser;

        public User CurrentUser
        {
            get => _currentUser;
            private set
            {
                _currentUser = value;
                OnPropertyChanged(nameof(CurrentUser));
            }
        }

        // Proprietăți pentru binding
        private string _username;
        public string Username
        {
            get => _username;
            set
            {
                _username = value;
                OnPropertyChanged(nameof(Username));
            }
        }

        private string _email;
        public string Email
        {
            get => _email;
            set
            {
                _email = value;
                OnPropertyChanged(nameof(Email));
            }
        }

        private string _name;
        public string Name
        {
            get => _name;
            set
            {
                _name = value;
                OnPropertyChanged(nameof(Name));
            }
        }

        private string _surname;
        public string Surname
        {
            get => _surname;
            set
            {
                _surname = value;
                OnPropertyChanged(nameof(Surname));
            }
        }

        private string _description;
        public string Description
        {
            get => _description;
            set
            {
                _description = value;
                OnPropertyChanged(nameof(Description));
            }
        }

        private string _phone;
        public string Phone
        {
            get => _phone;
            set
            {
                _phone = value;
                OnPropertyChanged(nameof(Phone));
            }
        }

        // Comenzi
        public ICommand EditCommand { get; }
        public ICommand SaveCommand { get; }

        public ICommand UploadProfilePictureCommand { get; }

        private byte[] _profileImageData;
        public byte[] ProfileImageData
        {
            get => _profileImageData;
            set
            {
                _profileImageData = value;
                OnPropertyChanged(nameof(ProfileImageData));
            }
        }

        private BitmapImage _profileImage;
        public BitmapImage ProfileImage
        {
            get => _profileImage;
            set
            {
                _profileImage = value;
                OnPropertyChanged(nameof(ProfileImage));
            }
        }

        private double _profileImageOpacity = 0; // Inițial opacitate 0
        public double ProfileImageOpacity
        {
            get => _profileImageOpacity;
            set
            {
                _profileImageOpacity = value;
                OnPropertyChanged(nameof(ProfileImageOpacity));
            }
        }


        private void UploadProfileImage()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog
            {
                Filter = "Image Files|*.jpg;*.jpeg;*.png"
            };

            if (openFileDialog.ShowDialog() == true)
            {
                // Încarcă imaginea din fișier
                BitmapImage bitmap = new BitmapImage();
                bitmap.BeginInit();
                bitmap.UriSource = new Uri(openFileDialog.FileName);
                bitmap.CacheOption = BitmapCacheOption.OnLoad;
                bitmap.EndInit();
                bitmap.Freeze();

                // Actualizează proprietățile
                ProfileImage = bitmap;
                ProfileImageOpacity = 1; // Setează opacitatea pe 1

                // Convertește imaginea în byte[] pentru salvare
                byte[] imageData = ImageToByteArray(bitmap);
                SaveProfileImage(imageData);
            }
        }

        private void SaveProfileImage(byte[] profileImageData)
        {
            using (var context = new FieldifyDataDataContext())
            {
                var user = context.Users.SingleOrDefault(u => u.Username == CurrentUser.Username);
                if (user != null)
                {
                    user.ProfileImage = profileImageData;
                    context.SubmitChanges();

                    // Actualizează și obiectul local
                    CurrentUser.ProfileImage = profileImageData;
                }
            }
        }

        private byte[] ImageToByteArray(BitmapImage image)
        {
            using (MemoryStream stream = new MemoryStream())
            {
                JpegBitmapEncoder encoder = new JpegBitmapEncoder();
                encoder.Frames.Add(BitmapFrame.Create(image));
                encoder.Save(stream);
                return stream.ToArray();
            }
        }

        private string _editingField;
        public string EditingField
        {
            get => _editingField;
            set
            {
                _editingField = value;
                OnPropertyChanged(nameof(IsEditingUsername));
                OnPropertyChanged(nameof(IsEditingEmail));
                OnPropertyChanged(nameof(IsEditingName));
                OnPropertyChanged(nameof(IsEditingSurname));
                OnPropertyChanged(nameof(IsEditingDescription));
                OnPropertyChanged(nameof(IsEditingPhone));
            }
        }

        public bool IsEditingUsername => EditingField == nameof(Username);
        public bool IsEditingEmail => EditingField == nameof(Email);
        public bool IsEditingName => EditingField == nameof(Name);
        public bool IsEditingSurname => EditingField == nameof(Surname);
        public bool IsEditingDescription => EditingField == nameof(Description);
        public bool IsEditingPhone => EditingField == nameof(Phone);

   
        private void LoadUser(User user)
        {
            CurrentUser = user;

            if (CurrentUser != null)
            {
                Username = CurrentUser.Username;
                Email = CurrentUser.Email;
                Name = CurrentUser.Nume;
                Surname = CurrentUser.Prenume;
                Description = CurrentUser.Description;
                Phone = CurrentUser.NumarTelefon;

                if (user.ProfileImage != null)
                {
                    ProfileImage = ByteArrayToImage(user.ProfileImage.ToArray());
                    ProfileImageOpacity = 1;
                }
            }
        }

        private BitmapImage ByteArrayToImage(byte[] byteArray)
        {
            if (byteArray == null) return null;

            BitmapImage image = new BitmapImage();
            using (MemoryStream stream = new MemoryStream(byteArray))
            {
                image.BeginInit();
                image.StreamSource = stream;
                image.CacheOption = BitmapCacheOption.OnLoad;
                image.EndInit();
            }
            return image;
        }

        private void SaveChanges()
        {

                CurrentUser.Username = Username;
                CurrentUser.Email = Email;
                CurrentUser.Nume = Name;
                CurrentUser.Prenume = Surname;
                CurrentUser.Description = Description;
                CurrentUser.NumarTelefon = Phone;

                MyProfilePageModel.SaveUserDetails(CurrentUser);
            

            EditingField = null; // Oprește editarea
        }
    }
}
