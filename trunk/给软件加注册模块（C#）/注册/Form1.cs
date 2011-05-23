using System;
using System.Windows.Forms;
using System.IO;

namespace 注册
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        LicenseCheck lc = new LicenseCheck();
        LicenseString ls = new LicenseString();
        private void 生成机器码Btn_Click(object sender, EventArgs e)
        {
            ls.regEmail = textBox1.Text;
            textBox2.Text = ls.regDateFile;
            textBox3.Text = ls.machineNum;
            SaveFileDialog saveFileDialog1 = new SaveFileDialog();
            saveFileDialog1.Filter = "Request File|*.req";
            saveFileDialog1.Title = "Save an Request File";
            saveFileDialog1.ShowDialog();
            if (saveFileDialog1.FileName != "")
                生成注册号(saveFileDialog1.FileName);
            this.Close();

        }
        void 生成注册号(string reqFile)
        {
            if (File.Exists(reqFile) == true)
                File.Delete(reqFile);
            StreamWriter sw = File.CreateText(reqFile);
            sw.WriteLine(lc.Encrypt(ls.regEmail, lc.keyStr));
            sw.WriteLine(lc.Encrypt(ls.regDateFile, lc.keyStr));
            sw.WriteLine(lc.Encrypt(ls.machineNum, lc.keyStr));
            sw.WriteLine(lc.Encrypt(ls.regDateTime.ToString(), lc.keyStr));
            sw.Flush();
            sw.Close();
        }
        private void 注册Btn_Click(object sender, EventArgs e)
        {
            string appPath = Application.StartupPath.ToString();
            string licPath = appPath + "\\machine.lic";
            string licDate = DateTime.Now.ToString();
            OpenFileDialog dlgOpenfile = new OpenFileDialog();
            string strFileFullName = null;
            dlgOpenfile.Filter = "License File|*.lic";
            dlgOpenfile.Title = "Open";
            dlgOpenfile.ShowDialog();
            dlgOpenfile.RestoreDirectory = true;
            if (!string.IsNullOrEmpty(dlgOpenfile.FileName))
            {
                strFileFullName = dlgOpenfile.FileName;
                if (File.Exists(licPath))
                    File.Delete(licPath);
                File.Copy(strFileFullName, licPath);
                File.AppendAllText(licPath, lc.Encrypt(licDate, lc.keyStr) + Environment.NewLine);
                File.AppendAllText(licPath, lc.Encrypt(ls.machineNum, lc.keyStr) + Environment.NewLine);
                MessageBox.Show("注册成功");
                this.Close();
            }
            else
            {
                return;
            }
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            ls.regDateFile= ls.regDateTime.ToFileTime().ToString();
            ls.machineNum = lc.获取机器码(ls.regDateFile);
        }
    }
}