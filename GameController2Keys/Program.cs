using CsvHelper;
using CsvHelper.Configuration;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Threading;
using System.Windows.Forms;
using WindowsInput;
using WindowsInput.Native;

using XnaInput = Microsoft.Xna.Framework.Input;

namespace GameController2Keys
{
    partial class Program
    {
        public static Dictionary<Tuple<String, String>, String> Combinations = 
            new Dictionary<Tuple<string,string>,string>();
        private static string config;

        [DllImport("user32.dll", CharSet = CharSet.Auto, CallingConvention = CallingConvention.StdCall)]
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

        private static KeyboardSimulator simulator = new KeyboardSimulator(new InputSimulator());

        
        public static void KeepCheckingState()
        {
            var previousPressedButtons = new List<String>();
            var justPressed = new List<String>();
            var justReleased = new List<String>();
            var down = new List<String>();
            string action;
            bool shouldStop = false;

            while (!shouldStop)
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

                justPressed = pressedButtons.Except(previousPressedButtons).ToList();
                justReleased = previousPressedButtons.Except(pressedButtons).ToList();
                down = down.Except(justReleased).ToList();
                down.AddRange(justPressed);

                if (rightX != 0 || rightY != 0)
                {
                    Cursor.Position = new System.Drawing.Point(
                        Cursor.Position.X + (int)(rightX * 10),
                        Cursor.Position.Y - (int)(rightY * 10));
                }
                if (leftY != 0)
                {
                    Scroll(leftY * 10);
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
                    if (Combinations.TryGetValue(trigger, out action))
                    {
                        var _action = ParseKeysString(action);
                        var actionModifiers = _action.Item1.Split(' ');
                        var actionKey = _action.Item2;

                        VirtualKeyCode modifierCode;
                        var modifierCodes = new List<VirtualKeyCode>();
                        foreach (var modifier in actionModifiers)
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

        private static Dictionary<Tuple<String, String>, String> ReadCombinations(string path)
        {
            using (var stream = new FileStream(path, FileMode.Open, FileAccess.Read, FileShare.ReadWrite))
            using (var reader = new StreamReader(stream))
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

        private static Tuple<String, String> ParseKeysString(string trigger)
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

        public static string ConfigPath = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\.XKeys");

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            Debug.WriteLine("OnChanged");
            Combinations = ReadCombinations(ConfigPath);
        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            Debug.WriteLine("OnRenamed");
            Combinations = ReadCombinations(ConfigPath);
        }

        private static void Main()
        {
            var watcher = new FileSystemWatcher
            {
                Path = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%"),
                //Filter = ".XKeys",
                //NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName
            };
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);
            watcher.EnableRaisingEvents = true;
            Debug.WriteLine(string.Format("watcher path: {0}", watcher.Path));
            if(File.Exists(ConfigPath)) Combinations = ReadCombinations(ConfigPath);
            var gamePadThread = new Thread(new ThreadStart(KeepCheckingState));
            gamePadThread.Start();
            Application.Run(new MainForm());
        }
    }
}