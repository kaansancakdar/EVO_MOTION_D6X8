using EVO_MOTION_D6X8.Properties;
using System;
using System.Drawing;
using System.Windows;
using System.Windows.Ink;
using System.Windows.Media;
using System.Windows.Shapes;
using Brush = System.Drawing.Brush;
using Color = System.Windows.Media.Color;

namespace Button_Tags
{
    public class IconProperties
    {

        #region Button Icon Image (Not Used)
        //public static readonly DependencyProperty TagOnMouseOverProperty =
        //    DependencyProperty.RegisterAttached("TagOnMouseOver", typeof(ImageSource), typeof(IconProperties));

        //public static void SetTagOnMouseOver(UIElement element, ImageSource value)
        //{
        //    element.SetValue(TagOnMouseOverProperty, value);
        //}

        //public static ImageSource GetTagOnMouseOver(UIElement element)
        //{
        //    return (ImageSource)element.GetValue(TagOnMouseOverProperty);
        //}

        //public static readonly DependencyProperty TagOnPressedProperty =
        //    DependencyProperty.RegisterAttached("TagOnPressed", typeof(ImageSource), typeof(IconProperties));

        //public static void SetTagOnPressed(UIElement element, ImageSource value)
        //{
        //    element.SetValue(TagOnPressedProperty, value);
        //}

        //public static ImageSource GetTagOnPressed(UIElement element)
        //{
        //    return (ImageSource)element.GetValue(TagOnPressedProperty);
        //}
        #endregion

        #region Button Icon Path Data Normal

        public static readonly DependencyProperty TagProperty =
            DependencyProperty.RegisterAttached("Tag", typeof(Geometry), typeof(IconProperties), new FrameworkPropertyMetadata(null));

        public static Geometry GetTag(DependencyObject obj)
        {
            return (Geometry)obj.GetValue(TagProperty);
        }

        public static void SetTag(DependencyObject obj, Geometry value)
        {
            obj.SetValue(TagProperty, value);
        }
        #endregion

        #region Button Icon Path Data Normal 1

        public static readonly DependencyProperty Tag1Property =
           DependencyProperty.RegisterAttached("Tag1", typeof(Geometry), typeof(IconProperties), new FrameworkPropertyMetadata(null));

        public static Geometry GetTag1(DependencyObject obj)
        {
            return (Geometry)obj.GetValue(Tag1Property);
        }

        public static void SetTag1(DependencyObject obj, Geometry value)
        {
            obj.SetValue(Tag1Property, value);
        }
        #endregion

        #region Button Icon Path Data Mouse Over

        public static readonly DependencyProperty TagOnMouseOverProperty =
            DependencyProperty.RegisterAttached("TagOnMouseOver", typeof(Geometry), typeof(IconProperties), new FrameworkPropertyMetadata(null));

        public static Geometry GetTagOnMouseOver(DependencyObject obj)
        {
            return (Geometry)obj.GetValue(TagOnMouseOverProperty);
        }

        public static void SetTagOnMouseOver(DependencyObject obj, Geometry value)
        {
            obj.SetValue(TagOnMouseOverProperty, value);
        }
        #endregion

        #region Button Icon Path Data Mouse Over 1

        public static readonly DependencyProperty TagOnMouseOverProperty1 =
           DependencyProperty.RegisterAttached("TagOnMouseOver1", typeof(Geometry), typeof(IconProperties), new FrameworkPropertyMetadata(null));

        public static Geometry GetTagOnMouseOver1(DependencyObject obj)
        {
            return (Geometry)obj.GetValue(TagOnMouseOverProperty1);
        }

        public static void SetTagOnMouseOver1(DependencyObject obj, Geometry value)
        {
            obj.SetValue(TagOnMouseOverProperty1, value);
        }
        #endregion

        #region Button Icon Path Data Mouse Pressed

        public static readonly DependencyProperty TagOnPressedProperty =
           DependencyProperty.RegisterAttached("TagOnPressed", typeof(Geometry), typeof(IconProperties), new FrameworkPropertyMetadata(null));

        public static Geometry GetTagOnPressed(DependencyObject obj)
        {
            return (Geometry)obj.GetValue(TagOnPressedProperty);
        }

        public static void SetTagOnPressed(DependencyObject obj, Geometry value)
        {
            obj.SetValue(TagOnPressedProperty, value);
        }
        #endregion

        #region Button Icon Path Data Mouse Pressed 1

        public static readonly DependencyProperty TagOnPressedProperty1 =
            DependencyProperty.RegisterAttached("TagOnPressed1", typeof(Geometry), typeof(IconProperties), new FrameworkPropertyMetadata(null));

        public static Geometry GetTagOnPressed1(DependencyObject obj)
        {
            return (Geometry)obj.GetValue(TagOnPressedProperty1);
        }

        public static void SetTagOnPressed1(DependencyObject obj, Geometry value)
        {
            obj.SetValue(TagOnPressedProperty1, value);
        }
        #endregion

        #region Button Icon Path Data Fill 

        public static readonly DependencyProperty TagFillProperty =
            DependencyProperty.RegisterAttached("TagFill", typeof(SolidColorBrush), typeof(IconProperties), new FrameworkPropertyMetadata(null));

        public static SolidColorBrush GetTagFill(DependencyObject obj)
        {
            return (SolidColorBrush)obj.GetValue(TagFillProperty);
        }

        public static void SetTagFill(DependencyObject obj, SolidColorBrush value)
        {
            obj.SetValue(TagFillProperty, value);
        }
        #endregion

        #region Icon Path Data Fill Mouse Over

        public static readonly DependencyProperty TagFillMouseOverProperty =
            DependencyProperty.RegisterAttached("TagFillMouseOver", typeof(SolidColorBrush), typeof(IconProperties), new FrameworkPropertyMetadata(null));

        public static SolidColorBrush GetTagFillMouseOver(DependencyObject obj)
        {
            return (SolidColorBrush)obj.GetValue(TagFillMouseOverProperty);
        }

        public static void SetTagFillMouseOver(DependencyObject obj, SolidColorBrush value)
        {
            obj.SetValue(TagFillMouseOverProperty, value);
        }
        #endregion

        #region Icon Path Data Fill Mouse Pressed

        public static readonly DependencyProperty TagFillMousePressedProperty =
            DependencyProperty.RegisterAttached("TagFillMousePressed", typeof(SolidColorBrush), typeof(IconProperties), new FrameworkPropertyMetadata(null));

        public static SolidColorBrush GetTagFillMousePressed(DependencyObject obj)
        {
            return (SolidColorBrush)obj.GetValue(TagFillMousePressedProperty);
        }

        public static void SetTagFillMousePressed(DependencyObject obj, SolidColorBrush value)
        {
            obj.SetValue(TagFillMousePressedProperty, value);
        }
        #endregion

        #region Button Icon Path Data Stroke Color

        public static readonly DependencyProperty TagStrokeProperty =
            DependencyProperty.RegisterAttached("TagStroke", typeof(SolidColorBrush), typeof(IconProperties), new FrameworkPropertyMetadata(null));

        public static SolidColorBrush GetTagStroke(DependencyObject obj)
        {
            return (SolidColorBrush)obj.GetValue(TagStrokeProperty);
        }

        public static void SetTagStroke(DependencyObject obj, SolidColorBrush value)
        {
            obj.SetValue(TagStrokeProperty, value);
        }
        #endregion

        #region Button Icon Path Data Stroke Color Mouse Over

        public static readonly DependencyProperty TagStrokeMouseOverProperty =
            DependencyProperty.RegisterAttached("TagStrokeMouseOver", typeof(SolidColorBrush), typeof(IconProperties), new FrameworkPropertyMetadata(null));

        public static SolidColorBrush GetTagStrokeMouseOver(DependencyObject obj)
        {
            return (SolidColorBrush)obj.GetValue(TagStrokeMouseOverProperty);
        }

        public static void SetTagStrokeMouseOver(DependencyObject obj, SolidColorBrush value)
        {
            obj.SetValue(TagStrokeMouseOverProperty, value);
        }
        #endregion

        #region Button Icon Path Data Stroke Color Mouse Pressed

        public static readonly DependencyProperty TagStrokeMousePressedProperty =
            DependencyProperty.RegisterAttached("TagStrokeMousePressed", typeof(SolidColorBrush), typeof(IconProperties), new FrameworkPropertyMetadata(null));

        public static SolidColorBrush GetTagStrokeMousePressed(DependencyObject obj)
        {
            return (SolidColorBrush)obj.GetValue(TagStrokeMousePressedProperty);
        }

        public static void SetTagStrokeMousePressed(DependencyObject obj, SolidColorBrush value)
        {
            obj.SetValue(TagStrokeMousePressedProperty, value);
        }
        #endregion

        #region Button Icon Path Data Stroke Thicness

        public static readonly DependencyProperty TagStrokeThicknessProperty =
            DependencyProperty.RegisterAttached("TagStrokeThickenss", typeof(int), typeof(IconProperties), new FrameworkPropertyMetadata(null));

        public static int GetTagStrokeThickenss(DependencyObject obj)
        {
            return (int)obj.GetValue(TagStrokeThicknessProperty);
        }

        public static void SetTagStrokeThickenss(DependencyObject obj, int value)
        {
            obj.SetValue(TagStrokeThicknessProperty, value);
        }
        #endregion

        #region Button Icon Path Data Stroke Thicness Mouse Over

        public static readonly DependencyProperty TagStrokeThicknessMouseOverProperty =
            DependencyProperty.RegisterAttached("TagStrokeThicknessMouseOver", typeof(int), typeof(IconProperties), new FrameworkPropertyMetadata(null));

        public static int GetTagStrokeThicknessMouseOver(DependencyObject obj)
        {
            return (int)obj.GetValue(TagStrokeThicknessMouseOverProperty);
        }

        public static void SetTagStrokeThicknessMouseOver(DependencyObject obj, int value)
        {
            obj.SetValue(TagStrokeThicknessMouseOverProperty, value);
        }
        #endregion

        #region Button Icon Path Data Stroke Thicness Mouse Pressed

        public static readonly DependencyProperty TagStrokeThicknessMousePressedProperty =
            DependencyProperty.RegisterAttached("TagStrokeThicknessMousePressed", typeof(int), typeof(IconProperties), new FrameworkPropertyMetadata(null));

        public static int GetTagStrokeThicknessMousePressed(DependencyObject obj)
        {
            return (int)obj.GetValue(TagStrokeThicknessMousePressedProperty);
        }

        public static void SetTagStrokeThicknessMousePressed(DependencyObject obj, int value)
        {
            obj.SetValue(TagStrokeThicknessMousePressedProperty, value);
        }
        #endregion

        #region Button Icon Width

        public static readonly DependencyProperty TagIconWidthProperty =
            DependencyProperty.RegisterAttached("TagIconWidth", typeof(int), typeof(IconProperties), new FrameworkPropertyMetadata(null));

        public static int GetTagIconWidth(DependencyObject obj)
        {
            return (int)obj.GetValue(TagIconWidthProperty);
        }

        public static void SetTagIconWidth(DependencyObject obj, int value)
        {
            obj.SetValue(TagIconWidthProperty, value);
        }
        #endregion

        #region Button Icon Height

        public static readonly DependencyProperty TagIconHeightProperty =
            DependencyProperty.RegisterAttached("TagIconHeight", typeof(int), typeof(IconProperties), new FrameworkPropertyMetadata(null));

        public static int GetTagIconHeight(DependencyObject obj)
        {
            return (int)obj.GetValue(TagIconHeightProperty);
        }

        public static void SetTagIconHeight(DependencyObject obj, int value)
        {
            obj.SetValue(TagIconHeightProperty, value);
        }
        #endregion

    }






}
