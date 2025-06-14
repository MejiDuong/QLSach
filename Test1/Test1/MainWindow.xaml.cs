using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;
using Test1.Models;
using System.Text.RegularExpressions;
using System.Reflection;
using System.Linq;

namespace Test1
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        QlsachContext db=new QlsachContext();
        public MainWindow()
        {
            InitializeComponent();
        }

        private void Window_Loaded(object sender, RoutedEventArgs e)
        {
            HienThiDL();
            HienThiCB();
        }
        private void HienThiCB()
        {
            var query = from t in db.TacGia
                        select t;
            cboTG.ItemsSource = query.ToList();
            cboTG.DisplayMemberPath = "TenTacGia";
            cboTG.SelectedValuePath = "MaTG";
            cboTG.SelectedIndex = 0;
        }
        private List<ThongTin> LayDL()
        {
            var query = from t in db.Saches
                        where t.SoTrang >= 0
                        orderby t.SoTrang descending
                        select new ThongTin
                        {
                            MaSach = t.MaSach,
                            TenSach = t.TenSach,
                            MaTG = t.MaTg,
                            NamXB = t.NamXuatBan,
                            SoTrang = t.SoTrang,
                            TongTien = t.SoTrang * 80000
                        };
            return query.ToList<ThongTin>();
        }
        private void HienThiDL()
        {
            dgv_dsSach.ItemsSource = LayDL();
        }

        private void dgv_dsSach_SelectedCellsChanged(object sender, SelectedCellsChangedEventArgs e)
        {
            if(dgv_dsSach.SelectedItem != null)
            {
                try
                {
                    Type t = dgv_dsSach.SelectedItem.GetType();
                    PropertyInfo[] p = t.GetProperties();
                    txtMaSach.Text = p[0].GetValue(dgv_dsSach.SelectedValue).ToString();
                    txtTenSach.Text = p[1].GetValue(dgv_dsSach.SelectedValue).ToString();
                    txtNamXB.Text = p[2].GetValue(dgv_dsSach.SelectedValue).ToString();
                    txtSoTrang.Text = p[3].GetValue(dgv_dsSach.SelectedValue).ToString();
                    cboTG.SelectedValue = p[4].GetValue(dgv_dsSach.SelectedValue).ToString();
                }
                catch(Exception ex) {
                    MessageBox.Show("Có lỗi khi chọn hàng"+ ex.Message,"Thông báo",MessageBoxButton.OK,MessageBoxImage.Error);
                }
            }
        }

        private void bntThem_Click(object sender, RoutedEventArgs e)
        {
            var query =db.Saches.SingleOrDefault(t=>t.MaSach.Equals(txtMaSach.Text));
            if (query != null) {
                MessageBox.Show("Đã tồn tại", "Thông báo", MessageBoxButton.OK);
            }
            else
            {
                Sach a = new Sach();
                a.MaSach = txtMaSach.Text;
                a.TenSach = txtTenSach.Text;
                a.SoTrang=int.Parse(txtSoTrang.Text);
                a.NamXuatBan=int.Parse(txtNamXB.Text);
                TacGium dm = (TacGium)cboTG.SelectedItem;
                a.MaTg = dm.MaTg;
                db.Saches.Add(a);
                db.SaveChanges();
                MessageBox.Show("Thêm thành công", "Thông báo", MessageBoxButton.OK);
                HienThiDL();
            }
        }

        private void bntSua_Click(object sender, RoutedEventArgs e)
        {
            var spsua = db.Saches.SingleOrDefault(t => t.MaSach ==txtMaSach.Text);
            if (spsua != null)
            {
                spsua.TenSach = txtTenSach.Text;
                spsua.SoTrang = int.Parse(txtSoTrang.Text);
                spsua.NamXuatBan = int.Parse(txtNamXB.Text);
                spsua.MaTg = ((TacGium)(cboTG.SelectedItem)).MaTg;
                db.SaveChanges();
                HienThiDL();
            }
            else {
                MessageBox.Show("Khong co ma nay");
            }
        }

        private void bntXoa_Click(object sender, RoutedEventArgs e)
        {
            var query = db.Saches.SingleOrDefault(t=>t.MaSach.Equals(txtMaSach.Text));
            if (query != null) { 
                db.Saches.Remove(query);
                db.SaveChanges();
                HienThiDL();
            }
            else
            {
                MessageBox.Show("Khong ton tai ma de xoa!");
            }
        }

        private void bntThongKe_Click(object sender, RoutedEventArgs e)
        {
            new Window1().ShowDialog();
            
        }
    }
}