using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jumper {

    //drops a command from the app
    public class RemoveCommand : Command {

        public RemoveCommand(JumperSettings settings, ArgumentReader reader)
            : base(settings, reader) {
        }

        public override void Run() {
            string name = this.Arguments.GetSwitch("-remove");

            //make sure a command name was present
            if (string.IsNullOrEmpty(name)) {
                Console.WriteLine("You need to provide a name of the command to remove.");
                return;
            }
            else if (!this.Settings.HasCommand(name)) {
                Console.WriteLine("No command named '{0}' was found to remove.", name);
                return;
            }

            //removes the command from the app
            Console.WriteLine("Removing command '{0}'", name);
            this.Settings.RemoveCommand(name);
            this.Settings.Save();

        }

    }

}
