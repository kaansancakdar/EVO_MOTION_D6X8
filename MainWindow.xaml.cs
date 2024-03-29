using System;
using System.Drawing;
using System.Windows;
using System.Windows.Media;
using System.Windows.Input;
using Button_Tags;
using HidSharp;
using USB_Communication;
using System.Data;
using System.Windows.Controls;
using System.ComponentModel;
using GlobalVariables;
using _Functions;

//namespace SpaceNavGUI_1
namespace EVO_MOTION_D6X8
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {


        USB_Connection UC=new USB_Connection();
        Global_Variables GV = new Global_Variables();
        Functions FC = new Functions();

        System.Windows.Forms.NotifyIcon ni = new System.Windows.Forms.NotifyIcon();
        Save_Profile_Window save_profile_window = new Save_Profile_Window();

        Page_1_Main pg1 = new Page_1_Main();
        Page_2_Button pg2 = new Page_2_Button();
        Page_3_LED pg3 = new Page_3_LED();
        Page_4_Virtual_Keys pg4 = new Page_4_Virtual_Keys();

        public MainWindow()
        {
            InitializeComponent();

            ni.Icon = new System.Drawing.Icon("icons/icon1.ico");
            ni.Visible = true;

            ni.DoubleClick +=
            delegate (object sender, EventArgs args)
            {
                MainWindow_1.Show();
                this.WindowState = WindowState.Normal;
            };

            btn_Main.IsEnabled          = false;
            btn_buttonconfig.IsEnabled  = false;
            btn_LED.IsEnabled           = false;
            btn_VirtualKeys.IsEnabled   = false;
            btn_minimize.IsEnabled      = false;
            btn_VirtualKeys.IsEnabled   = false;
            ni.ContextMenuStrip         = new System.Windows.Forms.ContextMenuStrip();
            ni.ContextMenuStrip.Items.Add("Close", null, this.Notify_Close_Click);

            pg1.GetDevice(UC);
            pg1.GetGlobalVariables(GV);
            pg1.GetSaveWindow(save_profile_window);
            pg1.GetMainWindow((MainWindow)MainWindow_1);
            pg1.GetFunctions(FC);
            pg1.GetPage2(pg2);

            pg2.GetDevice(UC);
            pg2.GetGlobalVariables(GV);
            pg2.GetFunctions(FC);
            pg2.GetPage1(pg1);

            pg3.GetDevice(UC);
            pg3.GetGlobalVariables(GV);
            pg3.GetFunctions(FC);

            pg4.GetGlobalVariables(GV);
            pg4.GetFunctions(FC);

            save_profile_window.GetMainWindow(this);

        }

        public void Notify_Close_Click(object sender, EventArgs e)
        {
            ni.Dispose();
            System.Windows.Application.Current.Shutdown();
        }

        protected override void OnStateChanged(EventArgs e)
        {
            if (WindowState == System.Windows.WindowState.Minimized)
            {
                Hide();
            }

            base.OnStateChanged(e);
        }


        #region Side Bar Buttons Actions

        private void btn_Main_Click(object sender, RoutedEventArgs e)
        {
  
            if (UC.stream != null)
            {
                UC.stream.Dispose();
            }
            frm_Main.Content = pg1;
             
            lbl_PageName.Content = "Main";

            btn_Main.IsEnabled = false;
            btn_buttonconfig.IsEnabled = true;
            btn_LED.IsEnabled = true;
            btn_VirtualKeys.IsEnabled = true;
            btn_minimize.IsEnabled = true;
            btn_VirtualKeys.IsEnabled = true;

        }

        private void btn_Macrobtn_Click(object sender, RoutedEventArgs e)
        {
            

            if (UC.stream!=null)
            {
                UC.stream.Dispose();
            }
            frm_Main.Content = pg2;
            lbl_PageName.Content = "Button Configuration";
            pg2.Page_Loaded(null,null);
            btn_Main.IsEnabled = true;
            btn_buttonconfig.IsEnabled = false;
            btn_LED.IsEnabled = true;
            btn_VirtualKeys.IsEnabled = true;
            btn_minimize.IsEnabled = true;
            btn_VirtualKeys.IsEnabled = true;
        }

        private void btn_VirtualKeys_Click(object sender, RoutedEventArgs e)
        {

            frm_Main.Content = pg4;
            lbl_PageName.Content = "Virtual Keys Configuration";

            btn_Main.IsEnabled = true;
            btn_buttonconfig.IsEnabled = true;
            btn_LED.IsEnabled = true;
            btn_VirtualKeys.IsEnabled = true;
            btn_minimize.IsEnabled = true;
            btn_VirtualKeys.IsEnabled = false;
        }

        private void btn_Led_Click(object sender, RoutedEventArgs e)
        {

            if (UC.stream != null)
            {
                UC.stream.Dispose();
            }
            frm_Main.Content = pg3;
            lbl_PageName.Content = "LED configuration";

            btn_Main.IsEnabled = true;
            btn_buttonconfig.IsEnabled = true;
            btn_LED.IsEnabled = false;
            btn_VirtualKeys.IsEnabled = true;
            btn_minimize.IsEnabled = true;
            btn_VirtualKeys.IsEnabled = true;
        }

        private void btn_minimize_Click(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;

        }
        #endregion
        
        private void window_top_MouseDown(object sender, MouseButtonEventArgs e)
        {
            if (e.LeftButton == MouseButtonState.Pressed)
            {
                DragMove();
            }
        }

        private void btn_connectDevice_Click(object sender, RoutedEventArgs e)
        {
            if(btn_connectDevice.IsChecked == true)
            {
                bool _deviceConnect = UC.Connect_Device();

                if (_deviceConnect == true)
                {
                    UC.device.TryOpen(out UC.stream);

                    btn_connectDevice.Content = "Device Connected";
                    SolidColorBrush solidColor = new SolidColorBrush(Colors.LimeGreen);
                    IconProperties.SetTagFillMousePressed(btn_connectDevice, solidColor);
                    btn_connectDevice.Foreground = solidColor;
                    btn_connectDevice.IsChecked = true;

                    btn_Main.IsEnabled = true;
                    btn_buttonconfig.IsEnabled = true;
                    btn_LED.IsEnabled = true;
                    btn_VirtualKeys.IsEnabled = true;
                    btn_minimize.IsEnabled = true;
                    btn_VirtualKeys.IsEnabled = true;

                    btn_Main_Click(null, null);
                    btn_Main.IsChecked = true;

                    FC.GetDevice(UC);
                    FC.GetGlobalVariables(GV);

                    //Read device configuration from device
                    FC.Read_Device_Configurations();

                    //Read button values from device
                    System.Threading.Thread.Sleep(500);
                    FC.Read_All_Button_Data();
                    System.Threading.Thread.Sleep(500);
                    GV.MK_Data_Send = FC.Convert_Read_Button_Data_to_Send_Data(GV.MK_Data_Read);

                    pg2.Page_Loaded(null, null);

                }
                else
                {
                    btn_connectDevice.IsChecked = false;
                    btn_Main.IsEnabled = false;
                    btn_buttonconfig.IsEnabled = false;
                    btn_LED.IsEnabled = false;
                    btn_VirtualKeys.IsEnabled = false;
                    btn_minimize.IsEnabled = false;
                    btn_VirtualKeys.IsEnabled = false;
                    btn_connectDevice.Content = "Connect Device";
                }
            }

            if(btn_connectDevice.IsChecked == false)
            {
                frm_Main.Content = null;

                SolidColorBrush solidColor = new SolidColorBrush(Colors.DarkRed);
                IconProperties.SetTagFillMousePressed(btn_connectDevice, solidColor);
                btn_connectDevice.Foreground = solidColor;

                btn_connectDevice.IsChecked = false;

                btn_Main.IsEnabled = false;
                btn_buttonconfig.IsEnabled = false;
                btn_LED.IsEnabled = false;
                btn_VirtualKeys.IsEnabled = false;
                btn_minimize.IsEnabled = false;
                btn_VirtualKeys.IsEnabled = false;
                btn_connectDevice.Content = "Connect Device";
                try
                {
                    UC.stream.Close();
                }
                catch
                {

                }
            }
        }

    }
}
