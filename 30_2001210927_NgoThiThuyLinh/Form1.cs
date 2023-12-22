using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Data.SqlClient;

namespace _30_2001210927_NgoThiThuyLinh
{
    public partial class Form_DangNhap : Form
    {
        Connect cont = new Connect();
        SqlConnection consql;
        
        public Form_DangNhap()
        {
            InitializeComponent();
            consql = cont.connect;
        }

        private bool Login()
        {
            try
            {
                if (consql.State == ConnectionState.Closed)
                {
                    consql.Open();
                }
                string Tim;
                Tim = "select * from Taikhoan where username='"+txt_TenDN.Text+"'and password= '"+txt_MatKhau.Text+"'";
                SqlCommand cmd = new SqlCommand(Tim, consql);
                SqlDataReader rdr = cmd.ExecuteReader();

                if (rdr.Read())
                {
                    string ten = rdr["username"].ToString();
                    string pass = rdr["password"].ToString();
                    return true;
                }

                if (consql.State == ConnectionState.Open)
                {
                    consql.Close();
                }

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                
            }
            return false;

        }
        private void Form_DangNhap_Load(object sender, EventArgs e)
        {

        }

        private void btn_DangNhap_Click(object sender, EventArgs e)
        {
            if (Login())
            {
                Form f_Cau2 = new Cau2();
                f_Cau2.Show();
                this.Hide();
            }
            else
            {
                MessageBox.Show("Sai tên đăng nhập hoặc mật khẩu! ");
            }
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
