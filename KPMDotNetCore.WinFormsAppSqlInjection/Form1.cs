using KPMDotNetCore.Shared;

namespace KPMDotNetCore.WinFormsAppSqlInjection
{
    public partial class FrmLogin : Form
    {
        private readonly DapperService _dapperService;
        public FrmLogin()
        {
            InitializeComponent();
            _dapperService=new DapperService(ConnectionStrings.sqlConnectionStringBuilder.ConnectionString); 
        }

        private void btnLogin_Click(object sender, EventArgs e)
        {
            //string query = $"select * from tbl_user where email='{txtEmail.Text.Trim()}' and password='{txtPassword.Text.Trim()}'";
            string query = $"select * from tbl_user where email=@Email and password=@Password";
            var model = _dapperService.QueryFirstOrDefault<UserModel>(query, new
            {
                Email=txtEmail.Text.Trim(),
                password=txtPassword.Text.Trim(),
            });
            if(model is null) 
            {
                MessageBox.Show("User doesn't exist;");
            }
            MessageBox.Show("Is Admin:" + model.Email);

        }
    }

    public class UserModel
    {
        public string Email { get; set; }
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

    }

}
