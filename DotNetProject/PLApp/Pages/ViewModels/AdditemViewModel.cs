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
using System.Text;
using System.Threading.Tasks;
using System.Web.UI.WebControls;
using System.Windows;
using System.Windows.Input;

namespace PLApp.Pages.ViewModels
{
    public class AdditemViewModel : INotifyPropertyChanged
    {
        private AddItemModel additemM;
        public BE.Item itemViewSource;
        private string _imagepath;
        public string imagepath
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
            itemViewSource = new BE.Item() { BarcodeNumber = 123465};
            additemM = new AddItemModel();
            DriveItems = new ObservableCollection<BE.Item>(additemM.Items);
            LoadImageCommand = new RelayCommand(new Action<object>(OpenFileDialog));
            AddButtonCommand  = new RelayCommand(new Action<object>(AddButton_Click));
        }

        public ICommand AddButtonCommand { get; set; }
        public ICommand LoadImageCommand { get; set; }


        private void OpenFileDialog(object sender)
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();
            if (openFileDialog.ShowDialog() == true)
            {
                var newImagePath = Path.Combine(Environment.CurrentDirectory, "Images", itemViewSource.ItemId+System.IO.Path.GetExtension(openFileDialog.FileName));
                File.Copy(openFileDialog.FileName, newImagePath);
                itemViewSource.ItemPic = newImagePath;
                imagepath = newImagePath;
            }
        }
        private void AddButton_Click(object sender)
        {
            //TODO: check Fields
            MessageBox.Show(itemViewSource.ItemName + " Add success");
            foreach (Window item in Application.Current.Windows)
            {
                if (item.DataContext == this) item.Close();
            }
        }

        public event PropertyChangedEventHandler PropertyChanged;

        private void OnPropertyChanged(string propertyName)
        {
            if (PropertyChanged != null)
                PropertyChanged(this, new PropertyChangedEventArgs(propertyName));
        }
    }
}
