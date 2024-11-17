using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using PhasmoGadget.HotKeyAPI.Contracts;
using PhasmoGadget.HotKeyAPI.Structs;

namespace PhasmoGadget.HotKeyAPI.Services {
    public class HotKeyRecorder:IHotKeyRecorder {
        private const int ERROR_HOTKEY_ARLEADY_REGISTERED = 1409;
        private readonly IntPtr _owner;
        private static IDictionary<HotKey,IntPtr> _storedHotKeys=(IDictionary<HotKey,IntPtr>)new Dictionary<HotKey,IntPtr>();

        public HotKeyRecorder(IntPtr owner) => this._owner=owner;
        public HotKeyRecorder() => this._owner=IntPtr.Zero;

        public bool RegisterHotKey(HotKey hotKey) {
            int num=HotKeyRecorder.RegisterHotKey(this._owner,hotKey.Id,this.TranslateModifiers(hotKey),hotKey.KeyCode);
            if(Marshal.GetLastWin32Error()==1409) {
                return false;
            }
            if(num==0) {
                throw new UnauthorizedAccessException(string.Format("Cannot Register a HotKey to the specified handle -> {0}",(object)this._owner));
            }
            HotKeyRecorder._storedHotKeys.Add(hotKey,this._owner);
            return true;
        }

        public bool Unregister(HotKey hotkey) {
            int num=HotKeyRecorder.UnregisterHotKey(this._owner,hotkey.Id)!=0?1:0;
            if(num==0) {
                return num!=0;
            }
            HotKeyRecorder._storedHotKeys.Remove(hotkey);
            return num!=0;
        }
        public bool Unregister(int hotkeyId) {
            HotKeyRecorder.UnregisterHotKey(this._owner,hotkeyId);
            HotKey key=HotKeyRecorder._storedHotKeys.SingleOrDefault<KeyValuePair<HotKey,IntPtr>>((Func<KeyValuePair<HotKey,IntPtr>,bool>)(pair => pair.Key.Id==hotkeyId)).Key;
            return key!=(HotKey)null&&this.Unregister(key);
        }

        public void CleanHotKeys() {
            foreach(KeyValuePair<HotKey,IntPtr> storedHotKey in (IEnumerable<KeyValuePair<HotKey,IntPtr>>)HotKeyRecorder._storedHotKeys)
                HotKeyRecorder.UnregisterHotKey(storedHotKey.Value,storedHotKey.Key.Id);
            HotKeyRecorder._storedHotKeys.Clear();
        }

        private uint TranslateModifiers(HotKey hotKey) {
            return (uint)((hotKey.AltPressed?1:0)|(hotKey.ControlPressed?2:0)|(hotKey.ShiftPressed?4:0)|(hotKey.WindowPressed?8:0));
        }

        [DllImport("user32.dll",SetLastError = true)]
        private static extern int RegisterHotKey(IntPtr owner,int id,uint modifiers,int keyCode);

        [DllImport("user32.dll",SetLastError = true)]
        private static extern int UnregisterHotKey(IntPtr owner,int id);
    }
}
