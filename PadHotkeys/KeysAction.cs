using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput;
using WindowsInput.Native;

namespace PadHotkeys
{
    public class KeysAction : Action
    {
        private VirtualKeyCode _actionCode;
        private List<VirtualKeyCode> _modifierCodes;
        private static KeyboardSimulator simulator = new KeyboardSimulator(new InputSimulator());
        
        private static List<VirtualKeyCode> mouseCodes = new List<VirtualKeyCode>
        {
            VirtualKeyCode.LBUTTON,
            VirtualKeyCode.RBUTTON,
            VirtualKeyCode.MBUTTON,
            VirtualKeyCode.XBUTTON1,
            VirtualKeyCode.XBUTTON1
        };

        public KeysAction(VirtualKeyCode actionCode, List<VirtualKeyCode> modifierCodes)
        {
            _actionCode = actionCode;
            _modifierCodes = modifierCodes;
        }

        public override void OnPress()
        {
            if (mouseCodes.Contains(_actionCode))
            {
                Mouse.Press(_actionCode);
            }
            else
            {
                if (_modifierCodes.Count() > 0)
                {
                    simulator.ModifiedKeyStroke(_modifierCodes, _actionCode);
                }
                else
                {
                    simulator.KeyPress(_actionCode);
                }
            }
        }

        public override void OnRelease()
        {
            if (mouseCodes.Contains(_actionCode))
            {
                Mouse.Release(_actionCode);
            }
        }
    }
}
