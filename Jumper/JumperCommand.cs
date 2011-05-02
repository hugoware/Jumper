using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;

namespace Jumper {

    /// <summary>
    /// Saved commands to execute
    /// </summary>
    public class JumperCommand {

        #region Properties

        public string Name { get; set; }
        public string Command { get; set; }
        public string ArgumentPrefix { get; set; }
        public string Path { get; set; }
        public bool QuietMode { get; set; }

        public bool ShouldRepath {
            get { return !string.IsNullOrEmpty(this.Path); }
        }

        public bool IncludeArguments {
            get { return !string.IsNullOrEmpty(this.ArgumentPrefix); }
        }

        #endregion

        #region Methods

        public void GenerateCommand() {
        }

        //gets the total count of arguments found
        public int GetArgumentCount() {
            if (!this.IncludeArguments) return 0;
            return Regex.Matches(this.Command, string.Format(@"{0}\d+", Regex.Escape(this.ArgumentPrefix))).Count;
        }

        //creates the command to run from the batch file
        public string BuildCommand(string[] args) {
            if (!this.IncludeArguments) return this.Command;

            //replace arguments as required
            string command = this.Command;
            for (int i = 0; i < args.Length; i++) {
                string key = string.Concat(this.ArgumentPrefix, i);
                string value = args[i];
                command = command.Replace(key, value);
            }

            //return the generated value
            return command;
        }

        #endregion

    }

}
