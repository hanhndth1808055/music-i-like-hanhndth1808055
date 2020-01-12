using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices.WindowsRuntime;
using Windows.Foundation;
using Windows.Foundation.Collections;
using Windows.Storage;
using Windows.Storage.Pickers;
using Windows.UI.Xaml;
using Windows.UI.Xaml.Controls;
using Windows.UI.Xaml.Controls.Primitives;
using Windows.UI.Xaml.Data;
using Windows.UI.Xaml.Input;
using Windows.UI.Xaml.Media;
using Windows.UI.Xaml.Navigation;
using FinalProjectMusic.Services;

// The Blank Page item template is documented at https://go.microsoft.com/fwlink/?LinkId=234238

namespace FinalProjectMusic.Pages
{
    /// <summary>
    /// An empty page that can be used on its own or navigated to within a Frame.
    /// </summary>
    public sealed partial class DemoFilePage : Page
    {
        public DemoFilePage()
        {
            this.InitializeComponent();
        }

        private async void Write_Clicked(object sender, RoutedEventArgs e)
        {
            FileContent.Text = "";
            if (!String.IsNullOrEmpty(FileName.Text) && !String.IsNullOrEmpty(Content1.Text))
            {
                await FileHandleService.WriteToFile(FileName.Text, Content1.Text);
                FileContent.Text = "Create file successfully!";
                FileName.Text = "";
                Content1.Text = "";
                return;
            }

            FileContent.Text = "Please insert file name and content!";

        }

        private async void Read_Clicked(object sender, RoutedEventArgs e)
        {
            FileContent.Text = "";
            if (!String.IsNullOrEmpty(FileName.Text))
            {
                string fileContent = await FileHandleService.ReadFile(FileName.Text);

                FileContent.Text = fileContent;
                FileName.Text = "";
                Content1.Text = "";
                return;
            }

            FileContent.Text = "Please insert file name!";
        }

        private async void Write_Picker_Clicked(object sender, RoutedEventArgs e)
        {
            FileSavePicker savePicker = new FileSavePicker();
            savePicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            // Dropdown of file types the user can save the file as
            savePicker.FileTypeChoices.Add("Plain Text", new List<string>() { ".txt" });
            // Default file name if the user does not type one in or select a file to replace
            savePicker.SuggestedFileName = "New Document";

            StorageFile file = await savePicker.PickSaveFileAsync();
            
                if (file != null)
            {
                FileHandleService.WriteToFile(file, Content1.Text);
            }
        }

        private async void Read_Picker_Clicked(object sender, RoutedEventArgs e)
        {
            FileOpenPicker openPicker = new FileOpenPicker();
            openPicker.ViewMode = PickerViewMode.Thumbnail;
            openPicker.SuggestedStartLocation = PickerLocationId.DocumentsLibrary;
            openPicker.FileTypeFilter.Add(".txt");
            openPicker.FileTypeFilter.Add(".jpeg");
            openPicker.FileTypeFilter.Add(".png");

            StorageFile file = await openPicker.PickSingleFileAsync();
            if (file != null)
            {
                // Application now has read/write access to the picked file
                FileContent.Text = "Picked file: " + file.Name;
            }
            else
            {
                FileContent.Text = "Operation cancelled.";
            }
        }
    }
}
