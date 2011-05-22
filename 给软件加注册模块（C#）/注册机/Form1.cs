using System;
using System.Windows.Forms;
using System.IO;

namespace 注册机
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        LicenseCheck lc = new LicenseCheck();
        LicenseString ls = new LicenseString();
        void 生成注册号Btn_Click(object sender, EventArgs e)
        {
            textBox2.Text += lc.GetRegHead2(textBox1.Text);
            textBox2.Text += "-";
            textBox2.Text += lc.GetRegTail3(textBox1.Text);

            ls.regLicense = textBox2.Text;

            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "License File|*.lic";
            saveFileDialog1.Title = "Save an License File";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
                lc.keyfile=saveFileDialog1.FileName;

            if (File.Exists(lc.keyfile) == true)
            {
                File.Delete(lc.keyfile);
            }

            StreamWriter sw = File.CreateText(lc.keyfile);

            sw.WriteLine(lc.Encrypt(textBox1.Text, lc.keyStr));
            sw.WriteLine(lc.Encrypt(textBox2.Text, lc.keyStr));
            sw.WriteLine(lc.Encrypt(textBox3.Text, lc.keyStr));
            sw.WriteLine(lc.Encrypt(textBox4.Text, lc.keyStr));
            sw.WriteLine(lc.Encrypt(textBox5.Text, lc.keyStr));
            sw.Flush();
            sw.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ls.machineNum= textBox1.Text = lc.获取机器码(textBox2.Text);
            ls.expTimes=textBox3.Text = "30";
            ls.expireDate=textBox4.Text =  ls.regDateTime.AddDays(7).ToString();
            ls.regDate=textBox5.Text = ls.regDateTime.ToFileTime().ToString();

            //textBox5.Text = "shanghai china mobile 1";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            OpenFileDialog dlgOpenfile = new OpenFileDialog();
            dlgOpenfile.Filter = "Request File|*.req|License File|*.lic";
            dlgOpenfile.Title = "Open an Request File or an License File ";
            dlgOpenfile.ShowDialog();
            dlgOpenfile.RestoreDirectory = true;
            if (!string.IsNullOrEmpty(dlgOpenfile.FileName))
            {
                lc.keyfile = dlgOpenfile.FileName;
            }
            if (File.Exists(lc.keyfile) == true)
            {
                StreamReader reader = new StreamReader(lc.keyfile);
                for (int i = 0; i < 5; i++)
                {
                    string line1 = reader.ReadLine();
                    richTextBox1.AppendText(line1 + Environment.NewLine);
                    line1 = lc.Decrypt(line1, lc.keyStr);
                    richTextBox1.AppendText(line1 + Environment.NewLine);
                }

                for (int j = 0; j < 2; j++)
                {
                    string line2 = reader.ReadLine();
                    richTextBox1.AppendText(line2 + Environment.NewLine);
                    line2 = lc.Decrypt(line2, "icdredge");
                    richTextBox1.AppendText(line2 + Environment.NewLine);
                }
            }
        }
    }
}