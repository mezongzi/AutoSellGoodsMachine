﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace AutoSellGoodsMachine
{
    /// <summary>
    /// FrmAdvanCfg_OnlineEntityCard.xaml 的交互逻辑
    /// </summary>
    public partial class FrmAdvanCfg_OnlineEntityCard : Window
    {
        private bool m_IsInit = true;

        public FrmAdvanCfg_OnlineEntityCard()
        {
            InitializeComponent();

            m_IsInit = true;
            InitForm();
        }

        private void InitForm()
        {
            #region 初始化界面资源

            tbTitle.Text = PubHelper.p_LangOper.GetStringBundle("SysCfg_Menu_AdvanCfg_OnlineEntityCard_Title");
            tbSwitch.Text = PubHelper.p_LangOper.GetStringBundle("Pub_Payment_Control");
            tbPort.Text = PubHelper.p_LangOper.GetStringBundle("SysCfg_Menu_AdvanCfg_OnlineEntityCard_Port");
            tbSwitch_Pwd.Text = PubHelper.p_LangOper.GetStringBundle("SysCfg_Menu_AdvanCfg_OnlineEntityCard_Pwd");

            btnSave.Content = PubHelper.p_LangOper.GetStringBundle("Pub_Btn_Save");
            btnCancel.Content = PubHelper.p_LangOper.GetStringBundle("Pub_Btn_Cancel");

            string strRun = PubHelper.p_LangOper.GetStringBundle("Pub_Run");
            string strStop = PubHelper.p_LangOper.GetStringBundle("Pub_Stop");
            rdbSwitch_Run.Content = rdbSwitch_Pwd_Run.Content =  strRun;
            rdbSwitch_Stop.Content = rdbSwitch_Pwd_Stop.Content =  strStop;

            for (int i = 1; i < 11; i++)
            {
                cmbPort.Items.Add("COM" + i.ToString());
            }

            #endregion

            #region 加载数据

            string strSwitch = PubHelper.p_BusinOper.SysCfgOper.GetSysCfgValue("OnlineEntityCardSwitch");
            if (strSwitch == "0")
            {
                rdbSwitch_Stop.IsChecked = true;
                cmbPort.IsEnabled = rdbSwitch_Pwd_Run.IsEnabled = rdbSwitch_Pwd_Stop.IsEnabled = false;
            }
            else
            {
                rdbSwitch_Run.IsChecked = true;
                cmbPort.IsEnabled = rdbSwitch_Pwd_Run.IsEnabled = rdbSwitch_Pwd_Stop.IsEnabled = true;
            }

            strSwitch = PubHelper.p_BusinOper.SysCfgOper.GetSysCfgValue("OnlineEntityCardIsPwd");
            if (strSwitch == "0")
            {
                rdbSwitch_Pwd_Stop.IsChecked = true;
            }
            else
            {
                rdbSwitch_Pwd_Run.IsChecked = true;
            }

            cmbPort.Text = "COM" + PubHelper.p_BusinOper.SysCfgOper.GetSysCfgValue("OnlineEntityCardDevicePort");

            #endregion

            m_IsInit = false;
        }

        private void btnCancel_Click(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void btnSave_Click(object sender, RoutedEventArgs e)
        {
            btnSave.IsEnabled = btnCancel.IsEnabled = false;
            DispatcherHelper.DoEvents();

            string strSwitch = "0";
            if (rdbSwitch_Run.IsChecked == true)
            {
                strSwitch = "1";
            }

            string strSwitch_Pwd = "0";
            if (rdbSwitch_Pwd_Run.IsChecked == true)
            {
                strSwitch_Pwd = "1";
            }

            string strPort = cmbPort.Text.Replace("COM", "");

            // 保存参数
            PubHelper.p_BusinOper.UpdateSysCfgValue("OnlineEntityCardSwitch", strSwitch);
            if (strSwitch == "1")
            {
                PubHelper.p_BusinOper.UpdateSysCfgValue("OnlineEntityCardDevicePort", strPort);
                PubHelper.p_BusinOper.UpdateSysCfgValue("OnlineEntityCardIsPwd", strSwitch_Pwd);
            }

            PubHelper.ShowMsgInfo(PubHelper.p_LangOper.GetStringBundle("Pub_OperSuc"), PubHelper.MsgType.Ok);
            this.Close();
        }

        private void rdbSwitch_Run_Checked(object sender, RoutedEventArgs e)
        {
            if (!m_IsInit)
            {
                cmbPort.IsEnabled = rdbSwitch_Pwd_Run.IsEnabled = rdbSwitch_Pwd_Stop.IsEnabled  = true;
            }
        }

        private void rdbSwitch_Stop_Checked(object sender, RoutedEventArgs e)
        {
            if (!m_IsInit)
            {
                cmbPort.IsEnabled = rdbSwitch_Pwd_Run.IsEnabled = rdbSwitch_Pwd_Stop.IsEnabled  = false;
            }
        }
    }
}
