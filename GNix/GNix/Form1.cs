using GNix.Properties;
using Microsoft.Web.WebView2.Core;
using System;
using System.Drawing;
using System.Text.RegularExpressions;
using System.Threading.Tasks; // Add for Task usage
using System.Windows.Forms;
using EasyTabs;

namespace GNix
{
    public partial class Form1 : Form
    {
       
        protected TitleBarTabs ParentTabs
        {
            get
            {
                return (ParentForm as TitleBarTabs);
            }
        }


        public Form1()
        {
            InitializeComponent();
            InitializeAsync();
            textBox1.GotFocus += textBox1_GotFocus;
            textBox1.LostFocus += textBox1_LostFocus;
            textBox1.KeyDown += textBox1_KeyDown; // Handle Enter key press
            webView21.NavigationStarting += webView21_Validatings;
            webView21.NavigationCompleted += webView21_Validateds;
            webView21.NavigationCompleted += webView21_NavigationCompleted; // Added for history checking
       

        }

        private async void InitializeAsync()
        {
            // Ensure that WebView2 is initialized
            await webView21.EnsureCoreWebView2Async(null);

            webView21.Source = new Uri("https://google.com");
        

    }

        private void Form1_Load(object sender, EventArgs e)
        {
            textBox1.Text = "Search on Google";
            textBox1.ForeColor = Color.Gray;
        }

        private void textBox1_GotFocus(object sender, EventArgs e)
        {
            if (textBox1.Text == "Search on Google")
            {
                textBox1.Clear();
                textBox1.ForeColor = Color.Black;
            }
        }

        private void textBox1_LostFocus(object sender, EventArgs e)
        {
            if (string.IsNullOrWhiteSpace(textBox1.Text))
            {
                textBox1.Text = "Search on Google";
                textBox1.ForeColor = Color.Gray;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 frm = new Form1();
            frm.Show(); // Opening new form
        }

        private void webView21_Validatings(object sender, CoreWebView2NavigationStartingEventArgs e)
        {
           
            // Handle new window requests
            if (e.Uri.Contains("new_window_identifier")) // Adjust this condition as necessary
            {
                e.Cancel = true; // Cancel the current navigation
                Form1 newForm = new Form1(); // Create a new instance of the same form
                newForm.Show(); // Show the new form
                newForm.webView21.Source = new Uri(e.Uri); // Navigate the new form's WebView
            }
            else
            {
                loading.Image = Properties.Resources.load; // Show loading image
            }

            textBox1.Text = e.Uri;
            this.Text = e.Uri;

        }

        private void webView21_Validateds(object sender, EventArgs e)
        {
            loading.Image = Properties.Resources.google; // Show loaded image
        }
       
        private async void button4_Click(object sender, EventArgs e)
        {
            string input = textBox1.Text.Trim();
            if (IsValidUrl(input))
            {
                webView21.Source = new Uri(input); // Navigate to the URL
            }
            else
            {
                string googleSearchUrl = $"https://www.google.com/search?q={Uri.EscapeDataString(input)}"; // Google search URL
                webView21.Source = new Uri(googleSearchUrl); // Navigate to Google search
            }

            await Task.Run(() => UpdateButtonStates()); // Run UI updates in background
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Enter)
            {
                button4.PerformClick(); // Simulate button click
                e.SuppressKeyPress = true; // Prevent the "ding" sound
            }
        }

        private bool IsValidUrl(string url)
        {
            string pattern = @"^(http|https)://";
            return Regex.IsMatch(url, pattern);
        }

        private void button2_Click(object sender, EventArgs e) // Back Button
        {
            if (webView21.CanGoBack)
            {
                webView21.GoBack(); // Go back in the navigation history
            }
        }

        private void button3_Click(object sender, EventArgs e) // Forward Button
        {
            if (webView21.CanGoForward)
            {
                webView21.GoForward(); // Go forward in the navigation history
            }
        }

        private void webView21_NavigationCompleted(object sender, CoreWebView2NavigationCompletedEventArgs e)
        {
            UpdateButtonStates(); // Update button states on navigation completed
        }

        private void UpdateButtonStates()
        {
            // Enable or disable back and forward buttons based on navigation history
            button2.Invoke((MethodInvoker)(() =>
            {
                button2.Enabled = webView21.CanGoBack; // Enable back button if there is a history
                button2.Cursor = button2.Enabled ? Cursors.Hand : Cursors.No;
            }));

            button3.Invoke((MethodInvoker)(() =>
            {
                button3.Enabled = webView21.CanGoForward; // Enable forward button if there is a history
                button3.Cursor = button3.Enabled ? Cursors.Hand : Cursors.No;
            }));
        }

        private void button5_Click(object sender, EventArgs e)
        {
        
            webView21.Source = new Uri("https://chatgpt.com/");
          
        }

        private void button6_Click(object sender, EventArgs e)
        {
            webView21.Source = new Uri("https://google.com/");
        }

        private void tableLayoutPanel1_Paint(object sender, PaintEventArgs e)
        {

        }
    }
}
