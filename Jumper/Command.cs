using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jumper {

    public abstract class Command {

        #region Constructors

        public Command(JumperSettings settings, ArgumentReader reader) {
            this.Arguments = reader;
            this.Settings = settings;
        }

        #endregion

        #region Properties

        protected ArgumentReader Arguments { get; set; }
        protected JumperSettings Settings { get; set; }

        #endregion

        #region Methods
        
        public abstract void Run();

        #endregion

    }

}
