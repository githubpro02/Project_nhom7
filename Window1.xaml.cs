using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Data.SqlClient;
using System.Data;

namespace Project_nhom7
{
    /// <summary>
    /// Interaction logic for Window1.xaml
    /// </summary>
    public partial class Window1 : Window
    {
        SqlConnection dbcon = new SqlConnection(@"Data Source=LAPTOP-MTF4792D\SQLEXPRESS;Initial Catalog=ql_nghidinh;Integrated Security=True");
        public Window1()
        {
            InitializeComponent();
            loadGird();
        }
        public void loadGird()
        {
            SqlCommand cmd = new SqlCommand("select * from nghidinh", dbcon);
            DataTable dt = new DataTable();
            dbcon.Open();
            SqlDataReader sdr = cmd.ExecuteReader();
            dt.Load(sdr);
            dbcon.Close();
            dataGrid.ItemsSource = dt.DefaultView;
            cbbox.SelectedIndex = 0;
        }
        public bool isValid()
        {
            if (txtChuong.Text == String.Empty)
            {
                MessageBox.Show("Chưa điền chương", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtNoidungchuong.Text == String.Empty)
            {
                MessageBox.Show("Chưa điền nội dung chương", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtMuc.Text == String.Empty)
            {
                MessageBox.Show("Chưa điền mục", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtNoidungmuc.Text == String.Empty)
            {
                MessageBox.Show("Chưa điền nội dung mục", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtDieu.Text == String.Empty)
            {
                MessageBox.Show("Chưa điền điều", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtNoidungdieu.Text == String.Empty)
            {
                MessageBox.Show("Chưa điền nội dung điều", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtKhoan.Text == String.Empty)
            {
                MessageBox.Show("Chưa điền khoản", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            if (txtNoidungkhoan.Text == String.Empty)
            {
                MessageBox.Show("Chưa điền nội dung khoản", "Failed", MessageBoxButton.OK, MessageBoxImage.Error);
                return false;
            }
            return true;
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn thêm không? ", "Thêm dữ liệu", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                try
                {
                    if (isValid())
                    {
                        dbcon.Open();
                        SqlCommand cmd = new SqlCommand("INSERT INTO nghidinh VALUES (@Chuong, @Noidungchuong, @Muc, @Noidungmuc, @Dieu, @Noidungdieu, @Khoan, @Noidungkhoan)", dbcon);
                        cmd.CommandType = CommandType.Text;
                        cmd.Parameters.AddWithValue("Chuong", txtChuong.Text);
                        cmd.Parameters.AddWithValue("Noidungchuong", txtNoidungchuong.Text);
                        cmd.Parameters.AddWithValue("Muc", txtMuc.Text);
                        cmd.Parameters.AddWithValue("Noidungmuc", txtNoidungmuc.Text);
                        cmd.Parameters.AddWithValue("Dieu", txtDieu.Text);
                        cmd.Parameters.AddWithValue("Noidungdieu", txtNoidungdieu.Text);
                        cmd.Parameters.AddWithValue("Khoan", txtKhoan.Text);
                        cmd.Parameters.AddWithValue("Noidungkhoan", txtNoidungkhoan.Text);
                        cmd.ExecuteNonQuery();
                        dbcon.Close();
                        loadGird();
                        MessageBox.Show("Thêm thành công", "Đã lưu", MessageBoxButton.OK, MessageBoxImage.Information);
                    }
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
            else
            {
                dbcon.Close();

            }
        }

        private void btn_Sua_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn sửa không? ", "Sửa dữ liệu", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                dbcon.Open();
                SqlCommand cmd = new SqlCommand("UPDATE nghidinh SET Chuong = '" + txtChuong.Text + "', Noidungchuong = '" + txtNoidungchuong.Text + "', Muc = '" + txtMuc.Text + "', Noidungmuc = '" + txtNoidungmuc.Text + "', Noidungdieu = '" + txtNoidungdieu.Text + "', Noidungkhoan = '" + txtNoidungkhoan.Text + "' WHERE Dieu = '" + txtDieu.Text + "' AND Khoan = '" + txtKhoan.Text + "' ", dbcon);
                
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã sửa thành công", "Đã lưu", MessageBoxButton.OK, MessageBoxImage.Information);
                    dbcon.Close();
                    loadGird();
                    dbcon.Close();
                }
                catch (SqlException ex)
                {
                    MessageBox.Show(ex.Message);
                }
                finally
                {
                    dbcon.Close();
                }
            }
            else
            {
                dbcon.Close();
            }
        }

        private void btn_Xoa_Click(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show("Bạn có chắc chắn muốn xóa không? ", "Xóa dữ liệu", MessageBoxButton.YesNo, MessageBoxImage.Question);
            if (result == MessageBoxResult.Yes)
            {
                dbcon.Open();
                SqlCommand cmd = new SqlCommand("DELETE FROM nghidinh WHERE Dieu = " + txtDieu.Text + "AND Khoan = " + txtKhoan.Text, dbcon);
                try
                {
                    cmd.ExecuteNonQuery();
                    MessageBox.Show("Đã xóa thành công", "Đã lưu", MessageBoxButton.OK, MessageBoxImage.Information);
                    dbcon.Close();
                    loadGird();
                    dbcon.Close();

                }
                catch (SqlException ex)
                {
                    MessageBox.Show("Không xóa được" + ex.Message);
                }
                finally
                {
                    dbcon.Close();
                }
            }
            else
            {
                dbcon.Close();
            }
        }


        private void btn_Search_Click(object sender, RoutedEventArgs e)
        {
            try
            {
                if (dbcon.State == ConnectionState.Closed)
                    dbcon.Open();
                using (DataTable dt = new DataTable("nghidinh"))
                {
                    string query = "SELECT * FROM nghidinh ";
                    if (cbbox.SelectedIndex == 0)
                    {
                        query += "WHERE Chuong =" + txtTimkiem.Text;
                    }
                    else
                    {
                        if (cbbox.SelectedIndex == 1)
                        {
                            query += "WHERE Muc = " + txtTimkiem.Text;
                        }
                        if (cbbox.SelectedIndex == 2)
                        {
                            query += "WHERE Dieu  =" + txtTimkiem.Text;
                        }
                        if (cbbox.SelectedIndex == 3)
                        {
                            query += "WHERE Khoan =" + txtTimkiem.Text;
                        }
                    }
                    using (SqlCommand cmd = new SqlCommand(query, dbcon))
                    {
                        cmd.Parameters.AddWithValue("@Chuong", txtTimkiem.Text);
                        cmd.Parameters.AddWithValue("@Muc", txtTimkiem.Text);
                        cmd.Parameters.AddWithValue("@Dieu", txtTimkiem.Text);
                        cmd.Parameters.AddWithValue("@Khoan", txtTimkiem.Text);
                        SqlDataAdapter adapter = new SqlDataAdapter(cmd);
                        adapter.Fill(dt);
                        dataGrid.ItemsSource = dt.DefaultView;
                    }
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }

            dbcon.Close();
        }
    }
}
