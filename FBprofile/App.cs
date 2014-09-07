using Facebook;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Net.NetworkInformation;
using System.Text;
using System.Windows.Forms;
using System.Xml.Linq;


namespace FBprofile
{
    public partial class App : Form
    {
        StringBuilder sb = new StringBuilder();

        public App()
        {
            InitializeComponent();
        }

        /// <summary>
        /// Event for Page Load
        /// </summary>
        private void App_Load(object sender, EventArgs e)
        {
            sb.Append(Environment.NewLine + " Cheking Internet availability...");

            if (IsConnectedToInternet() == true)
            {
                sb.Append(Environment.NewLine + " Internet connection status : Available");
                sb.Append(Environment.NewLine + " Wait for few seconds after entering username");
            }
            else
            {
                sb.Append(Environment.NewLine + " Internet connection status : Not Available");
                sb.Append(Environment.NewLine + " You need an active Internet connection to use this application.");
            }

            rtDetails.Text = sb.ToString();
        }

        /// <summary>
        /// Mthod to define Event after Submit button click
        /// </summary>
        private void btnGetDetails_Click(object sender, EventArgs e)
        {
            if (txtUserName.Text.Length != 0)
            {
                sb.Clear();
                rtDetails.Clear();
                if (IsConnectedToInternet() == true)
                {
                    try
                    {
                        var client = new FacebookClient();
                        string strUserName = txtUserName.Text;
                        dynamic person = client.Get(strUserName.ToString());

                        if (person.gender == null)
                        {
                            sb.Append(Environment.NewLine + "Details for entered Facebook account are: " + Environment.NewLine);
                            sb.Append(Environment.NewLine + "Account type: Facebook page");
                            sb.Append(Environment.NewLine + "Category : " + person.category);
                            sb.Append(Environment.NewLine + "Facebook Id: " + person.id);
                            sb.Append(Environment.NewLine + "Full name: " + person.name);
                            sb.Append(Environment.NewLine + "Page URL: " + person.link);
                            sb.Append(Environment.NewLine + "Official website : " + person.website);
                            sb.Append(Environment.NewLine + "RSS Feeds URL: https://www.facebook.com/feeds/page.php?id=" + person.id + "&format=rss20");
                            sb.Append(Environment.NewLine + "About page : " + person.about);

                        }
                        else
                        {
                            sb.Append(Environment.NewLine + "Details for entered Facebook account are: " + Environment.NewLine);
                            sb.Append(Environment.NewLine + "Account type: Facebook person profile");
                            sb.Append(Environment.NewLine + "Facebook Id: " + person.id);
                            sb.Append(Environment.NewLine + "Full name: " + person.name);
                            sb.Append(Environment.NewLine + "First name: " + person.first_name);
                            sb.Append(Environment.NewLine + "Last name: " + person.last_name);
                            sb.Append(Environment.NewLine + "Page URL: " + person.link);
                            sb.Append(Environment.NewLine + "Gender: " + person.gender);
                            sb.Append(Environment.NewLine + "Locale: " + person.about);
                        }

                        rtDetails.Text = sb.ToString();

                    }
                    catch (Exception)
                    {
                        rtDetails.Text = Environment.NewLine + "  Sorry! Account doesnot exists.";
                    }
                    finally
                    {

                    }
                }
                else
                {
                    rtDetails.Text = Environment.NewLine + "  Sorry! You are not connected to Internet.";
                }
            }
            else
            {
                rtDetails.Text = Environment.NewLine + "  Excuse me! It is mandatory to provide a UserName.";
            }
        }

        /// <summary>
        /// Required method to check internet connection
        /// </summary>
        public bool IsConnectedToInternet()
        {
            try
            {
                System.Net.Sockets.TcpClient client = new System.Net.Sockets.TcpClient("www.google.com", 80);
                client.Close();
                return true;
            }
            catch (System.Exception)
            {
                return false;
            }
        }

        /// <summary>
        /// Event to trigger Submit button on hitting enter key
        /// </summary>
        private void txtUserName_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                btnGetDetails.PerformClick();
            }
        }
    }
}
