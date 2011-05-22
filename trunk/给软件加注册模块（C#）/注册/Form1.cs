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

        }
        void 生成注册号(string reqFile)
        {
            if (File.Exists(reqFile) == true)
                File.Delete(reqFile);
            StreamWriter sw = File.CreateText(reqFile);
            sw.WriteLine(lc.Encrypt(ls.regEmail, lc.keyStr));
            sw.WriteLine(lc.Encrypt(ls.regDateFile, lc.keyStr));
            sw.WriteLine(lc.Encrypt(ls.machineNum, lc.keyStr));
            sw.Flush();
            sw.Close();
        }
        private void 注册Btn_Click(object sender, EventArgs e)
        {
            string appPath = Application.StartupPath.ToString();
            string licPath = appPath + "\\LAY3-T.lic";
            string licDate = dt.ToString();


            OpenFileDialog dlgOpenfile = new OpenFileDialog();
            string strFileFullName = null;
            dlgOpenfile.Filter = "txt files (*.txt)|*.txt|All files (*.*)|*.*";
            dlgOpenfile.Title = "Open";
            dlgOpenfile.ShowDialog();
            dlgOpenfile.RestoreDirectory = true;
            if (!string.IsNullOrEmpty(dlgOpenfile.FileName))
            {
                strFileFullName = dlgOpenfile.FileName;

                if (File.Exists(licPath))
                {
                    StreamReader reader = new StreamReader(licPath);
                    machineNum = reader.ReadLine();
                    regNum = reader.ReadLine();
                    regTimes = reader.ReadLine();
                    regDate = reader.ReadLine();
                    serialNum = reader.ReadLine();
                    reader.Close();
                    StreamReader reader1 = new StreamReader(strFileFullName);
                    machineNum1 = reader1.ReadLine();
                    regNum1 = reader1.ReadLine();
                    regTimes1 = reader1.ReadLine();
                    regDate1 = reader1.ReadLine();
                    serialNum1 = reader1.ReadLine();
                    reader1.Close();
                    if (string.Compare(regDate, regDate1) == 0)
                    {
                        MessageBox.Show("注册失败");
                        return;
                    }
                    else
                    {
                        File.Delete(licPath);
                        File.Copy(strFileFullName, licPath);
                        File.AppendAllText(licPath, lc.Encrypt(licDate, "icdredge") + Environment.NewLine);
                        File.AppendAllText(licPath, lc.Encrypt(textBox3.Text, "icdredge") + Environment.NewLine);
                        MessageBox.Show("注册成功");
                    }
                }
                else
                {
                    File.Copy(strFileFullName, licPath);
                    File.AppendAllText(licPath, lc.Encrypt(licDate, "icdredge") + Environment.NewLine);
                    File.AppendAllText(licPath, lc.Encrypt(textBox3.Text, "icdredge") + Environment.NewLine);
                    MessageBox.Show("注册成功");
                }
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