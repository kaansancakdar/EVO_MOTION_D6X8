using Button_Tags;
using HidSharp;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Media;

namespace USB_Communication
{
    internal class USB_Connection
    {
        public HidStream stream;
        public HidDevice device;
        public bool connection_state;
        public bool device_open_state=false;

        public int[] raw_values = new int[7];
        byte[] bytesRaw;

        public bool Connect_Device()
        {
            var devicelist = DeviceList.Local;
            var deviceList_ = devicelist.GetHidDevices();
            foreach (var dev in deviceList_)
            {
                if (dev.VendorID == 1155 && dev.ProductID == 22352 && dev.GetMaxInputReportLength() == 65)
                {
                    device = dev as HidDevice;
                    connection_state = true;
                    break;
                }
                else
                {
                    connection_state = false;
                }
            }
            if(connection_state== false)
            {
                DialogResult result = MessageBox.Show("Device not found. Please plug the device","Device Connection Error",MessageBoxButtons.RetryCancel,MessageBoxIcon.Error);
                if(result == DialogResult.Retry)
                {
                    Connect_Device();
                }
            }
            return connection_state;
        }

        public float Map(float value, float leftMin, float leftMax, float rightMin, float rightMax)
        {
            return rightMin + (value - leftMin) * (rightMax - rightMin) / (leftMax - leftMin);
        }

        public byte[] USB_Read(HidDevice _device)
        {

            if (!_device.TryOpen(out stream))
            {
                MessageBox.Show("Failed to open device.", "Error", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }

            stream = device.Open();

            if (!stream.CanRead || !stream.CanWrite)
            {
                MessageBox.Show("Failed to open HID stream for reading and writing.");
                return null;
            }
            bytesRaw = new byte[_device.GetMaxInputReportLength()];
            try
            {
                int count = stream.Read(bytesRaw);
            }
            catch
            {

            }
            finally
            {
                stream.Close();
                //stream.Dispose();
            }

            return bytesRaw;;
        }
    }
}
