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
using Soccer.Score.Forecast.LicenseCheck;

namespace 示例
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

        bool bRegOK = true;

        LicenseCheck lc = new LicenseCheck();
        DateTime dt = DateTime.Now;

        private void Form1_Load(object sender, EventArgs e)
        {
            //加密模块开始 
            string appPath = Application.StartupPath.ToString();
            string licPath = appPath + "\\LAY3-T.lic";
            注册.Form1 form = new 注册.Form1();
            if (!File.Exists(licPath))
            {
                if (!File.Exists(licPath))
                {
                    this.Close();
                    return;
                }
            }
            //加密模块检查序列号 
            StreamReader reader = new StreamReader(licPath);
            machineNum = reader.ReadLine();
            regNum = reader.ReadLine();
            regTimes = reader.ReadLine();
            regDate = reader.ReadLine();
            serialNum = reader.ReadLine();
            string licDate = reader.ReadLine();
            string licQQ = reader.ReadLine();
            reader.Close();
            if (machineNum == null || regNum == null || regTimes == null || regDate == null || licDate == null || licQQ == null)
            {
                form.ShowDialog();
                this.Close();
                return;
            }
            licQQ = lc.Decrypt(licQQ, "icdredge");//新写入
            if (licQQ != "QQ-619498525")
            {
                MessageBox.Show("注册号不正确，系统将退出，请与软件供应商联系！");
                this.Close();
                return;
            }
            regNum = lc.Decrypt(regNum, "icomicom");
            machineNum = lc.Decrypt(machineNum, "icomicom");
            if (!lc.CheckRegHead(machineNum, regNum))
            {
                MessageBox.Show("序列号不正确，系统将退出，请与软件供应商联系！");
                this.Close();
                return;
            }
            licDate = lc.Decrypt(licDate, "icdredge");//新写入
            regDate = lc.Decrypt(regDate, "icomicom");
            DateTime trial = DateTime.Parse(licDate);
            DateTime YouregDate = DateTime.Parse(regDate);
            if (DateTime.Compare(trial, YouregDate) > 0)
            {
                MessageBox.Show("试用期限超出，系统将退出，请与软件供应商联系！");
                this.Close();
                return;
            }
            regTimes = lc.Decrypt(regTimes, "icomicom");
            long YouregTimes = Int64.Parse(regTimes);
            serialNum = lc.Decrypt(serialNum, "icomicom");
            if (YouregTimes == 0)
            {
                MessageBox.Show("试用次数超出，系统将退出，请与软件供应商联系！");
                this.Close();
                return;
            }
            else
            {
                if (File.Exists(licPath) == true)
                {
                    File.Delete(licPath);
                }
                StreamWriter sw = File.CreateText(licPath);
                sw.WriteLine(lc.Encrypt(machineNum, "icomicom"));
                sw.WriteLine(lc.Encrypt(regNum, "icomicom"));
                sw.WriteLine(lc.Encrypt((YouregTimes - 1).ToString(), "icomicom"));
                sw.WriteLine(lc.Encrypt(regDate, "icomicom"));
                sw.WriteLine(lc.Encrypt(serialNum, "icomicom"));
                DateTime dt = DateTime.Now;
                licDate = dt.ToString();
                sw.WriteLine(lc.Encrypt(licDate, "icdredge"));
                sw.WriteLine(lc.Encrypt(licQQ, "icdredge"));
                sw.Flush();
                sw.Close();
            }
        }

        private void 校验注册号代码Btn_Click(object sender, EventArgs e)
        {
            //检验注册号，把检验结果找一内存保存起来。不要在校验结果代码附近做任何影响程序正常工作的处理，这样不易被跟踪。
            string regTail3 =lc. GetRegTail3ByMac(serialNum);
            if (string.Compare(regTail3, 0, regNum, 12, 17) == 0)
                bRegOK = true;
            else
                bRegOK = false;
        }

        private void 核心功能代码代表Btn_Click(object sender, EventArgs e)
        {
            int result;
            if (bRegOK)
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