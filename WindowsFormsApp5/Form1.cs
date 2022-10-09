using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace WindowsFormsApp5
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();

            this.Load += Form1_Load;
            comboBox1.SelectedIndexChanged += ComboBox1_SelectedIndexChanged;
            listBox1.SelectedIndexChanged += ListBox1_SelectedIndexChanged;
        }

        public void ComboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1)
            {
                string seciliSurucu = comboBox1.Items[comboBox1.SelectedIndex].ToString();

                //var dizinler = new DirectoryInfo(seciliSurucu);

                var dizinler = Directory.GetDirectories(seciliSurucu);

                listBox1.Items.Clear();

                foreach (var item in dizinler)
                {
                    listBox1.Items.Add(item);
                }

                DirectoryInfo dosya = new DirectoryInfo(seciliSurucu);
                FileInfo[] dosyalar = dosya.GetFiles();
                listBox2.Items.Clear();

                foreach (var item in dosyalar)
                {
                    listBox2.Items.Add(item);
                }

                //listBox1.DataSource = dizinler;
                //listBox1.DisplayMember = "Name";
                //listBox1.ValueMember = "Name";

            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            
            List<DriveInfo> suruculer = DriveInfo.GetDrives()
                .Where(s=>s.IsReady==true).ToList();

            //foreach (var item in suruculer)
            //{
            //    comboBox1.Items.Add(item.Name);
            //}

            comboBox1.DataSource = suruculer;
            comboBox1.ValueMember = "Name";
            comboBox1.DisplayMember = "Name";

            //data binding
        }

        private void btnGeri_Click(object sender, EventArgs e)
        {
            string seciliKlasor = label1.Text;

            if (label1.Text != comboBox1.SelectedItem.ToString())
            {         
                DirectoryInfo klasor = new DirectoryInfo(seciliKlasor);
                var parent = klasor.Parent.FullName.ToString();

                DirectoryInfo ustKlasor = new DirectoryInfo(parent);
                DirectoryInfo[] ustKlasoreGidis = ustKlasor.GetDirectories();
                listBox1.Items.Clear();

                foreach (var item in ustKlasoreGidis)
                {
                    listBox1.Items.Add(item.FullName.ToString());
                    label1.Text = item.Parent.FullName.ToString();
                }

                FileInfo[] ustDosyayaGidis = ustKlasor.GetFiles();
                listBox2.Items.Clear();

                foreach (var item in ustDosyayaGidis)
                {
                    listBox2.Items.Add(item.Name.ToString());
                }
                //FileInfo ustDosya = new FileInfo(parent);
                //FileInfo[] usdDosyayaGidis = 


            }

            //listbox1.datasource = klasorler;
            //listbox1.displaymember = "name";
            //listbox1.valuemember = "fullname";


        }

        private void listBox1_MouseDoubleClick(object sender, MouseEventArgs e)
        {

            if (listBox1.SelectedIndex > -1)
            {
                string seciliDizin = listBox1.SelectedItem.ToString();
                var dizin = new DirectoryInfo(seciliDizin);

                FileInfo[] dosyalar = null;

                label1.Text = listBox1.SelectedItem.ToString();

                try
                {
                    dosyalar = dizin.GetFiles();
                }
                catch (Exception)
                {
                    //MessageBox.Show("Dosyaya Erişim Reddedildi!","Uyari",MessageBoxButtons.OK,MessageBoxIcon.Warning);
                }
                if (dosyalar != null)
                {
                    listBox2.Items.Clear();

                    foreach (var item in dosyalar)
                    {
                        listBox2.Items.Add(item.Name.ToString());
                    }
                }


                //listBox2.DataSource = dosyalar;
                //listBox2.DisplayMember = "Name";
                //listBox2.ValueMember = "FullName";

                string seciliKlasor = listBox1.SelectedItem.ToString();
                DirectoryInfo klasor = new DirectoryInfo(seciliKlasor);
                DirectoryInfo[] klasorler = null;

                try
                {
                    klasorler = klasor.GetDirectories();
                }
                catch (Exception)
                {
                    MessageBox.Show("Dosyaya Erişim Reddedildi!", "Uyari", MessageBoxButtons.OK, MessageBoxIcon.Warning);
                }

                if (klasorler != null)
                {
                    listBox1.Items.Clear();

                    foreach (var item in klasorler)
                    {
                        listBox1.Items.Add(item.FullName.ToString());
                    }

                    //listbox1.datasource = klasorler;
                    //listbox1.displaymember = "name";
                    //listbox1.valuemember = "fullname";
                }
            }

        }

        private void ListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    

        //private void listBox2_SelectedIndexChanged(object sender, EventArgs e)
        //{
        //    toolStripStatusLabel1.Text = (listBox2.SelectedItem as FileInfo)
        //        .CreationTime.Date.ToString();

        //    //File.Delete((listBox2.SelectedItem as FileInfo).FullName);

        //}
    }
}
