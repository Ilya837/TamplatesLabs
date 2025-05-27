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

        public Pen GetPen()
        {
            return (Pen)p.Clone();
        }

        public abstract void DrawPoint(IPoint point, Int32 r, Graphics g);

        public abstract void DrawLine(Line l, Graphics g);
        public abstract void DrawBezier(Bezier b, Graphics g);

        public abstract SvgElement GetSVGLine(Line l);

        public abstract SvgElement GetSVGBezier(Bezier b);

        public abstract SvgElement GetSVGPoint(IPoint point, Int32 r);

        public abstract AGraphicsAdaptor GetCopy();


    }


    public abstract class VisualCurve : Geometry.ICurve, IDrawable
    {
        private AGraphicsAdaptor graphicAdaptor;
        public VisualCurve(AGraphicsAdaptor graphicAdaptor)
        {
            this.graphicAdaptor = graphicAdaptor;
        }

        public AGraphicsAdaptor GetGraphicsAdaptor()
        {
            return graphicAdaptor.GetCopy();
        }

        protected void DrawLine(Line l, Graphics g)
        {
            graphicAdaptor.DrawLine(l, g);
        }
        protected void DrawBezier(Bezier b, Graphics g)
        {
            graphicAdaptor.DrawBezier(b, g);
        }

        protected void DrawPoint(IPoint point, Int32 r, Graphics g)
        {
            graphicAdaptor.DrawPoint(point, r, g);
        }
        public abstract void Draw(Graphics g);

        public abstract void GetPoint(double t, out IPoint p);

        public abstract ICurve GetCopy();


        public abstract SvgElement GetSVGElement(AGraphicsAdaptor ga);
    }

    class GraphicsAdaptor : AGraphicsAdaptor
    {
        public GraphicsAdaptor(Pen p): base(p) {
        
        }

        public override AGraphicsAdaptor GetCopy()
        {
            return new GraphicsAdaptor(p);
        }

        public override void DrawLine(Line l, Graphics g)
        {
            System.Drawing.Point sp = new System.Drawing.Point((Int32)l.GetA().GetX(), (Int32)l.GetA().GetY());
            System.Drawing.Point ep = new System.Drawing.Point((Int32)l.GetB().GetX(), (Int32)l.GetB().GetY());

            g.DrawLine(p, sp, ep);
        }

        public override void DrawBezier(Bezier b, Graphics g)
        {
            System.Drawing.Point ap = new System.Drawing.Point((Int32)b.GetA().GetX(), (Int32)b.GetA().GetY());
            System.Drawing.Point bp = new System.Drawing.Point((Int32)b.GetB().GetX(), (Int32)b.GetB().GetY());
            System.Drawing.Point cp = new System.Drawing.Point((Int32)b.GetC().GetX(), (Int32)b.GetC().GetY());
            System.Drawing.Point dp = new System.Drawing.Point((Int32)b.GetD().GetX(), (Int32)b.GetD().GetY());

            g.DrawBezier(p, ap, cp, dp, bp);
        }

        public override void DrawPoint(IPoint point, Int32 r, Graphics g)
        {
            g.DrawEllipse(p, (UInt32)(point.GetX() - r / 2), (UInt32)(point.GetY() - r / 2), r, r);
        }

        

        public override SvgElement GetSVGLine(Line l)
        {
            
            return new SvgLine()
            {
                StartX = new SvgUnit(((float)l.GetA().GetX())),
                StartY = new SvgUnit(((float)l.GetA().GetY())),
                EndX = new SvgUnit(((float)l.GetB().GetX())),
                EndY = new SvgUnit(((float)l.GetB().GetY())),
                Stroke = new SvgColourServer(getPenColor()),
                StrokeWidth = 1
            };
        }

        public override SvgElement GetSVGBezier(Bezier b)
        {

            string pathData = $"M {b.GetA().GetX()},{b.GetA().GetY()}" +
                $" C {b.GetC().GetX()},{b.GetC().GetY()}" +
                $" {b.GetD().GetX()},{b.GetD().GetY()}" +
                $" {b.GetB().GetX()},{b.GetB().GetY()}";


            return new SvgPath
            {
                PathData = SvgPathBuilder.Parse(pathData),
                Stroke = new SvgColourServer(getPenColor()),
                Fill = SvgPaintServer.None
            };
        }

        public override SvgElement GetSVGPoint(IPoint point, int r)
        {
            return new SvgCircle
            {
                CenterX = new SvgUnit(((float)point.GetX())),
                CenterY = new SvgUnit(((float)point.GetY())),
                Radius = r,
                Stroke = new SvgColourServer(getPenColor()),
                Fill = new SvgColourServer(Color.Transparent),
                StrokeWidth = 1
            };
        }
    }

    class GraphicsAdaptorLinable : AGraphicsAdaptor
    {
        private Int32 n;
        public GraphicsAdaptorLinable(Pen p, Int32 n) : base(p) {
        
            this.n = n;
        }

        public override AGraphicsAdaptor GetCopy()
        {
            return new GraphicsAdaptorLinable(p,n);
        }


        public override void DrawLine(Line l, Graphics g)
        {
            System.Drawing.Point sp = new System.Drawing.Point((Int32)l.GetA().GetX(), (Int32)l.GetA().GetY());
            System.Drawing.Point ep = new System.Drawing.Point((Int32)l.GetB().GetX(), (Int32)l.GetB().GetY());

            g.DrawLine(p, sp, ep);

            DrawPoint(l.GetA(), 4, g);

            DrawPoint(l.GetB(), 4, g);
        }

        public override void DrawBezier(Bezier b, Graphics g)
        {
            System.Drawing.Point Point1 = new System.Drawing.Point((Int32)b.GetA().GetX(), (Int32)b.GetA().GetY());
            System.Drawing.Point Point2 = new System.Drawing.Point();

            IPoint tmpPoint;


            DrawPoint(b.GetA(), 4, g);



            for (double i = 0; i < n; i++)
            {
                b.GetPoint((i + 1) / n, out tmpPoint);

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

                g.DrawLine(Pens.Black, Point1, Point2);

            }

            b.GetPoint(1, out tmpPoint);

            DrawPoint(b.GetB(), 4, g);

        }

        public override void DrawPoint(IPoint point, Int32 r, Graphics g)
        {
            g.DrawEllipse(p, (UInt32)(point.GetX() - r / 2), (UInt32)(point.GetY() - r / 2), r, r);
        }

        public override SvgElement GetSVGLine(Line l)
        {

            SvgGroup g = new SvgGroup();

            g.Children.Add(GetSVGPoint(l.GetA(), 4));
            g.Children.Add(GetSVGPoint(l.GetB(), 4));
            g.Children.Add(new SvgLine()
            {
                StartX = new SvgUnit(((float)l.GetA().GetX())),
                StartY = new SvgUnit(((float)l.GetA().GetY())),
                EndX = new SvgUnit(((float)l.GetB().GetX())),
                EndY = new SvgUnit(((float)l.GetB().GetY())),
                Stroke = new SvgColourServer(getPenColor()),
                StrokeWidth = 1
            });

            return g;
        }

        public override SvgElement GetSVGBezier(Bezier b)
        {
            SvgGroup g = new SvgGroup();

            System.Drawing.Point Point1 = new System.Drawing.Point((Int32)b.GetA().GetX(), (Int32)b.GetA().GetY());
            System.Drawing.Point Point2 = new System.Drawing.Point();

            IPoint tmpPoint;

            g.Children.Add(GetSVGPoint(b.GetA(),4));


            for (double i = 0; i < n; i++)
            {
                b.GetPoint((i + 1) / n, out tmpPoint);

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

            g.Children.Add(GetSVGPoint(b.GetB(), 4));

            return g;
        }

        public override SvgElement GetSVGPoint(IPoint point, int r)
        {
            return new SvgCircle
            {
                CenterX = new SvgUnit(((float)point.GetX())),
                CenterY = new SvgUnit(((float)point.GetY())),
                Radius = r,
                Stroke = new SvgColourServer(getPenColor()),
                Fill = new SvgColourServer(Color.Transparent),
                StrokeWidth = 1
            };
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


            DrawLine(l, g);


            Console.WriteLine("Draw Line");
        }

        public override ICurve GetCopy()
        {
            return new VisualLine(l, GetGraphicsAdaptor());
        }

        public override void GetPoint(double t, out IPoint p)
        {
            l.GetPoint(t, out p);
        }

        public override SvgElement GetSVGElement(AGraphicsAdaptor ga)
        {
            return ga.GetSVGLine(l);
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

            DrawBezier(b, g);


            Console.WriteLine("Draw Bezier");
        }

        public override ICurve GetCopy()
        {
            return new VisualBezier(b, GetGraphicsAdaptor());
        }


        public override void GetPoint(double t, out IPoint p)
        {
            b.GetPoint(t, out p);
        }

        public override SvgElement GetSVGElement(AGraphicsAdaptor ga)
        {
            return ga.GetSVGBezier(b);
        }

    }

    public class VisualChain : VisualCurve
    {
        VisualCurve curve1;
        VisualCurve curve2;

        public VisualChain(VisualCurve curve1, VisualCurve curve2, AGraphicsAdaptor ga) : base(ga)
        {
            this.curve1 = (VisualCurve)curve1.GetCopy();
            this.curve2 = (VisualCurve)curve2.GetCopy();
        }

        public override void Draw(Graphics g)
        {

            curve1.Draw(g);
            curve2.Draw(g);
        }

        public override ICurve GetCopy()
        {
            return new VisualChain((VisualCurve)curve1.GetCopy(), (VisualCurve)curve2.GetCopy(), GetGraphicsAdaptor());
        }

        public override void GetPoint(double t, out IPoint p)
        {
            if (t <= 0.5) curve1.GetPoint(t * 2, out p);
            else curve2.GetPoint(t * 2 - 1, out p);
        }

        public override SvgElement GetSVGElement(AGraphicsAdaptor ga)
        {
            SvgGroup group = new SvgGroup();
            group.Children.Add(curve1.GetSVGElement(ga));
            group.Children.Add(curve2.GetSVGElement(ga));
            return group;
        }

    }
}

