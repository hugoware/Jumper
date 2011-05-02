using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jumper {

    //determines the correct command to display
    public class CommandLocator {

        public CommandLocator(JumperSettings settings, ArgumentReader reader) {
            this._Settings = settings;
            this._Reader = reader;
        }

        private readonly JumperSettings _Settings;
        private readonly ArgumentReader _Reader;

        //detects the command type from the arguments provided
        public Command CreateCommand() {
            return this._Reader.CommandType == CommandTypes.List ? this.ActivateCommand<ListCommand>()
                : this._Reader.CommandType == CommandTypes.Add ? this.ActivateCommand<AddCommand>()
                : this._Reader.CommandType == CommandTypes.Remove ? this.ActivateCommand<RemoveCommand>()
                : this._Reader.CommandType == CommandTypes.Help ? this.ActivateCommand<HelpCommand>()
                : this._Reader.CommandType == CommandTypes.Run ? this.ActivateCommand<RunCommand>()
                : this.ActivateCommand<UnknownCommand>();
        }

        //creates an instance of a specific command
        public Command ActivateCommand<T>() where T : Command {
            return Activator.CreateInstance(typeof(T), new object[] { this._Settings, this._Reader }) as T;
        }

    }
}
