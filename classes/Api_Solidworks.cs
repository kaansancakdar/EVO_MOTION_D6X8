using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.AccessControl;
using System.Text;
using System.Threading.Tasks;
using SolidWorks.Interop.sldworks;
using SolidWorks.Interop.swconst;

namespace Solidworks_Api
{
    internal class Api_Solidworks
    {
        public SldWorks swApp;
        ModelDoc2 swDoc;
        ModelView myModelView;

        ModelDocExtension Extn;
        MassProperty MyMassProp;

        //Feature swFeature;

        public void RandomRotate()
        {

            swDoc = swApp.ActiveDoc;
            myModelView = ((ModelView)(swDoc.ActiveView));

            myModelView.RotateAboutCenter(-1.8730103729387098, -3.5845057162270511);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.25921125696919645, -0.71844618881274935);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.01672330690123848, -0.023175683510088689);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.00706, 0.0084200000000000056);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.0075200000000000111, -0.00312000000000001);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.13378645520990784, -0.772522783669623);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.10452066813274051, -0.91930211256685135);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0, -0.10815318971374722);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.01672330690123848, -0.18540546808070951);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.02926578707716734, -0.20085592375410197);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.0081799999999999876, -0.0060999999999999934);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.00049999999999998657, 0.00354000000000001);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.13378645520990784, 0);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.17141389573769442, 0.054076594856873612);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.15050976211114633, 0.069527050530266074);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.38463605872848505, 0.32445956914124169);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.17141389573769442, 0.16222978457062084);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.21740298971610025, 0.20085592375410197);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.14632893538583672, 0.13132887322383593);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.10452066813274051, 0.16995501240731706);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.0836165345061924, 0.13905410106053215);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.0418082672530962, 0.084977506203658529);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.03344661380247696, 0.06180182269356984);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.02926578707716734, 0.054076594856873612);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.01672330690123848, 0.01545045567339246);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.00418082672530962, 0);

            ModelView swModelView = null;
            swModelView = ((ModelView)(swDoc.ActiveView));
            swModelView.RollBy(0);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.41808267253096204, -1.6918248962364746);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.03344661380247696, -0.29355865779445678);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.079435707780882783, -0.55621640424212859);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.05853157415433468, -0.44806321452838138);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.037627440527786583, -0.20858115159079821);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.00836165345061924, -0.03090091134678492);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0, -0.00772522783669623);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.010099999999999998, -0.00052000000000000939);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.026940000000000013, 0.00516);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.0082600000000000017, -0.0062799999999999974);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.0062400000000000016, 0.0081199999999999935);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.0022800000000000155, -0.0092400000000000034);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.0041799999999999858, 0.00064000000000000688);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.013660000000000005, 0.0016800000000000148);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.00023999999999999578, -0.011639999999999996);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.0057599999999999874, 0.011639999999999996);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.0062799999999999974, -0.008080000000000009);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.12668000000000001, 0.0037999999999999813);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.01432, 0.0016399999999999971);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.01714, 0.016480000000000005);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.026500000000000013, 0.012819999999999988);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.036240000000000008, -0.0057599999999999986);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.049139999999999996, 0.002100000000000002);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.028120000000000003, 0.012419999999999999);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.0082000000000000024, 0.0043599999999999976);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.02142, -0.00232);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.00948, 0.0036599999999999966);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.00928, 0.0022800000000000042);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.007040000000000002, 0.0096999999999999986);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.013519999999999989, 0.011260000000000004);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.0053999999999999829, -0.010500000000000002);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.00855999999999999, -0.0014399999999999997);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.034780000000000012, -0.00426);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.019459999999999991, -0.004859999999999998);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.04596, -0.03056);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.0093200000000000071, -0.00076000000000000514);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.063319999999999987, 0.0038200000000000013);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.046540000000000005, -0.007619999999999994);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.06898, 0.0074799999999999979);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.03384, -0.0016399999999999971);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.03262, -0.022619999999999998);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.016080000000000007, -0.012280000000000003);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.011099999999999978, -0.011020000000000009);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.0077600000000000342, -0.010139999999999995);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.012539999999999996, -0.0053400000000000114);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.013660000000000005, -0.0012599999999999946);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.0066599999999999776, 0.011280000000000002);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.019240000000000014, 0.034980000000000011);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.022619999999999998, 0.0012600000000000057);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.03752, 0.0055999999999999826);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.0082600000000000225, -0.0019199999999999996);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.031099999999999996, -0.0072000000000000067);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.042779999999999957, -0.0006000000000000006);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.08504000000000006, 0.0033800000000000054);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.07382, -0.013080000000000003);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.08804, -0.015599999999999992);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.062159999999999993, -0.011020000000000009);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.024339999999999942, -0.0043199999999999905);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.021600000000000109, -0.0038200000000000013);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.011139999999999973, -0.01342000000000001);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.014200000000000124, -0.012480000000000003);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.045639999999999951, -0.019419999999999993);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.072780000000000025, -0.0087400000000000151);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.075859999999999955, 0.00960000000000001);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.24926, 0.068840000000000012);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.27616000000000007, 0.0096999999999999986);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(0.00875999999999999, 0.0032999999999999922);
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(-0.00875999999999999, -0.0032999999999999922);

            myModelView.EnableGraphicsUpdate = true;
        }

        /*
         * X, Y: Translation value in meter and relative to XY axex of the graphic area 
         */
        public void TranslateModel(double X, double Y)
        {
            swDoc = swApp.ActiveDoc;
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.TranslateBy(X, Y);
        }

        public void RollModel(double Angle)
        {
            swDoc = swApp.ActiveDoc;
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RollBy(Angle);
        }
        /*
         * Angle: Rotate Angle
         * Ptx, Pty, Ptz: Center of rotation
         * AxisX, AxisY, AxisZ: Rotate Axis
         */
        public void RoateAboutAxis(double Angle, double Ptx, double Pty, double Ptz, double AxisX, double AxisY, double AxisZ)
        {
            swDoc = swApp.ActiveDoc;
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutAxis(Angle, Ptx, Pty, Ptz, AxisX, AxisY, AxisZ);
        }

        public void RotateAboutCenter(double angleX, double angleY)
        {
            swDoc = swApp.ActiveDoc;
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.RotateAboutCenter(angleX, angleY);
        }

        public double[] GetCenterofMass()
        {
            swDoc = (ModelDoc2)swApp.ActiveDoc;
            Extn = swDoc.Extension;
            // Create mass properties
            MyMassProp = Extn.CreateMassProperty();
            
            // Use document property units (MKS)
            MyMassProp.UseSystemUnits = false;
            var CenterOfMass = (double[])MyMassProp.CenterOfMass;

            return CenterOfMass;
        }

        public void HideShowDesignTree(bool SH)
        {
            swDoc = (ModelDoc2)swApp.ActiveDoc;
            Extn = swDoc.Extension;
            bool bRet = Extn.HideFeatureManager(SH);
        }

        public void HideShowCenterofMass(bool SH)
        {
            swDoc = (ModelDoc2)swApp.ActiveDoc;
            swDoc.SetUserPreferenceToggle((int)swUserPreferenceToggle_e.swDisplayCenterOfMassSymbol, SH);
        }

        public void ZoomtoFit()
        {
            swDoc = (ModelDoc2)swApp.ActiveDoc;
            swDoc.ViewZoomtofit2();
        }

        #region Solidworks Views
        public void IsometricView()
        {
            swDoc = (ModelDoc2)swApp.ActiveDoc;
            swDoc.ShowNamedView2("*Isometric", 7);
            swDoc.ViewZoomtofit2();
        }

        public void BackView()
        {
            swDoc = (ModelDoc2)swApp.ActiveDoc;
            swDoc.ShowNamedView2("*Back", 2);
            swDoc.ViewZoomtofit2();
        }

        public void BottomView()
        {
            swDoc = (ModelDoc2)swApp.ActiveDoc;
            swDoc.ShowNamedView2("*Bottom", 6);
            swDoc.ViewZoomtofit2();
        }

        public void FrontView()
        {
            swDoc = (ModelDoc2)swApp.ActiveDoc;
            swDoc.ShowNamedView2("*Front", 1);
            swDoc.ViewZoomtofit2();
        }

        public void LeftView()
        {
            swDoc = (ModelDoc2)swApp.ActiveDoc;
            swDoc.ShowNamedView2("*Left", 3);
            swDoc.ViewZoomtofit2();
        }

        public void RightView()
        {
            swDoc = (ModelDoc2)swApp.ActiveDoc;
            swDoc.ShowNamedView2("*Right", 4);
            swDoc.ViewZoomtofit2();
        }

        public void TopView()
        {
            swDoc = (ModelDoc2)swApp.ActiveDoc;
            swDoc.ShowNamedView2("*Top", 5);
            swDoc.ViewZoomtofit2();
        }
        #endregion

        public void ZoombyFactor(double zoomFactor)
        {
            swDoc = swApp.ActiveDoc;
            myModelView = ((ModelView)(swDoc.ActiveView));
            myModelView.ZoomByFactor(zoomFactor);
        }
    }
}
