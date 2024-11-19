using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Windows;
using System.Windows.Media.Imaging;

namespace PhotoGallery
{
    public partial class MainWindow : Window
    {
        private List<string> imagePaths = new List<string>();
        private int currentIndex = -1; 

        public MainWindow()
        {
            InitializeComponent();
        }

        private void ShowImage()
        {
            if (currentIndex >= 0 && currentIndex < imagePaths.Count)
            {
                
                var image = new BitmapImage(new Uri(imagePaths[currentIndex], UriKind.Absolute));
                ImageViewer.Source = image;
            }
            else
            {
                
                ImageViewer.Source = null;
            }
        }

        private void PreviousImage_Click(object sender, RoutedEventArgs e)
        {
            if (imagePaths.Count > 0)
            {
               
                currentIndex = (currentIndex - 1 + imagePaths.Count) % imagePaths.Count;
                ShowImage();
            }
        }

        private void NextImage_Click(object sender, RoutedEventArgs e)
        {
            if (imagePaths.Count > 0)
            {
               
                currentIndex = (currentIndex + 1) % imagePaths.Count;
                ShowImage();
            }
        }

        private void OpenFile_Click(object sender, RoutedEventArgs e)
        {
           
            var openFileDialog = new OpenFileDialog
            {
                Filter = "Obrazy (*.jpg;*.jpeg;*.png;*.bmp)|*.jpg;*.jpeg;*.png;*.bmp",
                Multiselect = true 
            };

            if (openFileDialog.ShowDialog() == true)
            {
               
                foreach (var filePath in openFileDialog.FileNames)
                {
                    imagePaths.Add(filePath);
                }

                
                if (currentIndex == -1 && imagePaths.Count > 0)
                {
                    currentIndex = 0;
                }

              
                ShowImage();
            }
        }

        private void CloseApp_Click(object sender, RoutedEventArgs e)
        {
            
            Application.Current.Shutdown();
        }
    }
}