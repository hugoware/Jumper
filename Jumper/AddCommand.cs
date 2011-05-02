using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jumper {

    public class AddCommand : Command {

        public AddCommand(JumperSettings settings, ArgumentReader reader)
            : base(settings, reader) {
        }

        //adds a new command 
        public override void Run() {
            string name = this.Arguments.GetSwitch("-add");

            //make sure a command name was present
            if (string.IsNullOrEmpty(name)) {
                Console.WriteLine("You need to provide a name of the command to add.");
                return;
            }
            else if (string.IsNullOrEmpty(this.Arguments.Command)) {
                Console.WriteLine("The command to add is blank.");
                return;
            }

            //build the command to save
            JumperCommand command = new JumperCommand {
                Name = name,
                Command = this.Arguments.Command
            };

            //check alternate settings
            if (this.Arguments.GetSwitch("-rebase").Equals("true", StringComparison.OrdinalIgnoreCase)) 
                command.Path = Environment.CurrentDirectory;

            //check alternate settings
            command.QuietMode = true;
            if (this.Arguments.GetSwitch("-quiet").Equals("false", StringComparison.OrdinalIgnoreCase))
                command.QuietMode = false;

            //check for an alternate argument prefix
            command.ArgumentPrefix = this.Arguments.GetSwitch("-args");

            //check of this replaces or not
            if (this.Settings.HasCommand(name)) Console.WriteLine("Replacing command '{0}'", name);
            else Console.WriteLine("Added command '{0}'", name);
            this.Settings.AddCommand(command);
            this.Settings.Save();

        }

    }

}
