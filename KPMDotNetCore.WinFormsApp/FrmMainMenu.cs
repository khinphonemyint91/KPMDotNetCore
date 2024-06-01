using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KPMDotNetCore.WinFormsApp
{
    public partial class FrmMainMenu : Form
    {
        public FrmMainMenu()
        {
            InitializeComponent();
        }

        private void newBlogToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FrmNewBlog frmBlog = new FrmNewBlog();
            //frmBlog.Show();
            frmBlog.ShowDialog();
        }

        private void blogsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            FromBlogList frmBlogList = new FromBlogList();
            frmBlogList.ShowDialog();
        }
    }
}
