using PhasmoGadget.HotKeyAPI.Contracts;
using PhasmoGadget.HotKeyAPI.Structs;

namespace PhasmoGadget.HotKeyAPI.Services {
    public class HotKeyFactory:IHotKeyFactory {
        private bool _startedCreating;
        private int _keyCode;
        private bool _controlPressed;
        private bool _altPressed;
        private bool _shiftPressed;
        private bool _windowPressed;

        public HotKeyFactory CreateHotKey(int keyCode) {
            this._startedCreating=true;
            this._keyCode=keyCode;
            this._controlPressed=this._altPressed=this._shiftPressed=this._windowPressed=false;
            return this;
        }

        public HotKeyFactory WithControlPressed() {
            this._controlPressed=true;
            return this;
        }
        public HotKeyFactory WithAltPressed() {
            this._altPressed=true;
            return this;
        }
        public HotKeyFactory WithShiftPressed() {
            this._shiftPressed=true;
            return this;
        }
        public HotKeyFactory WithWindowPressed() {
            this._windowPressed=true;
            return this;
        }

        public HotKey GetHotKey() {
            return !this._startedCreating ? (HotKey)null:HotKey.From(this._keyCode,this._controlPressed,this._altPressed,this._shiftPressed,this._windowPressed);
        }
    }
}
