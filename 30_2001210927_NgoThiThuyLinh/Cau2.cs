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
    public partial class Cau2 : Form
    {
        Connect cont = new Connect();
        SqlConnection consql;
        DataSet ds_HH;
        DataColumn[] key = new DataColumn[1];
        public Cau2()
        {
            InitializeComponent();
            consql = cont.connect;
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có chắc chăn muốn Thoát không?","Thoát ?",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1);
            if (r == DialogResult.Yes)
            {
                this.Close();
            }
        }
        private void Load_Data()
        {
            try
            {
                string read;
                read = "select * from Nhacungcap ";
                DataSet ds = new DataSet();
                
                SqlDataAdapter da = new SqlDataAdapter(read, consql);
                
                da.Fill(ds, "NCC");
                
                dgv_Bang.DataSource = ds.Tables["NCC"];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Cau2_Load(object sender, EventArgs e)
        {
            Load_Data();
        }

        private void dgv_Bang_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dgv_Bang.Rows[e.RowIndex];
            txt_MaNCC .Text = Convert.ToString(row.Cells["MaNCC"].Value);
            txt_TenNCC.Text = Convert.ToString(row.Cells["TenNCC"].Value);
            txt_DiaChi.Text = Convert.ToString(row.Cells["DiaChi"].Value);
            
        }

        private void btn_Them_Click(object sender, EventArgs e)
        {
            try
            {
                if (consql.State == ConnectionState.Closed)
                {
                    consql.Open();
                }
                string Insert;
                int Id = int.Parse(txt_MaNCC.Text);
                Insert = "insert into Nhacungcap(MaNCC, TenNCC,Diachi) values("+Id+",N'"+txt_TenNCC.Text+"', N'"+txt_DiaChi.Text+"')";
                SqlCommand cmd = new SqlCommand(Insert, consql);
                cmd.ExecuteNonQuery();
                if (consql.State == ConnectionState.Open)
                {
                    consql.Close();
                }
                MessageBox.Show("Thêm Thành Công !");
                Load_Data();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
        }

        private void btn_Xoa_Click(object sender, EventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có chắc chăn muốn xóa không?","Xóa ?",MessageBoxButtons.YesNo,MessageBoxIcon.Question,MessageBoxDefaultButton.Button1);
            if (r == DialogResult.Yes)
            {

                try
                {
                    if (consql.State == ConnectionState.Closed)
                    {
                        consql.Open();
                    }
                    string Delete;
                    int Id = int.Parse(txt_MaNCC.Text);
                    Delete = "delete from Nhacungcap where MaNCC=" + Id + "";
                    SqlCommand cmd = new SqlCommand(Delete, consql);
                    cmd.ExecuteNonQuery();
                    if (consql.State == ConnectionState.Open)
                    {
                        consql.Close();
                    }
                    MessageBox.Show("Xóa thành công");
                    Load_Data();
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);

                }
            }
        }

        private void btn_XoaTrang_Click(object sender, EventArgs e)
        {
            txt_MaNCC.Clear();
            txt_TenNCC.Clear();
            txt_DiaChi.Clear();
            txt_MaNCC.Focus();
        }

        private void btn_XuatHD_Click(object sender, EventArgs e)
        {
            Form f_Cau3 = new Cau3();
            f_Cau3.Show();
            this.Hide();
        }
    }
}
