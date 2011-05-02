using System;
using System.Collections.Generic;
using System.Text;
using System.IO;

namespace Jumper {
    class Program {

        //usage
        // j command args
        // j -add:command -repath:false -arg:% <COMMAND TO RUN>
        // j -remove:command
        // j -list

        static void Main(string[] args) {
            
            //make sure the previous version is gone
            if (File.Exists(JumperSettings.BATFile))
                File.Delete(JumperSettings.BATFile);

            //verify the shortu
            JumperSettings.VerifyJumpBatFile();

            //execute the app as required
            var settings = JumperSettings.Load();
            var reader = new ArgumentReader(args);
            var locator = new CommandLocator(settings, reader);
            var command = locator.CreateCommand();
            command.Run();

        }

    }

}
