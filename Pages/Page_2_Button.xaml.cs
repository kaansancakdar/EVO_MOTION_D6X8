using HidSharp;
using KeyShortCut;
using KnowledgewareTypeLib;
using USB_Communication;
using GlobalVariables;
using _Functions;
using System;
using System.Collections.Generic;
using System.IO;
using System.Runtime.InteropServices.ComTypes;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using static System.Windows.Forms.VisualStyles.VisualStyleElement;
using static System.Windows.Forms.VisualStyles.VisualStyleElement.ToolBar;
using System.Collections;
using Newtonsoft.Json.Linq;
using Solidworks_Api;
using System.Linq;
using SolidWorks.Interop.sldworks;
using System.Windows.Input;
using Button_Tags;
using System.Windows.Media;
//using System.Windows.Forms; 

namespace EVO_MOTION_D6X8
{
    /// <summary>
    /// Interaction logic for Page_2_Button.xaml
    /// </summary>
    /// 
    
    public partial class Page_2_Button : System.Windows.Controls.Page
    {

        KeyCodes KY = new KeyCodes();
        Global_Variables GV = null;
        USB_Connection UC = null;
        Functions FC = null;
        Page_1_Main PG1 = null;

        Api_Solidworks SWApp = new Api_Solidworks();

        public List<string> active_keys = new List<string>();
        List<string> MK_Keys = new List<string>();
        public List<byte> active_bytes = new List<byte>();

        string text_shortcut=null;

        public Page_2_Button()
        {
            InitializeComponent();

            cmb_MacroButtonName_.SelectedIndex = 0;
            cmb_MacroButtonName.SelectedIndex = 0;
            //cmb_NavMovement.SelectedIndex = 0;

            txt_macroPrintText.Visibility = Visibility.Collapsed;
            cmb_shortcut_val1.Visibility = Visibility.Visible;
            cmb_shortcut_val2.Visibility = Visibility.Visible;
            cmb_shortcut_val3.Visibility = Visibility.Visible;
            cmb_NavMovement.Visibility = Visibility.Collapsed;

            cmb_Predfined_Custom_Shortcut.SelectedIndex = 1;
            dck_button_shortcut_text.Visibility = Visibility.Visible;
            stckp_shortcut.Visibility = Visibility.Visible;
            cmb_Predfined_Shortcut.Visibility = Visibility.Collapsed;
            cmb_Predefined_Shortcut_List.Visibility = Visibility.Collapsed;

            active_keys.AddRange(KY.get_active_keys());
            active_bytes.AddRange(KY.get_active_bytes());

            cmb_shortcut_val1.ItemsSource = active_keys;
            cmb_shortcut_val2.ItemsSource = active_keys;
            cmb_shortcut_val3.ItemsSource = active_keys;

            cmb_shortcut_val1.SelectedIndex = 0;
            cmb_shortcut_val2.SelectedIndex = 0;
            cmb_shortcut_val3.SelectedIndex = 0;

            cmb_MacroButtonName.ItemsSource = KY.MK_Button_Names;
            cmb_NavMovement.ItemsSource = KY.Navigator_Movement;

            SolidColorBrush solidColor = new SolidColorBrush(Colors.DarkRed);
            IconProperties.SetTagFill(btn_NavigationasButton, solidColor);
        }

        #region GETS
        internal void GetPage1(Page_1_Main pg)
        {
            PG1 = pg;
        }

        internal void GetDevice(USB_Connection uc)
        {
            UC = uc;
        }

        internal void GetGlobalVariables(Global_Variables gv)
        {
            GV = gv;
        }

        internal void GetFunctions(Functions fc)
        {
            FC = fc;
        }
        #endregion

        private void btn_setmacroShortcut_Click(object sender, RoutedEventArgs e)
        {
            txt_macroPrintText.Visibility = Visibility.Collapsed;
            stckp_shortcut.Visibility = Visibility.Visible;
            text_shortcut = "customShortcut";
        }

        private void btn_setmacroPrintText_Click(object sender, RoutedEventArgs e)
        {
            txt_macroPrintText.Visibility = Visibility.Visible;
            stckp_shortcut.Visibility = Visibility.Collapsed;
            text_shortcut = "Text";
        }

        private void btn_MacroSet_Click(object sender, RoutedEventArgs e)
        {

            if(cmb_Predfined_Custom_Shortcut.SelectedIndex == 1)
            {

                if (text_shortcut == "customShortcut")
                {
                    string[] keystructName = { "MK Button", "Modifier", "Reserved", "KeyCode1", "KeyCode2", "KeyCode3", "KeyCode4", "KeyCode5", "KeyCode6" };

                    int[] keystruct = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                    List<int> abc = new List<int>();
                    abc = KY.USB_HID_KEY(cmb_shortcut_val1.SelectedIndex, cmb_shortcut_val2.SelectedIndex, cmb_shortcut_val3.SelectedIndex, KY.GetLayoutCode());

                    //Button shortcut
                    if (cmb_MacroButtonName_.SelectedIndex == 0)
                    {
                        if (cmb_MacroButtonName.SelectedIndex == 0)
                        {
                            System.Windows.MessageBox.Show("Select MK Button");
                        }
                        else
                        {
                            keystruct[0] = cmb_MacroButtonName.SelectedIndex;

                            for (int i = 0; i < abc.Count; i++)
                            {
                                keystruct[i + 1] = abc[i];
                            }

                            lst_macroKeyValues.Items[cmb_MacroButtonName.SelectedIndex - 1] =
                                "BT" + Convert.ToString(cmb_MacroButtonName.SelectedIndex) + ": "
                                + cmb_shortcut_val1.SelectedItem + "-"
                                + cmb_shortcut_val2.SelectedItem + "-"
                                + cmb_shortcut_val3.SelectedItem;

                            GV.MK_Data_String[cmb_MacroButtonName.SelectedIndex - 1] = lst_macroKeyValues.Items[cmb_MacroButtonName.SelectedIndex - 1].ToString();

                            byte[] message = new byte[11];
                            message = FC.Create_Shortcut_Message(keystruct, cmb_MacroButtonName.SelectedIndex);

                            FC.Send_Device(message, message.Length);

                            for (int i = 0; i <= message.Length - 1; i++)
                            {
                                GV.MK_Data_Send[cmb_MacroButtonName.SelectedIndex - 1, i] = message[i];
                            }
                        }
                    }

                    //Navigation movement as button shortcut
                    if (cmb_MacroButtonName_.SelectedIndex == 1)
                    {
                        if (cmb_NavMovement.SelectedIndex == 0)
                        {
                            System.Windows.MessageBox.Show("Select Navigation Movement");
                        }

                        else
                        {
                            keystruct[0] = cmb_NavMovement.SelectedIndex;

                            for (int i = 0; i < abc.Count; i++)
                            {
                                keystruct[i + 1] = abc[i];
                            }

                            lst_navasbutton.Items[cmb_NavMovement.SelectedIndex - 1] =
                                cmb_NavMovement.SelectedItem.ToString()+": "
                                + cmb_shortcut_val1.SelectedItem + "-"
                                + cmb_shortcut_val2.SelectedItem + "-"
                                + cmb_shortcut_val3.SelectedItem;

                            GV.NavMove_String[cmb_NavMovement.SelectedIndex - 1] = lst_navasbutton.Items[cmb_NavMovement.SelectedIndex - 1].ToString();

                            byte[] message = new byte[11];
                            message = FC.Create_Shortcut_Message(keystruct, cmb_NavMovement.SelectedIndex);

                            //FC.Send_Device(message, message.Length);

                            for (int i = 0; i <= message.Length - 1; i++)
                            {
                                GV.NavMove_Data_Send[cmb_NavMovement.SelectedIndex - 1, i] = message[i];
                            }
                        }
                    }
                }

                if (text_shortcut == "Text")
                {
                   //Buttons text value
                    if (cmb_MacroButtonName_.SelectedIndex == 0)
                    {
                        if (cmb_MacroButtonName.SelectedIndex == 0)
                        {
                            System.Windows.MessageBox.Show("Select MK Button");
                        }
                        else
                        {
                            lst_macroKeyValues.Items[cmb_MacroButtonName.SelectedIndex - 1] =
                                "BT" + Convert.ToString(cmb_MacroButtonName.SelectedIndex) +
                                ": "
                                + txt_macroPrintText.Text;
                            GV.MK_Data_String[cmb_MacroButtonName.SelectedIndex - 1] = lst_macroKeyValues.Items[cmb_MacroButtonName.SelectedIndex].ToString();

                            byte[] bytes;
                            bytes = Encoding.UTF8.GetBytes(txt_macroPrintText.Text);
                            //txt_macroPrintText.Text += BitConverter.ToString(bytes);

                            byte[] message = new byte[64];
                            message = FC.Create_Text_Message(bytes, cmb_MacroButtonName.SelectedIndex);

                            FC.Send_Device(message, message.Length);

                            for (int i = 0; i <= message.Length - 1; i++)
                            {
                                GV.MK_Data_Send[cmb_MacroButtonName.SelectedIndex - 1, i] = message[i];
                            }

                        }
                    }

                    //Navigations as a button text value
                    if (cmb_MacroButtonName_.SelectedIndex == 1)
                    {
                        if (cmb_NavMovement.SelectedIndex == 0)
                        {
                            System.Windows.MessageBox.Show("Select Navigation Movement");
                        }
                        else
                        {
                            lst_navasbutton.Items[cmb_NavMovement.SelectedIndex - 1] =
                                cmb_NavMovement.SelectedItem.ToString() + ": "
                                + txt_macroPrintText.Text;

                            GV.NavMove_String[cmb_NavMovement.SelectedIndex - 1] = lst_navasbutton.Items[cmb_NavMovement.SelectedIndex].ToString();

                            byte[] bytes;
                            bytes = Encoding.UTF8.GetBytes(txt_macroPrintText.Text);
                            //txt_macroPrintText.Text += BitConverter.ToString(bytes);

                            byte[] message = new byte[64];
                            message = FC.Create_Text_Message(bytes, cmb_NavMovement.SelectedIndex);


                            for (int i = 0; i <= message.Length - 1; i++)
                            {
                                GV.NavMove_Data_Send[cmb_NavMovement.SelectedIndex - 1, i] = message[i];
                            }
                        }
                    }


                }
            }

            if(cmb_Predfined_Custom_Shortcut.SelectedIndex==0)
            {
                //Windows shortcut
                if (cmb_Predfined_Shortcut.SelectedIndex == 0) 
                {
                    
                }
                
                //Solidworks shortcut
                if (cmb_Predfined_Shortcut.SelectedIndex == 1)
                {
                    //System.Windows.Forms.MessageBox.Show(cmb_Predfined_Shortcut.Text);

                    if (cmb_MacroButtonName.SelectedIndex == 0)
                    {
                        System.Windows.MessageBox.Show("Select MK Button");
                    }

                    else
                    {
                        byte[] bytes;
                        bytes = Encoding.UTF8.GetBytes("Solidworks:" + cmb_Predefined_Shortcut_List.SelectedItem.ToString());

                        byte[] message = new byte[64];
                        message = FC.Create_CAD_Message(bytes, cmb_MacroButtonName.SelectedIndex);

                        FC.Send_Device(message, message.Length);
                        Console.WriteLine("[{0}]", string.Join(", ", message));
                        Console.WriteLine(System.Text.Encoding.UTF8.GetString(bytes));

                        for (int i = 0; i <= message.Length - 1; i++)
                        {
                            GV.MK_Data_Send[cmb_MacroButtonName.SelectedIndex - 1, i] = message[i];
                        }
                    }
                }

                //Catia shortcut
                if (cmb_Predfined_Shortcut.SelectedIndex == 2)
                {

                    if (cmb_MacroButtonName.SelectedIndex == 0)
                    {
                        System.Windows.MessageBox.Show("Select MK Button");
                    }

                    else
                    {
                        byte[] bytes;
                        bytes = Encoding.UTF8.GetBytes("Catia:" + cmb_Predefined_Shortcut_List.SelectedItem.ToString());

                        byte[] message = new byte[64];
                        message = FC.Create_CAD_Message(bytes, cmb_MacroButtonName.SelectedIndex);

                        FC.Send_Device(message, message.Length);

                        Console.WriteLine("[{0}]", string.Join(", ", message));
                        Console.WriteLine(System.Text.Encoding.UTF8.GetString(bytes));

                        for (int i = 0; i <= message.Length - 1; i++)
                        {
                            GV.MK_Data_Send[cmb_MacroButtonName.SelectedIndex - 1, i] = message[i];
                        }
                    }
                }
            }

            Page_Loaded(null,null);
        }

        public void Page_Loaded(object sender, RoutedEventArgs e)
        {
            btn_setmacroShortcut_Click(sender, e); 

            byte[] temp = new byte[64];
            
            List < Button_Values_Struct > MK_Buttons =new List<Button_Values_Struct> { GV.MK1_Values, GV.MK2_Values, GV.MK3_Values, GV.MK4_Values, 
                                                                                       GV.MK5_Values, GV.MK6_Values, GV.MK7_Values, GV.MK8_Values };
            
            List<Button_Values_Struct> NavasButton = new List<Button_Values_Struct> {GV.PitchDown_Values, GV.PitchUp_Values, GV.RollRight_Values,GV.RollLeft_Values, 
                                                                                     GV.YawCCW_Values, GV.YawCW_Values, GV.TransXPos_Values, GV.TransXNeg_Values, 
                                                                                     GV.TransYPos_Values, GV.TransYNeg_Values, GV.TransZPos_Values, GV.TransZNeg_Values};


            foreach (Button_Values_Struct o in MK_Buttons)
            {
                int i = MK_Buttons.IndexOf(o);

                for (int j=0; j<=63; j++)
                {
                    temp[j] = GV.MK_Data_Send[i, j];
                }

                FC.Button_Values(o, temp, active_keys, active_bytes);
                if (o.Text_or_Shortcut == 0)
                {
                    lst_macroKeyValues.Items[i] = KY.MK_Button_Names[i + 1]+ ": " + o.MK_Data_Shortcut_String_All;
                    o.Button_Name=KY.MK_Button_Names[i+1];
                }
                if (o.Text_or_Shortcut == 1)
                {
                    lst_macroKeyValues.Items[i] = KY.MK_Button_Names[i + 1] + ": " + o.MK_Data_Text_String;
                    o.Button_Name = KY.MK_Button_Names[i+1];
                }
            }

            foreach (Button_Values_Struct o in NavasButton)
            {
                int i = NavasButton.IndexOf(o);

                for (int j = 0; j <= 63; j++)
                {
                    temp[j] = GV.NavMove_Data_Send[i, j];
                }

                FC.Button_Values(o, temp, active_keys, active_bytes);
                if (o.Text_or_Shortcut == 0)
                {
                    lst_navasbutton.Items[i] = KY.Navigator_Movement[i+1] + ": " + o.MK_Data_Shortcut_String_All;
                    o.Button_Name = KY.Navigator_Movement[i+1];
                }
                if (o.Text_or_Shortcut == 1)
                {
                    lst_navasbutton.Items[i] = KY.Navigator_Movement[i+1] + ": " + o.MK_Data_Text_String;
                    o.Button_Name = KY.Navigator_Movement[i+1];
                }
            }
        }

        private void cmb_Predfined_Custom_Shortcut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (cmb_Predfined_Custom_Shortcut.SelectedIndex == 0) //Predefined shortcut selected
            {
                dck_button_shortcut_text.Visibility = Visibility.Collapsed;
                stckp_shortcut.Visibility = Visibility.Collapsed;
                cmb_Predfined_Shortcut.Visibility = Visibility.Visible;
                cmb_Predefined_Shortcut_List.Visibility = Visibility.Visible;
                txt_macroPrintText.Visibility = Visibility.Collapsed;
                cmb_shortcut_val1.Visibility = Visibility.Collapsed;
                cmb_shortcut_val2.Visibility = Visibility.Collapsed;
                cmb_shortcut_val3.Visibility = Visibility.Collapsed;
            }

            if(cmb_Predfined_Custom_Shortcut.SelectedIndex == 1) //Custom shortcut selected
            {
                dck_button_shortcut_text.Visibility = Visibility.Visible;
                stckp_shortcut.Visibility = Visibility.Visible;
                cmb_Predfined_Shortcut.Visibility = Visibility.Collapsed;
                cmb_Predefined_Shortcut_List.Visibility = Visibility.Collapsed;
                txt_macroPrintText.Visibility= Visibility.Collapsed;
                cmb_shortcut_val1.Visibility = Visibility.Visible;
                cmb_shortcut_val2.Visibility = Visibility.Visible;
                cmb_shortcut_val3.Visibility = Visibility.Visible;
            }
        }

        private void cmb_Predfined_Shortcut_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            List<string> list = new List<string>();

            if( cmb_Predfined_Shortcut.SelectedIndex == 0)
            {
                foreach(string a in KY.windows_shortcut)
                {
                    list.Add(a);
                }
                
                cmb_Predefined_Shortcut_List.ItemsSource = list;
                text_shortcut = "WindowsShortcut";
            }

            if(cmb_Predfined_Shortcut.SelectedIndex == 1)
            {
                foreach (Action act in PG1.ViewActions_Solidworks)
                {
                    list.Add(act.Method.Name);
                }
                cmb_Predefined_Shortcut_List.ItemsSource=list;
                text_shortcut = "SolidworksShortcut";
            }
            if (cmb_Predfined_Shortcut.SelectedIndex == 2)
            {
                foreach (Action act in PG1.ViewActions_Catia)
                {
                    list.Add(act.Method.Name);
                }
                cmb_Predefined_Shortcut_List.ItemsSource = list;
                text_shortcut = "CatiaShortcut";
            }
        }

        private void cmb_MacroButtonName_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (cmb_MacroButtonName.SelectedIndex != 0)
            {
                cmb_NavMovement.SelectedIndex = 0;
            }
            cmb_MacroButtonName_.SelectedIndex = 0;
            cmb_MacroButtonName_.IsDropDownOpen = false;
        }

        private void cmb_NavMovement_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            
            if (cmb_NavMovement.SelectedIndex != 0)
            {
                cmb_MacroButtonName.SelectedIndex = 0;
            }
            cmb_MacroButtonName_.SelectedIndex = 1;
            cmb_MacroButtonName_.IsDropDownOpen = false;            
        }

        public void btn_NavigationasButton_Click(object sender, RoutedEventArgs e)
        {
            if (btn_NavigationasButton.IsChecked == true)
            {
                SolidColorBrush solidColor = new SolidColorBrush(Colors.LimeGreen);
                IconProperties.SetTagFillMousePressed(btn_NavigationasButton, solidColor);

                btn_NavigationasButton.IsChecked = true;
                cmb_NavMovement.Visibility = Visibility.Visible;
                cmb_NavMovement.SelectedIndex = 0;

                PG1.cmb_deviceMode.IsEnabled = false;
                PG1.cmb_spaceNavProgram.IsEnabled = false;
                PG1.cmb_deviceMode.Opacity = 0.2;
                PG1.cmb_spaceNavProgram.Opacity = 0.2;

                lst_navasbutton.Visibility=Visibility.Visible;

                System.Windows.MessageBox.Show("Save your profile if dont lose navigation as button properties!");
            }
            if (btn_NavigationasButton.IsChecked == false)
            {
                SolidColorBrush solidColor = new SolidColorBrush(Colors.DarkRed);
                IconProperties.SetTagFill(btn_NavigationasButton, solidColor);

                btn_NavigationasButton.IsChecked = false;
                cmb_NavMovement.Visibility = Visibility.Collapsed;
                cmb_MacroButtonName_.SelectedIndex = 0;
                cmb_MacroButtonName.SelectedIndex = 0;

                PG1.cmb_deviceMode.IsEnabled = true;
                PG1.cmb_spaceNavProgram.IsEnabled = true;
                PG1.cmb_deviceMode.Opacity = 1;
                PG1.cmb_spaceNavProgram.Opacity = 1;

                lst_navasbutton.Visibility = Visibility.Collapsed;
            }

        }
    }
}
