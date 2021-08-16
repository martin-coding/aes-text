using System;
using System.Collections.Generic;
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

namespace AESTEXT
{
    public partial class MainWindow : Window
    {
        public static string pwinput;
        public static string ivinput;

        public MainWindow()
        {
            InitializeComponent();
        }

        private void Encrypt(object sender, RoutedEventArgs e)
        {
            if (pw_box.Password != "" && iv_box.Password != "")
            {
                if (txt_unv.Text != "")
                {
                    pwinput = pw_box.Password;
                    ivinput = iv_box.Password;

                    AES obj_aes = new AES();
                    try
                    {
                        txt_ver.Text = obj_aes.encrypt(txt_unv.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter your message", "ERROR - missing");
                }
            }
            else
            {
                MessageBox.Show("Please specify your iv and key", "ERROR - missing");
            }
        }

        private void Decrypt(object sender, RoutedEventArgs e)
        {
            if (pw_box.Password != "" && iv_box.Password != "")
            {
                if (txt_ver.Text != "")
                {
                    pwinput = pw_box.Password;
                    ivinput = iv_box.Password;

                    AES obj_aes = new AES();
                    try
                    {
                        txt_unv.Text = obj_aes.decrypt(txt_ver.Text);
                    }
                    catch (Exception ex)
                    {
                        MessageBox.Show(ex.Message);
                    }
                }
                else
                {
                    MessageBox.Show("Please enter your encrypted message", "ERROR - missing");
                }
            }
            else
            {
                MessageBox.Show("Password and initialization vector are required for decryption", "ERROR - missing");
            }
        }

        private void Clear(object sender, RoutedEventArgs e)
        {
            txt_unv.Text = "";
            txt_ver.Text = "";
            pw_box.Password = "";
            iv_box.Password = "";
        }
    }
}
