using BE;
using Microsoft.Win32;
using PLApp.Commands;
using PLApp.Pages.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace PLApp.Pages.ViewModels
{
    public class AdditemViewModel : INotifyPropertyChanged
    {
        public List<Item> ItemsList { get; set; }
        private AddItemModel additemM;
        public ObservableCollection<Item> DriveList { get; set; }
        private Item _itemViewSource;
        public Item itemViewSource
        {
            get => _itemViewSource;
            set
            {
                _itemViewSource = value;
                OnPropertyChanged("itemViewSource");
            }
        }
        private string _imagepath;
        public bool isValid = false;
        public string Imagepath
        {
            get => _imagepath;
            set
            {
                _imagepath = value;
                OnPropertyChanged("imagepath");
            }
        }
        public ObservableCollection<BE.Item> DriveItems { get; set; }

        public AdditemViewModel()
        {
            itemViewSource = new BE.Item() { BarcodeNumber = 123465 };
            ItemsList = App.db.GetAllItems().ToList();
            additemM = new AddItemModel();
            DriveItems = new ObservableCollection<BE.Item>(additemM.Items);
            LoadImageCommand = new RelayCommand(new Action<object>(OpenFileDialog));
            AddButtonCommand = new RelayCommand(new Action<object>(AddButton_Click));
            SearchSelectionChanged = new RelayCommand(new Action<object>(Search_SelectionChanged));
            DriveList = new ObservableCollection<Item>(additemM.Items);

        }

        public ICommand AddButtonCommand { get; set; }
        public ICommand LoadImageCommand { get; set; }
        public ICommand SearchSelectionChanged { get; set; }

        private string GenerateUniqName()
        {
            string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            char[] stringChars = new char[16];
            Random random = new Random();

            for (int i = 0; i < stringChars.Length; i++)
                stringChars[i] = chars[random.Next(chars.Length)];

            return new String(stringChars);
        }

        private void OpenFileDialog(object sender)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                string newImagePath = Path.Combine(Environment.CurrentDirectory, "Images", GenerateUniqName() + Path.GetExtension(openFileDialog.FileName));
                File.Copy(openFileDialog.FileName, newImagePath);
                itemViewSource.ItemPic = GenerateUniqName() + Path.GetExtension(openFileDialog.FileName);
                Imagepath = newImagePath;
                OnPropertyChanged("itemViewSource");
            }
        }
        
        private void AddButton_Click(object sender)
        {
            if (itemViewSource.BarcodeNumber > 0 && itemViewSource.ItemName != "" && itemViewSource.ItemPic != "" && itemViewSource.ItemPrice > 0
                && itemViewSource.Quantity != null && itemViewSource.Quantity > 0)
            {
                isValid = true;
                foreach (Window item in Application.Current.Windows)
                    if (item.DataContext == this) item.Close();
            }
            else MessageBox.Show("Please fill the whole data!", "Oops.. Something went wrong", MessageBoxButton.OK, MessageBoxImage.Error);
        }
        private void Search_SelectionChanged(object sender)
        {
            MessageBox.Show("Test");
        }
        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
