using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Jumper {

    //kinds of commands to execute
    public enum CommandTypes {
        List,
        Add,
        Remove,
        Run,
        Help,
        Undefined,
        TooManyCommands
    }

}
