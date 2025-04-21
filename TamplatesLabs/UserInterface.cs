using TamplatesLabs;
using Visual;
using Geometry;
using System.Windows.Forms;

namespace UserInterface
{
    internal static class UserInterface
    {
        /// <summary>
        ///  The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            ApplicationConfiguration.Initialize();

            Form f = new Form1();

            Geometry.Point a = new Geometry.Point();
            a.SetX(50);
            a.SetY(50);

            Geometry.Point b = new Geometry.Point();
            b.SetX(250);
            b.SetY(250);

            Geometry.Point c = new Geometry.Point();
            c.SetX(250);
            c.SetY(50);

            Geometry.Point d = new Geometry.Point();
            d.SetX(50);
            d.SetY(250);


            VisualLine vl1 = new VisualLine(new Line(a, b));
            vl1.Draw(f);

            VisualLine vl2 = new VisualLine(new Line(c, d));
            vl2.Draw(f);

            VisualBezier vb = new VisualBezier( new Bezier( a, c, d, b));
            vb.Draw(f);
            
            

            Application.Run(f);


        }
    }
}