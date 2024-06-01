using Dapper;
using KPMDotNetCore.Shared;
using KPMDotNetCore.WinFormsApp.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.SqlClient;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace KPMDotNetCore.WinFormsApp
{
    public partial class FromBlogList : Form
    {
        private readonly DapperService _dapperService;
        //private const int _edit = 1;
        //private const int _delete = 2;
        public FromBlogList()
        {
            InitializeComponent();
            dgvData.AutoGenerateColumns = false;
            _dapperService = new DapperService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString);
        }

        private void FromBlogList_Load(object sender, EventArgs e)
        {
            Bloglist();
        }
        private void Bloglist()
        {
            List<BlogModel> lst = _dapperService.Query<BlogModel>("select * from tbl_blog");
            dgvData.DataSource = lst;
        }
        private void dgvData_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            //int columnidx= e.ColumnIndex ;
            //int rowindx=e.RowIndex  ;

            if (e.RowIndex == -1) return;

            #region If Case

            var blogId = Convert.ToInt32(dgvData.Rows[e.RowIndex].Cells["colId"].Value);

            if (e.ColumnIndex == (int)EnumFormControlType.Edit)
            {
                FrmNewBlog frmNewBlog = new FrmNewBlog(blogId);
                frmNewBlog.ShowDialog();

                Bloglist();
            }
            else if (e.ColumnIndex == (int)EnumFormControlType.Delete)
            {
                var dialogresult = MessageBox.Show("Are you sure want to delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);

                if (dialogresult != DialogResult.Yes) return;
                DeleteBlog(blogId);
            }

            #endregion
            #region Switch Case

            int index = e.ColumnIndex;
            EnumFormControlType enumFormControlType =(EnumFormControlType) index; //conversion Enum type

            switch (enumFormControlType)
            {
                case EnumFormControlType.Edit:
                    FrmNewBlog frmNewBlog = new FrmNewBlog(blogId);
                    frmNewBlog.ShowDialog();

                    Bloglist();

                    break;
                case EnumFormControlType.Delete:
                    var dialogresult = MessageBox.Show("Are you sure want to delete?", "", MessageBoxButtons.YesNo, MessageBoxIcon.Question);
                    if (dialogresult != DialogResult.Yes) return;
                    DeleteBlog(blogId);

                    break;
                case EnumFormControlType.None:
                    default:
                    MessageBox.Show("Invalid Case.");
                    break;
            }
            //EnumFormControlType enumFormControlType=EnumFormControlType.None;
            // switch(enumFormControlType)
            // {
            //     case EnumFormControlType.None:
            //         break;
            //     case EnumFormControlType.Edit:
            //         break;
            //     case EnumFormControlType.Delete:
            //         break;
            //     default:
            //         break;

            // }
            #endregion 
        }


        private void DeleteBlog (int id)
        {
            string query = @"Delete From [dbo].[Tbl_Blog] WHERE BlogId=@BlogId";

            int result = _dapperService.Execute(query, new { BlogId = id });

            string message = result > 0 ? "Deleting Successful." : "Deleting Failed.";
            MessageBox.Show(message);
            Bloglist();
        }
    }
}
