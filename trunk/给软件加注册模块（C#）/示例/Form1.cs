using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Management;
using System.Security.Cryptography;

namespace 示例
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }
        LincenseString ls = new LincenseString();
        LicenseCheck lc = new LicenseCheck();
        private void Form1_Load(object sender, EventArgs e)
        {
            string appPath = Application.StartupPath.ToString();
            string licPath = appPath + "\\machine.lic";
            LicenseReadLib lr = new LicenseReadLib(ls, lc);
            if (lr.ReadLicense(licPath))
            {
                if (lr.CheckLicenseUse())
                {
                    if (lr.CheckLicenseSeries())
                    {
                        if (lr.CheckLicenseDate())
                        {
                            if (lr.CheckLicenseTimes()) { lr.WriteLicense(licPath); }
                            else { this.Close(); }
                        }
                        else { this.Close(); }
                    }
                    else { this.Close(); }
                }
                else { this.Close(); }
            }
            else { this.Close(); }
        }


        private void 校验注册号代码Btn_Click(object sender, EventArgs e)
        {
            //检验注册号，把检验结果找一内存保存起来。不要在校验结果代码附近做任何影响程序正常工作的处理，这样不易被跟踪。
            string regTail3 = lc.GetRegTail3ByMac(ls.regDateFile);
            if (string.Compare(regTail3, 0, ls.regLicense, 12, 17) == 0)
                ls.bRegOK = true;
            else
                ls.bRegOK = false;
        }

        private void 核心功能代码代表Btn_Click(object sender, EventArgs e)
        {
            int result;
            if (ls.bRegOK)
            {
                result = 10 + 10;
            }
            else
            {
                result = 10 + 10 + 1;  //发现注册码正常的标记为false时,对核心功能代码进行不易发觉的修改，导致结果无法使用，注：不要弹出易被跟踪的消息事件等。
            }
            textBox1.Text = result.ToString();
        }
    }
}