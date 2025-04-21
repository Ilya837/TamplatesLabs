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

        public void DrawLine(Pen p, IPoint startPoint, IPoint endPoint)
        {
            System.Drawing.Point sp = new System.Drawing.Point((Int32)startPoint.GetX(), (Int32)startPoint.GetY());
            System.Drawing.Point ep = new System.Drawing.Point((Int32)endPoint.GetX(), (Int32)endPoint.GetY());

            g.DrawLine(Pens.Black, sp, ep);
        }

        public void DrawBezier(Pen p, IPoint a, IPoint c, IPoint d, IPoint b)
        {
            System.Drawing.Point ap = new System.Drawing.Point((Int32)a.GetX(), (Int32)a.GetY());
            System.Drawing.Point bp = new System.Drawing.Point((Int32)b.GetX(), (Int32)b.GetY());
            System.Drawing.Point cp = new System.Drawing.Point((Int32)c.GetX(), (Int32)c.GetY());
            System.Drawing.Point dp = new System.Drawing.Point((Int32)d.GetX(), (Int32)d.GetY());

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

                ga.DrawLine(Pens.Black, l.GetA(), l.GetB());
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

                ga.DrawBezier(Pens.Black, b.GetA(),b.GetC(),b.GetD(),b.GetB());


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
