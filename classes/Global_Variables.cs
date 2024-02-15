using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlobalVariables
{
    internal class Global_Variables
    {
        public byte[,] MK_Data_Read = new byte[8, 64];
        public byte[,] MK_Data_Send = new byte[8, 64];
        public string[] MK_Data_String = new string[8];

        public byte[,] Vk_Data_Send = new byte[8, 64];
        public string[] Vk_Data_String = new string[8];

        public byte[,] NavMove_Data_Send = new byte[12, 64];
        public string[] NavMove_String = new string[12];

        //
        public float move_X;
        public float move_Y;
        public float turn;
        public int tilt;
        public int roll;

        //LED Properties
        public int clr_R;
        public int clr_G;
        public int clr_B;
        public int brightness;
        public int LED_Mode;
        public string LED_Mode_Name;

        //Device Mode Properties
        /*
         * Device Mode  :   Device Mode Name
         *      1       :   Null
         *      2       :   Null
         *      3       :   Null
         *      4       :   Mouse
         */

        public int DeviceMode;
        public string DeviceMode_Name;
        public int CADSoftware;
        public string CADSoftware_Name;

        public LED_Properties LED_Properties = new LED_Properties();
        public Device_Mode_Properties Device_Mode_Properties = new Device_Mode_Properties();

        //Button Properties

        public Button_Values_Struct MK1_Values = new Button_Values_Struct();
        public Button_Values_Struct MK2_Values = new Button_Values_Struct();
        public Button_Values_Struct MK3_Values = new Button_Values_Struct();
        public Button_Values_Struct MK4_Values = new Button_Values_Struct();
        public Button_Values_Struct MK5_Values = new Button_Values_Struct();
        public Button_Values_Struct MK6_Values = new Button_Values_Struct();
        public Button_Values_Struct MK7_Values = new Button_Values_Struct();
        public Button_Values_Struct MK8_Values = new Button_Values_Struct();

        public Button_Values_Struct Vk1_Values = new Button_Values_Struct();
        public Button_Values_Struct Vk2_Values = new Button_Values_Struct();
        public Button_Values_Struct Vk3_Values = new Button_Values_Struct();
        public Button_Values_Struct Vk4_Values = new Button_Values_Struct();
        public Button_Values_Struct Vk5_Values = new Button_Values_Struct();
        public Button_Values_Struct Vk6_Values = new Button_Values_Struct();
        public Button_Values_Struct Vk7_Values = new Button_Values_Struct();
        public Button_Values_Struct Vk8_Values = new Button_Values_Struct();

        //Navigation Values

        public Button_Values_Struct PitchDown_Values = new Button_Values_Struct();
        public Button_Values_Struct PitchUp_Values = new Button_Values_Struct();
        public Button_Values_Struct RollRight_Values = new Button_Values_Struct();
        public Button_Values_Struct RollLeft_Values = new Button_Values_Struct();
        public Button_Values_Struct YawCW_Values = new Button_Values_Struct();
        public Button_Values_Struct YawCCW_Values = new Button_Values_Struct();
        public Button_Values_Struct TransXPos_Values = new Button_Values_Struct();
        public Button_Values_Struct TransXNeg_Values = new Button_Values_Struct();
        public Button_Values_Struct TransYPos_Values = new Button_Values_Struct();
        public Button_Values_Struct TransYNeg_Values = new Button_Values_Struct();
        public Button_Values_Struct TransZPos_Values = new Button_Values_Struct();
        public Button_Values_Struct TransZNeg_Values = new Button_Values_Struct();
    }

    public class LED_Properties
    {
        public int clr_R;
        public int clr_G;
        public int clr_B;
        public int brightness;
        public int led_Mode;
        public string led_Mode_Name;
    }

    public class Device_Mode_Properties
    {
        public int DeviceMode;
        public string DeviceMode_Name;
        public int CADSoftware;
        public string CADSoftware_Name;
    }

    public class Button_Values_Struct
    {
        public int Button_No;
        public string Button_Name;
        public byte[] MK_Data_Send;
        public int DataLength;
        public int Text_or_Shortcut; //0:Shortcut   1:Text
        public byte[] Data;

        public string MK_Data_Text_String;

        public string[] MK_Data_Shortcut_String;
        public string MK_Data_Shortcut_String_All;
        public int Modifier;
        public int cmb_val1;
        public int cmb_val2;
        public int cmb_val3;
    }

}
