﻿using BE;
using Microsoft.Win32;
using PLApp.Commands;
using PLApp.Pages.Models;
using System;
using System.Collections.ObjectModel;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Input;

namespace PLApp.Pages.ViewModels
{
    public class AdditemViewModel : INotifyPropertyChanged
    {
        private AddItemModel additemM;
        public Item itemViewSource;
        private string _imagepath;
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
            additemM = new AddItemModel();
            DriveItems = new ObservableCollection<BE.Item>(additemM.Items);
            LoadImageCommand = new RelayCommand(new Action<object>(OpenFileDialog));
            AddButtonCommand = new RelayCommand(new Action<object>(AddButton_Click));
        }

        public ICommand AddButtonCommand { get; set; }
        public ICommand LoadImageCommand { get; set; }

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
                itemViewSource.ItemPic = newImagePath;
                Imagepath = newImagePath;
            }
        }
        
        private void AddButton_Click(object sender)
        {
            if (itemViewSource.BarcodeNumber > 0 && itemViewSource.ItemName != "" && itemViewSource.ItemPic != "" && itemViewSource.ItemPrice > 0
                && itemViewSource.Quantity != null && itemViewSource.Quantity > 0)
            {
                foreach (Window item in Application.Current.Windows)
                    if (item.DataContext == this) item.Close();
            }
            else MessageBox.Show("Please fill the whole data!", "Oops.. Something went wrong", MessageBoxButton.OK, MessageBoxImage.Error);
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}