using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jumper {

    public class ListCommand : Command {

        public ListCommand(JumperSettings settings, ArgumentReader reader)
            : base(settings, reader) {
        }

        public override void Run() {

            //get the longest command value
            int longest = this.Settings.Commands.Max(item => item.Name.Length) + 5;

            //add each item
            Console.Write("Name".PadRight(longest));
            Console.WriteLine("Args (if any)");
            Console.WriteLine("- - - - - - - - - - - - - - - - - - - - - - - - - - - -");

            //show each item
            foreach (JumperCommand command in this.Settings.Commands) {
                int count = command.GetArgumentCount();
                Console.Write(command.Name.PadRight(longest));
                Console.WriteLine(count);
            }

            Console.WriteLine();

        }

    }

}
