using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;

namespace Jumper {


    public class RunCommand : Command {

        public RunCommand(JumperSettings settings, ArgumentReader reader)
            : base(settings, reader) {
        }

        public override void Run() {

            //get the parts to execute
            string[] parts = this.Arguments.Command.Split(new char[] { ' ' });
            string name = parts.First();
            parts = parts.Skip(1).ToArray();
            
            //try and find the command to execute
            JumperCommand command = this.Settings.GetCommandByName(name);
            if (command == null) {
                Console.WriteLine("No command was found with the name '{0}'.", name);
                return;
            }

            //build the actual string to execute 
            string execute = command.BuildCommand(parts);

            //create the BAT to run
            var builder = new StringBuilder();
            builder.AppendLine("@ECHO OFF");

            //construct the command
            if (command.ShouldRepath) {
                builder.AppendLine("SET JUMPER_START_DIRECTORY=%CD%");
                builder.AppendLine(string.Format("CD /D \"{0}\"", command.Path));
            }

            //include the command to run
            if (!command.QuietMode) builder.AppendLine("@ECHO ON");
            builder.AppendLine(string.Format("CALL {0}", execute));
            builder.AppendLine("@ECHO OFF");

            //return to the original directory if needed
            if (command.ShouldRepath)
                builder.AppendLine("CD /D %JUMPER_START_DIRECTORY%");

            //save the command to execute immediately
            File.WriteAllText(JumperSettings.BATFile, builder.ToString());

        }

    }

}
