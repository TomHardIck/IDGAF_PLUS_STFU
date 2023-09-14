using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
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
using API.Models;
using System.Security.Cryptography;
using Newtonsoft.Json.Linq;
using Microsoft.Win32;
using System.Windows.Media.Effects;
using System.Security.Cryptography.X509Certificates;
using System.Net.Security;

namespace DesktopApp
{
    /// <summary>
    /// Логика взаимодействия для MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        HttpClient client = new HttpClient();
        RegistryKey curUserKey = Registry.CurrentUser;
        public MainWindow()
        {
            InitializeComponent();
            client.BaseAddress = new Uri("https://192.168.56.1:7187/api/");
            System.Net.ServicePointManager.ServerCertificateValidationCallback = delegate (object sender, X509Certificate certificate, X509Chain chain, SslPolicyErrors sslPolicyErrors) { return true; };
            client.DefaultRequestHeaders.Accept.Clear();
            client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));

            RegistryKey key = curUserKey.OpenSubKey("WindowSize");
            if(key != null)
            {
                Height = Convert.ToDouble(key.GetValue("Height"));
                Width = Convert.ToDouble(key.GetValue("Width"));
                Left = Convert.ToDouble(key.GetValue("Left"));
                Top = Convert.ToDouble(key.GetValue("Top"));
                key.Close();
            }

            RegistryKey key1 = curUserKey.OpenSubKey("LastUser");
            if (key1 != null)
            {
                loginBox.Text = key1.GetValue("Login").ToString();
                passwordBox.Text = key1.GetValue("Password").ToString();
                key1.Close();
            }
        }

        private async void authButton_Click(object sender, RoutedEventArgs e)
        {
            var data = (JObject)JsonConvert.DeserializeObject(await client.GetStringAsync("Users"));
            List<User> allUsers = data["data"].ToObject<List<User>>();
            foreach (User user in allUsers)
            {
                if(user.UserLogin == loginBox.Text && AreEqual(passwordBox.Text, user.UserPassword, user.Salt) && user.UserRoleId == 1)
                {
                    RegistryKey key = curUserKey.CreateSubKey("LastUser");
                    key.SetValue("Login", loginBox.Text);
                    key.SetValue("Password", passwordBox.Text);

                    MessageBox.Show("Успешный вход!");
                    DataWindow dataWindow = new DataWindow();
                    dataWindow.Show();
                }
            }
        }

        public string GenerateHash(string input, string salt)
        {
            byte[] bytes = Encoding.UTF8.GetBytes(input + salt);
            SHA256Managed sHA256Managed = new SHA256Managed();
            byte[] hash = sHA256Managed.ComputeHash(bytes);
            return Convert.ToBase64String(hash);
        }

        public bool AreEqual(string text, string hash, string salt)
        {
            string newPin = GenerateHash(text, salt);
            return newPin.Equals(hash);
        }

        private void Window_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            RegistryKey key = curUserKey.CreateSubKey("WindowSize");
            key.SetValue("Height", Height);
            key.SetValue("Width", Width);
            key.SetValue("Top", Top);
            key.SetValue("Left", Left);
        }
    }
}
