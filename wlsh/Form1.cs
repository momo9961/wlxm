﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Threading;
using SH_MyUtil;
namespace wlsh
{
    public partial class Form1 : Form
    {
        /// <summary>
        /// 主线程 定时显示运行时间
        /// </summary>
        private Thread thread;
        private delegate void changeText(string result);
        public Form1()
        {
            InitializeComponent();
            qicheng();
        }
        private void qicheng(){
            ThreadStart threadStart = new ThreadStart(shzhong);//通过ThreadStart委托告诉子线程执行什么方法　
            Thread thread = new Thread(threadStart);
            thread.Name = "wodegaozhanghao";
            thread.Start();
            ThreadStart threadStart2 = new ThreadStart(foo);
            this.thread = new Thread(threadStart2);
            this.thread.Start();
        }
        private void shzhong() {
            long ks = MyFuncUtil.GetTimestamp();
            long ks2 = MyFuncUtil.GetTimestamp();
            while (true) {
                long js = MyFuncUtil.GetTimestamp();
                if ((js - ks) > 1000 * 60 * 5) {
                    ks = MyFuncUtil.GetTimestamp();
                    ShouHu s = new ShouHu();
                    s.wohaihuozhe();
                    bool t = s.panDuanChongQi(MyFuncUtil.getMachineName());
                    if (t) {
                        System.Diagnostics.Process.Start("shutdown.exe", "-r -f -t 15");
                    }
                }
                if ((js - ks2) > 1000 * 60 * 60)
                {
                    ks2 = MyFuncUtil.GetTimestamp();
                    ShouHu s = new ShouHu();                    
                    bool t = s.panDuanChongQi(MyFuncUtil.getMachineName());
                    if (!MyFuncUtil.getMachineName().ToLower().Equals("wlzhongkong") && t)
                    {
                        System.Diagnostics.Process.Start("shutdown.exe", "-r -f -t 15");
                    }
                }
            }
        }
        private void foo()
        {

            var ks = MyFuncUtil.GetTimestamp();
            var ks_gxyunxing = MyFuncUtil.GetTimestamp();
            var ks_cqyunxing = MyFuncUtil.GetTimestamp();
            var i = 1;
            string zidong = "";
            while (true)
            {
                Thread.Sleep(1000);
                var js = MyFuncUtil.GetTimestamp();
                i++;

                //MyFuncUtil.SecondToHour(+i + (js - ks) / 1000+" "
                CalcFinished("程序已运行:" + MyFuncUtil.SecondToHour(js - ks) + zidong);
                this.label1.ForeColor = Color.Red;
                if ((js - ks) > 1000 * 10 && (js - ks) <= 1000 * 20)
                {
                    WriteLog.WriteLogFile("程序搞自动倒计时 " + (20 - (js - ks) / 1000) + "秒");
                }
            }
        }
        private void CalcFinished(string result)
        {
            if (this.InvokeRequired)
            {
                this.BeginInvoke(new changeText(CalcFinished), result);
            }
            else
            {
                this.label1.Text = result.ToString();
            }
        }
    }
}