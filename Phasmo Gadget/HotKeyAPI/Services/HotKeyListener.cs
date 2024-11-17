using System;
using System.Runtime.InteropServices;
using PhasmoGadget.HotKeyAPI.Contracts;
using PhasmoGadget.HotKeyAPI.Structs;

namespace PhasmoGadget.HotKeyAPI.Services {
    public class HotKeyListener:IHotKeyListener {
        private const uint WM_HOTKEY=786;
        private readonly HotKeyFactory _factory;
        private IntPtr _handle;
        private HotKeyListener.GetMsgProc _callBack;

        public event EventHandler<HotKeyPressedEventArgs> OnHotKeyPressed;

        public bool IsListening => this._handle != IntPtr.Zero;

        public HotKeyListener() {
            this._factory=new HotKeyFactory();
            this._callBack=new HotKeyListener.GetMsgProc(this.CallBack);
            this._handle=IntPtr.Zero;
        }
        
        public void StartListening() {
            int currentThreadId=(int)HotKeyListener.GetCurrentThreadId();
            this._handle=HotKeyListener.SetWindowsHookEx(HookType.WH_GETMESSAGE,this._callBack,IntPtr.Zero,currentThreadId);
        }
        public void StopListening() {
            if(!HotKeyListener.UnhookWinEvent(this._handle)) {
                return;
            }
            this._handle=IntPtr.Zero;
        }

        private IntPtr CallBack(int code,IntPtr wParam,IntPtr lParam) {
            WindowMessage structure=(WindowMessage)Marshal.PtrToStructure(lParam,typeof(WindowMessage));
            if(structure.message==786U) {
                int key=(int)structure.lParam>>16 & (int)ushort.MaxValue;
                int modifiers=(int)structure.lParam & (int)ushort.MaxValue;
                if(this.OnHotKeyPressed!=null) {
                    this.OnHotKeyPressed((object)this,new HotKeyPressedEventArgs(this.GetPressed(key,modifiers)));
                }
            }
            return HotKeyListener.CallNextHookEx(IntPtr.Zero,code,wParam,lParam);
        }

        private HotKey GetPressed(int key,int modifiers) {
            HotKeyFactory hotKeyFactory = this._factory.CreateHotKey(key);
            switch(modifiers) {
                case 1:
                    hotKeyFactory=hotKeyFactory.WithAltPressed();
                    break;
                case 2:
                    hotKeyFactory=hotKeyFactory.WithControlPressed();
                    break;
                case 3:
                    hotKeyFactory=hotKeyFactory.WithControlPressed().WithAltPressed();
                    break;
                case 4:
                    hotKeyFactory=hotKeyFactory.WithShiftPressed();
                    break;
                case 5:
                    hotKeyFactory=hotKeyFactory.WithAltPressed().WithShiftPressed();
                    break;
                case 6:
                    hotKeyFactory=hotKeyFactory.WithControlPressed().WithShiftPressed();
                    break;
                case 7:
                    hotKeyFactory=hotKeyFactory.WithAltPressed().WithControlPressed();
                    break;
                case 8:
                    hotKeyFactory=hotKeyFactory.WithWindowPressed();
                    break;
                case 9:
                    hotKeyFactory=hotKeyFactory.WithWindowPressed().WithAltPressed();
                    break;
                case 10:
                    hotKeyFactory=hotKeyFactory.WithWindowPressed().WithControlPressed();
                    break;
                case 11:
                    hotKeyFactory=hotKeyFactory.WithWindowPressed().WithAltPressed().WithControlPressed();
                    break;
                case 12:
                    hotKeyFactory=hotKeyFactory.WithWindowPressed().WithShiftPressed();
                    break;
                case 13:
                    hotKeyFactory=hotKeyFactory.WithWindowPressed().WithShiftPressed().WithAltPressed();
                    break;
                case 14:
                    hotKeyFactory=hotKeyFactory.WithWindowPressed().WithShiftPressed().WithControlPressed();
                    break;
                case 15:
                    hotKeyFactory=hotKeyFactory.WithWindowPressed().WithShiftPressed().WithAltPressed().WithControlPressed();
                    break;
            }
            return hotKeyFactory.GetHotKey();
        }

        [DllImport("kernel32.dll")]
        private static extern uint GetCurrentThreadId();

        [DllImport("kernel32.dll")]
        private static extern IntPtr GetModuleHandle(string name);

        [DllImport("user32.dll",SetLastError=true)]
        private static extern IntPtr SetWindowsHookEx(
            HookType hookType,
            HotKeyListener.GetMsgProc lpfn,
            IntPtr hMod,
            int dwThreadId);

        [DllImport("user32.dll")]
        private static extern bool UnhookWinEvent(IntPtr hWinEventHook);

        [DllImport("user32.dll")]
        private static extern IntPtr CallNextHookEx(
            IntPtr hhk,
            int nCode,
            IntPtr wParam,
            IntPtr lParam);

        private delegate IntPtr GetMsgProc(int code,IntPtr wParam, IntPtr lParam);
    }
}
