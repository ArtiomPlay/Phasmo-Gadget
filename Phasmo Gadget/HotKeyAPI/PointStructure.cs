using System.Drawing;

namespace PhasmoGadget.HotKeyAPI {
    internal struct PointStructure {
        public int X;
        public int Y;

        public PointStructure(int x,int y) {
            this.X=x;
            this.Y=y;
        }

        public PointStructure(Point pt):this(pt.X,pt.Y) {}

        public static implicit operator Point(PointStructure p) => new Point(p.X,p.Y);
        public static implicit operator PointStructure(Point p) => new PointStructure(p.X,p.Y);
    }
}
