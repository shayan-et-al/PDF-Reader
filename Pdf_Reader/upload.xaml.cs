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
using System.IO;

namespace Pdf_Reader
{
    /// <summary>
    /// Interaction logic for upload.xaml
    /// </summary>
    public partial class upload : Page
    {
        string filename;
        
        public upload()
        {
            InitializeComponent();
        }

        private void upbtn_Click(object sender, RoutedEventArgs e)
        {
            Microsoft.Win32.OpenFileDialog dlg = new Microsoft.Win32.OpenFileDialog();
            dlg.DefaultExt = ".((p|P)(d|D)(f|F))";
            dlg.Filter = "PDF documents (.pdf)|*.pdf|PDF documents(*.PDF)|*.PDF";
            if (dlg.ShowDialog() == true)
            {
                filename = dlg.FileName.ToString();
                tb1.Text = filename;
            }

        }

        private void subbtn_Click(object sender, RoutedEventArgs e)
        {
            
            if (string.IsNullOrEmpty(tb1.Text))
            {
                MessageBox.Show("Enter title");
                if (string.IsNullOrEmpty(filename))
                {
                    MessageBox.Show("Select Pdf files");

                }
            }
            else
            {
                byte[] pdfBT = null;
                FileStream fstream = new FileStream(filename, FileMode.Open, FileAccess.Read);
                BinaryReader br = new BinaryReader(fstream);
                pdfBT = br.ReadBytes((int)fstream.Length);
                br.Close();
                fstream.Close();
                SQLiteConnection con = new SQLiteConnection("Data Source=new.db;Version=3;");
                con.Open();
                string query = "insert into file(file_name) values(@PDF)";
                SQLiteCommand cmd = new SQLiteCommand(query, con);
                cmd.Parameters.Add(new SQLiteParameter("@PDF", pdfBT));
                SQLiteDataReader dr = cmd.ExecuteReader();
                MessageBox.Show("PDF uploaded successfully");
                con.Close();
            }
        }
    }
}
