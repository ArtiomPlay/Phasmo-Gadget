using System.CodeDom.Compiler;
using System.Configuration;
using System.Diagnostics;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace PhasmoGadget.PhasmoGadget.Properties {
    [CompilerGenerated]
    [GeneratedCode("Microsoft.VisualStudio.Editors.SettingsDesigner.SettingsSingleFileGenerator","16.10.0.0")]
    internal sealed class Settings:ApplicationSettingsBase{
        private static Settings defaultInstance=(Settings)SettingsBase.Synchronized((SettingsBase)new Settings());

        public static Settings Default => Settings.defaultInstance;

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("F1")]
        public string KeyID {
            get => (string)this[nameof(KeyID)];
            set => this[nameof(KeyID)]=(object)value;
        }
        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("F2")]
        public string TimerID {
            get => (string)this[nameof(TimerID)];
            set => this[nameof(TimerID)]=(object)value;
        }
        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("6")]
        public int Lang {
            get => (int)this[nameof(Lang)];
            set => this[nameof(Lang)]=(object)value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        public int PositionX {
            get => (int)this[nameof(PositionX)];
            set => this[nameof(PositionX)]=(object)value;
        }
        [UserScopedSetting]
        [DebuggerNonUserCode]
        public int PositionY {
            get => (int)this[nameof(PositionY)];
            set => this[nameof(PositionY)]=(object)value;
        }
        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("0,0")]
        public Point MousePos {
            get => (Point)this[nameof(MousePos)];
            set => this[nameof(MousePos)]=(object)value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("75")]
        public double Opacity {
            get => (double)this[nameof(Opacity)];
            set => this[nameof(Opacity)]=(object)value;
        }
        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("1")]
        public int Size {
            get => (int)this[nameof(Size)];
            set => this[nameof(Size)]=(object)value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("1")]
        public int SoundOnOff {
            get => (int)this[nameof(SoundOnOff)];
            set => this[nameof(SoundOnOff)]=(object)value;
        }
        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("0")]
        public int CompactMode {
            get => (int)this[nameof(CompactMode)];
            set => this[nameof(CompactMode)]=(object)value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("255")]
        public int TextColorR {
            get => (int)this[nameof(TextColorR)];
            set => this[nameof(TextColorR)]=(object)value;
        }
        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("255")]
        public int TextColorG {
            get => (int)this[nameof(TextColorG)];
            set => this[nameof(TextColorG)]=(object)value;
        }
        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("255")]
        public int TextColorB {
            get => (int)this[nameof(TextColorB)];
            set => this[nameof(TextColorB)]=(object)value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("255")]
        public int BoxForeColorR {
            get => (int)this[nameof(BoxForeColorR)];
            set => this[nameof(BoxForeColorR)]=(object)value;
        }
        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("255")]
        public int BoxForeColorG {
            get => (int)this[nameof(BoxForeColorG)];
            set => this[nameof(BoxForeColorG)]=(object)value;
        }
        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("255")]
        public int BoxForeColorB {
            get => (int)this[nameof(BoxForeColorB)];
            set => this[nameof(BoxForeColorB)]=(object)value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("1")]
        public int BoxBackColorR {
            get => (int)this[nameof(BoxBackColorR)];
            set => this[nameof(BoxBackColorR)]=(object)value;
        }
        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("5")]
        public int BoxBackColorG {
            get => (int)this[nameof(BoxBackColorG)];
            set => this[nameof(BoxBackColorG)]=(object)value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("9")]
        public int BoxBackColorB {
            get => (int)this[nameof(BoxBackColorB)];
            set => this[nameof(BoxBackColorB)]=(object)value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("96")]
        public int HintTextColorR {
            get => (int)this[nameof(HintTextColorR)];
            set => this[nameof(HintTextColorR)]=(object)value;
        }
        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("214")]
        public int HintTextColorG {
            get => (int)this[nameof(HintTextColorG)];
            set => this[nameof(HintTextColorG)]=(object)value;
        }
        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("138")]
        public int HintTextColorB {
            get => (int)this[nameof(HintTextColorB)];
            set => this[nameof(HintTextColorB)]=(object)value;
        }

        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("1")]
        public int BackgroundColorR {
            get => (int)this[nameof(BackgroundColorR)];
            set => this[nameof(BackgroundColorR)]=(object)value;
        }
        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("5")]
        public int BackgroundColorG {
            get => (int)this[nameof(BackgroundColorG)];
            set => this[nameof(BackgroundColorG)]=(object)value;
        }
        [UserScopedSetting]
        [DebuggerNonUserCode]
        [DefaultSettingValue("9")]
        public int BackgroundColorB {
            get => (int)this[nameof(BackgroundColorB)];
            set => this[nameof(BackgroundColorB)]=(object)value;
        }
    }
}
