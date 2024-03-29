using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using USB_Communication;
using GlobalVariables;
using _Functions;
using System.Windows.Forms;
using System.Security.RightsManagement;
using Microsoft.Win32;
using static System.Net.Mime.MediaTypeNames;
using HidSharp.Reports;
using System.Collections;
using KeyShortCut;
using System.Security.Cryptography;

namespace _Functions
{
    internal class Functions
    {

        USB_Connection UC = null;
        Global_Variables GV = null;
        KeyCodes KY = new KeyCodes();

        internal void GetDevice(USB_Connection uc)
        {
            UC = uc;
        }

        internal void GetGlobalVariables(Global_Variables gv)
        {
            GV = gv;
        }

        internal void GetKeyCodes(KeyCodes ky)
        {
            KY = ky;
        }

        public void Read_Device_Configurations()
        {
            byte[] message = new byte[64];
            message[0] = 1; //First byte is report id
            message[1] = Convert.ToByte(1); //Second byte is length of data
            message[2] = Convert.ToByte(3); //Data type config read data

            Send_Device(message, message.Length);

            byte[] bytesConfig = new byte[UC.device.GetMaxInputReportLength()];

            Read_Device(bytesConfig);

            for(int i=0; i<bytesConfig.Length; i++)
            {
                Console.Write(bytesConfig[i] + "\t");
            }


            if (bytesConfig[1]!=3)
            {
                Read_Device_Configurations();
            }
            else
            {
                GV.clr_R = bytesConfig[2];
                GV.clr_G = bytesConfig[3];
                GV.clr_B = bytesConfig[4];
                GV.LED_Mode = bytesConfig[5];
                GV.DeviceMode = bytesConfig[6];
                GV.brightness = bytesConfig[7];
            }
        }

        public void write_LED_Connfig(int led_r, int led_g, int led_b, int brightness, int led_mode)
        {

            byte[] message = new byte[64];
            message[0] = 1; //First byte is report id
            message[1] = Convert.ToByte(8); //Second byte is length of data
            message[2] = Convert.ToByte(131);
            message[3] = Convert.ToByte(led_r);
            message[4] = Convert.ToByte(led_g);
            message[5] = Convert.ToByte(led_b);
            message[6] = Convert.ToByte((byte)led_mode); //LED Effect
            message[7] = Convert.ToByte(brightness);

            Send_Device(message, message.Length);

            //UC.stream.Close();
        }

        public void Change_Device_Mode(int mode)
        {
            byte[] message = new byte[64];
            message[0] = 1; //First byte is report id
            message[1] = Convert.ToByte(8); //Second byte is length of data
            message[2] = Convert.ToByte(195); //Device mode change
            message[7] = Convert.ToByte(mode); //Device mode 1:Null 2:Null 3: Null 4:Mouse
            Send_Device(message, message.Length);

        }

        /*
         * Button No 1 : Data Type 201
         * Button No 2 : Data Type 202
         * Button No 3 : Data Type 203
         * Button No 4 : Data Type 204
         * Button No 5 : Data Type 205
         * Button No 6 : Data Type 206
         * Button No 7 : Data Type 207
         * Button No 8 : Data Type 208
         */
        public void Read_Button_Data(int buttonno)
        {
            byte[] bytesConfig = new byte[UC.device.GetMaxInputReportLength()];

            //Send Read Command

            byte[] message = new byte[64];
            message[0] = 1;                 //First byte is report id
            message[1] = Convert.ToByte(1); //Second byte is length of data
            message[2] = Convert.ToByte(buttonno + 201); //Data type button config read data
            Send_Device(message, message.Length);

            //Read Button Data from Device
            Read_Device(bytesConfig);

            for (int j = 0; j < 64; j++)
            {
                Console.Write(bytesConfig[j].ToString() + "\t");
            }
            Console.WriteLine();

            if (bytesConfig[1] == 194 || bytesConfig[1] == 226 || bytesConfig[1]==210)
            {
                for (int i = 0; i <= 63; i++)
                {
                    GV.MK_Data_Read[buttonno, i] = bytesConfig[i + 1];
                }
            }
            else
            {
                Console.WriteLine("Not button value");
                Read_Button_Data(buttonno);
            }
            System.Threading.Thread.Sleep(250);
        }

        public void Read_All_Button_Data()
        {

            for (int i = 201; i <= 208; i++)
            {
                int ButtonNo = i - 201;
                Read_Button_Data(ButtonNo);
            }

            for (int i = 0; i < GV.MK_Data_Read.GetLength(0); i++)
            {
                for (int j = 0; j < GV.MK_Data_Read.GetLength(1); j++)
                {
                    Console.Write(GV.MK_Data_Read[i, j] + "\t");
                }
                Console.WriteLine();
            }


        }

        public byte[] Create_Text_Message(byte[] text, int buttonNo)
        {
            byte[] msg = new byte[64];

            msg[0] = 1;                             // First byte is report id
            msg[1] = Convert.ToByte(text.Length);   // Second byte is length of data
            msg[2] = Convert.ToByte(226);           // Print Text
            msg[3] = (byte)buttonNo;                // Macro key number

            for (int i = 0; i < text.Length; i++)
            {
                msg[i + 5] = text[i];
            }

            return msg;
        }

        public byte[] Create_CAD_Message(byte[] text, int buttonNo)
        {
            byte[] msg = new byte[64];

            msg[0] = 1;                             // First byte is report id
            msg[1] = Convert.ToByte(text.Length);   // Second byte is length of data
            msg[2] = Convert.ToByte(210);           // Print Text
            msg[3] = (byte)buttonNo;                // Macro key number

            for (int i = 0; i < text.Length; i++)
            {
                msg[i + 5] = text[i];
            }

            return msg;
        }

        public byte[] Create_Shortcut_Message(int[] keystruct, int buttonNo)
        {
            byte[] msg = new byte[11];

            msg[0] = 1;                                         // First byte is report id
            msg[1] = Convert.ToByte(msg.Length);                // Second byte is length of data
            msg[2] = Convert.ToByte(194);                       // Key shortcut
            msg[3] = (byte)buttonNo;                            // Macro key number
            msg[4] = Convert.ToByte(keystruct[1]);              // Second byte is button modifier byte
            //message[5] = Convert.ToByte(keystruct[2]);        // Reserved from USB keyboard definition
            msg[5] = Convert.ToByte(keystruct[3]);              // Keycode1
            msg[6] = Convert.ToByte(keystruct[4]);              // Keycode2
            msg[7] = Convert.ToByte(keystruct[5]);              // Keycode3
            msg[8] = Convert.ToByte(keystruct[6]);              // Keycode4
            msg[9] = Convert.ToByte(keystruct[7]);              // Keycode5
            msg[10] = Convert.ToByte(keystruct[8]);             // Keycode6

            return msg;
        }

        public byte[,] Convert_Read_Button_Data_to_Send_Data(byte[,] readData)
        {
            byte[,] convertedSendData = new byte[readData.GetLength(0), readData.GetLength(1)];
            byte[] temp = new byte[convertedSendData.GetLength(1)];

            for (int i = 0; i < convertedSendData.GetLength(0); i++)
            {
                for (int j = 0; j < convertedSendData.GetLength(1); j++)
                {
                    temp[j] = readData[i, j];
                }

                if (temp[0] == 226) //Text print data
                {
                    int index = Array.IndexOf(temp, (byte)0);
                    int datalength = index - 1;
                    convertedSendData[i, 0] = 1;                //Report ID
                    convertedSendData[i, 1] = (byte)datalength; //Data length
                    convertedSendData[i, 2] = 226;              //Text print
                    convertedSendData[i, 3] = (byte)(i + 1);      //Button no

                    for (int k = 0; k < datalength; k++)
                    {
                        convertedSendData[i, k + 5] = Convert.ToByte(temp[k + 1]);
                    }
                }

                if (temp[0] == 194) //Shortcut data
                {

                    int index = Array.IndexOf(temp, (byte)0);

                    if (index == 1)
                    {
                        index = Array.IndexOf(temp, (byte)0, 2);
                    }

                    int datalength = index - 1;
                    convertedSendData[i, 0] = 1;                //Report ID
                    convertedSendData[i, 1] = (byte)datalength; //Data length
                    convertedSendData[i, 2] = 194;              //Key Shortcut identifier
                    convertedSendData[i, 3] = (byte)(i + 1);    //Button no
                    convertedSendData[i, 4] = temp[1];          //Button modifier

                    for (int l = 0; l < datalength; l++)
                    {
                        convertedSendData[i, l + 5] = Convert.ToByte(temp[l + 2]);
                    }

                }

                if (temp[0] == 210) //Shortcut data
                {

                    int index = Array.IndexOf(temp, (byte)0);

                    if (index == 1)
                    {
                        index = Array.IndexOf(temp, (byte)0, 2);
                    }

                    int datalength = index - 1;
                    convertedSendData[i, 0] = 1;                //Report ID
                    convertedSendData[i, 1] = (byte)datalength; //Data length
                    convertedSendData[i, 2] = 210;              //CAD Software shortcut definer
                    convertedSendData[i, 3] = (byte)(i + 1);    //Button no
                    convertedSendData[i, 4] = 0;                //Button modifier

                    for (int l = 0; l < datalength; l++)
                    {
                        convertedSendData[i, l + 5] = Convert.ToByte(temp[l + 1]);
                    }

                }



            }

            for (int i = 0; i < convertedSendData.GetLength(0); i++)
            {
                for (int j = 0; j < convertedSendData.GetLength(1); j++)
                {
                    Console.Write(convertedSendData[i, j] + "\t");
                }
                Console.WriteLine();
            }

            return convertedSendData;
        }

        public int[] Find_Modifier_Combination(byte _modifier)
        {


            BitArray bitArray = new BitArray(new byte[] { _modifier });
            List<int> indices = new List<int>();

            for (int i = 0; i < bitArray.Length; i++)
            {
                if (bitArray[i] == true)
                {
                    indices.Add(i);
                }
            }

            int[] temp = new int[indices.Count];

            if (indices.Count == 1)
            {
                temp[0] = indices[0] + 1;
            }

            if (indices.Count == 2)
            {
                temp[0] = indices[0] + 1;
                temp[1] = indices[1] + 1;
            }

            if (indices.Count == 3)
            {
                temp[0] = indices[0] + 1;
                temp[1] = indices[1] + 1;
                temp[2] = indices[2] + 1;
            }

            return temp;
        }

        public void Send_Device(byte[] _data, int _dataLength)
        {
            try
            {
                if (UC.device.TryOpen(out UC.stream) != true)
                {
                    System.Windows.Forms.MessageBox.Show("Failed to open device.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }

                using (UC.stream)
                {
                    UC.stream.Write(_data, 0, _dataLength);
                }

            }
            catch
            {

            }
            
        }

        public byte[] Read_Device(byte[] _bytesConfig)
        {

            if (!UC.device.TryOpen(out UC.stream))
            {
                System.Windows.Forms.MessageBox.Show("Failed to open device.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
            try
            {
                int count = UC.stream.Read(_bytesConfig);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show(ex.Message);
            }

            return _bytesConfig;

        }
        
        public void Button_Values(Button_Values_Struct ButtonStruct,byte[] _sendData , List<string> _activeKeys, List<byte> _activeBytes)
        {
            byte[] temp = _sendData;

            ButtonStruct.MK_Data_Send = (byte[])temp.Clone();
            

            ButtonStruct.Button_No = ButtonStruct.MK_Data_Send[3];

            /*
             * Shortcut Operations
             */
            if (ButtonStruct.MK_Data_Send[2] == 194)
            {
                ButtonStruct.MK_Data_Text_String = null;
                ButtonStruct.DataLength = ButtonStruct.MK_Data_Send[1];
                ButtonStruct.Text_or_Shortcut = 0; //Shortcut

                //ButtonStruct.Data = new byte[ButtonStruct.DataLength - 1];
                ButtonStruct.Data = new byte[3] {0,0,0};
                ButtonStruct.MK_Data_Shortcut_String = new string[3];

                for (int k = 0; k <= ButtonStruct.Data.Length - 1; k++)
                {
                    ButtonStruct.Data[k] = ButtonStruct.MK_Data_Send[k + 5];
                }



                //byte[] deneme=Convert.FromBase64String(ButtonStruct.Data);
                //byte[] data= Convert.FromBase64String(ButtonStruct.MK_Data_Send);

                #region Find Modifier
                ButtonStruct.Modifier = ButtonStruct.MK_Data_Send[4];

                int[] modifierCombination = Find_Modifier_Combination(ButtonStruct.MK_Data_Send[4]);
                int modifierCombinationLength = modifierCombination.Length;

                if (modifierCombinationLength == 0)
                {
                    if (ButtonStruct.Data[0] == 0)
                    {
                        ButtonStruct.cmb_val1 = 0;
                    }
                    if (ButtonStruct.Data[0] != 0)
                    {
                        ButtonStruct.cmb_val1 = _activeBytes.IndexOf(ButtonStruct.Data[0], 9);
                    }

                    if (ButtonStruct.Data[1] == 0)
                    {
                        ButtonStruct.cmb_val2 = 0;
                    }
                    if (ButtonStruct.Data[1] != 0)
                    {
                        ButtonStruct.cmb_val2 = _activeBytes.IndexOf(ButtonStruct.Data[1], 9);
                    }

                    if (ButtonStruct.Data[2] == 0)
                    {
                        ButtonStruct.cmb_val3 = 0;
                    }
                    if (ButtonStruct.Data[2] != 0)
                    {
                        ButtonStruct.cmb_val3 = _activeBytes.IndexOf(ButtonStruct.Data[2], 9);
                    }

                    ButtonStruct.MK_Data_Shortcut_String[0] = _activeKeys[ButtonStruct.cmb_val1];
                    ButtonStruct.MK_Data_Shortcut_String[1] = _activeKeys[ButtonStruct.cmb_val2];
                    ButtonStruct.MK_Data_Shortcut_String[2] = _activeKeys[ButtonStruct.cmb_val3];

                    ButtonStruct.MK_Data_Shortcut_String_All = ButtonStruct.MK_Data_Shortcut_String[0] + "+" +
                                                              ButtonStruct.MK_Data_Shortcut_String[1] + "+" +
                                                              ButtonStruct.MK_Data_Shortcut_String[2];
                }
                if (modifierCombinationLength == 1)
                {
                    ButtonStruct.cmb_val1 = modifierCombination[0];

                    if(ButtonStruct.Data[0]==0)
                    {
                        ButtonStruct.cmb_val2 = 0;
                    }
                    if(ButtonStruct.Data[0] != 0)
                    {
                        ButtonStruct.cmb_val2 = _activeBytes.IndexOf(ButtonStruct.Data[0], 9);
                    }

                    if (ButtonStruct.Data[1] == 0)
                    {
                        ButtonStruct.cmb_val3 = 0;
                    }
                    if (ButtonStruct.Data[1] != 0)
                    {
                        ButtonStruct.cmb_val3 = _activeBytes.IndexOf(ButtonStruct.Data[1], 9);
                    }


                    ButtonStruct.MK_Data_Shortcut_String[0] = _activeKeys[ButtonStruct.cmb_val1];
                    ButtonStruct.MK_Data_Shortcut_String[1] = _activeKeys[ButtonStruct.cmb_val2];
                    ButtonStruct.MK_Data_Shortcut_String[2] = _activeKeys[ButtonStruct.cmb_val3];

                    ButtonStruct.MK_Data_Shortcut_String_All = ButtonStruct.MK_Data_Shortcut_String[0] + "+" +
                                                              ButtonStruct.MK_Data_Shortcut_String[1] + "+" +
                                                              ButtonStruct.MK_Data_Shortcut_String[2];
                }
                if (modifierCombinationLength == 2)
                {
                    ButtonStruct.cmb_val1 = modifierCombination[0];
                    ButtonStruct.cmb_val2 = modifierCombination[1];
                    if (ButtonStruct.Data[2] == 0)
                    {
                        ButtonStruct.cmb_val3 = 0;
                    }
                    if (ButtonStruct.Data[2] != 0)
                    {
                        ButtonStruct.cmb_val3 = _activeBytes.IndexOf(ButtonStruct.Data[0], 9);
                    }

                    ButtonStruct.MK_Data_Shortcut_String[0] = _activeKeys[ButtonStruct.cmb_val1];
                    ButtonStruct.MK_Data_Shortcut_String[1] = _activeKeys[ButtonStruct.cmb_val2];
                    ButtonStruct.MK_Data_Shortcut_String[2] = _activeKeys[ButtonStruct.cmb_val3];

                    ButtonStruct.MK_Data_Shortcut_String_All = ButtonStruct.MK_Data_Shortcut_String[0] + "+" +
                                                               ButtonStruct.MK_Data_Shortcut_String[1] + "+" +
                                                               ButtonStruct.MK_Data_Shortcut_String[2];
                }
                if (modifierCombinationLength == 3)
                {
                    ButtonStruct.cmb_val1 = modifierCombination[0];
                    ButtonStruct.cmb_val2 = modifierCombination[1];
                    ButtonStruct.cmb_val3 = modifierCombination[2];

                    ButtonStruct.MK_Data_Shortcut_String[0] = _activeKeys[ButtonStruct.cmb_val1];
                    ButtonStruct.MK_Data_Shortcut_String[1] = _activeKeys[ButtonStruct.cmb_val2];
                    ButtonStruct.MK_Data_Shortcut_String[2] = _activeKeys[ButtonStruct.cmb_val3];

                    ButtonStruct.MK_Data_Shortcut_String_All = ButtonStruct.MK_Data_Shortcut_String[0] + "+" +
                                                               ButtonStruct.MK_Data_Shortcut_String[1] + "+" +
                                                               ButtonStruct.MK_Data_Shortcut_String[2];
                }
                #endregion

                #region Write to console
                Console.WriteLine();
                Console.WriteLine("--------------");
                Console.WriteLine("Button No: " + ButtonStruct.Button_No);
                Console.Write("Data Send: " + "\t");
                for (int j = 0; j <= 63; j++)
                {
                    Console.Write(ButtonStruct.MK_Data_Send[j] + "\t");
                }
                Console.WriteLine();
                Console.WriteLine("Data Length: " + ButtonStruct.DataLength);
                Console.WriteLine("Text or Shortcut: " + ButtonStruct.Text_or_Shortcut + " -> 0:Shortcut   1:Text");

                Console.Write("Data:" + "\t");
                for (int j = 0; j <= ButtonStruct.Data.Length - 1; j++)
                {
                    Console.Write(ButtonStruct.Data[j] + "\t");
                }
                //Console.WriteLine();

                Console.WriteLine(ButtonStruct.MK_Data_Text_String);

                Console.Write("Shortcut String: " + "\t");
                for (int j = 0; j <= 2; j++)
                {
                    Console.Write(ButtonStruct.MK_Data_Shortcut_String[j] + "\t");
                }
                Console.WriteLine();
                Console.WriteLine(ButtonStruct.MK_Data_Shortcut_String_All);
                Console.WriteLine("Modifier: " + ButtonStruct.Modifier);
                Console.WriteLine("ComboBox 1 Val: " + ButtonStruct.cmb_val1);
                Console.WriteLine("ComboBox 2 Val: " + ButtonStruct.cmb_val2);
                Console.WriteLine("ComboBox 3 Val: " + ButtonStruct.cmb_val3);
                #endregion
            }
            /*
            * Text Operations
            */
            if (ButtonStruct.MK_Data_Send[2] == 226)
            {
                ButtonStruct.MK_Data_Shortcut_String = null;
                ButtonStruct.Modifier = -1;
                ButtonStruct.cmb_val1 = -1;
                ButtonStruct.cmb_val2 = -1;
                ButtonStruct.cmb_val3 = -1;

                ButtonStruct.DataLength = ButtonStruct.MK_Data_Send[1];
                ButtonStruct.Button_No = ButtonStruct.MK_Data_Send[3];
                ButtonStruct.Text_or_Shortcut = 1; //Text

                ButtonStruct.Data = new byte[ButtonStruct.DataLength];

                for (int k = 0; k <= ButtonStruct.Data.Length - 1; k++)
                {
                    ButtonStruct.Data[k] = ButtonStruct.MK_Data_Send[k + 5];
                }

                ButtonStruct.MK_Data_Text_String = Encoding.UTF8.GetString(ButtonStruct.Data);

                #region Write to console
                Console.WriteLine();
                Console.WriteLine("--------------");
                Console.WriteLine("Button No: " + ButtonStruct.Button_No);
                Console.Write("Data Send: " + "\t");
                for (int j = 0; j <= 63; j++)
                {
                    Console.Write(ButtonStruct.MK_Data_Send[j] + "\t");
                }
                Console.WriteLine();
                Console.WriteLine("Data Length: " + ButtonStruct.DataLength);
                Console.WriteLine("Text or Shortcut: " + ButtonStruct.Text_or_Shortcut + " -> 0:Shortcut   1:Text");

                Console.Write("Data:" + "\t");
                for (int j = 0; j <= ButtonStruct.Data.Length - 1; j++)
                {
                    Console.Write(ButtonStruct.Data[j] + "\t");
                }
                Console.WriteLine();

                Console.WriteLine("Text String: "+ButtonStruct.MK_Data_Text_String);

                Console.WriteLine("Shortcut String: " + ButtonStruct.MK_Data_Shortcut_String);


                Console.WriteLine("Modifier: " + ButtonStruct.Modifier);
                Console.WriteLine("ComboBox 1 Val: " + ButtonStruct.cmb_val1);
                Console.WriteLine("ComboBox 2 Val: " + ButtonStruct.cmb_val2);
                Console.WriteLine("ComboBox 3 Val: " + ButtonStruct.cmb_val3);
                #endregion
            }

            if (ButtonStruct.MK_Data_Send[2] == 210)
            {
                ButtonStruct.MK_Data_Shortcut_String = null;
                ButtonStruct.Modifier = -1;
                ButtonStruct.cmb_val1 = -1;
                ButtonStruct.cmb_val2 = -1;
                ButtonStruct.cmb_val3 = -1;

                ButtonStruct.DataLength = ButtonStruct.MK_Data_Send[1];
                ButtonStruct.Button_No = ButtonStruct.MK_Data_Send[3];
                ButtonStruct.Text_or_Shortcut = 1; //Text

                ButtonStruct.Data = new byte[ButtonStruct.DataLength];

                for (int k = 0; k <= ButtonStruct.Data.Length - 1; k++)
                {
                    ButtonStruct.Data[k] = ButtonStruct.MK_Data_Send[k + 5];
                }

                ButtonStruct.MK_Data_Text_String = Encoding.UTF8.GetString(ButtonStruct.Data);

                #region Write to console
                Console.WriteLine();
                Console.WriteLine("--------------");
                Console.WriteLine("Button No: " + ButtonStruct.Button_No);
                Console.Write("Data Send: " + "\t");
                for (int j = 0; j <= 63; j++)
                {
                    Console.Write(ButtonStruct.MK_Data_Send[j] + "\t");
                }
                Console.WriteLine();
                Console.WriteLine("Data Length: " + ButtonStruct.DataLength);
                Console.WriteLine("Text or Shortcut: " + ButtonStruct.Text_or_Shortcut + " -> 0:Shortcut   1:Text");

                Console.Write("Data:" + "\t");
                for (int j = 0; j <= ButtonStruct.Data.Length - 1; j++)
                {
                    Console.Write(ButtonStruct.Data[j] + "\t");
                }
                Console.WriteLine();

                Console.WriteLine("Text String: " + ButtonStruct.MK_Data_Text_String);

                Console.WriteLine("Shortcut String: " + ButtonStruct.MK_Data_Shortcut_String);


                Console.WriteLine("Modifier: " + ButtonStruct.Modifier);
                Console.WriteLine("ComboBox 1 Val: " + ButtonStruct.cmb_val1);
                Console.WriteLine("ComboBox 2 Val: " + ButtonStruct.cmb_val2);
                Console.WriteLine("ComboBox 3 Val: " + ButtonStruct.cmb_val3);
                #endregion
            }

        }

        public string NavigationAsButtonString(Button_Values_Struct ButtonStruct, List<string> _active_keys)
        {
            string _NavigationAsButtonString=null;

            if(ButtonStruct.Text_or_Shortcut==0)
            {
                
                int seperator;
                if(ButtonStruct.Modifier!=0)
                {
                    string modifier = _active_keys[ButtonStruct.cmb_val1];
                    string key1 = _active_keys[ButtonStruct.cmb_val2];
                    string key2 = _active_keys[ButtonStruct.cmb_val3];

                    if (modifier.Contains("Control"))
                    {
                        modifier = "^";
                    }
                    if (modifier.Contains("Shift"))
                    {
                        modifier = "+";
                    }
                    if (modifier.Contains("Alt"))
                    {
                        modifier = "%";
                    }

                    seperator = key1.IndexOf("_");
                    if (key1 == "KEY_NONE")
                    {
                        key1 = "";
                    }

                    else if (key1.Substring(0, seperator) == "LETTER" || key1.Substring(0, seperator) == "NUM" ||
                        key1.Substring(0, seperator) == "FN" || key1.Substring(0, seperator) == "ARROW" ||
                        key1.Substring(0, seperator) == "KP" || key1.Substring(0, seperator) == "KEY")
                    {
                        if (key1.Substring(0, seperator) == "LETTER" || key1.Substring(0, seperator) == "NUM" || key1.Substring(0, seperator) == "KP")
                        {
                            key1 = key1.Substring(seperator + 1).ToLower();
                        }
                        else if (key1.Substring(0, seperator) == "FN" || key1.Substring(0, seperator) == "ARROW" || key1.Substring(0, seperator) == "KEY")
                        {
                            key1 = "{" + key1.Substring(seperator + 1).ToUpper() + "}";
                        }

                    }

                    seperator = key2.IndexOf("_");
                    if (key2 == "KEY_NONE")
                    {
                        key2 = "";
                    }

                    else if (key2.Substring(0, seperator) == "LETTER" || key2.Substring(0, seperator) == "NUM" ||
                        key2.Substring(0, seperator) == "FN" || key2.Substring(0, seperator) == "ARROW" ||
                        key2.Substring(0, seperator) == "KP" || key2.Substring(0, seperator) == "KEY")
                    {
                        if (key2.Substring(0, seperator) == "LETTER" || key2.Substring(0, seperator) == "NUM" || key2.Substring(0, seperator) == "KP")
                        {
                            key2 = key2.Substring(seperator + 1).ToLower();
                        }
                        else if (key2.Substring(0, seperator) == "FN" || key2.Substring(0, seperator) == "ARROW" || key2.Substring(0, seperator) == "KEY")
                        {
                            key2 = "{" + key2.Substring(seperator + 1).ToUpper() + "}";
                        }

                    }

                    _NavigationAsButtonString = modifier + key1 + key2;

                }

                if(ButtonStruct.Modifier == 0)
                {
                    string key1 = _active_keys[ButtonStruct.cmb_val1];
                    string key2 = _active_keys[ButtonStruct.cmb_val2];
                    string key3 = _active_keys[ButtonStruct.cmb_val3];

                    seperator = key1.IndexOf("_");
                    if (key1 == "KEY_NONE")
                    {
                        key1 = "";
                    }
                    else if (key1.Substring(0, seperator) == "LETTER" || key1.Substring(0, seperator) == "NUM" ||
                        key1.Substring(0, seperator) == "FN" || key1.Substring(0, seperator) == "ARROW" ||
                        key1.Substring(0, seperator) == "KP" || key1.Substring(0, seperator) == "KEY")
                    {
                        if (key1.Substring(0, seperator) == "LETTER" || key1.Substring(0, seperator) == "NUM" || key1.Substring(0, seperator) == "KP")
                        {
                            
                            if(key1.Substring(seperator+1) == "Delete")
                            {
                                key1 = "{" + key1.Substring(seperator + 1).ToUpper() + "}";
                            }
                            else if (key1.Substring(seperator+1) == "Enter")
                            {
                                key1 = "{" + key1.Substring(seperator + 1).ToUpper() + "}";
                            }
                            else if (key1.Substring(seperator+1) == "NumLock")
                            {
                                key1 = "{" + key1.Substring(seperator + 1).ToUpper() + "}";
                            }
                            else
                            {
                                key1 = key1.Substring(seperator + 1).ToLower();
                            }
                        }
                        else if (key1.Substring(0, seperator) == "FN" || key1.Substring(0, seperator) == "ARROW" || key1.Substring(0, seperator) == "KEY")
                        {
                            key1 = "{" + key1.Substring(seperator + 1).ToUpper() + "}";
                        }
                    }

                    seperator = key2.IndexOf("_");
                    if (key2 == "KEY_NONE")
                    {
                        key2 = "";
                    }

                    else if (key2.Substring(0, seperator) == "LETTER" || key2.Substring(0, seperator) == "NUM" ||
                        key2.Substring(0, seperator) == "FN" || key2.Substring(0, seperator) == "ARROW" ||
                        key2.Substring(0, seperator) == "KP" || key2.Substring(0, seperator) == "KEY")
                    {
                        if (key2.Substring(0, seperator) == "LETTER" || key2.Substring(0, seperator) == "NUM" || key2.Substring(0, seperator) == "KP")
                        {
                            key2 = key2.Substring(seperator + 1).ToLower();
                        }
                        else if (key2.Substring(0, seperator) == "FN" || key2.Substring(0, seperator) == "ARROW" || key2.Substring(0, seperator) == "KEY")
                        {
                            key2 = "{" + key2.Substring(seperator + 1).ToUpper() + "}";
                        }

                    }


                    seperator = key3.IndexOf("_");
                    if (key3 == "KEY_NONE")
                    {
                        key3 = "";
                    }

                    else if (key3.Substring(0, seperator) == "LETTER" || key3.Substring(0, seperator) == "NUM" ||
                        key3.Substring(0, seperator) == "FN" || key3.Substring(0, seperator) == "ARROW" ||
                        key3.Substring(0, seperator) == "KP" || key3.Substring(0, seperator) == "KEY")
                    {
                        if (key3.Substring(0, seperator) == "LETTER" || key3.Substring(0, seperator) == "NUM" || key3.Substring(0, seperator) == "KP")
                        {
                            key3 = key3.Substring(seperator + 1).ToLower();
                        }
                        else if (key3.Substring(0, seperator) == "FN" || key3.Substring(0, seperator) == "ARROW" || key3.Substring(0, seperator) == "KEY")
                        {
                            key3 = "{" + key3.Substring(seperator + 1).ToUpper() + "}";
                        }
                    }


                    _NavigationAsButtonString = key1 + key2 + key3;
                }
                
            }

            if(ButtonStruct.Text_or_Shortcut==1)
            {
                _NavigationAsButtonString= ButtonStruct.MK_Data_Text_String;
            }

            return _NavigationAsButtonString;
        }

        
        
    }
}
