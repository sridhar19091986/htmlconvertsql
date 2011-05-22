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
        string serialNum;
        string machineNum;
        string regNum;
        string regDate;
        string regTimes;
        string serialNum1;
        string machineNum1;
        string regNum1;
        string regDate1;
        string regTimes1;
        string regEmail;
       
        DateTime dt = DateTime.Now;
        SsfLicenseCheck.LicenseCheck lc = new SsfLicenseCheck.LicenseCheck();
        private void 生成机器码Btn_Click(object sender, EventArgs e)
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
            }

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

        }
    }
}