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
            sw.WriteLine(lc.Encrypt(textBox6.Text, lc.keyStr));
            sw.WriteLine(lc.Encrypt(textBox7.Text, lc.keyStr));
            sw.Flush();
            sw.Close();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ls.regDateFile= textBox5.Text = ls.regDateTime.ToFileTime().ToString();
            ls.machineNum = textBox1.Text = lc.获取机器码(ls.regDateFile);
            ls.expTimes=textBox3.Text = "30";
            ls.expireDate=textBox4.Text =  ls.regDateTime.AddDays(7).ToString();
            ls.regEmail = textBox6.Text = "cn.wei.hp@gmail.com";
            ls.regDate = textBox7.Text = ls.regDateTime.ToString();
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
                if (Path.GetExtension(lc.keyfile) == ".req")
                {
                    textBox6.Text = lc.Decrypt(reader.ReadLine(), lc.keyStr);
                    textBox5.Text = lc.Decrypt(reader.ReadLine(), lc.keyStr);
                    textBox1.Text = lc.Decrypt(reader.ReadLine(), lc.keyStr);
                    textBox7.Text = lc.Decrypt(reader.ReadLine(), lc.keyStr);
                }
                else
                {
                    textBox1.Text = lc.Decrypt(reader.ReadLine(), lc.keyStr);
                    textBox2.Text = lc.Decrypt(reader.ReadLine(), lc.keyStr);
                    textBox3.Text = lc.Decrypt(reader.ReadLine(), lc.keyStr);
                    textBox4.Text = lc.Decrypt(reader.ReadLine(), lc.keyStr);
                    textBox5.Text = lc.Decrypt(reader.ReadLine(), lc.keyStr);
                    textBox6.Text = lc.Decrypt(reader.ReadLine(), lc.keyStr);
                    textBox7.Text = lc.Decrypt(reader.ReadLine(), lc.keyStr);
                    if (lc.keyfile.IndexOf("machine.lic") != -1)
                    {
                        textBox8.Text = lc.Decrypt(reader.ReadLine(), lc.keyStr);
                        textBox9.Text = lc.Decrypt(reader.ReadLine(), lc.keyStr);

                    }
                }
            }
        }
    }
}