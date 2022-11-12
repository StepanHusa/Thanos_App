using System;
using System.Collections;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Microsoft.WindowsAPICodePack.Dialogs;

namespace Thannos_App
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
        }

        private void selectButton_Click(object sender, RoutedEventArgs e)
        {
            var dialog = new CommonOpenFileDialog();
            dialog.IsFolderPicker = true;
            dialog.Multiselect = false;
            CommonFileDialogResult result = dialog.ShowDialog();

            if(result == CommonFileDialogResult.Ok)
            {
                string path = dialog.FileName;

                var files = GetFilesFromSubdirectories(path);
                var len = files.Count;

                var random = new Random();
                var buffer = new byte[len / 8 + 1];
                random.NextBytes(buffer);
                var dieThrow = new BitArray(buffer);

                for (int i = 0; i < len; i++)
                {                    
                    if (dieThrow[i])
                    {
                        File.Delete(files[i]);
                    }
                }

                background.Visibility = Visibility.Collapsed;
                selectButton.Visibility = Visibility.Collapsed;
                finalImage.Visibility = Visibility.Visible;
                labelDelete.Visibility = Visibility.Visible;
            }
        }

        private List<string> GetFilesFromSubdirectories(string path)
        {
            List<string> files = new List<string>();

            files.AddRange(Directory.GetFiles(path));

            return files;
        }
    }
}
