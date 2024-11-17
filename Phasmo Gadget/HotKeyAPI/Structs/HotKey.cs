using System;

namespace PhasmoGadget.HotKeyAPI.Structs {
    [Serializable]
    public class HotKey : ICloneable, IEquatable<HotKey> {
        private const int _maxId=49151;
        private static int _id;
        [NonSerialized]
        private int _actualId;

        public static HotKey From(int keyCode) {
            return new HotKey() {
                KeyCode=keyCode,
                ControlPressed=true
            };
        }

        public static HotKey From(
            int keyCode,
            bool plusControl,
            bool plusAlt,
            bool plusShift,
            bool plusWindows
            ){
            return new HotKey() {
                KeyCode=keyCode,
                ControlPressed=plusControl,
                AltPressed=plusAlt,
                WindowPressed=plusWindows,
                ShiftPressed=plusShift
            };
        }

        public HotKey() => this.Id=this.CalculateId();
        public int Id {
            get => this._actualId;
            private set => this._actualId = value;
        }
        public bool ControlPressed {get;set;}
        public bool AltPressed {get;set;}
        public bool ShiftPressed {get;set;}
        public bool WindowPressed {get;set;}
        public virtual int KeyCode {get;set;}

        public object Clone()=>this.MemberwiseClone();

        public override int GetHashCode() {
            return Convert.ToInt32(this.ControlPressed)+Convert.ToInt32(this.AltPressed)+Convert.ToInt32(this.AltPressed)+Convert.ToInt32(this.ShiftPressed)+this.KeyCode;
        }

        public override bool Equals(object obj) {
            if((object)this==obj)
                return true;
            return !(obj as HotKey==(HotKey)null)&&this.Equals(obj as HotKey);
        }
        public bool Equals(HotKey other) => this==other;

        public static bool operator ==(HotKey attr1, HotKey attr2) {
            if((object)attr1==(object)attr2)
                return true;
            return (object)attr1!=null&&(object)attr2!=null&&attr1.KeyCode==attr2.KeyCode&&attr1.ControlPressed==attr2.ControlPressed&&attr1.AltPressed==attr2.AltPressed&&attr1.WindowPressed==attr2.WindowPressed&&attr1.ShiftPressed==attr2.ShiftPressed;
        }
        public static bool operator !=(HotKey attr1, HotKey attr2) => !(attr1 == attr2);

        private int CalculateId() {
            int id=HotKey._id;
            ++HotKey._id;
            return id;
        }
    }
}
