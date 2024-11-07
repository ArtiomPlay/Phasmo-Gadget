using PhasmoGadget.HotKeyAPI.Structs;

namespace PhasmoGadget.HotKeyAPI.Contracts {
    public interface IHotKeyRecorder {
        bool RegisterHotKey(HotKey hotKey);
        bool Unregister(int hotKeyId);
        bool Unregister(HotKey hotKey);
        void CleanHotKeys();
    }
}
