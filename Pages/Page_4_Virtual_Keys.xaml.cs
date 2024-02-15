using System;
using System.Collections.Generic;
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
using System.Windows.Navigation;
using System.Windows.Shapes;
using KeyShortCut;
using _Functions;
using GlobalVariables;

namespace EVO_MOTION_D6X8
{
    /// <summary>
    /// Interaction logic for Virtual_Keys.xaml
    /// </summary>
    public partial class Page_4_Virtual_Keys : System.Windows.Controls.Page
    {
        KeyCodes KY = new KeyCodes();
        Global_Variables GV = null;
        Functions FC = null;

        List<string> active_keys = new List<string>();
        List<byte> active_bytes = new List<byte>();

        bool text_shortcut = true;

        public Page_4_Virtual_Keys()
        {
            InitializeComponent();
            cmb_MacroButtonName.SelectedIndex = 0;

            txt_macroPrintText.Visibility = Visibility.Collapsed;
            cmb_shortcut_val1.Visibility = Visibility.Visible;
            cmb_shortcut_val2.Visibility = Visibility.Visible;
            cmb_shortcut_val3.Visibility = Visibility.Visible;

            active_keys.AddRange(KY.get_active_keys());
            active_bytes.AddRange(KY.get_active_bytes());

            cmb_shortcut_val1.ItemsSource = active_keys;
            cmb_shortcut_val2.ItemsSource = active_keys;
            cmb_shortcut_val3.ItemsSource = active_keys;

            cmb_shortcut_val1.SelectedIndex = 0;
            cmb_shortcut_val2.SelectedIndex = 0;
            cmb_shortcut_val3.SelectedIndex = 0;

        }

        internal void GetGlobalVariables(Global_Variables gv)
        {
            GV = gv;
        }

        internal void GetFunctions(Functions fc)
        {
            FC = fc;
        }

        private void btn_setmacroShortcut_Click(object sender, RoutedEventArgs e)
        {
            txt_macroPrintText.Visibility = Visibility.Collapsed;
            stckp_shortcut.Visibility = Visibility.Visible;
            text_shortcut = true;
        }

        private void btn_setmacroPrintText_Click(object sender, RoutedEventArgs e)
        {
            txt_macroPrintText.Visibility = Visibility.Visible;
            stckp_shortcut.Visibility = Visibility.Collapsed;
            text_shortcut = !true;
        }

        private void btn_MacroSet_Click(object sender, RoutedEventArgs e)
        {
            #region Key Shortcut 
            if (text_shortcut == true)
            {
                string[] keystructName = { "MK Button", "Modifier", "Reserved", "KeyCode1", "KeyCode2", "KeyCode3", "KeyCode4", "KeyCode5", "KeyCode6" };

                int[] keystruct = new int[9] { 0, 0, 0, 0, 0, 0, 0, 0, 0 };
                List<int> abc = new List<int>();
                abc = KY.USB_HID_KEY(cmb_shortcut_val1.SelectedIndex, cmb_shortcut_val2.SelectedIndex, cmb_shortcut_val3.SelectedIndex, KY.GetLayoutCode());

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
                        "Vk" + Convert.ToString(cmb_MacroButtonName.SelectedIndex) + ": "
                        + cmb_shortcut_val1.SelectedItem + "-"
                        + cmb_shortcut_val2.SelectedItem + "-"
                        + cmb_shortcut_val3.SelectedItem;

                    GV.Vk_Data_String[cmb_MacroButtonName.SelectedIndex - 1] = lst_macroKeyValues.Items[cmb_MacroButtonName.SelectedIndex - 1].ToString();

                   
                    byte[] message = new byte[11];

                    message = FC.Create_Shortcut_Message(keystruct, cmb_MacroButtonName.SelectedIndex);

                    for (int i = 0; i <= message.Length - 1; i++)
                    {
                        GV.Vk_Data_Send[cmb_MacroButtonName.SelectedIndex - 1, i] = message[i];
                    }
                }
            }
            #endregion

            #region Key Print Text
            if (text_shortcut == false)
            {
                if (cmb_MacroButtonName.SelectedIndex == 0)
                {
                    System.Windows.MessageBox.Show("Select MK Button");
                }
                else
                {
                    lst_macroKeyValues.Items[cmb_MacroButtonName.SelectedIndex - 1] =
                        "Vk" + Convert.ToString(cmb_MacroButtonName.SelectedIndex) +
                        ": "
                        + txt_macroPrintText.Text;
                    GV.Vk_Data_String[cmb_MacroButtonName.SelectedIndex - 1] = lst_macroKeyValues.Items[cmb_MacroButtonName.SelectedIndex].ToString();

                    byte[] bytes;
                    bytes = Encoding.UTF8.GetBytes(txt_macroPrintText.Text);
                    //txt_macroPrintText.Text += BitConverter.ToString(bytes);


                    byte[] message = new byte[64];

                    message = FC.Create_Text_Message(bytes, cmb_MacroButtonName.SelectedIndex);


                    for (int i = 0; i <= message.Length - 1; i++)
                    {
                        GV.Vk_Data_Send[cmb_MacroButtonName.SelectedIndex - 1, i] = message[i];
                    }

                }
            }
            #endregion

            #region set key values to global
            byte[] temp = new byte[64];
            for (int i = 0; i <= 7; i++)
            {
                for (int j = 0; j <= 63; j++)
                {
                    temp[j] = GV.Vk_Data_Send[i, j];
                }

                if (i == 0)
                {
                    FC.Button_Values(GV.Vk1_Values, temp, active_keys, active_bytes);
                    if (GV.Vk1_Values.Text_or_Shortcut == 0)
                    {
                        lst_macroKeyValues.Items[i] = "Vk1:" + GV.Vk1_Values.MK_Data_Shortcut_String_All;
                    }
                    if (GV.Vk1_Values.Text_or_Shortcut == 1)
                    {
                        lst_macroKeyValues.Items[i] = "Vk1:" + GV.Vk1_Values.MK_Data_Text_String;
                    }
                }

                if (i == 1)
                {
                    FC.Button_Values(GV.Vk2_Values, temp, active_keys, active_bytes);
                    if (GV.Vk2_Values.Text_or_Shortcut == 0)
                    {
                        lst_macroKeyValues.Items[i] = "Vk2:" + GV.Vk2_Values.MK_Data_Shortcut_String_All;
                    }
                    if (GV.Vk2_Values.Text_or_Shortcut == 1)
                    {
                        lst_macroKeyValues.Items[i] = "Vk2:" + GV.Vk2_Values.MK_Data_Text_String;
                    }
                }

                if (i == 2)
                {
                    FC.Button_Values(GV.Vk3_Values, temp, active_keys, active_bytes);
                    if (GV.Vk3_Values.Text_or_Shortcut == 0)
                    {
                        lst_macroKeyValues.Items[i] = "Vk3:" + GV.Vk3_Values.MK_Data_Shortcut_String_All;
                    }
                    if (GV.Vk3_Values.Text_or_Shortcut == 1)
                    {
                        lst_macroKeyValues.Items[i] = "Vk3:" + GV.Vk3_Values.MK_Data_Text_String;
                    }
                }

                if (i == 3)
                {
                    FC.Button_Values(GV.Vk4_Values, temp, active_keys, active_bytes);
                    if (GV.Vk4_Values.Text_or_Shortcut == 0)
                    {
                        lst_macroKeyValues.Items[i] = "Vk4:" + GV.Vk4_Values.MK_Data_Shortcut_String_All;
                    }
                    if (GV.Vk4_Values.Text_or_Shortcut == 1)
                    {
                        lst_macroKeyValues.Items[i] = "Vk4:" + GV.Vk4_Values.MK_Data_Text_String;
                    }
                }

                if (i == 4)
                {
                    FC.Button_Values(GV.Vk5_Values, temp, active_keys, active_bytes);
                    if (GV.Vk5_Values.Text_or_Shortcut == 0)
                    {
                        lst_macroKeyValues.Items[i] = "Vk5:" + GV.Vk5_Values.MK_Data_Shortcut_String_All;
                    }
                    if (GV.Vk5_Values.Text_or_Shortcut == 1)
                    {
                        lst_macroKeyValues.Items[i] = "Vk5:" + GV.Vk5_Values.MK_Data_Text_String;
                    }
                }

                if (i == 5)
                {
                    FC.Button_Values(GV.Vk6_Values, temp, active_keys, active_bytes);
                    if (GV.Vk6_Values.Text_or_Shortcut == 0)
                    {
                        lst_macroKeyValues.Items[i] = "Vk6:" + GV.Vk6_Values.MK_Data_Shortcut_String_All;
                    }
                    if (GV.Vk6_Values.Text_or_Shortcut == 1)
                    {
                        lst_macroKeyValues.Items[i] = "Vk6:" + GV.Vk6_Values.MK_Data_Text_String.ToString();
                    }
                }

                if (i == 6)
                {
                    FC.Button_Values(GV.Vk7_Values, temp, active_keys, active_bytes);
                    if (GV.Vk7_Values.Text_or_Shortcut == 0)
                    {
                        lst_macroKeyValues.Items[i] = "Vk7:" + GV.Vk7_Values.MK_Data_Shortcut_String_All;
                    }
                    if (GV.Vk7_Values.Text_or_Shortcut == 1)
                    {
                        lst_macroKeyValues.Items[i] = "Vk7:" + GV.Vk7_Values.MK_Data_Text_String;
                    }
                }

                if (i == 7)
                {
                    FC.Button_Values(GV.Vk8_Values, temp, active_keys, active_bytes);
                    if (GV.Vk8_Values.Text_or_Shortcut == 0)
                    {
                        lst_macroKeyValues.Items[i] = "Vk8:" + GV.Vk8_Values.MK_Data_Shortcut_String_All;
                    }
                    if (GV.Vk8_Values.Text_or_Shortcut == 1)
                    {
                        lst_macroKeyValues.Items[i] = "Vk8:" + GV.Vk8_Values.MK_Data_Text_String;
                    }
                }
            }
            #endregion
        }
    }
}
