using System;
using Newtonsoft.Json;
using System.Windows.Forms;
using System.Net;
using System.ComponentModel;
using Newtonsoft.Json.Linq;

namespace Fr0gs_Discord_ID_To_Roblox
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        string OldText;

        private void button1_Click(object sender, EventArgs e)
        {
            OldText = richTextBox1.Text;
            richTextBox1.Clear();
        }

        private void button3_Click(object sender, EventArgs e)
        {
            richTextBox1.Text = OldText;
        }

        object endresult;

        private void button2_Click(object sender, EventArgs e)
        {
             try
             {
            richTextBox1.Clear();
                string thing = "https://verify.eryn.io/api/user/" + textBox1.Text;
                endresult = JsonConvert.DeserializeObject(new WebClient().DownloadString(thing));
                richTextBox1.AppendText("----------------------------------------------------------------------------------------------------\n");
                JObject ee = JObject.Parse(endresult.ToString());
                string username = ee["robloxUsername"].ToString();
                string followers = JObject.Parse(new WebClient().DownloadString("https://friends.roblox.com/v1/users/" + ee["robloxId"].ToString() + "/followers/count"))["count"].ToString();
                string onlinestatus = JObject.Parse(new WebClient().DownloadString("https://api.roblox.com/users/" + ee["robloxId"].ToString() + "/onlinestatus"))["IsOnline"].ToString();
            string friends = JObject.Parse(new WebClient().DownloadString("https://friends.roblox.com/v1/users/" + ee["robloxId"].ToString() + "/friends/count"))["count"].ToString();
            if (onlinestatus == "False")
            {
                onlinestatus = "not online";
            }
            else if(onlinestatus == "True")
            {
                onlinestatus = "is online";
            }
                richTextBox1.AppendText("username: " + username + "\n");
                richTextBox1.AppendText("online status: " + onlinestatus + "\n");
            if(onlinestatus == "True")
            {
                richTextBox1.AppendText("status: " + JObject.Parse(new WebClient().DownloadString("https://api.roblox.com/users/" + ee["robloxId"].ToString() + "/onlinestatus"))["LastLocation"].ToString() + "\n");
            }
                richTextBox1.AppendText("followers: " + followers + "\n");
                richTextBox1.AppendText("friends: " + friends + "\n");

            richTextBox1.AppendText("----------------------------------------------------------------------------------------------------\n");
            }
          catch (Exception ex)
            {
                richTextBox1.AppendText("----------------------------------------------------------------------------------------------------\n");
                richTextBox1.AppendText("Failed To Find User!\n");
                richTextBox1.AppendText("----------------------------------------------------------------------------------------------------\n");
            }
        }
    }
}
