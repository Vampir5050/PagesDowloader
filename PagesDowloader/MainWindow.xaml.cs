using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading;
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

namespace PagesDowloader
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

        private async void downloadButton_Click(object sender, RoutedEventArgs e)
        {
            responseTextBlock.Text = "";
            downloadPCButton.IsEnabled = false;
            statusCode.Content = "";
            if (!addressTextBox.Text.StartsWith("http") || !addressTextBox.Text.StartsWith("https")) return;
            using (HttpClient client = new HttpClient())
            {
                try
                {

                    var response = await client.GetAsync(addressTextBox.Text);
                    responseTextBlock.Text = await response.Content.ReadAsStringAsync();
                    statusCode.Content = response.StatusCode;
                }
                catch (HttpRequestException ex)
                {
                    statusCode.Content = ex.Message;
                }

            }
            if (!string.IsNullOrWhiteSpace(responseTextBlock.Text)) downloadPCButton.IsEnabled = true;

        }

        private async void downloadPCButton_Click(object sender, RoutedEventArgs e)
        {
            await using StreamWriter sw = new StreamWriter("page.http", false, Encoding.UTF8);
            await sw.WriteLineAsync(responseTextBlock.Text);
            sw.Close();
        }
    }
}