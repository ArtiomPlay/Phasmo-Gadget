using PhasmoGadget.HotKeyAPI.Services;
using PhasmoGadget.HotKeyAPI.Structs;

namespace PhasmoGadget.HotKeyAPI.Contracts {
    public interface IHotKeyFactory {
        HotKeyFactory CreateHotKey(int keyCode);
        HotKeyFactory WithControlPressed();
        HotKeyFactory WithAltPressed();
        HotKeyFactory WithShiftPressed();
        HotKeyFactory WithWindowPressed();
        HotKey GetHotKey();
    }
}
