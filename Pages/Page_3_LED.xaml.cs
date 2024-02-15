using System.Windows.Controls;
using System.Windows.Forms;
using System;
using System.Drawing;
using System.IO;
using System.Windows.Input;
using System.Windows.Media;
using static System.Net.Mime.MediaTypeNames;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;
using System.Runtime.InteropServices.ComTypes;
using USB_Communication;
using GlobalVariables;
using _Functions;
using System.Reflection.Emit;

namespace EVO_MOTION_D6X8
{
    /// <summary>
    /// Interaction logic for Page_3_LED.xaml
    /// </summary>
    public partial class Page_3_LED : Page
    {

        USB_Connection UC = null;
        Global_Variables GV = null;
        Functions FC = null;


        public Page_3_LED()
        {
            InitializeComponent();  
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

        private void cmb_LedEffects_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if (cmb_LedEffects.SelectedIndex == 0)
            {
                border_colorwhell.IsEnabled = true;
                border_colorwhell.Opacity = 1;
            }
            else
            {
                border_colorwhell.IsEnabled = false;
                border_colorwhell.Opacity = 0.2;
            }

            try
            {
                FC.write_LED_Connfig(GV.clr_R, GV.clr_G, GV.clr_B, GV.brightness, cmb_LedEffects.SelectedIndex);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
            }

            GV.LED_Mode = cmb_LedEffects.SelectedIndex;
            GV.LED_Mode_Name= cmb_LedEffects.SelectedValue.ToString();
        }


        Bitmap bmp;
        System.Drawing.Color Pixel_Color;

        private void Canvas_MouseMove(object sender, System.Windows.Input.MouseEventArgs e)
        {
            //string colorwhellPath = Directory.GetCurrentDirectory() + "\\pictures\\colorwhell\\pngegg (2).png";

            string _imagePath = pcb_colorwhellPicture.Background.GetValue(ImageBrush.ImageSourceProperty).ToString();

            string[] ImagePathLayers = _imagePath.Split('/');
            int lenght_abc= ImagePathLayers.Length;
            string colorwhellPath = Directory.GetCurrentDirectory() +"\\"+ ImagePathLayers[lenght_abc-3] + "\\" + ImagePathLayers[lenght_abc-2] + "\\" + ImagePathLayers[lenght_abc-1];


            System.Drawing.Image colorwhellImage = System.Drawing.Image.FromFile(colorwhellPath);
            bmp = new Bitmap(colorwhellImage, colorwhellImage.Width/(colorwhellImage.Width/(int)pcb_colorwhellPicture.Width),colorwhellImage.Height/(colorwhellImage.Height/(int)pcb_colorwhellPicture.Height));

            System.Windows.Point pointtowindow = Mouse.GetPosition(this);
            System.Windows.Thickness margin_canvas = pcb_colorwhellPicture.Margin;
            
            int x_pos = (int)pointtowindow.X - (int)margin_canvas.Left-(int)border_colorwhell.Margin.Left;
            int y_pos = (int)pointtowindow.Y - (int)margin_canvas.Top - (int)border_colorwhell.Margin.Top;

            if (x_pos>=pcb_colorwhellPicture.Width)
            {
                x_pos = (int)pcb_colorwhellPicture.Width;
            }
            if(x_pos<0)
            {
                x_pos=0;
            }
            if(y_pos>=pcb_colorwhellPicture.Height)
            {
                y_pos = (int)pcb_colorwhellPicture.Height;
            }
            if(y_pos<0)
            {
                y_pos=0;
            }
            Pixel_Color = bmp.GetPixel((int)x_pos, (int)y_pos);
            
        }

        private void pcb_colorwhellPicture_MouseDown(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {   
            lbl_RED.Content = Pixel_Color.R;
            lbl_GREEN.Content = Pixel_Color.G;
            lbl_BLUE.Content = Pixel_Color.B;

            GV.clr_R = Pixel_Color.R;
            GV.clr_G = Pixel_Color.G;
            GV.clr_B = Pixel_Color.B;

            try
            {
                FC.write_LED_Connfig(GV.clr_R, GV.clr_G, GV.clr_B, GV.brightness, GV.LED_Mode);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Console.WriteLine(ex.ToString());
            }

        }

       private void slider_Brightness_ValueChanged(object sender, System.Windows.RoutedPropertyChangedEventArgs<double> e)
        {
            //Set the LED brightness
            try
            {
                FC.write_LED_Connfig(GV.clr_R, GV.clr_G, GV.clr_B, (int)slider_Brightness.Value, GV.LED_Mode);
            }
            catch (Exception ex)
            {
                //MessageBox.Show(ex.Message);
                Console.WriteLine(ex.ToString());
            }
            //Write the brightness value to global values
            GV.brightness=(int)slider_Brightness.Value;
        }

        public void Page_Loaded(object sender, System.Windows.RoutedEventArgs e)
        {
            lbl_BLUE.Content = GV.clr_B;
            lbl_GREEN.Content = GV.clr_G;
            lbl_RED.Content = GV.clr_R;
            cmb_LedEffects.SelectedIndex = GV.LED_Mode;
            slider_Brightness.Value = GV.brightness;

        }
    }
}
