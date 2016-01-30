using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PadHotkeys
{
    public class ShellAction : Action
    {
        private string _fileName;
        private string _arguments;
        public ShellAction(string command)
        {
            int firstSpace;
            if(command.First() == '"')
            {
                var index = command.IndexOf("\" ", 1);
                if (index != -1)
                   firstSpace = index + 1;
                else
                   firstSpace = index;
            }
            else
            {
                firstSpace = command.IndexOf(' ');
            }
            if(firstSpace > -1)
            {
                _fileName = command.Substring(0, firstSpace);
                _arguments = command.Substring(firstSpace);
            }
            else
            {
                _fileName = command;
            }
        }

        public override void OnPress()
        {
            try
            {

                if (string.IsNullOrEmpty(_arguments))
                    Process.Start(_fileName);
                else
                    Process.Start(_fileName, _arguments);
            }
            catch(Exception e)
            {
                Debug.WriteLine(string.Format("file name: {0}", _fileName));
                Debug.WriteLine(string.Format("arguments: {0}", _arguments));
                Debug.WriteLine(e.Message);
                Debug.WriteLine(e.StackTrace);
            }
        }

        public override void OnRelease()
        {
            return;
        }
    }
}
