using System;using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Xna.Framework.Input;
using XnaInput = Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework;
using System.Threading;
using System.Windows.Forms;
using CsvHelper;
using System.IO;
using CsvHelper.Configuration;
using WindowsInput;
using WindowsInput.Native;
using System.Runtime.InteropServices;


namespace GameController2Keys
{

    class Row
    {
        public string Trigger { get; set; }
        public string Action { get; set; }
    }

    class RowMap : CsvClassMap<Row>
    {
        public RowMap()
        {
            Map(m => m.Trigger).Index(0);
            Map(m => m.Action).Index(1);
        }
    }

    class Program
    {

        static Dictionary<Tuple<String, String>, String> combinations;
        static string config;


        [DllImport("user32.dll",CharSet=CharSet.Auto, CallingConvention=CallingConvention.StdCall)]
        public static extern void mouse_event(uint dwFlags, uint dx, uint dy, uint cButtons, uint dwExtraInfo);

        private const int LeftDown = 0x02;
        private const int LeftUp = 0x04;
        private const int RightDown = 0x08;
        private const int RightUp = 0x10;
        private const int MiddleDown = 0x00000020;
        private const int MiddleUp = 0x00000040;
        private const int Wheel = 0x800;
        
        public static void LeftClick()
        {
            int X = Cursor.Position.X;
            int Y = Cursor.Position.Y;
            mouse_event(LeftDown | LeftUp, (uint)X, (uint)Y, 0, 0);
        }

        public static void LeftPress()
        {
            int X = Cursor.Position.X;
            int Y = Cursor.Position.Y;
            mouse_event(LeftDown, (uint)X, (uint)Y, 0, 0);
            Console.WriteLine("Left mouse pressed");
        }

        public static void LeftRelease()
        {
            int X = Cursor.Position.X;
            int Y = Cursor.Position.Y;
            mouse_event(LeftUp, (uint)X, (uint)Y, 0, 0);
            Console.WriteLine("Left mouse released");

        }

        public static void MiddleClick()
        {
            int X = Cursor.Position.X;
            int Y = Cursor.Position.Y;
            mouse_event(MiddleDown | MiddleUp, (uint)X, (uint)Y, 0, 0);
        }

        public static void Scroll(float stickY)
        {
            mouse_event(Wheel, 0, 0, (uint)stickY, 0);
        }

        static KeyboardSimulator simulator = new KeyboardSimulator(new InputSimulator());
        static Dictionary<String, VirtualKeyCode> codes = new Dictionary<String, VirtualKeyCode>() {
            { "lbutton", VirtualKeyCode.LBUTTON },
            { "rbutton", VirtualKeyCode.RBUTTON },
            { "cancel", VirtualKeyCode.CANCEL },
            { "mbutton", VirtualKeyCode.MBUTTON },
            { "xbutton1", VirtualKeyCode.XBUTTON1 },
            { "xbutton2", VirtualKeyCode.XBUTTON2 },
            { "back", VirtualKeyCode.BACK },
            { "tab", VirtualKeyCode.TAB },
            { "clear", VirtualKeyCode.CLEAR },
            { "return", VirtualKeyCode.RETURN },
            { "shift", VirtualKeyCode.SHIFT },
            { "control", VirtualKeyCode.CONTROL },
            { "alt", VirtualKeyCode.MENU },
            { "pause", VirtualKeyCode.PAUSE },
            { "capital", VirtualKeyCode.CAPITAL },
            { "kana", VirtualKeyCode.KANA },
            { "hangeul", VirtualKeyCode.HANGEUL },
            { "hangul", VirtualKeyCode.HANGUL },
            { "junja", VirtualKeyCode.JUNJA },
            { "final", VirtualKeyCode.FINAL },
            { "kanji", VirtualKeyCode.KANJI },
            { "hanja", VirtualKeyCode.HANJA },
            { "escape", VirtualKeyCode.ESCAPE },
            { "convert", VirtualKeyCode.CONVERT },
            { "nonconvert", VirtualKeyCode.NONCONVERT },
            { "accept", VirtualKeyCode.ACCEPT },
            { "modechange", VirtualKeyCode.MODECHANGE },
            { "space", VirtualKeyCode.SPACE },
            { "pgup", VirtualKeyCode.PRIOR },
            { "pgdn", VirtualKeyCode.NEXT },
            { "end", VirtualKeyCode.END },
            { "home", VirtualKeyCode.HOME },
            { "left", VirtualKeyCode.LEFT },
            { "up", VirtualKeyCode.UP },
            { "right", VirtualKeyCode.RIGHT },
            { "down", VirtualKeyCode.DOWN },
            { "select", VirtualKeyCode.SELECT },
            { "print", VirtualKeyCode.PRINT },
            { "execute", VirtualKeyCode.EXECUTE },
            { "snapshot", VirtualKeyCode.SNAPSHOT },
            { "insert", VirtualKeyCode.INSERT },
            { "delete", VirtualKeyCode.DELETE },
            { "help", VirtualKeyCode.HELP },
            { "0", VirtualKeyCode.VK_0 },
            { "1", VirtualKeyCode.VK_1 },
            { "2", VirtualKeyCode.VK_2 },
            { "3", VirtualKeyCode.VK_3 },
            { "4", VirtualKeyCode.VK_4 },
            { "5", VirtualKeyCode.VK_5 },
            { "6", VirtualKeyCode.VK_6 },
            { "7", VirtualKeyCode.VK_7 },
            { "8", VirtualKeyCode.VK_8 },
            { "9", VirtualKeyCode.VK_9 },
            { "a", VirtualKeyCode.VK_A },
            { "b", VirtualKeyCode.VK_B },
            { "c", VirtualKeyCode.VK_C },
            { "d", VirtualKeyCode.VK_D },
            { "e", VirtualKeyCode.VK_E },
            { "f", VirtualKeyCode.VK_F },
            { "g", VirtualKeyCode.VK_G },
            { "h", VirtualKeyCode.VK_H },
            { "i", VirtualKeyCode.VK_I },
            { "j", VirtualKeyCode.VK_J },
            { "k", VirtualKeyCode.VK_K },
            { "l", VirtualKeyCode.VK_L },
            { "m", VirtualKeyCode.VK_M },
            { "n", VirtualKeyCode.VK_N },
            { "o", VirtualKeyCode.VK_O },
            { "p", VirtualKeyCode.VK_P },
            { "q", VirtualKeyCode.VK_Q },
            { "r", VirtualKeyCode.VK_R },
            { "s", VirtualKeyCode.VK_S },
            { "t", VirtualKeyCode.VK_T },
            { "u", VirtualKeyCode.VK_U },
            { "v", VirtualKeyCode.VK_V },
            { "w", VirtualKeyCode.VK_W },
            { "x", VirtualKeyCode.VK_X },
            { "y", VirtualKeyCode.VK_Y },
            { "z", VirtualKeyCode.VK_Z },
            { "lwin", VirtualKeyCode.LWIN },
            { "rwin", VirtualKeyCode.RWIN },
            { "apps", VirtualKeyCode.APPS },
            { "sleep", VirtualKeyCode.SLEEP },
            { "numpad0", VirtualKeyCode.NUMPAD0 },
            { "numpad1", VirtualKeyCode.NUMPAD1 },
            { "numpad2", VirtualKeyCode.NUMPAD2 },
            { "numpad3", VirtualKeyCode.NUMPAD3 },
            { "numpad4", VirtualKeyCode.NUMPAD4 },
            { "numpad5", VirtualKeyCode.NUMPAD5 },
            { "numpad6", VirtualKeyCode.NUMPAD6 },
            { "numpad7", VirtualKeyCode.NUMPAD7 },
            { "numpad8", VirtualKeyCode.NUMPAD8 },
            { "numpad9", VirtualKeyCode.NUMPAD9 },
            { "multiply", VirtualKeyCode.MULTIPLY },
            { "add", VirtualKeyCode.ADD },
            { "separator", VirtualKeyCode.SEPARATOR },
            { "subtract", VirtualKeyCode.SUBTRACT },
            { "decimal", VirtualKeyCode.DECIMAL },
            { "divide", VirtualKeyCode.DIVIDE },
            { "f1", VirtualKeyCode.F1 },
            { "f2", VirtualKeyCode.F2 },
            { "f3", VirtualKeyCode.F3 },
            { "f4", VirtualKeyCode.F4 },
            { "f5", VirtualKeyCode.F5 },
            { "f6", VirtualKeyCode.F6 },
            { "f7", VirtualKeyCode.F7 },
            { "f8", VirtualKeyCode.F8 },
            { "f9", VirtualKeyCode.F9 },
            { "f10", VirtualKeyCode.F10 },
            { "f11", VirtualKeyCode.F11 },
            { "f12", VirtualKeyCode.F12 },
            { "f13", VirtualKeyCode.F13 },
            { "f14", VirtualKeyCode.F14 },
            { "f15", VirtualKeyCode.F15 },
            { "f16", VirtualKeyCode.F16 },
            { "f17", VirtualKeyCode.F17 },
            { "f18", VirtualKeyCode.F18 },
            { "f19", VirtualKeyCode.F19 },
            { "f20", VirtualKeyCode.F20 },
            { "f21", VirtualKeyCode.F21 },
            { "f22", VirtualKeyCode.F22 },
            { "f23", VirtualKeyCode.F23 },
            { "f24", VirtualKeyCode.F24 },
            { "numlock", VirtualKeyCode.NUMLOCK },
            { "scroll", VirtualKeyCode.SCROLL },
            { "lshift", VirtualKeyCode.LSHIFT },
            { "rshift", VirtualKeyCode.RSHIFT },
            { "lcontrol", VirtualKeyCode.LCONTROL },
            { "rcontrol", VirtualKeyCode.RCONTROL },
            { "lalt", VirtualKeyCode.LMENU },
            { "ralt", VirtualKeyCode.RMENU },
            { "browser_back", VirtualKeyCode.BROWSER_BACK },
            { "browser_forward", VirtualKeyCode.BROWSER_FORWARD },
            { "browser_refresh", VirtualKeyCode.BROWSER_REFRESH },
            { "browser_stop", VirtualKeyCode.BROWSER_STOP },
            { "browser_search", VirtualKeyCode.BROWSER_SEARCH },
            { "browser_favorites", VirtualKeyCode.BROWSER_FAVORITES },
            { "browser_home", VirtualKeyCode.BROWSER_HOME },
            { "volume_mute", VirtualKeyCode.VOLUME_MUTE },
            { "volume_down", VirtualKeyCode.VOLUME_DOWN },
            { "volume_up", VirtualKeyCode.VOLUME_UP },
            { "media_next_track", VirtualKeyCode.MEDIA_NEXT_TRACK },
            { "media_prev_track", VirtualKeyCode.MEDIA_PREV_TRACK },
            { "media_stop", VirtualKeyCode.MEDIA_STOP },
            { "media_play_pause", VirtualKeyCode.MEDIA_PLAY_PAUSE },
            { "launch_mail", VirtualKeyCode.LAUNCH_MAIL },
            { "launch_media_select", VirtualKeyCode.LAUNCH_MEDIA_SELECT },
            { "launch_app1", VirtualKeyCode.LAUNCH_APP1 },
            { "launch_app2", VirtualKeyCode.LAUNCH_APP2 },
            { "oem_1", VirtualKeyCode.OEM_1 },
            { "oem_plus", VirtualKeyCode.OEM_PLUS },
            { "oem_comma", VirtualKeyCode.OEM_COMMA },
            { "oem_minus", VirtualKeyCode.OEM_MINUS },
            { "oem_period", VirtualKeyCode.OEM_PERIOD },
            { "oem_2", VirtualKeyCode.OEM_2 },
            { "oem_3", VirtualKeyCode.OEM_3 },
            { "oem_4", VirtualKeyCode.OEM_4 },
            { "oem_5", VirtualKeyCode.OEM_5 },
            { "oem_6", VirtualKeyCode.OEM_6 },
            { "oem_7", VirtualKeyCode.OEM_7 },
            { "oem_8", VirtualKeyCode.OEM_8 },
            { "oem_102", VirtualKeyCode.OEM_102 },
            { "processkey", VirtualKeyCode.PROCESSKEY },
            { "packet", VirtualKeyCode.PACKET },
            { "attn", VirtualKeyCode.ATTN },
            { "crsel", VirtualKeyCode.CRSEL },
            { "exsel", VirtualKeyCode.EXSEL },
            { "ereof", VirtualKeyCode.EREOF },
            { "play", VirtualKeyCode.PLAY },
            { "zoom", VirtualKeyCode.ZOOM },
            { "noname", VirtualKeyCode.NONAME },
            { "pa1", VirtualKeyCode.PA1 },
            { "oem_clear", VirtualKeyCode.OEM_CLEAR }
        };

        public static void KeepCheckingState()
        {
            var previousPressedButtons = new List<String>();
            var justPressed = new List<String>();
            var justReleased = new List<String>();
            var down = new List<String>();
            string action;
            bool shouldStop = false;
            
            while(!shouldStop)
            {
                var gamePadState = GamePad.GetState(PlayerIndex.One);

                var leftX = gamePadState.ThumbSticks.Left.X;
                var leftY = gamePadState.ThumbSticks.Left.Y;

                var rightX = gamePadState.ThumbSticks.Right.X;
                var rightY = gamePadState.ThumbSticks.Right.Y;
                
                var buttonStates = new Dictionary<String, XnaInput.ButtonState>();
                buttonStates.Add("A", gamePadState.Buttons.A);
                buttonStates.Add("B", gamePadState.Buttons.B);
                buttonStates.Add("X", gamePadState.Buttons.X);
                buttonStates.Add("Y", gamePadState.Buttons.Y);
                buttonStates.Add("Back", gamePadState.Buttons.Back);
                buttonStates.Add("BigButton", gamePadState.Buttons.BigButton);
                buttonStates.Add("Start", gamePadState.Buttons.Start);
                buttonStates.Add("LeftShoulder", gamePadState.Buttons.LeftShoulder);
                buttonStates.Add("LeftStick", gamePadState.Buttons.LeftStick);
                buttonStates.Add("RightShoulder", gamePadState.Buttons.RightShoulder);
                buttonStates.Add("RightStick", gamePadState.Buttons.RightStick);
                buttonStates.Add("Down", gamePadState.DPad.Down);
                buttonStates.Add("Up", gamePadState.DPad.Up);
                buttonStates.Add("Left", gamePadState.DPad.Left);
                buttonStates.Add("Right", gamePadState.DPad.Right);

                List<String> pressedButtons = (from state in buttonStates
                                               where state.Value == XnaInput.ButtonState.Pressed
                                               select state.Key).ToList();

                if (pressedButtons.Contains("Back")) 
                    shouldStop = true;
                if (pressedButtons.Contains("LeftStick")) 
                    combinations = ReadCombinations(config);

                justPressed = pressedButtons.Except(previousPressedButtons).ToList();
                justReleased = previousPressedButtons.Except(pressedButtons).ToList();
                down = down.Except(justReleased).ToList();
                down.AddRange(justPressed);

                if (rightX != 0 || rightY !=0)
                {
                    Cursor.Position = new System.Drawing.Point(
                        Cursor.Position.X + (int)(rightX*10), 
                        Cursor.Position.Y - (int)(rightY*10));
                }
                if (leftY != 0)
                {
                    Scroll(leftY*10);
                }

                var mouseActions = new List<String>() { "LeftShoulder", "RightShoulder" };
                var pressedMouseButtons = justPressed.Intersect(mouseActions).ToList();
                var releasedMouseButtons = justReleased.Intersect(mouseActions).ToList();

               if (justPressed.Count() > 0)
                {
                    var triggerModifiers = string.Join(" ", previousPressedButtons);
                    var triggerKey = justPressed[0];

                    action = "";
                    var trigger = Tuple.Create(triggerModifiers, triggerKey);
                    Console.WriteLine("Trigger: " + String.Join(" ", trigger.Item1) + " + " + trigger.Item2);
                    if (combinations.TryGetValue(trigger, out action))
                    {
                        var _action = ParseKeysString(action);
                        var actionModifiers = _action.Item1.Split(' ');
                        var actionKey = _action.Item2;


                        VirtualKeyCode modifierCode;
                        var modifierCodes = new List<VirtualKeyCode>();
                        foreach(var modifier in actionModifiers)
                        {
                            if (codes.TryGetValue(modifier, out modifierCode))
                            {
                                modifierCodes.Add(modifierCode);
                            }
                        }

                        VirtualKeyCode actionCode;
                        if (codes.TryGetValue(actionKey, out actionCode))
                        {
                            if (modifierCodes.Count() > 0)
                            {
                                simulator.ModifiedKeyStroke(modifierCodes, actionCode);
                                Console.WriteLine(
                                    "ModifiedKeyStroke: " + 
                                    string.Join(" ", modifierCodes.Select(x => x.ToString())) + " + " + actionCode);
                            }

                            else
                            {
                                
                                simulator.KeyPress(actionCode);
                                Console.WriteLine("KeyPress: " + actionCode);
                            }
                        }                        
                    }
                    else
                    {
                        if (pressedMouseButtons.Count() > 0)
                        {
                            switch (pressedMouseButtons[0])
                            {
                                case "LeftShoulder":
                                    LeftPress();
                                    break;
                                case "RightShoulder":
                                    MiddleClick();
                                    break;
                            }
                        }
                        
                    }

                }
                if (justReleased.Count() > 0)
                {
                    if (releasedMouseButtons.Count() > 0)
                    {
                        switch (releasedMouseButtons[0])
                        {
                            case "LeftShoulder":
                                LeftRelease();
                                break;
                        }
                    }
                }

                previousPressedButtons = pressedButtons;
                Thread.Sleep(10);
            }
        }

        static Dictionary<Tuple<String, String>, String> ReadCombinations(string path)
        {
            using (var reader = new StreamReader(path))
            {
                var csv = new CsvReader(reader);
                csv.Configuration.Delimiter = "::";
                csv.Configuration.HasHeaderRecord = false;
                csv.Configuration.RegisterClassMap<RowMap>();
                var rows = csv.GetRecords<Row>().ToList();
                var trigger = ParseKeysString(rows[0].Trigger);
                var result = new Dictionary<Tuple<String, String>, String>();
                foreach (var row in rows)
                {
                    result.Add(ParseKeysString(row.Trigger), row.Action);
                }
                return result;
            }
            
        }

        static Tuple<String, String> ParseKeysString(string trigger)
        {
            if (trigger.Contains('+'))
            {
                var parts = trigger.Split('+');
                var modifiers = parts[0].Trim();
                var key = parts[1].Trim();
                return Tuple.Create(modifiers, key);
            }
            else
            {
                return Tuple.Create("", trigger.Trim());
            }
        }

        static void Main(string[] args)
        {
            try
            {
                if (args.Count() > 0)
                {
                    config = args[0];
                }
                else
                {
                    config = "config.csv";
                }
                combinations = ReadCombinations(config);
                var gamePadThread = new Thread(new ThreadStart(KeepCheckingState));
                gamePadThread.Start();
                gamePadThread.Join();
            }
            catch(FileNotFoundException)
            {
                Console.WriteLine("Configuration file not found");
            }
            
        }
    }
}
