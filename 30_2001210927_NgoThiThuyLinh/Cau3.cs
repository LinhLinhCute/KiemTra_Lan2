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
    public partial class Cau3 : Form
    {
        Connect cont = new Connect();
        SqlConnection consql;
        DataSet ds_HH;
        DataColumn[] key = new DataColumn[1];
        public Cau3()
        {
            InitializeComponent();
            consql = cont.connect;
        }

        private void Load_Data(){
            try
            {
                string read;
                read = "select * from HoaDonNhap ";
                DataSet ds = new DataSet();
                SqlDataAdapter da = new SqlDataAdapter(read, consql);
                da.Fill(ds, "HoaDon");
                dgv_HoaDon.DataSource = ds.Tables["HoaDon"];

            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }
        private void Load_CbB()
        {
            try
            {
                if (consql.State == ConnectionState.Closed)
                {
                    consql.Open();
                }
                string read;
                read = "select TenNCC from Nhacungcap ";
                SqlCommand cmd = new SqlCommand(read, consql);
                SqlDataReader rdr = cmd.ExecuteReader();

                while (rdr.Read())
                {
                    string ten = rdr["TenNCC"].ToString();
                    cbB_NCC.Items.Add(ten);

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
        }
        private void Cau3_Load(object sender, EventArgs e)
        {
            Load_Data();
            Load_CbB();
        }

        private void btn_Thoat_Click(object sender, EventArgs e)
        {
            DialogResult r;
            r = MessageBox.Show("Bạn có chắc chăn muốn Thoát không?", "Thoát ?", MessageBoxButtons.YesNo, MessageBoxIcon.Question, MessageBoxDefaultButton.Button1);
            if (r == DialogResult.Yes)
            {
                this.Close();
            }
        }

        private void dgv_HoaDon_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {
            DataGridViewRow row = new DataGridViewRow();
            row = dgv_HoaDon.Rows[e.RowIndex];
            txt_MaThuoc .Text = Convert.ToString(row.Cells["MaThuoc"].Value);
            txt_TenThuoc.Text = Convert.ToString(row.Cells["TenThuoc"].Value);
            txt_SoLuong.Text = Convert.ToString(row.Cells["SoLuong"].Value);
            txt_NgayNhap.Text = Convert.ToString(row.Cells["NgayNhap"].Value);
            txt_DonGia.Text = Convert.ToString(row.Cells["DonGia"].Value);
        }

        private void btn__XoaTrang_Click(object sender, EventArgs e)
        {
            txt_MaThuoc.Clear();
            txt_TenThuoc.Clear();
            txt_SoLuong.Clear();
            txt_NgayNhap.Clear();
            txt_DonGia.Clear();
            txt_TenThuoc.Focus();
        }
        private int KiemTra()
        {
            int dem=-1;
            int kt = -1;
            try
            {
                if (consql.State == ConnectionState.Closed)
                {
                    consql.Open();
                }
                string count;
                count = "select count(*) from HoaDonNhap";
                SqlCommand cmd = new SqlCommand(count, consql);
                dem = (int)cmd.ExecuteScalar();
                if (consql.State == ConnectionState.Open)
                {
                    consql.Close();
                }
                MessageBox.Show("Thêm Thành Công !");
                Load_Data();
                Load_CbB();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }
            if (int.Parse(txt_MaThuoc.Text) <= dem)
                return kt = 0;
            else if (int.Parse(txt_SoLuong.Text) <= 0)
                return kt= 1;
            else if (int.Parse(txt_DonGia.Text) <= 0)
                return kt= 2;
            return kt;
        }
        private void btn_Them_Click(object sender, EventArgs e)
        {
            string Insert;
            int Id = int.Parse(txt_MaThuoc.Text);
            int SL = int.Parse(txt_SoLuong.Text);
            int DonGia = int.Parse(txt_DonGia.Text);
            switch (KiemTra())
            {
                case 0:
                    {
                        MessageBox.Show("KhÔNG ĐƯỢC TRÙNG MÃ HÓA ĐƠN !");
                        txt_MaThuoc.Focus();
                        break;
                    }
                case 1:
                    {
                        MessageBox.Show("Số lương thước phải lớn hơn 0!");
                        txt_SoLuong.Focus();
                        break;
                    }
                case 2:
                    {
                        MessageBox.Show("Đơn giá không được bằng 0");
                        txt_DonGia.Focus();
                        break;
                    }
            }
            try
            {
                if (consql.State == ConnectionState.Closed)
                {
                    consql.Open();
                }
                DateTime NgayNhapThuoc = Convert.ToDateTime(txt_NgayNhap.Text);
                Insert = "insert into HoaDonNhap values(" + Id + ",N'" + txt_TenThuoc.Text + "', " + SL + "," + DonGia + ",'" + NgayNhapThuoc.ToString("yyyy-MM-dd") + "')";
                SqlCommand cmd = new SqlCommand(Insert, consql);
                cmd.ExecuteNonQuery();
                if (consql.State == ConnectionState.Open)
                {
                    consql.Close();
                }
                MessageBox.Show("Thêm Thành Công !");
                Load_Data();
                Load_CbB();
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);

            }

        }
    }
}
