using System;
using PhasmoGadget.HotKeyAPI.Structs;

namespace PhasmoGadget.HotKeyAPI.Contracts {
    public class HotKeyPressedEventArgs:EventArgs {
        public HotKeyPressedEventArgs(HotKey hotKey) => this.HotKey=hotKey;
        public HotKey HotKey {get;set;}
    }
}
