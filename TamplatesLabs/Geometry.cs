using Microsoft.VisualBasic.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace Geometry
{
    public interface IPoint
    {
        public double GetX();
        public void SetX(double x);
        public double GetY();
        public void SetY(double y);

        public IPoint GetCopy();
    }

    public interface ICurve
    {
        public void GetPoint(double t, out IPoint p);
    }

    public class Point : IPoint
    {
        protected double X, Y;
        public double GetX()
        {
            return X;
        }

        public double GetY()
        {
            return Y;
        }

        public void SetX(double x)
        {
            X = x;
        }

        public void SetY(double y)
        {
            Y = y;
        }

        public IPoint GetCopy()
        {
            Point p = new Point();
            p.X = X;
            p.Y = Y;
            return p;
        }

    }

    public abstract class ACurve : ICurve
    {
        protected IPoint a, b;

        public ACurve(IPoint a, IPoint b)
        {
            this.a = new Point();
            this.a.SetX(a.GetX());
            this.a.SetY(a.GetY());

            this.b = new Point();
            this.b.SetX(b.GetX());
            this.b.SetY(b.GetY());
        }

        public void SetA(IPoint a)
        {
            this.a.SetX(a.GetX());
            this.a.SetY(a.GetY());
        }

        public void SetB(IPoint b)
        {
            this.b.SetX(b.GetX());
            this.b.SetY(b.GetY());
        }

        public IPoint GetA()
        {
            return a.GetCopy();
        }

        public IPoint GetB()
        {
            return b.GetCopy();
        }
        public abstract void GetPoint(double t, out IPoint p);
    }

    public class Line : ACurve
    {
        public Line(IPoint a, IPoint b) : base(a,b) {}

        public override void GetPoint(double t, out IPoint p)
        {
            p = new Point();
            if (t < 0 || t > 1)
            {
                Console.WriteLine("Curve.GetPoint parametr t not in 0..1 interval");
                return; //как предупредить об ошибке?
            }

            p.SetX((1 - t) * a.GetX() + t * b.GetX());
            p.SetY((1 - t) * a.GetY()+ t * b.GetY());
            return;
        }
    }

    public class Bezier: ACurve
    {
        private IPoint c, d;
        public Bezier(IPoint a,IPoint c, IPoint d, IPoint b) : base(a, b) {

            this.c = new Point();
            this.c.SetX(c.GetX());
            this.c.SetY(c.GetY());

            this.d = new Point();
            this.d.SetX(d.GetX());
            this.d.SetY(d.GetY());

        }

        public void SetC(IPoint c)
        {
            this.c.SetX(c.GetX());
            this.c.SetY(c.GetY());
        }

        public void SetD(IPoint d)
        {
            this.d.SetX(d.GetX());
            this.d.SetY(d.GetY());
        }

        public IPoint GetC()
        {
            return c.GetCopy();
        }

        public IPoint GetD()
        {
            return d.GetCopy();
        }

        public override void GetPoint(double t, out IPoint p)
        {
            p = new Point();
            if (t < 0 || t > 1)
            {
                Console.WriteLine("Curve.GetPoint parametr t not in 0..1 interval");
                return; //как предупредить об ошибке?
            }

            p.SetX(Math.Pow((1 - t), 3) * a.GetX() + 3 * t * Math.Pow((1 - t), 2) * c.GetX() + 3 * t * t * (1 - t) * d.GetX() + t * t * t * b.GetX());
            p.SetY(Math.Pow((1 - t), 3) * a.GetY() + 3 * t * Math.Pow((1 - t), 2) * c.GetY() + 3 * t * t * (1 - t) * d.GetY() + t * t * t * b.GetY());
            return;
        }
    }

}
