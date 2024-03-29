using _Functions;
using Button_Tags;
using catia_api;
using GlobalVariables;
using Newtonsoft.Json;
using SolidWorks.Interop.sldworks;
using Solidworks_Api;
using System;
using System.Data;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Forms;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Threading;
using USB_Communication;
using MessageBox = System.Windows.Forms.MessageBox;
using System.Runtime.InteropServices;
using KeyShortCut;
using SolidWorks.Interop.swconst;
using System.Windows.Input;
using System.Globalization;
using LibUsbDotNet.Main;

namespace EVO_MOTION_D6X8
{
    /// <summary>
    /// Interaction logic for Page_1_Main.xaml
    /// </summary>
    public partial class Page_1_Main : Page
    {
        private DispatcherTimer timer = new DispatcherTimer();

        private DispatcherTimer timer1 = new DispatcherTimer();



        USB_Connection UC = null;
        Global_Variables GV = null;
        Save_Profile_Window SPW = null;
        MainWindow MW = null;
        Functions FC = null;
        KeyCodes KY = new KeyCodes();

        Page_2_Button PG2 = null;

        new KeyCodes.V_Keys_TR Virtual_Keys = new KeyCodes.V_Keys_TR();

        public List<Button_Values_Struct> ListofButtonValues = new List<Button_Values_Struct>();

        //Catia Parameters
        Api_Catia CATApi = new Api_Catia();
        INFITF.Application mycatia = null;
        public bool flag_CATIA = false;

        //Solidworks Parameters
        Api_Solidworks SWApi = new Api_Solidworks();
        SldWorks swApp = null;
        public bool flag_SOLIDWORKS = false;


        public List<Action> ViewActions_Solidworks = new List<Action>(); //Solidworks action list
        public List<Action> ViewActions_Catia = new List<Action>(); //Catia action list

        public readonly BackgroundWorker worker = new BackgroundWorker();

        public byte[] bytesRaw;
        int enc_old_value = 0;
        int enc_old_value_1 = 0;

        ImageBrush ib = new ImageBrush();

        string current_directory = Directory.GetCurrentDirectory();

        public Page_1_Main()
        {
            InitializeComponent();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += Timer_Tick;
            timer1.Tick += Timer1_Tick;

            worker.DoWork += worker_DoWork;
            worker.ProgressChanged += Worker_ProgressChanged;

            worker.WorkerReportsProgress = true;
            worker.WorkerSupportsCancellation = true;

            ViewActions_Solidworks.Add(SWApi.BackView);
            ViewActions_Solidworks.Add(SWApi.IsometricView);
            ViewActions_Solidworks.Add(SWApi.FrontView);
            ViewActions_Solidworks.Add(SWApi.RightView);
            ViewActions_Solidworks.Add(SWApi.LeftView);
            ViewActions_Solidworks.Add(SWApi.TopView);
            ViewActions_Solidworks.Add(SWApi.BottomView);
            ViewActions_Solidworks.Add(SWApi.ZoomtoFit);

            ViewActions_Catia.Add(CATApi.BackView);
            ViewActions_Catia.Add(CATApi.IsometricView);
            ViewActions_Catia.Add(CATApi.FrontView);
            ViewActions_Catia.Add(CATApi.RightView);
            ViewActions_Catia.Add(CATApi.LeftView);
            ViewActions_Catia.Add(CATApi.TopView);
            ViewActions_Catia.Add(CATApi.BottomView);
        }

        #region Gets
        internal void GetPage2(Page_2_Button pg)
        {
            PG2 = pg;
        }

        internal void GetDevice(USB_Connection uc)
        {
            UC = uc;
        }

        internal void GetGlobalVariables(Global_Variables gv)
        {
            GV = gv;

            ListofButtonValues.Add(GV.MK1_Values);
            ListofButtonValues.Add(GV.MK2_Values);
            ListofButtonValues.Add(GV.MK3_Values);
            ListofButtonValues.Add(GV.MK4_Values);
            ListofButtonValues.Add(GV.MK5_Values);
            ListofButtonValues.Add(GV.MK6_Values);
            ListofButtonValues.Add(GV.MK7_Values);
            ListofButtonValues.Add(GV.MK8_Values);
        }

        public void GetSaveWindow(Save_Profile_Window spw)
        {
            SPW = spw;
        }

        public void GetMainWindow(MainWindow mainWindow)
        {
            MW = mainWindow;
        }

        internal void GetFunctions(Functions fc)
        {
            FC = fc;
        }
        #endregion

        #region Worker Operations

        bool flag_picture_pitchup = true;
        bool flag_picture_pitchdown = true;
        bool flag_picture_rollright = true;
        bool flag_picture_rollleft = true;
        bool flag_picture_MoveXpos = true;
        bool flag_picture_MoveXneg = true;
        bool flag_picture_MoveYpos = true;
        bool flag_picture_MoveYneg = true;
        bool flag_picture_MoveZpos = true;
        bool flag_picture_MoveZneg = true;
        bool flag_picture_TurnCW = true;
        bool flag_picture_TurnCCW = true;

        bool flag_navigation_pitchup = true;
        bool flag_navigation_pitchdown = true;
        bool flag_navigation_rollright = true;
        bool flag_navigation_rollleft = true;
        bool flag_navigation_MoveXpos = true;
        bool flag_navigation_MoveXneg = true;
        bool flag_navigation_MoveYpos = true;
        bool flag_navigation_MoveYneg = true;
        bool flag_navigation_MoveZpos = true;
        bool flag_navigation_MoveZneg = true;
        bool flag_navigation_TurnCW = true;
        bool flag_navigation_TurnCCW = true;

        void Navigation_Value_Send(bool PictureFlagName, string PictureName, bool NavigationFlagName, float Navigation, Button_Values_Struct NavigationStruct, List<string> ActiveKeys)
        {
            if (PictureFlagName == true)
            {
                PictureFlagName = false;


                ib.ImageSource = new BitmapImage(new Uri(current_directory + "\\pictures\\" + PictureName + ".png"));
                SpaceNavImage.Background = ib;
            }

            if (PG2.btn_NavigationasButton.IsChecked == true && Navigation > 500)
            {
                if (NavigationFlagName == true)
                {
                    NavigationFlagName = false;

                    System.Windows.Forms.SendKeys.SendWait(FC.NavigationAsButtonString(NavigationStruct, ActiveKeys));
                }

                if (PG2.btn_NavigationasButton.IsChecked == true && GV.tilt > 500)
                {
                    if (NavigationFlagName == true)
                    {
                        NavigationFlagName = false;
                        System.Windows.Forms.SendKeys.SendWait(FC.NavigationAsButtonString(NavigationStruct, ActiveKeys));
                    }

                    if (timer1_count >= 250)
                    {
                        if (NavigationStruct.Text_or_Shortcut == 0)
                        {
                            keybd_event(KY.VKeys_trq[NavigationStruct.cmb_val1], 0, 0, 0);
                            keybd_event(KY.VKeys_trq[NavigationStruct.cmb_val2], 0, 0, 0);
                            keybd_event(KY.VKeys_trq[NavigationStruct.cmb_val3], 0, 0, 0);
                        }
                        else
                        {

                        }


                    }
                }
            }

        }

        [DllImport("user32.dll")]
        static extern void keybd_event(byte bVk, byte bScan, uint dwFlags, int dwExtraInfo);

        [DllImport("user32.dll")] 
        static extern short VkKeyScan(char ch);

        public void Worker_ProgressChanged(object sender, ProgressChangedEventArgs e)
        {
            timer1_count++;

            int hallsensor1 = UC.raw_values[0];
            int hallsensor2 = UC.raw_values[1];
            int hallsensor3 = UC.raw_values[6];
            GV.tilt = UC.raw_values[2];
            GV.roll = UC.raw_values[3];
            int tempretatue_raw = UC.raw_values[4];
            int encoder = UC.raw_values[5];

            Tilt_Raw.Content = GV.tilt.ToString();
            Roll_Raw.Content = GV.roll.ToString();

            Hall_Sensor_1.Content = hallsensor1;
            Hall_Sensor_2.Content = hallsensor2;
            Hall_Sensor_3.Content = hallsensor3;

            float temp = (float)(((Int16)tempretatue_raw / (float)340.0) + (float)36.53);
            lbl_tempretaureValue.Content = (int)temp + " Deg";

            if (temp < 22 && temp > 15)
            {
                lbl_tempretaureValue.Foreground = System.Windows.Media.Brushes.Blue;
            }
            if (temp < 30 && temp >= 22)
            {
                lbl_tempretaureValue.Foreground = System.Windows.Media.Brushes.Orange;
            }
            if (temp >= 30)
            {
                lbl_tempretaureValue.Foreground = System.Windows.Media.Brushes.Red;
            }

            float tilt_map = UC.Map((float)GV.tilt, -2500, 2500, -1, 1);
            float roll_map = UC.Map((float)GV.roll, -2500, 2500, -1, 1);
            float offset = (float)0.08;

            float hallsensor_1 = UC.Map((float)hallsensor1, -400, 400, (float)-0.01, (float)0.01);
            float hallsensor_2 = UC.Map((float)hallsensor2, -400, 400, (float)-0.01, (float)0.01);
            float hallsensor_3 = UC.Map((float)hallsensor3, -400, 400, (float)-0.01, (float)0.01);

            float offset_hallsensor = (float)0.003;

            #region Extra Calculation

            if (tilt_map > offset || tilt_map < -offset || roll_map > offset || roll_map < -offset)
            {
                Tilt_Map.Content = (tilt_map).ToString();
                Roll_Map.Content = (roll_map).ToString();
            }

            else
            {
                Tilt_Map.Content = "0";
                Roll_Map.Content = "0";
            }

            if ((hallsensor_1 < offset_hallsensor && hallsensor_3 > offset_hallsensor) ||
                (hallsensor_1 > offset_hallsensor && hallsensor_3 < offset_hallsensor) ||
                (hallsensor_2 > offset_hallsensor) || (hallsensor_2 < offset_hallsensor))
            {
                GV.move_X = hallsensor_3 - hallsensor_1;
                GV.move_Y = hallsensor_2;
                Move_X.Content = GV.move_X;
                Move_Y.Content = GV.move_Y;
            }

            else
            {
                Move_X.Content = "0";
                Move_Y.Content = "0";
            }

            if ((hallsensor1 > 0 && hallsensor3 > 0 && hallsensor2 > 0) || (hallsensor1 < 0 && hallsensor3 < 0 && hallsensor2 < 0))
            {

                GV.turn = (float)(hallsensor1 + hallsensor3 + hallsensor2) / 10000;
                Turn.Content = GV.turn;
                GV.move_X = 0;
                GV.move_Y = 0;
                Move_X.Content = 0;
                Move_Y.Content = 0;
            }

            else
            {
                Turn.Content = "0";
                GV.turn = 0;
            }

            if (encoder - enc_old_value > 0)
            {
                Encoder_Rot.Content = "CW";
            }

            if (encoder - enc_old_value < 0)
            {
                Encoder_Rot.Content = "CCW";
            }

            enc_old_value = encoder;
            Encoder_Val.Content = enc_old_value;
            #endregion

            #region Navigation Movements
            bool shift_flag = false;
            bool ctrl_flag = false;
            bool alt_flag = false;


            if (GV.tilt > 200) //Pitch Up
            {

                //Navigation_Value_Send(flag_picture_pitchup, "Picth_Up", flag_navigation_pitchup, GV.tilt, GV.PitchUp_Values, PG2.active_keys);

                if (flag_picture_pitchup == true)
                {
                    flag_picture_pitchup = false;

                    ib.ImageSource = new BitmapImage(new Uri(current_directory + "\\pictures\\Picth_Up.png"));
                    SpaceNavImage.Background = ib;
                }

                if (PG2.btn_NavigationasButton.IsChecked == true && GV.tilt > 500)
                {
                    if (flag_navigation_pitchup == true)
                    {
                        flag_navigation_pitchup = false;
                        //System.Windows.Forms.SendKeys.SendWait(FC.NavigationAsButtonString(GV.PitchUp_Values, PG2.active_keys));
                        if(GV.PitchUp_Values.Modifier!=0)
                        {
                            foreach(var a in GV.PitchUp_Values.MK_Data_Shortcut_String)
                            {
                                if (a.Contains("Shift")==true) { shift_flag = true; keybd_event(0xA0, 0, 0, 0); }
                                if (a.Contains("Control") == true) { ctrl_flag = true; keybd_event(0xA2, 0, 0, 0); }
                                if (a.Contains("Alt") == true) { alt_flag = true; keybd_event(0xA4, 0, 0, 0); }
                            }

                            string key1 = GV.PitchUp_Values.MK_Data_Shortcut_String[1];
                            string key2 = GV.PitchUp_Values.MK_Data_Shortcut_String[2];

                            //if(key1.Contains("LETTER")==true | key1.Contains("NUM")==true | key2.Contains("LETTER") == true | key2.Contains("NUM")==true)
                            //{
                                int key1_1 = VkKeyScan(Convert.ToChar(key1.Substring(key1.IndexOf("_") + 1).ToLower()));
                                int key2_2 = VkKeyScan(Convert.ToChar(key2.Substring(key2.IndexOf("_") + 1).ToLower()));
                                keybd_event((byte)key1_1, 0, 0, 0);
                                keybd_event((byte)key2_2, 0, 0, 0);
                                keybd_event((byte)key1_1, 0, 0x0002, 0);
                                keybd_event((byte)key2_2, 0, 0x0002, 0);
                            //}

                            keybd_event(0xA0, 0, 0x0002, 0);
                            keybd_event(0xA2, 0, 0x0002, 0);
                            keybd_event(0xA4, 0, 0x0002, 0);
                            

                        }

                        else
                        {
                            string key1 = GV.PitchUp_Values.MK_Data_Shortcut_String[0];
                            string key2 = GV.PitchUp_Values.MK_Data_Shortcut_String[1];
                            string key3 = GV.PitchUp_Values.MK_Data_Shortcut_String[2];

                            //if (key1.Contains("LETTER") == true | key1.Contains("NUM") == true | key2.Contains("LETTER") == true | key2.Contains("NUM") == true)
                            //{
                            //    int key1_1 = VkKeyScan(Convert.ToChar(key1.Substring(key1.IndexOf("_") + 1).ToLower()));
                            //int key2_2 = VkKeyScan(Convert.ToChar(key2.Substring(key2.IndexOf("_") + 1).ToLower()));
                            //int key3_3 = VkKeyScan(Convert.ToChar(key3.Substring(key3.IndexOf("_") + 1).ToLower()));
                            //keybd_event((byte)key1_1, 0, 0, 0);
                            //keybd_event((byte)key2_2, 0, 0, 0);
                            //keybd_event((byte)key3_3, 0, 0, 0);
                            //keybd_event((byte)key1_1, 0, 0x0002, 0);
                            //keybd_event((byte)key2_2, 0, 0x0002, 0);
                            //keybd_event((byte)key3_3, 0, 0x0002, 0);

                            //}

                        }

                    }

                    if (timer1_count >= 250)
                    {
                        
                    }
                }
            }

            else if (GV.tilt < -200) //Pitch Down
            {
                //Navigation_Value_Send(flag_picture_pitchdown, "Picth_Down", flag_navigation_pitchdown, GV.tilt, GV.PitchDown_Values, PG2.active_keys);

                if (flag_picture_pitchdown == true)
                {
                    flag_picture_pitchdown = false;

                    ib.ImageSource = new BitmapImage(new Uri(current_directory + "\\pictures\\Picth_Down.png"));
                    SpaceNavImage.Background = ib;
                }

                if (PG2.btn_NavigationasButton.IsChecked == true && GV.tilt < -500)
                {
                    if (flag_navigation_pitchdown == true)
                    {
                        flag_navigation_pitchdown = false;
                        System.Windows.Forms.SendKeys.SendWait(FC.NavigationAsButtonString(GV.PitchDown_Values, PG2.active_keys));
                    }

                    if (timer1_count >= 250)
                    {

                        keybd_event(KY.VKeys_trq[GV.PitchDown_Values.cmb_val1], 0, 0, 0);
                        keybd_event(KY.VKeys_trq[GV.PitchDown_Values.cmb_val2], 0, 0, 0);
                        keybd_event(KY.VKeys_trq[GV.PitchDown_Values.cmb_val3], 0, 0, 0);

                        
                    }
                }
            }

            else if (GV.roll > 200) //Roll Right
            {
                //Navigation_Value_Send(flag_picture_rollright, "Roll_Right", flag_navigation_rollright, GV.roll, GV.RollRight_Values, PG2.active_keys);

                if (flag_picture_rollright == true)
                {
                    flag_picture_rollright = false;

                    ib.ImageSource = new BitmapImage(new Uri(current_directory + "\\pictures\\Roll_Right.png"));
                    SpaceNavImage.Background = ib;
                }

                if (PG2.btn_NavigationasButton.IsChecked == true && GV.roll > 500)
                {
                    if (flag_navigation_rollright == true)
                    {
                        flag_navigation_rollright = false;
                        System.Windows.Forms.SendKeys.SendWait(FC.NavigationAsButtonString(GV.RollRight_Values, PG2.active_keys));
                    }

                    if (timer1_count >= 250)
                    {
                        keybd_event(KY.VKeys_trq[GV.RollRight_Values.cmb_val1], 0, 0, 0);
                        keybd_event(KY.VKeys_trq[GV.RollRight_Values.cmb_val2], 0, 0, 0);
                        keybd_event(KY.VKeys_trq[GV.RollRight_Values.cmb_val3], 0, 0, 0);
                    }
                }
            }

            else if (GV.roll < -200) //Roll Left
            {
                //Navigation_Value_Send(flag_picture_rollleft, "Roll_Left", flag_navigation_rollleft, GV.roll, GV.RollLeft_Values, PG2.active_keys);

                if (flag_picture_rollleft == true)
                {
                    flag_picture_rollleft = false;

                    ib.ImageSource = new BitmapImage(new Uri(current_directory + "\\pictures\\Roll_Left.png"));
                    SpaceNavImage.Background = ib;
                }

                if (PG2.btn_NavigationasButton.IsChecked == true && GV.roll < -500)
                {
                    if (flag_navigation_rollleft == true)
                    {
                        //flag_navigation_rollleft = false;
                        System.Windows.Forms.SendKeys.SendWait(FC.NavigationAsButtonString(GV.RollLeft_Values, PG2.active_keys));
                    }

                    if (timer1_count >= 250)
                    {
                        keybd_event(KY.VKeys_trq[GV.RollLeft_Values.cmb_val1], 0, 0, 0);
                        keybd_event(KY.VKeys_trq[GV.RollLeft_Values.cmb_val2], 0, 0, 0);
                        keybd_event(KY.VKeys_trq[GV.RollLeft_Values.cmb_val3], 0, 0, 0);
                    }
                }
            }
            else if (GV.move_X > 0.001) //Move Positive X
            {
                //Navigation_Value_Send(flag_picture_MoveXpos, "Move_+X", flag_navigation_MoveXpos, GV.move_X, GV.TransXPos_Values, PG2.active_keys);

                if (flag_picture_MoveXpos == true)
                {
                    flag_picture_MoveXpos = false;

                    ib.ImageSource = new BitmapImage(new Uri(current_directory + "\\pictures\\Move_+X.png"));
                    SpaceNavImage.Background = ib;
                }

                if (PG2.btn_NavigationasButton.IsChecked == true && GV.move_X > 0.001)
                {
                    if (flag_navigation_MoveXpos == true)
                    {
                        flag_navigation_MoveXpos = false;
                        System.Windows.Forms.SendKeys.SendWait(FC.NavigationAsButtonString(GV.TransXPos_Values, PG2.active_keys));
                    }

                    if (timer1_count >= 250)
                    {
                        keybd_event(KY.VKeys_trq[GV.TransXPos_Values.cmb_val1], 0, 0, 0);
                        keybd_event(KY.VKeys_trq[GV.TransXPos_Values.cmb_val2], 0, 0, 0);
                        keybd_event(KY.VKeys_trq[GV.TransXPos_Values.cmb_val3], 0, 0, 0);
                    }
                }
            }

            else if (GV.move_X < -0.001) //Move Negative X
            {
                //Navigation_Value_Send(flag_picture_MoveXneg, "Move_-X", flag_navigation_MoveXneg, GV.move_X, GV.TransXNeg_Values, PG2.active_keys);

                if (flag_picture_MoveXneg == true)
                {
                    flag_picture_MoveXneg = false;

                    ib.ImageSource = new BitmapImage(new Uri(current_directory + "\\pictures\\Move_-X.png"));
                    SpaceNavImage.Background = ib;
                }

                if (PG2.btn_NavigationasButton.IsChecked == true && GV.move_X < -0.001)
                {
                    if (flag_navigation_MoveXneg == true)
                    {
                        flag_navigation_MoveXneg = false;
                        System.Windows.Forms.SendKeys.SendWait(FC.NavigationAsButtonString(GV.TransXNeg_Values, PG2.active_keys));
                    }

                    if (timer1_count >= 250)
                    {
                        keybd_event(KY.VKeys_trq[GV.TransXNeg_Values.cmb_val1], 0, 0, 0);
                        keybd_event(KY.VKeys_trq[GV.TransXNeg_Values.cmb_val2], 0, 0, 0);
                        keybd_event(KY.VKeys_trq[GV.TransXNeg_Values.cmb_val3], 0, 0, 0);
                    }
                }
            }

            else if (GV.move_Y > 0.001) //Move Positive Y
            {
                //Navigation_Value_Send(flag_picture_MoveYpos, "Move_+Y", flag_navigation_MoveYpos, GV.move_Y, GV.TransYPos_Values, PG2.active_keys);

                if (flag_picture_MoveYpos == true)
                {
                    flag_picture_MoveYpos = false;

                    ib.ImageSource = new BitmapImage(new Uri(current_directory + "\\pictures\\Move_+Y.png"));
                    SpaceNavImage.Background = ib;
                }

                if (PG2.btn_NavigationasButton.IsChecked == true && GV.move_Y > 0.001)
                {
                    if (flag_navigation_MoveYpos == true)
                    {
                        flag_navigation_MoveYpos = false;
                        System.Windows.Forms.SendKeys.SendWait(FC.NavigationAsButtonString(GV.TransYPos_Values, PG2.active_keys));
                    }

                    if (timer1_count >= 250)
                    {
                        keybd_event(KY.VKeys_trq[GV.TransYPos_Values.cmb_val1], 0, 0, 0);
                        keybd_event(KY.VKeys_trq[GV.TransYPos_Values.cmb_val2], 0, 0, 0);
                        keybd_event(KY.VKeys_trq[GV.TransYPos_Values.cmb_val3], 0, 0, 0);
                    }
                }
            }

            else if (GV.move_Y < -0.001) //Move Negative Y
            {
                //Navigation_Value_Send(flag_picture_MoveYneg, "Move_-Y", flag_navigation_MoveYneg, GV.move_Y, GV.TransYNeg_Values, PG2.active_keys);

                if (flag_picture_MoveYneg == true)
                {
                    flag_picture_MoveYneg = false;

                    ib.ImageSource = new BitmapImage(new Uri(current_directory + "\\pictures\\Move_-Y.png"));
                    SpaceNavImage.Background = ib;
                }

                if (PG2.btn_NavigationasButton.IsChecked == true && GV.move_Y < -0.001)
                {
                    if (flag_navigation_MoveYneg == true)
                    {
                        flag_navigation_MoveYneg = false;
                        System.Windows.Forms.SendKeys.SendWait(FC.NavigationAsButtonString(GV.TransYNeg_Values, PG2.active_keys));
                    }

                    if (timer1_count >= 250)
                    {
                        keybd_event(KY.VKeys_trq[GV.TransYNeg_Values.cmb_val1], 0, 0, 0);
                        keybd_event(KY.VKeys_trq[GV.TransYNeg_Values.cmb_val2], 0, 0, 0);
                        keybd_event(KY.VKeys_trq[GV.TransYNeg_Values.cmb_val3], 0, 0, 0);
                    }
                }
            }

            else if (GV.turn > 0.001) //Turn CW
            {
                //Navigation_Value_Send(flag_picture_TurnCW, "Yaw_CW", flag_navigation_TurnCW, GV.turn, GV.YawCW_Values, PG2.active_keys);

                if (flag_picture_TurnCW == true)
                {
                    flag_picture_TurnCW = false;

                    ib.ImageSource = new BitmapImage(new Uri(current_directory + "\\pictures\\Yaw_CW.png"));
                    SpaceNavImage.Background = ib;
                }

                if (PG2.btn_NavigationasButton.IsChecked == true && GV.turn > 500)
                {
                    if (flag_navigation_TurnCW == true)
                    {
                        flag_navigation_TurnCW = false;
                        System.Windows.Forms.SendKeys.SendWait(FC.NavigationAsButtonString(GV.YawCW_Values, PG2.active_keys));
                    }

                    if (timer1_count >= 250)
                    {
                        keybd_event(KY.VKeys_trq[GV.YawCW_Values.cmb_val1], 0, 0, 0);
                        keybd_event(KY.VKeys_trq[GV.YawCW_Values.cmb_val2], 0, 0, 0);
                        keybd_event(KY.VKeys_trq[GV.YawCW_Values.cmb_val3], 0, 0, 0);
                    }
                }
            }

            else if (GV.turn < -0.001) //Turn CCW
            {
                //Navigation_Value_Send(flag_picture_TurnCCW, "Yaw_CCW", flag_navigation_TurnCCW, GV.turn, GV.YawCCW_Values, PG2.active_keys);

                if (flag_picture_TurnCCW == true)
                {
                    flag_picture_TurnCCW = false;

                    ib.ImageSource = new BitmapImage(new Uri(current_directory + "\\pictures\\Yaw_CCW.png"));
                    SpaceNavImage.Background = ib;
                }

                if (PG2.btn_NavigationasButton.IsChecked == true && GV.turn > 500)
                {
                    if (flag_navigation_TurnCCW == true)
                    {
                        flag_navigation_TurnCCW = false;
                        System.Windows.Forms.SendKeys.SendWait(FC.NavigationAsButtonString(GV.YawCCW_Values, PG2.active_keys));
                    }

                    if (timer1_count >= 250)
                    {
                        keybd_event(KY.VKeys_trq[GV.YawCCW_Values.cmb_val1], 0, 0, 0);
                        keybd_event(KY.VKeys_trq[GV.YawCCW_Values.cmb_val2], 0, 0, 0);
                        keybd_event(KY.VKeys_trq[GV.YawCCW_Values.cmb_val3], 0, 0, 0);
                    }
                }
            }

            else
            {
                ib.ImageSource = new BitmapImage(new Uri(current_directory + "\\pictures\\colorwhell\\colorwhell.png"));
                SpaceNavImage.Background = ib;
                flag_picture_pitchup = true;
                flag_picture_pitchdown = true;
                flag_picture_rollright = true;
                flag_picture_rollleft = true;
                flag_picture_MoveXpos = true;
                flag_picture_MoveXneg = true;
                flag_picture_MoveYpos = true;
                flag_picture_MoveYneg = true;
                flag_picture_MoveZpos = true;
                flag_picture_MoveZneg = true;
                flag_picture_TurnCW = true;
                flag_picture_TurnCCW = true;

                flag_navigation_pitchup = true;
                flag_navigation_pitchdown = true;
                flag_navigation_rollright = true;
                flag_navigation_rollleft = true;
                flag_navigation_MoveXpos = true;
                flag_navigation_MoveXneg = true;
                flag_navigation_MoveYpos = true;
                flag_navigation_MoveYneg = true;
                flag_navigation_MoveZpos = true;
                flag_navigation_MoveZneg = true;
                flag_navigation_TurnCW = true;
                flag_navigation_TurnCCW = true;

                timer1_count = 0;
            }

            #endregion

            #region Solidworks Actions

            if (flag_SOLIDWORKS == true)
            {

                if (Math.Abs(tilt_map) > offset || Math.Abs(roll_map) > offset)
                {
                    SWApi.RotateAboutCenter((tilt_map - offset) / 5, (roll_map - offset) / 5);
                }

                if (Math.Abs(GV.move_X) > offset_hallsensor || Math.Abs(GV.move_Y) > offset_hallsensor)
                {
                    SWApi.TranslateModel(GV.move_Y, GV.move_X);
                }

                if (Math.Abs(GV.turn) > 0.02)
                {
                    SWApi.RollModel(GV.turn);
                }

                if (encoder - enc_old_value_1 > 0)
                {
                    if (flag_SOLIDWORKS == true)
                    {
                        SWApi.ZoombyFactor(1.05);
                    }
                }

                if (encoder - enc_old_value_1 < 0)
                {

                    if (flag_SOLIDWORKS == true)
                    {
                        SWApi.ZoombyFactor(0.95);
                    }
                }
                enc_old_value_1 = encoder;

            }
            #endregion

            #region Catia Actions

            if (flag_CATIA == true)
            {
                if (tilt_map > offset || tilt_map < -offset || roll_map > offset || roll_map < -offset)
                {
                    object[] abc = { 0, 0, 0 };

                    if (tilt_map > offset || tilt_map < -offset)
                    {
                        abc[0] = 1;
                        CATApi.RotateModel(abc, tilt_map);
                    }
                    if (roll_map > offset || roll_map < -offset)
                    {
                        abc[1] = 1;
                        CATApi.RotateModel(abc, roll_map);
                    }
                }

                if (Math.Abs(GV.move_X) > offset_hallsensor || Math.Abs(GV.move_Y) > offset_hallsensor)
                {
                    object[] translatevector = { GV.move_X, GV.move_Y };
                    CATApi.TranslateModel(translatevector);
                }

                if (Math.Abs(GV.turn) > 0.02)
                {


                }

                if (bytesRaw[1] != 25)
                {
                    int PressedButton = bytesRaw[1];

                    int seperator = ListofButtonValues[PressedButton - 1].MK_Data_Text_String.IndexOf(":");
                    //Console.WriteLine(seperator);

                    string CADSoftware = ListofButtonValues[PressedButton - 1].MK_Data_Text_String.Substring(0, seperator);
                    //Console.WriteLine(CADSoftware);

                    string command = ListofButtonValues[PressedButton - 1].MK_Data_Text_String.Substring(seperator + 1);
                    //Console.WriteLine(command);

                    if (CADSoftware == "Catia")
                    {
                        for (int i = 0; i <= ViewActions_Catia.Count - 1; i++)
                        {
                            if (ViewActions_Catia[i].Method.Name == ListofButtonValues[PressedButton - 1].MK_Data_Text_String)
                            {
                                ViewActions_Catia[i].Invoke();
                            }
                        }
                    }


                }
            }
            #endregion

        }



        public void worker_DoWork(object sender, DoWorkEventArgs e)
        {
            while (!worker.CancellationPending)
            {
                if (worker.CancellationPending == true)
                {
                    e.Cancel = true;
                }
                else
                {
                    bytesRaw = UC.USB_Read(UC.device);

                    try
                    {
                        int hallsensor_1 = (Int32)(bytesRaw[4] + (bytesRaw[5] << 8) + (bytesRaw[6] << 16) + (bytesRaw[7] << 24));
                        int hallsensor_2 = (Int32)(bytesRaw[8] + (bytesRaw[9] << 8) + (bytesRaw[10] << 16) + (bytesRaw[11] << 24));
                        int hallsensor_3 = (Int32)(bytesRaw[19] + (bytesRaw[20] << 8) + (bytesRaw[21] << 16) + (bytesRaw[22] << 24));
                        UC.raw_values[0] = hallsensor_1;
                        UC.raw_values[1] = hallsensor_2;
                        UC.raw_values[6] = hallsensor_3;

                        int tilt = (Int16)(bytesRaw[12] + (bytesRaw[13] << 8));
                        int roll = (Int16)(bytesRaw[14] + (bytesRaw[15] << 8));
                        UC.raw_values[2] = tilt;
                        UC.raw_values[3] = roll;

                        int temperature = (Int16)(bytesRaw[16] + (bytesRaw[17] << 8));
                        UC.raw_values[4] = temperature;

                        int encoder = (sbyte)bytesRaw[18];
                        UC.raw_values[5] = encoder;

                        for (int i = 0; i < 6; i++)
                        {
                            worker.ReportProgress(i, UC.raw_values[i]);
                        }

                        if (flag_SOLIDWORKS == true)
                        {
                            flag_SOLIDWORKS = false;

                            if (bytesRaw[1] != 25)
                            {
                                int PressedButton = bytesRaw[1];

                                int seperator = ListofButtonValues[PressedButton - 1].MK_Data_Text_String.IndexOf(":");
                                Console.WriteLine(seperator);

                                string CADSoftware = ListofButtonValues[PressedButton - 1].MK_Data_Text_String.Substring(0, seperator);
                                Console.WriteLine(CADSoftware);

                                string command = ListofButtonValues[PressedButton - 1].MK_Data_Text_String.Substring(seperator + 1);
                                Console.WriteLine(command);

                                if (CADSoftware == "Solidworks")
                                {
                                    for (int i = 0; i <= ViewActions_Solidworks.Count - 1; i++)
                                    {
                                        if (ViewActions_Solidworks[i].Method.Name == command)
                                        {
                                            ViewActions_Solidworks[i].Invoke();
                                        }
                                    }
                                }
                            }
                            flag_SOLIDWORKS = true;
                        }
                    }
                    catch (Exception ex)
                    {
                        //System.Windows.Forms.MessageBox.Show(ex.Message);
                    }
                }
            }
        }

        #endregion

        #region Timer Operations

        int timerr = 0;
        int timer1_count = 0;

        private void Timer1_Tick(object sender, EventArgs e)
        {
            timer1_count++;
        }

        private void Timer_Tick(object sender, EventArgs e)
        {
            timerr++;

            int minute = timerr / 60;
            int second = timerr % 60;
            int hour = timerr / 3600;
            lbl_timer.Content = hour.ToString("D2") + ":" + minute.ToString("D2") + ":" + second.ToString("D2");

        }

        private void btn_toggleStartTimer_Checked(object sender, RoutedEventArgs e)
        {
            timer.Start();
        }

        private void btn_toggleStartTimer_Unchecked(object sender, RoutedEventArgs e)
        {
            timer.Stop();
        }

        private void btn_timer_restart_Click(object sender, RoutedEventArgs e)
        {
            if (timerr > 0)
            {
                timer.Stop();
                timerr = 0;
                lbl_timer.Content = "00:00:00";
                timer.Start();
            }

        }

        private void btn_timer_stop_Click(object sender, RoutedEventArgs e)
        {
            timer.Stop();
            timerr = 0;
            lbl_timer.Content = "00:00:00";
            btn_timer_start.IsChecked = false;
        }
        #endregion

        private void Page_Loaded(object sender, RoutedEventArgs e)
        {

            cmb_deviceMode.SelectedIndex = GV.DeviceMode;
            PG2.btn_NavigationasButton_Click(sender, e);
            if (PG2.btn_NavigationasButton.IsChecked == true)
            {
                SolidColorBrush solidColor = new SolidColorBrush(Colors.LimeGreen);
                IconProperties.SetTagFill(btn_NavigationasButton_1, solidColor);

            }
            else
            {
                SolidColorBrush solidColor = new SolidColorBrush(Colors.DarkRed);
                IconProperties.SetTagFill(btn_NavigationasButton_1, solidColor);
            }
            try
            {
                if (worker.IsBusy == true)
                {

                }
                else
                {
                    worker.RunWorkerAsync();
                }
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show("bok2" + ex.Message);
            }

        }

        private void cmb_deviceMode_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_deviceMode.SelectedValue.ToString() == "Mouse")
            {
                cmb_spaceNavProgram.Visibility = Visibility.Collapsed;
                try
                {
                    FC.Change_Device_Mode(4);
                    flag_SOLIDWORKS = false;
                    GV.CADSoftware = 0;
                    GV.CADSoftware_Name = null;
                }

                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
                }

            }

            if (cmb_deviceMode.SelectedValue.ToString() == "SpaceNav")
            {
                cmb_spaceNavProgram.Visibility = Visibility.Visible;
                cmb_spaceNavProgram.SelectedIndex = 0;

                try
                {
                    FC.Change_Device_Mode(1);
                }

                catch (Exception ex)
                {
                    System.Windows.Forms.MessageBox.Show(ex.Message.ToString());
                }

                /*
                 * -Check selected CAD software is working
                 *      -If CAD software is not working ask for run 
                 *          -If say yes run the CAD program
                 *          -If say no break
                 *      -If CAD software is working show messagebox for selected CAD software
                 * -Run the code for selected CAD software
                 */
            }

            else
            {
                cmb_spaceNavProgram.Visibility = Visibility.Collapsed;
            }

            GV.DeviceMode = cmb_deviceMode.SelectedIndex;
            GV.DeviceMode_Name = cmb_deviceMode.SelectedValue.ToString();

        }

        private void cmb_spaceNavProgram_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            GV.CADSoftware = cmb_spaceNavProgram.SelectedIndex;
            GV.CADSoftware_Name = cmb_spaceNavProgram.SelectedValue.ToString();

            if (cmb_spaceNavProgram.SelectedIndex == 1) //Select Solidworks
            {
                if (flag_SOLIDWORKS == false)
                {
                    try
                    {
                        swApp = (SldWorks)System.Runtime.InteropServices.Marshal.GetActiveObject("SldWorks.Application");
                        System.Windows.Forms.MessageBox.Show("Solidworks is open");
                        SWApi.swApp = swApp;
                        flag_SOLIDWORKS = true;
                        flag_CATIA = false;
                    }
                    catch (Exception)
                    {
                        System.Windows.Forms.MessageBox.Show("Solidworks is not open");
                        cmb_spaceNavProgram.SelectedIndex = 0;
                        flag_SOLIDWORKS = false;
                        flag_CATIA = false;
                    }
                }
                else
                {

                }

            }

            if (cmb_spaceNavProgram.SelectedIndex == 2) //Select CATIA
            {
                if (flag_CATIA == false)
                {
                    try
                    {

                        mycatia = CATApi.GetCatia();
                        System.Windows.Forms.MessageBox.Show("Catia is open");
                        flag_CATIA = true;
                        flag_SOLIDWORKS = false;
                    }
                    catch (Exception)
                    {
                        System.Windows.Forms.MessageBox.Show("Catia is not open");
                        cmb_spaceNavProgram.SelectedIndex = 0;
                        flag_CATIA = false;
                        flag_SOLIDWORKS = false;
                    }
                }
                else
                {

                }


            }

            if (cmb_spaceNavProgram.SelectedIndex == 0)
            {
                if (flag_SOLIDWORKS == true)
                {
                    DialogResult quit;
                    quit = MessageBox.Show("Shutdown solidworks!", "Caution", MessageBoxButtons.YesNo);
                    if (quit == DialogResult.Yes)
                    {
                        swApp.ExitApp();
                        flag_SOLIDWORKS = false;
                    }
                    else
                    {
                        cmb_spaceNavProgram.SelectedIndex = 1;
                    }

                }

                if (flag_CATIA == true)
                {
                    DialogResult quit;
                    quit = MessageBox.Show("Catia solidworks!", "Caution", MessageBoxButtons.YesNo);
                    if (quit == DialogResult.Yes)
                    {
                        mycatia.Quit();
                        flag_CATIA = false;
                    }
                    else
                    {
                        cmb_spaceNavProgram.SelectedIndex = 2;
                    }

                }
            }

        }

        private void btn_Save_Click(object sender, RoutedEventArgs e)
        {

            SPW.GetGlobalVariables(GV);
            SPW.ReadGlobalValues();
            SPW.GetMainWindow(MW);
            SPW.GetFunctions(FC);

            try
            {
                SPW.Show();
                MW.IsEnabled = false;
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

        }

        private void btn_Load_Click(object sender, RoutedEventArgs e)
        {
            string filePath = null;
            string fileName = null;
            string[] jsonArray = null;



            OpenFileDialog file = new OpenFileDialog();



            file.Filter = "User Profile (.usp)|*.usp";
            file.RestoreDirectory = true;
            file.Multiselect = false;

            DialogResult Dialog_result = file.ShowDialog();

            if (Dialog_result == DialogResult.OK)
            {
                filePath = file.FileName;
                fileName = file.SafeFileName;

                lbl_UserProfile.Content = fileName.Substring(0, fileName.IndexOf("."));

                jsonArray = System.IO.File.ReadAllLines(filePath);

                int index_LedProperties = Array.IndexOf(jsonArray, "LED Properties");
                int index_DeviceProperties = Array.IndexOf(jsonArray, "Device Properties");
                int index_ButtonValues = Array.IndexOf(jsonArray, "Button Values");
                int index_NavigationMovements = Array.IndexOf(jsonArray, "Navigation Movement Values");

                if (index_LedProperties != -1)
                {
                    GV.LED_Properties = JsonConvert.DeserializeObject<LED_Properties>(jsonArray[index_LedProperties + 1]);
                    GV.clr_B = GV.LED_Properties.clr_B;
                    GV.clr_G = GV.LED_Properties.clr_G;
                    GV.clr_R = GV.LED_Properties.clr_R;
                    GV.brightness = GV.LED_Properties.brightness;
                    GV.LED_Mode = GV.LED_Properties.led_Mode;
                    FC.write_LED_Connfig(GV.clr_R, GV.clr_G, GV.clr_B, GV.brightness, GV.LED_Mode);
                }
                System.Threading.Thread.Sleep(100);
                if (index_DeviceProperties != -1)
                {
                    GV.Device_Mode_Properties = JsonConvert.DeserializeObject<Device_Mode_Properties>(jsonArray[index_DeviceProperties + 1]);
                    FC.Change_Device_Mode(GV.Device_Mode_Properties.DeviceMode);
                    cmb_deviceMode.SelectedIndex = GV.Device_Mode_Properties.DeviceMode;
                }
                System.Threading.Thread.Sleep(100);
                if (index_ButtonValues != -1)
                {

                    GV.MK1_Values = JsonConvert.DeserializeObject<Button_Values_Struct>(jsonArray[index_ButtonValues + 1]);
                    GV.MK2_Values = JsonConvert.DeserializeObject<Button_Values_Struct>(jsonArray[index_ButtonValues + 2]);
                    GV.MK3_Values = JsonConvert.DeserializeObject<Button_Values_Struct>(jsonArray[index_ButtonValues + 3]);
                    GV.MK4_Values = JsonConvert.DeserializeObject<Button_Values_Struct>(jsonArray[index_ButtonValues + 4]);
                    GV.MK5_Values = JsonConvert.DeserializeObject<Button_Values_Struct>(jsonArray[index_ButtonValues + 5]);
                    GV.MK6_Values = JsonConvert.DeserializeObject<Button_Values_Struct>(jsonArray[index_ButtonValues + 6]);
                    GV.MK7_Values = JsonConvert.DeserializeObject<Button_Values_Struct>(jsonArray[index_ButtonValues + 7]);
                    GV.MK8_Values = JsonConvert.DeserializeObject<Button_Values_Struct>(jsonArray[index_ButtonValues + 8]);

                    for (int j = 0; j < 63; j++)
                    {
                        GV.MK_Data_Send[0, j] = GV.MK1_Values.MK_Data_Send[j];
                    }
                    FC.Send_Device(GV.MK1_Values.MK_Data_Send, GV.MK1_Values.MK_Data_Send.Length);

                    for (int j = 0; j < 63; j++)
                    {
                        GV.MK_Data_Send[1, j] = GV.MK2_Values.MK_Data_Send[j];
                    }
                    FC.Send_Device(GV.MK2_Values.MK_Data_Send, GV.MK2_Values.MK_Data_Send.Length);

                    for (int j = 0; j < 63; j++)
                    {
                        GV.MK_Data_Send[2, j] = GV.MK3_Values.MK_Data_Send[j];
                    }
                    FC.Send_Device(GV.MK3_Values.MK_Data_Send, GV.MK3_Values.MK_Data_Send.Length);

                    for (int j = 0; j < 63; j++)
                    {
                        GV.MK_Data_Send[3, j] = GV.MK4_Values.MK_Data_Send[j];
                    }
                    FC.Send_Device(GV.MK4_Values.MK_Data_Send, GV.MK4_Values.MK_Data_Send.Length);

                    for (int j = 0; j < 63; j++)
                    {
                        GV.MK_Data_Send[4, j] = GV.MK5_Values.MK_Data_Send[j];
                    }
                    FC.Send_Device(GV.MK5_Values.MK_Data_Send, GV.MK5_Values.MK_Data_Send.Length);

                    for (int j = 0; j < 63; j++)
                    {
                        GV.MK_Data_Send[5, j] = GV.MK6_Values.MK_Data_Send[j];
                    }
                    FC.Send_Device(GV.MK6_Values.MK_Data_Send, GV.MK6_Values.MK_Data_Send.Length);

                    for (int j = 0; j < 63; j++)
                    {
                        GV.MK_Data_Send[6, j] = GV.MK7_Values.MK_Data_Send[j];
                    }
                    FC.Send_Device(GV.MK7_Values.MK_Data_Send, GV.MK7_Values.MK_Data_Send.Length);

                    for (int j = 0; j < 63; j++)
                    {
                        GV.MK_Data_Send[7, j] = GV.MK8_Values.MK_Data_Send[j];
                    }
                    FC.Send_Device(GV.MK8_Values.MK_Data_Send, GV.MK8_Values.MK_Data_Send.Length);
                }

                if (index_NavigationMovements != -1)
                {

                    GV.PitchDown_Values = JsonConvert.DeserializeObject<Button_Values_Struct>(jsonArray[index_NavigationMovements + 1]);
                    GV.PitchUp_Values = JsonConvert.DeserializeObject<Button_Values_Struct>(jsonArray[index_NavigationMovements + 2]);
                    GV.RollRight_Values = JsonConvert.DeserializeObject<Button_Values_Struct>(jsonArray[index_NavigationMovements + 3]);
                    GV.RollLeft_Values = JsonConvert.DeserializeObject<Button_Values_Struct>(jsonArray[index_NavigationMovements + 4]);
                    GV.YawCCW_Values = JsonConvert.DeserializeObject<Button_Values_Struct>(jsonArray[index_NavigationMovements + 5]);
                    GV.YawCW_Values = JsonConvert.DeserializeObject<Button_Values_Struct>(jsonArray[index_NavigationMovements + 6]);
                    GV.TransXPos_Values = JsonConvert.DeserializeObject<Button_Values_Struct>(jsonArray[index_NavigationMovements + 7]);
                    GV.TransXNeg_Values = JsonConvert.DeserializeObject<Button_Values_Struct>(jsonArray[index_NavigationMovements + 8]);
                    GV.TransYPos_Values = JsonConvert.DeserializeObject<Button_Values_Struct>(jsonArray[index_NavigationMovements + 9]);
                    GV.TransYNeg_Values = JsonConvert.DeserializeObject<Button_Values_Struct>(jsonArray[index_NavigationMovements + 10]);
                    GV.TransZPos_Values = JsonConvert.DeserializeObject<Button_Values_Struct>(jsonArray[index_NavigationMovements + 11]);
                    GV.TransZNeg_Values = JsonConvert.DeserializeObject<Button_Values_Struct>(jsonArray[index_NavigationMovements + 12]);

                    if (PG2.btn_NavigationasButton.IsChecked == false)
                    {
                        PG2.btn_NavigationasButton.IsChecked = true;
                    }
                    PG2.btn_NavigationasButton_Click(null, null);
                    Page_Loaded(null, null);
                }
                else
                {
                    if (PG2.btn_NavigationasButton.IsChecked == true)
                    {
                        PG2.btn_NavigationasButton.IsChecked = false;
                    }
                    PG2.btn_NavigationasButton_Click(null, null);
                    Page_Loaded(null, null);
                }

            }
            if (Dialog_result == DialogResult.Cancel)
            {
                System.Windows.Forms.MessageBox.Show("user file not selected");
            }

        }

        private void btn_Calibration_Click(object sender, RoutedEventArgs e)
        {
            byte[] message = new byte[11];

            message[0] = 1;                                         // First byte is report id
            message[1] = Convert.ToByte(message.Length);            // Second byte is length of data
            message[2] = Convert.ToByte(255);                       // Calibrate   

            FC.Send_Device(message, message.Length);
        }
    }
}
