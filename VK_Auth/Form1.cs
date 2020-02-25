using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using VkNet;
using VkNet.Enums.Filters;
using VkNet.Model.RequestParams;

namespace VK_Auth
{
    public partial class Form1 : Form
    {
        VkApi vk = new VkApi();
        private static long UserId;
        private static string Token = Store.Default.token;
        private static string NameID = null;
        private static string SurnameID = null;
        public Form1()
        {
            InitializeComponent();            
        }     

        public void Auth(string login, string pass)
        {
            string appid = "7038917";
            string scope = "friends,photos";
            string url = "https://oauth.vk.com/authorize?client_id="+ appid + "&scope=" + scope+ "&display=popup&redirect_uri=https://oauth.vk.com/blank.html&response_type=token&v=5.52&revoke=1";
            Form2 f2 = new Form2();
            f2.Show();
            WebBrowser browser = (WebBrowser)f2.Controls["webBrowser1"];
            browser.Navigate(url);
           
            UserId = long.Parse(Store.Default.id);
            vk.Authorize(new VkNet.Model.ApiAuthParams { AccessToken = Token });
            var getUserInfo = vk.Users.Get(new long[] { UserId }).FirstOrDefault();
            if (Store.Default.auth == true)
            {
                var getUserPhoto = vk.Users.Get(new long[] {UserId}, ProfileFields.Photo400Orig, VkNet.Enums.SafetyEnums.NameCase.Nom, true).FirstOrDefault();
                
                pictureBox1.ImageLocation = $"{getUserPhoto.Photo400Orig}";
                NameID = getUserInfo.FirstName;
                SurnameID = getUserInfo.LastName;
                label1.Text = "Вы вошли как  " + NameID + " " + SurnameID + ".";
            }
            

        }
        private void Form1_Load(object sender, EventArgs e)
        {
            
        }

        private void button1_Click(object sender, EventArgs e)
        {
            try
            {
                Auth(null, null);
            }
            catch
            {

            }
                    
        }

        private void button2_Click(object sender, EventArgs e)
        {
            try
            {
                listBox1.Items.Clear();
                GetFriends();
            }
            catch
            {
                MessageBox.Show("Вы не авторизованы!");
            }
            
        }
        public void GetFriends()
        {
            var users = vk.Friends.Get(new FriendsGetParams {
                UserId = UserId,
                Count = 5,
                Fields = ProfileFields.FirstName
            });
            foreach (var friend in users)
            {
                listBox1.Items.Add(friend.FirstName + " " + friend.LastName);                
            }
        }
    }
}
