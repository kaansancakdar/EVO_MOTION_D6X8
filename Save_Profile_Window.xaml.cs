using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Forms;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using GlobalVariables;
using _Functions;
using Microsoft.Win32;
using Newtonsoft.Json;
using System.Xml;

namespace EVO_MOTION_D6X8
{
    /// <summary>
    /// Interaction logic for Save_Profile_Window.xaml
    /// </summary>
    public partial class Save_Profile_Window : Window
    {
        Global_Variables GV = null;
        Functions FC = null;

        MainWindow MW = null;

        public Save_Profile_Window()
        {
            InitializeComponent();
        }

        internal void GetGlobalVariables(Global_Variables gv)
        {
            GV = gv;
        }

        public void GetMainWindow(MainWindow mainWindow)
        {
            MW = mainWindow;
        }

        internal void GetFunctions(Functions fc)
        {
            FC = fc;
        }

        

        public void ReadGlobalValues()
        {

            lbl_saveBLUE.Content = "Blue:" + GV.clr_B;
            lbl_saveGREEN.Content = "Green:" + GV.clr_G;
            lbl_saveRED.Content = "Red:" + GV.clr_R;
            lbl_brightness.Content = "Brightness:" + GV.brightness;
            lbl_saveLEDmode.Content = "Led Mode:" + GV.LED_Mode;
            lbl_saveLEDmodeName.Content = "Led Mode Name:" + GV.LED_Mode_Name;
            lbl_saveDeviceMode.Content = "Device Mode:" + GV.DeviceMode;
            lbl_saveDeviceModeName.Content = "Device Mode Name:" + GV.DeviceMode_Name;
            lbl_saveCAD.Content = "CAD Software:" + GV.CADSoftware;
            lbl_saveCADName.Content = "CAD Software:" + GV.CADSoftware_Name;

        }

        private void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
        {
            e.Cancel = true;
            this.Visibility = Visibility.Hidden;
            MW.IsEnabled = true;

        }

        private void btn_Save_Profile_Click(object sender, RoutedEventArgs e)
        {

            Microsoft.Win32.SaveFileDialog saveFileDialog = new Microsoft.Win32.SaveFileDialog();
            saveFileDialog.Filter = "User Profile (.usp)|*.usp";
            saveFileDialog.InitialDirectory=Directory.GetCurrentDirectory();

            bool result = (bool)saveFileDialog.ShowDialog();

            if (result == true)
            {
                if (File.Exists(saveFileDialog.FileName))
                {
                    File.Delete(saveFileDialog.FileName);
                }

                if (chk_LEDSave.IsChecked == true)
                {
                    GV.LED_Properties.clr_R = GV.clr_R;
                    GV.LED_Properties.clr_G = GV.clr_G;
                    GV.LED_Properties.clr_B = GV.clr_B;
                    GV.LED_Properties.brightness = GV.brightness;
                    GV.LED_Properties.led_Mode = GV.LED_Mode;
                    GV.LED_Properties.led_Mode_Name = GV.LED_Mode_Name;

                    string ledProperties_json = JsonConvert.SerializeObject(GV.LED_Properties);
                    File.AppendAllText(saveFileDialog.FileName, "LED Properties" + "\n");
                    File.AppendAllText(saveFileDialog.FileName, ledProperties_json);
                }

                if (chk_DeviceModeSave.IsChecked == true)
                {
                    GV.Device_Mode_Properties.DeviceMode = GV.DeviceMode;
                    GV.Device_Mode_Properties.DeviceMode_Name = GV.DeviceMode_Name;
                    GV.Device_Mode_Properties.CADSoftware = GV.CADSoftware;
                    GV.Device_Mode_Properties.CADSoftware_Name = GV.CADSoftware_Name;

                    string deviceproperties_json = JsonConvert.SerializeObject(GV.Device_Mode_Properties);
                    File.AppendAllText(saveFileDialog.FileName, "\n" + "Device Properties" + "\n");
                    File.AppendAllText(saveFileDialog.FileName, deviceproperties_json);
                }

                if (chk_NavasButton.IsChecked == true)
                {
                    File.AppendAllText(saveFileDialog.FileName, "\n" + "Navigation Movement Values" + "\n");

                    string PitchUp_json = JsonConvert.SerializeObject(GV.PitchUp_Values);
                    string PitchDown_json = JsonConvert.SerializeObject(GV.PitchDown_Values);
                    string RollRight_json = JsonConvert.SerializeObject(GV.RollRight_Values);
                    string RollLeft_json = JsonConvert.SerializeObject(GV.RollLeft_Values);
                    string YawCW_json = JsonConvert.SerializeObject(GV.YawCW_Values);
                    string YAWCCW_json = JsonConvert.SerializeObject(GV.YawCCW_Values);
                    string TransXpos_json = JsonConvert.SerializeObject(GV.TransXPos_Values);
                    string TransXneg_json = JsonConvert.SerializeObject(GV.TransXNeg_Values);
                    string TransYpos_json = JsonConvert.SerializeObject(GV.TransYPos_Values);
                    string TransYneg_json = JsonConvert.SerializeObject(GV.TransYNeg_Values);
                    string TransZpos_json = JsonConvert.SerializeObject(GV.TransZPos_Values);
                    string TransZneg_json = JsonConvert.SerializeObject(GV.TransZNeg_Values);

                    File.AppendAllText(saveFileDialog.FileName, PitchUp_json + "\n");
                    File.AppendAllText(saveFileDialog.FileName, PitchDown_json + "\n");
                    File.AppendAllText(saveFileDialog.FileName, RollRight_json + "\n");
                    File.AppendAllText(saveFileDialog.FileName, RollLeft_json + "\n");
                    File.AppendAllText(saveFileDialog.FileName, YawCW_json + "\n");
                    File.AppendAllText(saveFileDialog.FileName, YAWCCW_json + "\n");
                    File.AppendAllText(saveFileDialog.FileName, TransXpos_json + "\n");
                    File.AppendAllText(saveFileDialog.FileName, TransXneg_json + "\n");
                    File.AppendAllText(saveFileDialog.FileName, TransYpos_json + "\n");
                    File.AppendAllText(saveFileDialog.FileName, TransYneg_json + "\n");
                    File.AppendAllText(saveFileDialog.FileName, TransZpos_json + "\n");
                    File.AppendAllText(saveFileDialog.FileName, TransZneg_json + "\n");

                }

                if (chk_MKButtonSave.IsChecked == true)
                {
                    File.AppendAllText(saveFileDialog.FileName, "\n" + "Button Values" + "\n");

                    string MK1_save_json = JsonConvert.SerializeObject(GV.MK1_Values);
                    string MK2_save_json = JsonConvert.SerializeObject(GV.MK2_Values);
                    string MK3_save_json = JsonConvert.SerializeObject(GV.MK3_Values);
                    string MK4_save_json = JsonConvert.SerializeObject(GV.MK4_Values);
                    string MK5_save_json = JsonConvert.SerializeObject(GV.MK5_Values);
                    string MK6_save_json = JsonConvert.SerializeObject(GV.MK6_Values);
                    string MK7_save_json = JsonConvert.SerializeObject(GV.MK7_Values);
                    string MK8_save_json = JsonConvert.SerializeObject(GV.MK8_Values);

                    File.AppendAllText(saveFileDialog.FileName, MK1_save_json + "\n");
                    File.AppendAllText(saveFileDialog.FileName, MK2_save_json + "\n");
                    File.AppendAllText(saveFileDialog.FileName, MK3_save_json + "\n");
                    File.AppendAllText(saveFileDialog.FileName, MK4_save_json + "\n");
                    File.AppendAllText(saveFileDialog.FileName, MK5_save_json + "\n");
                    File.AppendAllText(saveFileDialog.FileName, MK6_save_json + "\n");
                    File.AppendAllText(saveFileDialog.FileName, MK7_save_json + "\n");
                    File.AppendAllText(saveFileDialog.FileName, MK8_save_json + "\n");
                }

                if (chk_VkButtonSave.IsChecked == true)
                {
                    File.AppendAllText(saveFileDialog.FileName, "\n" + "VK Button Values" + "\n");

                    string Vk1_save_json = JsonConvert.SerializeObject(GV.Vk1_Values);
                    string Vk2_save_json = JsonConvert.SerializeObject(GV.Vk2_Values);
                    string Vk3_save_json = JsonConvert.SerializeObject(GV.Vk3_Values);
                    string Vk4_save_json = JsonConvert.SerializeObject(GV.Vk4_Values);
                    string Vk5_save_json = JsonConvert.SerializeObject(GV.Vk5_Values);
                    string Vk6_save_json = JsonConvert.SerializeObject(GV.Vk6_Values);
                    string Vk7_save_json = JsonConvert.SerializeObject(GV.Vk7_Values);
                    string Vk8_save_json = JsonConvert.SerializeObject(GV.Vk8_Values);

                    File.AppendAllText(saveFileDialog.FileName, Vk1_save_json + "\n");
                    File.AppendAllText(saveFileDialog.FileName, Vk2_save_json + "\n");
                    File.AppendAllText(saveFileDialog.FileName, Vk3_save_json + "\n");
                    File.AppendAllText(saveFileDialog.FileName, Vk4_save_json + "\n");
                    File.AppendAllText(saveFileDialog.FileName, Vk5_save_json + "\n");
                    File.AppendAllText(saveFileDialog.FileName, Vk6_save_json + "\n");
                    File.AppendAllText(saveFileDialog.FileName, Vk7_save_json + "\n");
                    File.AppendAllText(saveFileDialog.FileName, Vk8_save_json + "\n");


                }

            }
        }
    }
}
