using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace VK_Auth
{
    public partial class Form2 : Form
    {
        public Form2()
        {
            InitializeComponent();
        }

        private void webBrowser1_DocumentCompleted(object sender, WebBrowserDocumentCompletedEventArgs e)
        {
            try
            {
                string url = webBrowser1.Url.ToString();
                string el = url.Split('#')[1];
                if (el[0] == 'a')
                {
                    string token = el.Split('&')[0].Split('=')[1];
                    string id = el.Split('=')[3];
                    
                    this.Close();
                    if (token != "")
                    {
                        this.Hide();
                        MessageBox.Show("Авторизация прошла успешно!");
                        Store.Default.token = token;
                        Store.Default.id = id;
                        Store.Default.auth = true;
                        Store.Default.Save();
                    }
                    else
                    {
                        Store.Default.auth = false;
                        Store.Default.Save();
                    }
                }                
            }
            catch
            {

            }
        }
    }
}
