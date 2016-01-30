using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WindowsInput.Native;

namespace PadHotkeys
{
    public abstract class Action
    {
        public static Tuple<String, String> ParseKeyCombination(string trigger)
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

        private static bool CreateKeyAction(Tuple<string, string> parsed, out Action action)
        {
            action = null;
            var actionModifiers = parsed.Item1.Split(' ');
            var actionKey = parsed.Item2;

            VirtualKeyCode modifierCode;
            var modifierCodes = new List<VirtualKeyCode>();
            foreach (var modifier in actionModifiers)
            {
                if (KeyCodes.Map.TryGetValue(modifier, out modifierCode))
                {
                    modifierCodes.Add(modifierCode);
                }
            }
            VirtualKeyCode actionCode;
            if (KeyCodes.Map.TryGetValue(actionKey, out actionCode))
            {
                action = new KeysAction(actionCode, modifierCodes);
                return true;
            }
            return false;
        }

        public static bool Parse(string actionDefinition, out Action action)
        {
            action = null;
            actionDefinition = actionDefinition.Trim();
            if(actionDefinition.StartsWith("run "))
            {
                action = new ShellAction(actionDefinition.Substring(4));
                return true;
            }
            else
            {
                var parsed = ParseKeyCombination(actionDefinition);
                return CreateKeyAction(parsed, out action);
            }
        }
        public abstract void OnPress();

        public abstract void OnRelease();

    }
}
