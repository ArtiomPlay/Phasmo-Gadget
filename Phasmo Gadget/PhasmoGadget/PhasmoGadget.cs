using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text.RegularExpressions;
using System.Web;
using System.Media;
using System.Windows.Forms;
using PhasmoGadget.HotKeyAPI.Contracts;
using PhasmoGadget.HotKeyAPI.Services;
using PhasmoGadget.HotKeyAPI.Structs;
using PhasmoGadget.PhasmoGadget.Properties;

namespace PhasmoGadget.PhasmoGadget {
    public partial class PhasmoGadget :Form {
        //Information
        private List<Helper_Ghosts>GhostList=new List<Helper_Ghosts>();
        private List<Helper_Timer>TimerList=new List<Helper_Timer>();
        private List<Helper_Tables>TableList=new List<Helper_Tables>();
        private string var_hint="";

        private List<string>EviListGreen=new List<string>();
        private List<string>EviListRed=new List<string>();
        private List<string>EviListHold=new List<string>();
        private List<string>EviListAll=new List<string>();

        //Hot Keys
        private IHotKeyFactory hkFactory;
        private IHotKeyRecorder hkRecorder;
        private IHotKeyListener hkListener;
        private int var_HotKeyPressed;
        private string var_KeyID=Settings.Default["KeyID"].ToString();
        private string var_TimerID=Settings.Default["TimerID"].ToString();

        //Variables
        private int var_countGhosts;
        private int var_selectedIndex1;
        private int var_selectedIndex2;
        private int var_selectedIndex3;
        private int var_selectedIndex_Ghosts;
        private int var_selectedIndex_Info;
        private int var_selectedIndexMaps;
        private int var_selectedIndexAnswers;
        private string var_selectedIndexTimerType=" ";

        private int var_dif=1;
        private int var_seconds;
        private int var_keep_seconds;
        private int custom_timer;

        private string var_language="";
        private double Opacity_Value=Convert.ToDouble(Settings.Default["Opacity"]);
        private int Size_Value =(int)Settings.Default["Size"];
        private int var_last_size;

        private int var_toggleInfo;
        private int var_last_toggleInfo;

        //Other
        private static readonly IntPtr HWND_TOPMIST=new IntPtr(-1);
        private const uint SWP_NOSIZE=1;
        private const uint SWP_NOMOVE=2;
        private const uint TOPMOST_FLAGS=3;
        public const int WM_NCLBUTTONDOWN=161;
        public const int HT_CAPTION=2;

        //Bitmaps
        private Bitmap EMFReaderIcon48;
        private Bitmap EMFReaderIcon48g;
        private Bitmap EMFReaderIcon48r;
        private Bitmap EMFReaderIcon48y;

        private Bitmap FingerprintIcon48;
        private Bitmap FingerprintIcon48g;
        private Bitmap FingerprintIcon48r;
        private Bitmap FingerprintIcon48y;

        private Bitmap BookIcon48;
        private Bitmap BookIcon48g;
        private Bitmap BookIcon48r;
        private Bitmap BookIcon48y;

        private Bitmap BreathingIcon48;
        private Bitmap BreathingIcon48g;
        private Bitmap BreathingIcon48r;
        private Bitmap BreathingIcon48y;

        private Bitmap GhostOrbIcon48;
        private Bitmap GhostOrbIcon48g;
        private Bitmap GhostOrbIcon48r;
        private Bitmap GhostOrbIcon48y;

        private Bitmap SpiritBoxIcon48;
        private Bitmap SpiritBoxIcon48g;
        private Bitmap SpiritBoxIcon48r;
        private Bitmap SpiritBoxIcon48y;

        private Bitmap DotsIcon48;
        private Bitmap DotsIcon48g;
        private Bitmap DotsIcon48r;
        private Bitmap DotsIcon48y;

        private Bitmap ResetIcon;
        private Bitmap SettingsIcon;
        private Bitmap btnMinIcon;
        private Bitmap btnExitIcon;
        private Bitmap arrow_down;
        private Bitmap arrow_up;
        private Bitmap LineIcon;
        private Bitmap OkIcon;
        private Bitmap HelpIcon;

        //Buttons
        private Button btnEMF;
        private Button btnFinger;
        private Button btnBook;
        private Button btnTemp;
        private Button btnOrb;
        private Button btnBox;
        private Button btnDots;

        private Button btnMin;
        private Button btnExit;
        private Button btnHintClose;
        private Button btnSetClose;
        private Button btnOK;
        private Button btnHelp;
        private Button btnReset;
        private Button btnResetColor;
        private Button btnSettings;
        private Button btnGhostInfo;
        
        //Check Boxes
        private CheckBox chbCompact;
        private CheckBox chbSoundOnOff;

        //Color Dialog
        private ColorDialog colorDialog;

        //Combo Boxes
        private ComboBox cbFirstName;
        private ComboBox cbLastName;
        private ComboBox cbAnswerType;
        private ComboBox cbTask1;
        private ComboBox cbTask2;
        private ComboBox cbTask3;
        private ComboBox cbGhosts;
        private ComboBox cbInfo;
        private ComboBox cbTimerType;
        private ComboBox cbMaps;
        private ComboBox cbLanguage;

        //Container
        private IContainer components;

        //Labels
        private Label lblFocus;
        private Label lblHint;
        private Label lblDif;
        private Label lblHotKey;
        private Label lblTimerKey;
        private Label lblLanguage;
        private Label lblOpacity;
        private Label lblOpacity_Value;
        private Label lblSize;
        private Label lblSize_Value;
        private Label lblCopyright;
        private Label lblVersion;
        private Label lblTextColor;
        private Label lblBoxForeColor;
        private Label lblBoxBackColor;
        private Label lblHintTextColor;
        private Label lblBackgroundColor;
        private Label lblGhInfoType;
        private Label lblGhInfoTypeData;
        private Label lblGhInfoHunt;
        private Label lblGhInfoHuntData;
        private Label lblGhInfoHuntAbilities;
        private Label lblGhInfoHuntAbilitiesData;
        private Label lblGhInfoCooldown;
        private Label lblGhInfoCooldownData;

        //Panel
        private Panel panel_settings;

        //Pictures
        private PictureBox picLine;
        private PictureBox picGhost;
        private PictureBox picGhostEvi1;
        private PictureBox picGhostEvi2;
        private PictureBox picGhostEvi3;
        private PictureBox picGhostEvi4;

        //Rich Text Boxes
        private RichTextBox rtbGhostTyp;
        private RichTextBox rtbGhInfoHintsData;
        private RichTextBox rtbInfo;

        //Sound Players
        private SoundPlayer beep_1;
        private SoundPlayer beep_2;
        private SoundPlayer beep_3;
        private SoundPlayer beep_finish;

        //Text Boxes
        private TextBox tbKeyID;
        private TextBox tbTimerID;
        private TextBox tbCounter;
        private TextBox tbTextColor_rgb;
        private TextBox tbBoxForeColor_rgb;
        private TextBox tbBoxBackColor_rgb;
        private TextBox tbHintTextColor_rgb;
        private TextBox tbBackgroundColor_rgb;
        private TextBox tbTextColorR;
        private TextBox tbTextColorG;
        private TextBox tbTextColorB;
        private TextBox tbBoxForeColorR;
        private TextBox tbBoxForeColorG;
        private TextBox tbBoxForeColorB;
        private TextBox tbBoxBackColorR;
        private TextBox tbBoxBackColorG;
        private TextBox tbBoxBackColorB;
        private TextBox tbHintTextColorR;
        private TextBox tbHintTextColorG;
        private TextBox tbHintTextColorB;
        private TextBox tbBackgroundColorR;
        private TextBox tbBackgroundColorG;
        private TextBox tbBackgroundColorB;
        
        //Timer
        private Timer timer;
        
        //Track Bars
        private TrackBar trbOpacity;
        private TrackBar trbSize;
        
        
        [DllImport("user32.dll")]
        public static extern int SendMessage(IntPtr hWnd,int Msg,int wParam,int lParam);

        [DllImport("user32.dll")]
        public static extern bool ReleaseCapture();

        [DllImport("user32.dll")]
        [return: MarshalAs(UnmanagedType.Bool)]
        public static extern bool SetWindowPos(
            IntPtr hWnd,
            IntPtr hWndInsertAfter,
            int X,
            int Y,
            int cx,
            int cy,
            uint uFlags);

        [DllImport("Gdi32.dll")]
        private static extern IntPtr CreateRoundRectRgn(
            int nLeftRect,
            int nTopRect,
            int nRightRect,
            int nBottomRect,
            int nWidthEllipse,
            int nHeightEllipse);


        public PhasmoGadget() {
            this.EviListAll=new List<string>((IEnumerable<string>)new string[7] {
                "EMF",
                "Finger",
                "Book",
                "Temp",
                "Orb",
                "Box",
                "Dots"
            });

            this.InitializeComponent();

            this.panel_settings.Hide();
            this.cbInfo.Hide();
            this.rtbInfo.Hide();
            this.btnHintClose.Hide();

            this.FormPosition();
            this.UserColors();
            this.init_wavs();
            this.init_icons();
            this.setting_language();
            this.load_files_once();
            this.load_files();

            this.check_ghosttyp();
            this.GetGhostNames();
            this.GetInfo();
            this.GetMaps();
            this.GetTimerTypes();
            this.GetObjectives();
            this.GetAnswers();
            
            this.Opacity=Convert.ToDouble(Settings.Default["Opacity"])/100.0;
            this.chbSoundOnOff.Checked=(int)Settings.Default["SoundOnOff"]==1;
            this.chbCompact.Checked=(int)Settings.Default["CompactMode"]==1;

            //Buttons
            this.btnEMF.BackgroundImage=(Image)this.EMFReaderIcon48;
            this.btnEMF.FlatStyle=FlatStyle.Flat;
            this.btnEMF.FlatAppearance.BorderSize=0;
            this.btnEMF.FlatAppearance.BorderColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.btnEMF.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            
            this.btnFinger.BackgroundImage=(Image)this.FingerprintIcon48;
            this.btnFinger.FlatStyle=FlatStyle.Flat;
            this.btnFinger.FlatAppearance.BorderSize=0;
            this.btnFinger.FlatAppearance.BorderColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.btnFinger.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            
            this.btnBook.BackgroundImage=(Image)this.BookIcon48;
            this.btnBook.FlatStyle=FlatStyle.Flat;
            this.btnBook.FlatAppearance.BorderSize=0;
            this.btnBook.FlatAppearance.BorderColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.btnBook.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);

            this.btnTemp.BackgroundImage=(Image)this.BreathingIcon48;
            this.btnTemp.FlatStyle=FlatStyle.Flat;
            this.btnTemp.FlatAppearance.BorderSize=0;
            this.btnTemp.FlatAppearance.BorderColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.btnTemp.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            
            this.btnOrb.BackgroundImage=(Image)this.GhostOrbIcon48;
            this.btnOrb.FlatStyle=FlatStyle.Flat;
            this.btnOrb.FlatAppearance.BorderSize=0;
            this.btnOrb.FlatAppearance.BorderColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.btnOrb.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);

            this.btnBox.BackgroundImage=(Image)this.SpiritBoxIcon48;
            this.btnBox.FlatStyle=FlatStyle.Flat;
            this.btnBox.FlatAppearance.BorderSize=0;
            this.btnBox.FlatAppearance.BorderColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.btnBox.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);

            this.btnDots.BackgroundImage=(Image)this.DotsIcon48;
            this.btnDots.FlatStyle=FlatStyle.Flat;
            this.btnDots.FlatAppearance.BorderSize=0;
            this.btnDots.FlatAppearance.BorderColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.btnDots.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);

            this.btnMin.BackgroundImage=(Image)this.btnMinIcon;
            this.btnMin.FlatStyle=FlatStyle.Flat;
            this.btnMin.FlatAppearance.BorderSize=0;
            this.btnMin.FlatAppearance.BorderColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.btnMin.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);

            this.btnExit.BackgroundImage=(Image)this.btnExitIcon;
            this.btnExit.FlatStyle=FlatStyle.Flat;
            this.btnExit.FlatAppearance.BorderSize=0;
            this.btnExit.FlatAppearance.BorderColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.btnExit.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            
            this.btnHintClose.BackgroundImage=(Image)this.btnExitIcon;
            this.btnHintClose.FlatStyle=FlatStyle.Flat;
            this.btnHintClose.FlatAppearance.BorderSize=0;
            this.btnHintClose.FlatAppearance.BorderColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.btnHintClose.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            
            this.btnSetClose.BackgroundImage=(Image)this.btnExitIcon;
            this.btnSetClose.FlatStyle=FlatStyle.Flat;
            this.btnSetClose.FlatAppearance.BorderSize=0;
            this.btnSetClose.FlatAppearance.BorderColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.btnSetClose.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue); 

            this.btnOK.BackgroundImage=(Image)this.OkIcon;
            this.btnOK.FlatStyle=FlatStyle.Flat;
            this.btnOK.FlatAppearance.BorderSize=0;
            this.btnOK.FlatAppearance.BorderColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.btnOK.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            
            this.btnHelp.BackgroundImage=(Image)this.HelpIcon;
            this.btnHelp.FlatStyle=FlatStyle.Flat;
            this.btnHelp.FlatAppearance.BorderSize=0;
            this.btnHelp.FlatAppearance.BorderColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.btnHelp.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            
            this.btnReset.BackgroundImage=(Image)this.ResetIcon;
            this.btnReset.FlatStyle=FlatStyle.Flat;
            this.btnReset.FlatAppearance.BorderSize=0;
            this.btnReset.FlatAppearance.BorderColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.btnReset.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);

            this.btnResetColor.BackgroundImage=(Image)this.ResetIcon;
            this.btnResetColor.FlatStyle=FlatStyle.Flat;
            this.btnResetColor.FlatAppearance.BorderSize=0;
            this.btnResetColor.FlatAppearance.BorderColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.btnResetColor.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);

            this.btnSettings.BackgroundImage=(Image)this.SettingsIcon;
            this.btnSettings.FlatStyle=FlatStyle.Flat;
            this.btnSettings.FlatAppearance.BorderSize=0;
            this.btnSettings.FlatAppearance.BorderColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.btnSettings.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
    
            this.btnGhostInfo.BackgroundImage=(Image)this.arrow_down;
            this.btnGhostInfo.FlatStyle=FlatStyle.Flat;
            this.btnGhostInfo.FlatAppearance.BorderSize=0;
            this.btnGhostInfo.FlatAppearance.BorderColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.btnGhostInfo.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);

            //Combo Boxes
            this.cbGhosts.SelectedIndex=0;
            this.cbInfo.SelectedIndex=0;
            this.cbMaps.SelectedIndex=0;
            this.cbTimerType.SelectedIndex=0;
            this.cbAnswerType.SelectedIndex=0;

            //Labels
            this.lblDif.BackColor=Color.WhiteSmoke;
            this.lblHotKey.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.lblTimerKey.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.lblLanguage.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.lblOpacity.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.lblOpacity_Value.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.lblSize.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.lblSize_Value.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.lblCopyright.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.lblVersion.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.lblTextColor.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.lblBoxForeColor.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.lblBoxBackColor.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.lblHintTextColor.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.lblBackgroundColor.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.lblGhInfoType.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.lblGhInfoTypeData.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.lblGhInfoHunt.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.lblGhInfoHuntData.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.lblGhInfoHuntAbilities.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.lblGhInfoHuntAbilitiesData.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.lblGhInfoCooldown.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.lblGhInfoCooldownData.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);

            //Pictures
            this.picLine.BackgroundImage=(Image)this.LineIcon;
            this.picGhost.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.picGhostEvi1.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.picGhostEvi2.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.picGhostEvi3.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);
            this.picGhostEvi4.BackColor=Color.FromArgb(0,(int)byte.MaxValue,(int)byte.MaxValue,(int)byte.MaxValue);

            //Text Boxrs
            this.tbCounter.BackColor=Color.WhiteSmoke;


            this.activate_HotKey();
        }
    }
}
