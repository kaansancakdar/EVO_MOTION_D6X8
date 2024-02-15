using KnowledgewareTypeLib;
using SolidWorks.Interop.swconst;
using Solidworks_Api;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Runtime.Remoting.Messaging;
using System.Text;
using System.Threading.Tasks;


namespace KeyShortCut
{
    internal class KeyCodes
    {
        private Api_Solidworks swApi = new Api_Solidworks();

        [DllImport("user32.dll")]
        private static extern long GetKeyboardLayoutName(StringBuilder pwszKLID);
        
        const int KL_NAMELENGTH = 10;

        public List<string> keys_trq = new List<string>
        {
            "KEY_NONE",
            "MOD_LeftControl",
            "MOD_LeftShift",
            "MOD_LeftAlt",
            "MOD_LeftWindows",
            "MOD_RightControl",
            "MOD_RightShift",
            "MOD_RightAlt",
            "MOD_RightWindows",

            "LETTER_A",
            "LETTER_B",
            "LETTER_C",
            "LETTER_Ç",
            "LETTER_D",
            "LETTER_E",
            "LETTER_F",
            "LETTER_G",
            "LETTER_Ğ",
            "LETTER_H",
            "LETTER_I",
            "LETTER_İ",
            "LETTER_J",
            "LETTER_K",
            "LETTER_L",
            "LETTER_M",
            "LETTER_N",
            "LETTER_O",
            "LETTER_Ö",
            "LETTER_P",
            "LETTER_Q",
            "LETTER_R",
            "LETTER_S",
            "LETTER_Ş",
            "LETTER_T",
            "LETTER_U",
            "LETTER_Ü",
            "LETTER_V",
            "LETTER_W",
            "LETTER_X",
            "LETTER_Y",
            "LETTER_Z",

            "NUM_1",
            "NUM_2",
            "NUM_3",
            "NUM_4",
            "NUM_5",
            "NUM_6",
            "NUM_7",
            "NUM_8",
            "NUM_9",
            "NUM_0",

            "FN_F1 ",
            "FN_F2 ",
            "FN_F3 ",
            "FN_F4 ",
            "FN_F5 ",
            "FN_F6 ",
            "FN_F7 ",
            "FN_F8 ",
            "FN_F9 ",
            "FN_F10 ",
            "FN_F11 ",
            "FN_F12 ",

            "ARROW_Up",
            "ARROW_Down",
            "ARROW_Left",
            "ARROW_Right",

            "KP_NumLock",
            "KP_Divide",
            "KP_Multiply",
            "KP_Subtract",
            "KP_7",
            "KP_8",
            "KP_9",
            "KP_4",
            "KP_5",
            "KP_6",
            "KP_1",
            "KP_2",
            "KP_3",
            "KP_0",
            "KP_Delete",
            "KP_Add",
            "KP_Enter",

            "KEY_-",
            "KEY_Grave",
            "KEY_,",
            "KEY_.",
            "KEY_?",
            "KEY_<",
            "KEY_Application",
            "KEY_BackSpace",
            "KEY_CapsLock ",
            "KEY_Delete",
            "KEY_End",
            "KEY_Enter",
            "KEY_Escape",
            "KEY_Home",
            "KEY_Insert",
            "KEY_Page Down",
            "KEY_Page Up",
            "KEY_Pause",
            "KEY_Print Screen",
            "KEY_ScrollLock",
            "KEY_Space",
            "KEY_Tab",
        };
        public List<byte> keys_trq_bytes = new List<byte>
        {
            0x00,
            0x1,
            0x2,
            0x4,
            0x8,
            0x10,
            0x20,
            0x40,
            0x80,

            0x4,
            0x5,
            0x6,
            0x37,
            0x7,
            0x8,
            0x9,
            0x0A,
            0x2F,
            0x0B,
            0x0C,
            0x34,
            0x0D,
            0x0E,
            0x0F,
            0x10,
            0x11,
            0x12,
            0x36,
            0x13,
            0x14,
            0x15,
            0x16,
            0x33,
            0x17,
            0x18,
            0x30,
            0x19,
            0x1A,
            0x1B,
            0x1C,
            0x1D,

            0x1E,
            0x1F,
            0x20,
            0x21,
            0x22,
            0x23,
            0x24,
            0x25,
            0x26,
            0x27,

            0x3A,
            0x3B,
            0x3C,
            0x3D,
            0x3E,
            0x3F,
            0x40,
            0x41,
            0x42,
            0x43,
            0x44,
            0x45,

            0x52,
            0x51,
            0x50,
            0x4F,

            0x53,
            0x54,
            0x55,
            0x56,
            0x5F,
            0x60,
            0x61,
            0x5C,
            0x5D,
            0x5E,
            0x59,
            0x5A,
            0x5B,
            0x62,
            0x63,
            0x57,
            0x58,

            0x2E,
            0x35,
            0x32,
            0x38,
            0x2D,
            0x64,
            0x65,
            0x2A,
            0x39,
            0x4C,
            0x4D,
            0x28,
            0x29,
            0x4A,
            0x49,
            0x4E,
            0x4B,
            0x48,
            0x46,
            0x47,
            0x2C,
            0x2B,


        };

        public List<string> keys_USInt = new List<string>
        {
            "KEY_NONE",
            "MOD_LeftControl",
            "MOD_LeftShift",
            "MOD_LeftAlt",
            "MOD_LeftWindows",
            "MOD_RightControl",
            "MOD_RightShift",
            "MOD_RightAlt",
            "MOD_RightWindows",

            "LETTER_A",
            "LETTER_B",
            "LETTER_C",
            "LETTER_D",
            "LETTER_E",
            "LETTER_F",
            "LETTER_M",
            "LETTER_N",
            "LETTER_O",
            "LETTER_P",
            "LETTER_Q",
            "LETTER_R",
            "LETTER_S",
            "LETTER_T",
            "LETTER_U",
            "LETTER_V",
            "LETTER_G",
            "LETTER_H",
            "LETTER_I",
            "LETTER_J",
            "LETTER_K",
            "LETTER_L",
            "LETTER_W",
            "LETTER_X",
            "LETTER_Y",
            "LETTER_Z",

            "NUM_1",
            "NUM_2",
            "NUM_3",
            "NUM_4",
            "NUM_5",
            "NUM_6",
            "NUM_7",
            "NUM_8",
            "NUM_9",
            "NUM_0",

            "FN_F1 ",
            "FN_F2 ",
            "FN_F3 ",
            "FN_F4 ",
            "FN_F5 ",
            "FN_F6 ",
            "FN_F7 ",
            "FN_F8 ",
            "FN_F9 ",
            "FN_F10 ",
            "FN_F11 ",
            "FN_F12 ",

            "ARROW_Left",
            "ARROW_Down",
            "ARROW_Up",
            "ARROW_Right",

            "KP_NumLock",
            "KP_Divide",
            "KP_Multiply",
            "KP_Subtract",
            "KP_Numpad7",
            "KP_Numpad8",
            "KP_Numpad9",
            "KP_Numpad4",
            "KP_Numpad5",
            "KP_Numpad6",
            "KP_Numpad1",
            "KP_Numpad2",
            "KP_Numpad3",
            "KP_Numpad0",
            "KP_Delete",
            "KP_Add",
            "KP_Keypad Enter",

            "KEY_Apostrophe",
            "KEY_-",
            "KEY_,",
            "KEY_.",
            "KEY_/",
            "KEY_;",
            "KEY_[",
            "KEY_BackSlash",
            "KEY_BackSlash",
            "KEY_]",
            "KEY_`",
            "KEY_=",
            "KEY_Application",
            "KEY_Back Space",
            "KEY_CapsLock ",
            "KEY_Delete",
            "KEY_End",
            "KEY_Enter",
            "KEY_Escape",
            "KEY_Home",
            "KEY_Insert",
            "KEY_Page Down",
            "KEY_Page Up",
            "KEY_Pause",
            "KEY_Print Screen",
            "KEY_ScrollLock",
            "KEY_Space",
            "KEY_Tab",

        };
        public List<byte> keys_USInt_bytes = new List<byte>
        {
            0x00,
            0x1,
            0x2,
            0x4,
            0x8,
            0x10,
            0x20,
            0x40,
            0x80,

            0x4,
            0x5,
            0x6,
            0x7,
            0x8,
            0x9,
            0x10,
            0x11,
            0x12,
            0x13,
            0x14,
            0x15,
            0x16,
            0x17,
            0x18,
            0x19,
            0x0A ,
            0x0B ,
            0x0C ,
            0x0D ,
            0x0E ,
            0x0F ,
            0x1A ,
            0x1B ,
            0x1C ,
            0x1D ,

            0x1E ,
            0x1F ,
            0x20,
            0x21,
            0x22,
            0x23,
            0x24,
            0x25,
            0x26,
            0x27,

            0x3A ,
            0x3B ,
            0x3C ,
            0x3D ,
            0x3E ,
            0x3F ,
            0x40,
            0x41,
            0x42,
            0x43,
            0x44,
            0x45,

            0x50,
            0x51,
            0x52,
            0x4F ,

            0x53,
            0x54,
            0x55,
            0x56,
            0x5F ,
            0x60,
            0x61,
            0x5C ,
            0x5D ,
            0x5E ,
            0x59,
            0x5A ,
            0x5B ,
            0x62,
            0x63,
            0x57,
            0x58,

            0x34,
            0x2D ,
            0x36,
            0x37,
            0x38,
            0x33,
            0x2F ,
            0x32,
            0x64,
            0x30,
            0x35,
            0x2E ,
            0x65,
            0x2A ,
            0x39,
            0x4C ,
            0x4D ,
            0x28,
            0x29,
            0x4A ,
            0x49,
            0x4E ,
            0x4B ,
            0x48,
            0x46,
            0x47,
            0x2C ,
            0x2B ,


        };

        public List<string> mouse = new List<string>
        {
         "MOUSE_LEFT","MOUSE_RIGHT","MOUSE_MIDDLE","MOUSEMOVE_UP","MOUSEMOVE_DOWN","MOUSEMOVE_RIGHT","MOUSEMOVE_LEFT"
        };

        public List<string> windows_shortcut = new List<string> 
        {
            "Ctrl+A",
            "Ctrl+C",
            "Ctrl+X",
            "Ctrl+V",
            "Ctrl+Z",
        };

        public List<string> Navigator_Movement = new List<string> {
           "Select Movement", "Pitch Down","Pitch Up", "Roll Right", "Roll Left", "CCW Turn", "CW Turn", "Translate +X", "Translate -X", "Translate +Y", "Translate -Y", "Translate +Z", "Translate -Z"
        };

        public List<string> MK_Button_Names = new List<string> {
            "Select Button","BT1", "BT2", "BT3", "BT4", "BT5", "BT6", "BT7", "BT8"
        };

        public string GetLayoutCode()
        {
            var name = new StringBuilder(KL_NAMELENGTH);
            GetKeyboardLayoutName(name);

            return name.ToString();
        }

        public List<string> get_active_keys()
        {
            List<string> _active_keys = new List<string>();

            if (GetLayoutCode() == "0000041F")
            {
                _active_keys.AddRange(keys_trq);
            }

            if (GetLayoutCode() == "00020409")
            {
                _active_keys.AddRange(keys_USInt);
            }

            return _active_keys;
        }

        public List<byte> get_active_bytes()
        {
            List<byte> _active_bytes = new List<byte>();

            if (GetLayoutCode() == "0000041F")
            {
                _active_bytes.AddRange(keys_trq_bytes);
            }

            if (GetLayoutCode() == "00020409")
            {
                _active_bytes.AddRange(keys_USInt_bytes);
            }

            return _active_bytes;
        }

        public String MapLayoutName(string code = null)
        {
            if (code == null)
                code = GetLayoutCode();

            switch (code)
            {
                case "0000041C":
                    return "Albanian";
                case "00000401":
                    return "Arabic (101)";
                case "00010401":
                    return "Arabic (102)";
                case "00020401":
                    return "Arabic (102) Azerty";
                case "0000042B":
                    return "Armenian eastern";
                case "0001042B":
                    return "Armenian Western";
                case "0000044D":
                    return "Assamese - inscript";
                case "0000082C":
                    return "Azeri Cyrillic";
                case "0000042C":
                    return "Azeri Latin";
                case "0000046D":
                    return "Bashkir";
                case "00000423":
                    return "Belarusian";
                case "0000080C":
                    return "Belgian French";
                case "00000813":
                    return "Belgian (period)";
                case "0001080C":
                    return "Belgian (comma)";
                case "00000445":
                    return "Bengali";
                case "00010445":
                    return "Bengali - inscript (legacy)";
                case "00020445":
                    return "Bengali - inscript";
                case "0000201A":
                    return "Bosnian (cyrillic)";
                case "00030402":
                    return "Bulgarian";
                case "00000402":
                    return "Bulgarian(typewriter)";
                case "00010402":
                    return "Bulgarian (latin)";
                case "00020402":
                    return "Bulgarian (phonetic)";
                case "00040402":
                    return "Bulgarian (phonetic traditional)";
                case "00011009":
                    return "Canada Multilingual";
                case "00001009":
                    return "Canada French";
                case "00000C0C":
                    return "Canada French (legacy)";
                case "00000404":
                    return "Chinese (traditional) - us keyboard";
                case "00000804":
                    return "Chinese (simplified) -us keyboard";
                case "00000C04":
                    return "Chinese (traditional, hong kong s.a.r.) - us keyboard";
                case "00001004":
                    return "Chinese (simplified, singapore) - us keyboard";
                case "00001404":
                    return "Chinese (traditional, macao s.a.r.) - us keyboard";
                case "00000405":
                    return "Czech";
                case "00020405":
                    return "Czech programmers";
                case "00010405":
                    return "Czech (qwerty)";
                case "0000041A":
                    return "Croatian";
                case "00000439":
                    return "Deanagari - inscript";
                case "00000406":
                    return "Danish";
                case "00000465":
                    return "Divehi phonetic";
                case "00010465":
                    return "Divehi typewriter";
                case "00000413":
                    return "Dutch";
                case "00000425":
                    return "Estonian";
                case "00000438":
                    return "Faeroese";
                case "0000040B":
                    return "Finnish";
                case "0001083B":
                    return "Finnish with sami";
                case "0000040C":
                    return "French";
                case "00011809":
                    return "Gaelic";
                case "00000437":
                    return "Georgian";
                case "00020437":
                    return "Georgian (ergonomic)";
                case "00010437":
                    return "Georgian (qwerty)";
                case "00000407":
                    return "German";
                case "00010407":
                    return "German (ibm)";
                case "0000046F":
                    return "Greenlandic";
                case "00000468":
                    return "Hausa";
                case "0000040D":
                    return "Hebrew";
                case "00010439":
                    return "Hindi traditional";
                case "00000408":
                    return "Greek";
                case "00010408":
                    return "Greek (220)";
                case "00030408":
                    return "Greek (220) latin";
                case "00020408":
                    return "Greek (319)";
                case "00040408":
                    return "Greek (319) latin";
                case "00050408":
                    return "Greek latin";
                case "00060408":
                    return "Greek polyonic";
                case "00000447":
                    return "Gujarati";
                case "0000040E":
                    return "Hungarian";
                case "0001040E":
                    return "Hungarian 101 key";
                case "0000040F":
                    return "Icelandic";
                case "00000470":
                    return "Igbo";
                case "0000085D":
                    return "Inuktitut - latin";
                case "0001045D":
                    return "Inuktitut - naqittaut";
                case "00001809":
                    return "Irish";
                case "00000410":
                    return "Italian";
                case "00010410":
                    return "Italian (142)";
                case "00000411":
                    return "Japanese";
                case "0000044B":
                    return "Kannada";
                case "0000043F":
                    return "Kazakh";
                case "00000453":
                    return "Khmer";
                case "00000412":
                    return "Korean";
                case "00000440":
                    return "Kyrgyz cyrillic";
                case "00000454":
                    return "Lao";
                case "0000080A":
                    return "Latin america";
                case "00000426":
                    return "Latvian";
                case "00010426":
                    return "Latvian (qwerty)";
                case "00010427":
                    return "Lithuanian";
                case "00000427":
                    return "Lithuanian ibm";
                case "00020427":
                    return "Lithuanian standard";
                case "0000046E":
                    return "Luxembourgish";
                case "0000042F":
                    return "Macedonian (fyrom)";
                case "0001042F":
                    return "Macedonian (fyrom) - standard";
                case "0000044C":
                    return "Malayalam";
                case "0000043A":
                    return "Maltese 47-key";
                case "0001043A":
                    return "Maltese 48-key";
                case "0000044E":
                    return "Marathi";
                case "00000481":
                    return "Maroi";
                case "00000450":
                    return "Mongolian cyrillic";
                case "00000850":
                    return "Mongolian (mongolian script)";
                case "00000461":
                    return "Nepali";
                case "00000414":
                    return "Norwegian";
                case "0000043B":
                    return "Norwegian with sami";
                case "00000448":
                    return "Oriya";
                case "00000463":
                    return "Pashto (afghanistan)";
                case "00000429":
                    return "Persian";
                case "00000415":
                    return "Polish (programmers)";
                case "00010415":
                    return "Polish (214)";
                case "00000816":
                    return "Portuguese";
                case "00000416":
                    return "Portuguese (brazillian abnt)";
                case "00010416":
                    return "Portuguese (brazillian abnt2)";
                case "00000446":
                    return "Punjabi";
                case "00010418":
                    return "Romanian (standard)";
                case "00000418":
                    return "Romanian (legacy)";
                case "00020418":
                    return "Romanian (programmers)";
                case "00000419":
                    return "Russian";
                case "00010419":
                    return "Russian (typewriter)";
                case "0002083B":
                    return "Sami extended finland-sweden";
                case "0001043B":
                    return "Sami extended norway";
                case "00000C1A":
                    return "Serbian (cyrillic)";
                case "0000081A":
                    return "Serbian (latin)";
                case "0000046C":
                    return "Sesotho sa Leboa";
                case "00000432":
                    return "Setswana";
                case "0000045B":
                    return "Sinhala";
                case "0001045B":
                    return "Sinhala -Wij 9";
                case "0000041B":
                    return "Slovak";
                case "0001041B":
                    return "Slovak (qwerty)";
                case "00000424":
                    return "Slovenian";
                case "0001042E":
                    return "Sorbian extended";
                case "0002042E":
                    return "Sorbian standard";
                case "0000042E":
                    return "Sorbian standard (legacy)";
                case "0000040A":
                    return "Spanish";
                case "0001040A":
                    return "Spanish variation";
                case "0000041D":
                    return "Swedish";
                case "0000083B":
                    return "Swedish with sami";
                case "00000807":
                    return "Swiss german";
                case "0000100C":
                    return "Swiss french";
                case "0000045A":
                    return "Syriac";
                case "0001045A":
                    return "Syriac phonetic";
                case "00000428":
                    return "Tajik";
                case "00000449":
                    return "Tamil";
                case "00000444":
                    return "Tatar";
                case "0000044A":
                    return "Telugu";
                case "0000041E":
                    return "Thai Kedmanee";
                case "0002041E":
                    return "Thai Kedmanee (non-shiftlock)";
                case "0001041E":
                    return "Thai Pattachote";
                case "0003041E":
                    return "Thai Pattachote (non-shiftlock)";
                case "00000451":
                    return "Tibetan (prc)";
                case "0001041F":
                    return "Turkish F";
                case "0000041F":
                    return "Turkish Q";
                case "00000442":
                    return "Turkmen";
                case "00000422":
                    return "Ukrainian";
                case "00020422":
                    return "Ukrainian (enhanced)";
                case "00000809":
                    return "United Kingdom";
                case "00000452":
                    return "United Kingdom Extended";
                case "00000409":
                    return "United States";
                case "00010409":
                    return "United States - dvorak";
                case "00030409":
                    return "United States - dvorak left hand";
                case "00050409":
                    return "United States - dvorak right hand";
                case "00004009":
                    return "United States - india";
                case "00020409":
                    return "United States - international";
                case "00000420":
                    return "Urdu";
                case "00010480":
                    return "Uyghur";
                case "00000480":
                    return "Uyghur (legacy)";
                case "00000843":
                    return "Uzbek cyrillic";
                case "0000042A":
                    return "Vietnamese";
                case "00000485":
                    return "Yakut";
                case "0000046A":
                    return "Yoruba";
                case "00000488":
                    return "Wolof";

                default:
                    return "unknown";
            }
        }
        
        public List<int> USB_HID_KEY(int key1, int key2, int key3, string _keyboardlayout)
        {
            List<int> _keystruct = new List<int>();

            List<string> _active_keys = new List<string>();
            List<byte> _active_bytes = new List<byte>();

            List<int> mods = new List<int>();
            List<int> keys = new List<int>();

            #region Select Keyboard Layout
            
            if (_keyboardlayout == "0000041F")
            {
                _active_keys.AddRange(keys_trq);
                _active_bytes.AddRange(keys_trq_bytes);
            }

            if (_keyboardlayout == "00020409")
            {
                _active_keys.AddRange(keys_USInt);
                _active_bytes.AddRange(keys_USInt_bytes);
            }
            #endregion

            #region Set Key Values
            if (key1 <= 8 && key1 >= 1)
            {
                mods.Add(_active_bytes[key1]);
            }

            else
            {
                keys.Add(_active_bytes[key1]);
                mods.Add(0);
            }

            if (key2 <= 8 && key2 >= 1)
            {
                mods.Add(_active_bytes[key2]);
            }

            else
            {
                keys.Add(_active_bytes[key2]);
                mods.Add(0);
            }

            if (key3 <= 8 && key3 >= 1)
            {
                mods.Add(_active_bytes[key3]);
            }

            else
            {
                keys.Add(_active_bytes[key3]);
                mods.Add(0);
            }
            #endregion
            
            int modifier = mods[0] | mods[1] | mods[2];

            _keystruct.Add(modifier);
            _keystruct.Add(0);
            _keystruct.AddRange(keys);

            return _keystruct;
        }

    }
}