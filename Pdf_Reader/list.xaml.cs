using System;
using System.Collections.Generic;
using System.Linq;
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
using System.Data.SQLite;
using System.Data;
using System.IO;

namespace Pdf_Reader
{
    /// <summary>
    /// Interaction logic for list.xaml
    /// </summary>
    public partial class list : Page
    {
        public list()
        {
            InitializeComponent();
        }

        private void Button_Click(object sender, RoutedEventArgs e)
        {
            SQLiteConnection con = new SQLiteConnection("Data Source=new.db;Version=3;");
            con.Open();
            Byte[] rdbt = null;
            string x = Convert.ToString(tb.Text);
            string y = "select file_name from file where id='" + x + "'";
            SQLiteCommand cmd = new SQLiteCommand(y, con);
            SQLiteDataReader dr = cmd.ExecuteReader();
            while (dr.Read())
            {
                rdbt = (Byte[])(dr[0]);
            }
            con.Close();
            string newpath = @"C:\Users\Public\Documents\" + x + ".pdf";
            File.WriteAllBytes(newpath, rdbt);
            webbrow.Visibility = Visibility.Visible;
            webbrow.Navigate(newpath);
        }

        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            dgrid.Visibility = Visibility.Visible;
            SQLiteConnection con = new SQLiteConnection("Data Source=new.db;Version=3;");
            try
            {
                con.Open();
                string s = "select id as'ID' from file";
                SQLiteCommand cmd = new SQLiteCommand(s, con);
                cmd.ExecuteNonQuery();
                SQLiteDataAdapter adp = new SQLiteDataAdapter(cmd);
                DataTable dt = new DataTable("new_donations");
                adp.Fill(dt);
                dgrid.ItemsSource = dt.DefaultView;
                adp.Update(dt);
                con.Close();
            }
            catch (Exception)
            {
                MessageBox.Show("There was error fetching data");
            }
        }
    }
}
