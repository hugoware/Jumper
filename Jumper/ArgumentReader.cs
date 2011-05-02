using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Jumper {

    //handles finding the command detail
    public class ArgumentReader {

        #region Constructors

        public ArgumentReader(string[] args) {
            this.Arguments = args;
        }

        #endregion

        #region Properties

        private readonly string[] Arguments;

        //the actual command to execute
        public string Command {
            get {
                if (this._Command == null) this._GetCommand();
                return this._Command;
            }
        }
        private string _Command;

        //the kind of command found
        public CommandTypes CommandType {
            get {
                if (this._CommandType == CommandTypes.Undefined) this._DetectCommandType();
                return this._CommandType;
            }
        }
        private CommandTypes _CommandType = CommandTypes.Undefined;

        #endregion

        #region Methods
        
        //determines the command being run (if any)
        private void _GetCommand() {
            this._Command = string.Join(" ", this.Arguments.SkipWhile(item => item.StartsWith("-")).ToArray()).Trim();
        }

        //finds the command to use
        private void _DetectCommandType() {
            this._TryLocateSwitch("-list", CommandTypes.List);
            this._TryLocateSwitch("-help", CommandTypes.Help);
            this._TryLocateSwitch("-remove", CommandTypes.Remove);
            this._TryLocateSwitch("-add", CommandTypes.Add);
            this._TryLocateSwitch("-help", CommandTypes.Add);
            
            //check to see if this might just be a command to execute
            if (this._CommandType != CommandTypes.Undefined) return;
            if (!string.IsNullOrEmpty(this.Command)) this._CommandType = CommandTypes.Run;
            if (string.IsNullOrEmpty(this.Command)) this._CommandType = CommandTypes.Help;
        }

        //tries to locate and set a switch value
        private void _TryLocateSwitch(string key, CommandTypes value) {
            if (this._CommandType == CommandTypes.TooManyCommands) return;
            if (this.HasSwitch(key)) this._CommandType = value;
        }

        //finds the value from the command line
        private string _LocateSwitch(string key) {
            string keyWithValue = string.Concat(key, ":");
            
            //return the full value if it exists
            foreach (string item in this.Arguments)
                if (item.StartsWith(keyWithValue, StringComparison.OrdinalIgnoreCase) || item.Equals(key)) return item;
            return null;
        }

        //checks if the swtich is found in the 
        public bool HasSwitch(string key) {
            return this._LocateSwitch(key) is string;
        }

        //gets the value of a switch
        public string GetSwitch(string key) {
            string value = this._LocateSwitch(key);
            int ignored = key.Length + 1;
            return value is string && value.Length > ignored 
                ? value.Substring(ignored) 
                : string.Empty;
        }

        #endregion

    }

}
