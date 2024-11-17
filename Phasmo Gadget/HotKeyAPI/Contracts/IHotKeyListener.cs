using System;

namespace PhasmoGadget.HotKeyAPI.Contracts {
    public interface IHotKeyListener {
        event EventHandler<HotKeyPressedEventArgs> OnHotKeyPressed;

        bool IsListening { get;}
        void StartListening();
        void StopListening();
    }
}
