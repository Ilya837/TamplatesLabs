using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Geometry;

namespace Visual
{
    
    public interface IDrawable
    {
        public void Draw(Form f);
    }

    public abstract class VisualCurve : Geometry.ICurve, IDrawable
    {
        public abstract void Draw(Form f);

        public abstract void GetPoint(double t, out IPoint p);

    }

    class GraphicsAdaptor
    {
        Graphics g;
        public GraphicsAdaptor(Graphics g)
        {
            this.g = g;
        }

        public void DrawLine(Pen p, Line l)
        {
            System.Drawing.Point sp = new System.Drawing.Point((Int32)l.GetA().GetX(), (Int32)l.GetA().GetY());
            System.Drawing.Point ep = new System.Drawing.Point((Int32)l.GetB().GetX(), (Int32)l.GetB().GetY());

            g.DrawLine(Pens.Black, sp, ep);
        }

        public void DrawBezier(Pen p, Bezier b)
        {
            System.Drawing.Point ap = new System.Drawing.Point((Int32)b.GetA().GetX(), (Int32)b.GetA().GetY());
            System.Drawing.Point bp = new System.Drawing.Point((Int32)b.GetB().GetX(), (Int32)b.GetB().GetY());
            System.Drawing.Point cp = new System.Drawing.Point((Int32)b.GetC().GetX(), (Int32)b.GetC().GetY());
            System.Drawing.Point dp = new System.Drawing.Point((Int32)b.GetD().GetX(), (Int32)b.GetD().GetY());

            g.DrawBezier(p, ap, cp, dp, bp);
        }

    }

    public class VisualLine : VisualCurve
    {
        Line l;

        public VisualLine(Line l)
        {
            this.l = new Line(l.GetA(), l.GetB());
        }

        public override void Draw(Form f)
        {

            f.Paint += (sender, e) =>
            {
                GraphicsAdaptor ga = new GraphicsAdaptor(e.Graphics);

                ga.DrawLine(Pens.Black, l);
            };

            Console.WriteLine("Draw Line");
        }

        public override void GetPoint(double t, out IPoint p)
        {   
            l.GetPoint(t,out p);
        }
    }

    public class VisualBezier : VisualCurve
    {
        Bezier b;

        public VisualBezier(Bezier b)
        {
            this.b = new Bezier(b.GetA(),b.GetC(),b.GetD(),b.GetB());
        }

        public override void Draw(Form f)
        {
            f.Paint += (sender, e) =>
            {

                GraphicsAdaptor ga = new GraphicsAdaptor(e.Graphics);

                ga.DrawBezier(Pens.Black, b);


                //int j = 1;

                //for (double i = 0.05; i <= 1; i = 0.05 * j)
                //{
                //    b.GetPoint(i, out p);

                //    if (j % 2 == 0)
                //        Point1 = new System.Drawing.Point((Int32)p.GetX(), (Int32)p.GetY());
                //    else
                //        Point2 = new System.Drawing.Point((Int32)p.GetX(), (Int32)p.GetY());

                //    g.DrawLine(Pens.Black, Point1, Point2);

                //    j++;
                //}

            };

            Console.WriteLine("Draw Bezier");
        }

        public override void GetPoint(double t, out IPoint p)
        {
            b.GetPoint(t, out p);
        }
    }

    

}
