using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics.Metrics;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using Geometry;
using Svg;
using Point = Geometry.Point;

namespace Visual
{

    public interface IDrawable
    {
        public void Draw(Graphics g);
    }

   

    public abstract class AGraphicsAdaptor
    {
        protected Pen p;

        public AGraphicsAdaptor(Pen p)
        {
            this.p = p;
        }

        protected Color getPenColor()
        {
            return Color.FromArgb(
                p.Color.A,
                p.Color.R,
                p.Color.G,
                p.Color.B
                );
        }
        
        public abstract void DrawCurve(ICurve c, Graphics g);

        public abstract SvgElement GetSVGElement(ICurve c);

    }


    public abstract class VisualCurve : Geometry.ICurve, IDrawable
    {
        private AGraphicsAdaptor graphicAdaptor;
        public VisualCurve(AGraphicsAdaptor graphicAdaptor)
        {
            this.graphicAdaptor = graphicAdaptor;
        }
        public void DrawCurve(ICurve c, Graphics g) { 
        
            graphicAdaptor.DrawCurve(c,g);
        }
        public SvgElement GetSVGElement(ICurve c) {
            return graphicAdaptor.GetSVGElement(c);
        }

        public abstract void Draw(Graphics g);

        public abstract void GetPoint(double t, out IPoint p);

        public abstract SvgElement GetSVGElement();

    }

    class GraphicsAdaptor : AGraphicsAdaptor
    {
        private Int32 n;
        public GraphicsAdaptor(Pen p, Int32 n) : base(p) {
        
            this.n = n;
        }


        public override void DrawCurve(ICurve c, Graphics g)
        {

            IPoint p1;

            c.GetPoint(0,out p1);

            System.Drawing.Point Point1 = new System.Drawing.Point((Int32)p1.GetX(), (Int32)p1.GetY());
            System.Drawing.Point Point2 = new System.Drawing.Point();

            for (double i = 0; i < n; i++)
            {
                c.GetPoint((i + 1) / n, out p1);

                if (i % 2 == 1)
                {
                    Point1.X = (Int32)p1.GetX();
                    Point1.Y = (Int32)p1.GetY();
                }
                else
                {
                    Point2.X = (Int32)p1.GetX();
                    Point2.Y = (Int32)p1.GetY();
                }

                g.DrawLine(p, Point1, Point2);

            }

            c.GetPoint(1, out p1);

        }


        public override SvgElement GetSVGElement(ICurve c)
        {
            SvgGroup g = new SvgGroup();
            IPoint p1;

            c.GetPoint(0, out p1);

            System.Drawing.Point Point1 = new System.Drawing.Point((Int32)p1.GetX(), (Int32)p1.GetY());
            System.Drawing.Point Point2 = new System.Drawing.Point();

            IPoint tmpPoint;



            for (double i = 0; i < n; i++)
            {
                c.GetPoint((i + 1) / n, out tmpPoint);

                if (i % 2 == 1)
                {
                    Point1.X = (Int32)tmpPoint.GetX();
                    Point1.Y = (Int32)tmpPoint.GetY();
                }
                else
                {
                    Point2.X = (Int32)tmpPoint.GetX();
                    Point2.Y = (Int32)tmpPoint.GetY();
                }

                g.Children.Add(new SvgLine()
                {
                    StartX = new SvgUnit(Point1.X),
                    StartY = new SvgUnit(Point1.Y),
                    EndX = new SvgUnit(Point2.X),
                    EndY = new SvgUnit(Point2.Y),
                    Stroke = new SvgColourServer(getPenColor()),
                    StrokeWidth = 1
                }

                );


            }


            return g;
        }
    }

    public class VisualLine : VisualCurve
    {
        private Line l;

        public VisualLine(Line l, AGraphicsAdaptor ga) : base(ga)
        {

            this.l = new Line(l.GetA(), l.GetB());
        }

        public override void Draw(Graphics g)
        {
            DrawCurve(l, g);

            Console.WriteLine("Draw Line");
        }

        public override void GetPoint(double t, out IPoint p)
        {
            l.GetPoint(t, out p);
        }

        public override SvgElement GetSVGElement()
        {
            return GetSVGElement(l);
        }
    }

    public class VisualBezier : VisualCurve
    {
        Bezier b;

        public VisualBezier(Bezier b, AGraphicsAdaptor ga) : base(ga)
        {
            this.b = new Bezier(b.GetA(), b.GetC(), b.GetD(), b.GetB());
        }

        public override void Draw(Graphics g)
        {

            DrawCurve(b, g);


            Console.WriteLine("Draw Bezier");
        }


        public override void GetPoint(double t, out IPoint p)
        {
            b.GetPoint(t, out p);
        }

        public override SvgElement GetSVGElement()
        {
            return GetSVGElement(b);
        }
    }
}

