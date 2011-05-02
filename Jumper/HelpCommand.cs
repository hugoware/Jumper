using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jumper {

    //shows help information for using the app
    public class HelpCommand : Command {

        public HelpCommand(JumperSettings settings, ArgumentReader reader)
            : base(settings, reader) {
        }

        public override void Run() {

            Console.WriteLine(string.Format("Jumper Version {0}", JumperSettings.VersionNumber));
            Console.WriteLine(@"Simple command line tool to create shortcuts for common commands.

Visit http://github.com/hugoware/jumper

Add / Update Command
  -add:commandName CMD TO EXECUTE
   Adds a new command. The command to execute should ALWAYS be the last 
   parameter on the list.

Add Options
  -quiet:true|false
   Sets ECHO OFF for the command (default is true)

  -rebase:true|false 
   Changes the execution directory to match the directory when the command 
   was added (default false)

  -args:<any string value>
   Sets the character prefix to expect for arguments when executing the
   command. Arguments are replaced by index starting at zero. (default is %)
   example - - -
     Command : CD C:\Temp\%0\%1
     Usage   : j temp dir file 
     Result  : CD C:\Temp\dir\file

Remove Command
   -remove:commandName
    Deletes a command entirely

Displaying Commands
   -list
    Shows all commands and the total number of arguments it expects

");
            
            

        }

    }

}
