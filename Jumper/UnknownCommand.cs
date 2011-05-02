using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jumper {


    public class UnknownCommand : Command {

        public UnknownCommand(JumperSettings settings, ArgumentReader reader)
            : base(settings, reader) {
        }

        public override void Run() {
            Console.WriteLine("No matching commands were found.");
            Console.WriteLine();
            (new HelpCommand(this.Settings, this.Arguments)).Run();
        }

    }

}
