using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Windows.Forms;

namespace 示例
{
    class LicenseReadLib
    {
        private LincenseString ls;
        private LicenseCheck lc;
        private 注册.Form1 form = new 注册.Form1();

        public LicenseReadLib(LincenseString ls, LicenseCheck lc)
        {
            this.ls = ls;
            this.lc = lc;
        }
        public bool ReadLicense(string licPath)
        {
            //加密模块开始 
            if (!File.Exists(licPath))
                MessageBox.Show("未注册，系统将退出，请与软件供应商联系！");
            if (!File.Exists(licPath))
            { form.ShowDialog(); return false; }

            //加密模块检查序列号 
            LincenseString lsH = new LincenseString();
            StreamReader reader = new StreamReader(licPath);
            lsH.machineNum = reader.ReadLine();//1
            lsH.regLicense = reader.ReadLine();//2
            lsH.expTimes = reader.ReadLine();//3
            lsH.expireDate = reader.ReadLine();//4
            lsH.regDateFile = reader.ReadLine();//5
            lsH.regEmail = reader.ReadLine();//6
            lsH.regDate = reader.ReadLine();//7
            lsH.licDate = reader.ReadLine();//8
            lsH.licMachine = reader.ReadLine();//9
            reader.Close();
            ls.machineNum = lc.Decrypt(lsH.machineNum, lc.keyStr);//= reader.ReadLine();//1
            ls.regLicense = lc.Decrypt(lsH.regLicense, lc.keyStr);// = reader.ReadLine();//2
            ls.expTimes = lc.Decrypt(lsH.expTimes, lc.keyStr);// = reader.ReadLine();//3
            ls.expireDate = lc.Decrypt(lsH.expireDate, lc.keyStr); //= reader.ReadLine();//4
            ls.regDateFile = lc.Decrypt(lsH.regDateFile, lc.keyStr);// = reader.ReadLine();//5
            ls.regEmail = lc.Decrypt(lsH.regEmail, lc.keyStr); //= reader.ReadLine();//6
            ls.regDate = lc.Decrypt(lsH.regDate, lc.keyStr);// = reader.ReadLine();//7
            ls.licDate = lc.Decrypt(lsH.licDate, lc.keyStr);//= reader.ReadLine();//8
            ls.licMachine = lc.Decrypt(lsH.licMachine, lc.keyStr);// = reader.ReadLine();//9

            return true;
        }
        public bool CheckLicenseUse()
        {
            if (ls.licDate == null || ls.licMachine == null)
                MessageBox.Show("未注册，系统将退出，请与软件供应商联系！");
            if (ls.licDate == null || ls.licMachine == null)
            { form.ShowDialog(); return false; }
            return true;
        }
        public bool CheckLicenseSeries()
        {
            if (!lc.CheckRegHead(ls.machineNum, ls.regLicense))
                MessageBox.Show("序列号不正确，系统将退出，请与软件供应商联系！");
            if (!lc.CheckRegHead(ls.machineNum, ls.regLicense))
            { form.ShowDialog(); return false; }
            return true;
        }
        public bool CheckLicenseDate()
        {
            DateTime trial = DateTime.Parse(ls.licDate);
            DateTime expireDate = DateTime.Parse(ls.expireDate);
            if (DateTime.Compare(trial, expireDate) > 0)
                MessageBox.Show("期限超出，系统将退出，请与软件供应商联系！");
            if (DateTime.Compare(trial, expireDate) > 0)
            { form.ShowDialog(); return false; }

            return true;
        }
        public bool CheckLicenseTimes()
        {
            long YouregTimes = Int64.Parse(ls.expTimes);
            if (YouregTimes == 0)
                MessageBox.Show("次数超出，系统将退出，请与软件供应商联系！");
            if (YouregTimes == 0)
            { form.ShowDialog(); return false; }

            return true;
        }
        public void WriteLicense(string licPath)
        {
            if (File.Exists(licPath) == true)
                File.Delete(licPath);

            StreamWriter sw = File.CreateText(licPath);
            sw.WriteLine(lc.Encrypt(ls.machineNum, lc.keyStr));//1
            sw.WriteLine(lc.Encrypt(ls.regLicense, lc.keyStr));//2
            ls.expTimes = (Int32.Parse(ls.expTimes) - 1).ToString();//试用次数调整
            sw.WriteLine(lc.Encrypt(ls.expTimes, lc.keyStr));//3
            sw.WriteLine(lc.Encrypt(ls.expireDate, lc.keyStr));//4
            sw.WriteLine(lc.Encrypt(ls.regDateFile, lc.keyStr));//5
            sw.WriteLine(lc.Encrypt(ls.regEmail, lc.keyStr));//6
            sw.WriteLine(lc.Encrypt(ls.regDate, lc.keyStr));//7
            ls.licDate = DateTime.Now.ToString();  //试用日期调整
            sw.WriteLine(lc.Encrypt(ls.licDate, lc.keyStr));//8
            sw.WriteLine(lc.Encrypt(ls.licMachine, lc.keyStr));//9
            sw.Flush();
            sw.Close();
        }
    }
}
