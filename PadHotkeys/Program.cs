using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Input;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;
using XnaInput = Microsoft.Xna.Framework.Input;

namespace PadHotkeys
{
    public partial class Program
    {
        public static Dictionary<Tuple<String, String>, String> Combinations =
            new Dictionary<Tuple<string, string>, string>();

        public static void KeepCheckingState()
        {
            var previousPressedButtons = new List<String>();
            var justPressed = new List<String>();
            var justReleased = new List<String>();
            var down = new List<String>();
            string actionDefinition;
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
                    Mouse.Scroll(leftY * 10);
                }
                var mouseActions = new List<String>() { "LeftShoulder", "RightShoulder" };
                var pressedMouseButtons = justPressed.Intersect(mouseActions).ToList();
                var releasedMouseButtons = justReleased.Intersect(mouseActions).ToList();

                if (justPressed.Count() > 0)
                {
                    var triggerModifiers = string.Join(" ", previousPressedButtons);
                    var triggerKey = justPressed[0];

                    actionDefinition = "";
                    var trigger = Tuple.Create(triggerModifiers, triggerKey);
                    if (Combinations.TryGetValue(trigger, out actionDefinition))
                    {
                        Action action;
                        if (Action.Parse(actionDefinition, out action))
                            action.OnPress();
                    }
                }
                if (justReleased.Count() > 0)
                {
                    var triggerKey = justReleased[0];
                    actionDefinition = "";
                    var trigger = Tuple.Create(string.Empty, triggerKey);
                    if (Combinations.TryGetValue(trigger, out actionDefinition))
                    {
                        Action action;
                        if (Action.Parse(actionDefinition, out action))
                            action.OnRelease();
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
                var rows = new List<Row>();
                while(true)
                {
                    var line = reader.ReadLine();
                    if (line == null)
                        break;
                    if (line.Trim().First() == '#' || string.IsNullOrWhiteSpace(line))
                        continue;
                    var row = new Row();
                    var delimiterIndex = line.IndexOf("..");
                    row.Trigger = line.Substring(0, delimiterIndex);
                    row.Action = line.Substring(delimiterIndex + 2);
                    rows.Add(row);
                }
                var result = new Dictionary<Tuple<String, String>, String>();
                foreach (var row in rows)
                {
                    result.Add(Action.ParseKeyCombination(row.Trigger), row.Action);
                }
                return result;
            }
        }

           
        public static string ConfigPath = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%\.XKeys");

        private static void OnChanged(object source, FileSystemEventArgs e)
        {
            Combinations = ReadCombinations(ConfigPath);
        }

        private static void OnRenamed(object source, RenamedEventArgs e)
        {
            Combinations = ReadCombinations(ConfigPath);
        }

        private static void Main()
        {
            var watcher = new FileSystemWatcher
            {
                Path = Environment.ExpandEnvironmentVariables(@"%USERPROFILE%"),
                Filter = ".XKeys",
                //NotifyFilter = NotifyFilters.LastWrite | NotifyFilters.FileName
            };
            watcher.Changed += new FileSystemEventHandler(OnChanged);
            watcher.Created += new FileSystemEventHandler(OnChanged);
            watcher.Deleted += new FileSystemEventHandler(OnChanged);
            watcher.Renamed += new RenamedEventHandler(OnRenamed);
            watcher.EnableRaisingEvents = true;
            if (File.Exists(ConfigPath)) Combinations = ReadCombinations(ConfigPath);
            var gamePadThread = new Thread(new ThreadStart(KeepCheckingState));
            gamePadThread.Start();
            Application.Run(new MainForm());
        }
    }
}