using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Reflection;
using System.IO;
using System.Xml.Serialization;

namespace Jumper {

    public class JumperSettings {

        #region Constants

        private const string DEFAULT_SHORTCUT_BAT_SCRIPT = @"
@ECHO OFF
""%~dp0jumper.exe"" %*
IF NOT EXIST ""%~dp0__JUMPER_EXECUTE.BAT"" GOTO QUIT
@ECHO ON
@""%~dp0__JUMPER_EXECUTE.BAT""
@ECHO OFF
DEL ""%~dp0__JUMPER_EXECUTE.BAT""
:QUIT";

        #endregion


        #region Constructors

        public JumperSettings() {
            this.Commands = new List<JumperCommand>();
        }

        #endregion

        #region Properties

        //the list of commands being used
        public List<JumperCommand> Commands { get; set; }

        #endregion

        #region Loading / Saving

        //loads the settings from memory
        public static JumperSettings Load() {

            //check for the settings first
            if (!File.Exists(JumperSettings.SettingsPath)) return new JumperSettings();

            //try and load the settings
            var serializer = new XmlSerializer(typeof(JumperSettings));
            using (var stream = File.Open(JumperSettings.SettingsPath, FileMode.Open))
                return serializer.Deserialize(stream) as JumperSettings;

        }

        //saves the settings
        public void Save() {
            var serializer = new XmlSerializer(typeof(JumperSettings));
            using (var stream = File.Open(JumperSettings.SettingsPath, FileMode.Create))
                serializer.Serialize(stream, this);
        }

        //makes sure the shortcut command item is available
        public static void VerifyJumpBatFile() {
            if (File.Exists(JumperSettings.ShortcutBATFile)) return;
            File.WriteAllText(JumperSettings.ShortcutBATFile, DEFAULT_SHORTCUT_BAT_SCRIPT);
        }

        #endregion

        #region Methods

        //gets the first commnd matching the name provided
        public JumperCommand GetCommandByName(string name) {
            return this.Commands.FirstOrDefault(item => item.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        //checks if a command name exists or not
        public bool HasCommand(string name) {
            return this.GetCommandByName(name) is JumperCommand;
        }

        //includes a new command
        public void AddCommand(JumperCommand command) {
            this.RemoveCommand(command.Name);   
            this.Commands.Add(command);
        }

        //drops a command
        public void RemoveCommand(string name) {
            this.Commands.RemoveAll(item => item.Name.Equals(name, StringComparison.OrdinalIgnoreCase));
        }

        #endregion

        #region Static Properties
        
        public static readonly string VersionNumber = "1.0";

        //gets the path to the settings for the app
        internal static string SettingsPath {
            get { return Path.Combine(JumperSettings.ApplicationDirectory, "settings.xml"); }
        }

        //the folder the app resides in
        internal static string ApplicationDirectory {
            get { return Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location); }
        }

        internal static string BATFile {
            get { return Path.Combine(JumperSettings.ApplicationDirectory, "__jumper_execute.bat"); }
        }

        internal static string ShortcutBATFile {
            get { return Path.Combine(JumperSettings.ApplicationDirectory, "j.bat"); }
        }

        #endregion

    }

}
