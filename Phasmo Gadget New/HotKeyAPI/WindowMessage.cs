using System;

namespace PhasmoGadget.HotKeyAPI {
    internal struct WindowMessage {
        public IntPtr hwnd;
        public uint message;
        public IntPtr wParam;
        public IntPtr lParam;
        public uint time;
        public PointStructure pt;
    }
}
