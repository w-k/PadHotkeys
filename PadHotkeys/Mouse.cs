using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using WindowsInput.Native;

namespace PadHotkeys
{
    public static class Mouse
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        private static Dictionary<VirtualKeyCode, uint> DownEvents = new Dictionary<VirtualKeyCode, uint>
        {
            {VirtualKeyCode.LBUTTON, 0x02},
            {VirtualKeyCode.RBUTTON, 0x08},
            {VirtualKeyCode.MBUTTON, 0x00000020},
        };

        private static Dictionary<VirtualKeyCode, uint> UpEvents = new Dictionary<VirtualKeyCode, uint>
        {
            {VirtualKeyCode.LBUTTON, 0x04},
            {VirtualKeyCode.RBUTTON, 0x10},
            {VirtualKeyCode.MBUTTON, 0x00000040},
        };

        public static void Press(VirtualKeyCode code)
        {
            var X = Cursor.Position.X;
            var Y = Cursor.Position.Y;
            var mouseEvent = DownEvents[code];
            mouse_event(mouseEvent, (uint)X, (uint)Y, 0, 0);
        }

        public static void Release(VirtualKeyCode code)
        {
            var X = Cursor.Position.X;
            var Y = Cursor.Position.Y;
            var mouseEvent = UpEvents[code];
            mouse_event(mouseEvent, (uint)X, (uint)Y, 0, 0);
        }

        private const int Wheel = 0x800;

        public static void Scroll(float stickY)
        {
            mouse_event(Wheel, 0, 0, (uint)stickY, 0);
        }
    }
}
