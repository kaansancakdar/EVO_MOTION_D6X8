using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using INFITF;
using MECMOD;
using PARTITF;
using ProductStructureTypeLib;
using System.Runtime.InteropServices;
using System.Windows.Forms;

namespace catia_api
{
    internal class Api_Catia
    {
        INFITF.Application mycatia = null;
        public bool flag_CATIA = false;
        //double firstzoom;

        public INFITF.Application GetCatia()
        {

            mycatia = (INFITF.Application)Marshal.GetActiveObject("CATIA.Application");
            return mycatia;
        }

        public void ZoomIn()
        {
            Viewer viwer = (Viewer)mycatia.ActiveWindow.ActiveViewer;
            viwer.ZoomIn();
        }

        public void ZoomOut()
        {
            Viewer viwer = (Viewer)mycatia.ActiveWindow.ActiveViewer;
            viwer.ZoomOut();
        }

        public void ReFrame()
        {
            Viewer viwer = (Viewer)mycatia.ActiveWindow.ActiveViewer;
            viwer.Reframe();
        }

        public void RotateModel(Array Axis, double Angle)
        {
            //Axis Array = [XAxis, YAxis, ZAxis]
            //For example X Axis rotate axis array = [1,0,0]
            Viewer3D viwer = (Viewer3D)mycatia.ActiveWindow.ActiveViewer;
         
            viwer.Rotate(Axis, Angle);
            viwer.Update();
        }

        public void TranslateModel(Array vector)
        {
            Viewer3D viwer = (Viewer3D)mycatia.ActiveWindow.ActiveViewer;
            
            viwer.Translate(vector);
            viwer.Update();
        }

        public void deneme()
        {
            Viewer3D cam = (Viewer3D)mycatia.ActiveWindow.ActiveViewer;

            Viewpoint3D aaa = cam.Viewpoint3D;
            double focus=aaa.FocusDistance;
            object[] origin = { 0,0,0};
            object[] sightdirection = { 0, 0, 0 };

            aaa.GetOrigin(origin);
            aaa.GetSightDirection(sightdirection);

            for (int i=0; i<=2; i++)
            {
                origin[i] = (double)origin[i] + focus * (double)sightdirection[i];
            }
            cam.Update();
        }

        #region Catia Views
        public void IsometricView()
        {
            Viewer3D viwer = (Viewer3D)mycatia.ActiveWindow.ActiveViewer;
            viwer.Viewpoint3D = (Viewpoint3D)mycatia.ActiveDocument.Cameras.Item(1);
            
            viwer.Reframe();
        }

        public void FrontView()
        {
            Viewer3D viwer = (Viewer3D)mycatia.ActiveWindow.ActiveViewer;
            viwer.Viewpoint3D = (Viewpoint3D)mycatia.ActiveDocument.Cameras.Item(2);

            viwer.Reframe();
        }

        public void BackView()
        {
            Viewer3D viwer = (Viewer3D)mycatia.ActiveWindow.ActiveViewer;
            viwer.Viewpoint3D = (Viewpoint3D)mycatia.ActiveDocument.Cameras.Item(3);

            viwer.Reframe();
        }

        public void LeftView()
        {
            Viewer3D viwer = (Viewer3D)mycatia.ActiveWindow.ActiveViewer;
            viwer.Viewpoint3D = (Viewpoint3D)mycatia.ActiveDocument.Cameras.Item(4);

            viwer.Reframe();
        }

        public void RightView()
        {
            Viewer3D viwer = (Viewer3D)mycatia.ActiveWindow.ActiveViewer;
            viwer.Viewpoint3D = (Viewpoint3D)mycatia.ActiveDocument.Cameras.Item(5);

            viwer.Reframe();
        }

        public void TopView()
        {
            Viewer3D viwer = (Viewer3D)mycatia.ActiveWindow.ActiveViewer;
            viwer.Viewpoint3D = (Viewpoint3D)mycatia.ActiveDocument.Cameras.Item(6);

            viwer.Reframe();
        }

        public void BottomView()
        {
            Viewer3D viwer = (Viewer3D)mycatia.ActiveWindow.ActiveViewer;
            viwer.Viewpoint3D = (Viewpoint3D)mycatia.ActiveDocument.Cameras.Item(7);

            viwer.Reframe();
        }
        #endregion

        public object[] GetOrigin()
        {
            Viewer3D viwer = (Viewer3D)mycatia.ActiveWindow.ActiveViewer;
            object[] origin = new object[3];
            viwer.Viewpoint3D.GetOrigin(origin);
            return origin;
        }

        public void ZoombyFactor(double ZoomFactor)
        {
            Viewer3D viwer = (Viewer3D)mycatia.ActiveWindow.ActiveViewer;
            viwer.Viewpoint3D.Zoom = ZoomFactor;
            viwer.Update();
        }
    }
}
