using System;
using System.Windows.Forms;
using EasyTabs;


namespace GNix
{
    public partial class AppContainer : TitleBarTabs
    {
        public AppContainer()
        {
            InitializeComponent();

            AeroPeekEnabled = true;
            TabRenderer = new ChromeTabRenderer(this);

        }

        // Handle the method CreateTab that allows the user to create a new Tab
        // on your app when clicking
        public override TitleBarTab CreateTab()
        {
            return new TitleBarTab(this)
            {
                // The content will be an instance of another Form
                // In our example, we will create a new instance of the Form1
                Content = new Form1
                {
                    Text = this.Text,
                    Icon = this.Icon
                }
            };
        }

        private void AppContainer_Load(object sender, EventArgs e)
        {

        }

     
    }
}
