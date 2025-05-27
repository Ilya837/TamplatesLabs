using TamplatesLabs2;
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
            Form f2 = new Dialog();

            Geometry.Point a = new Geometry.Point();
            a.SetX(50);
            a.SetY(50);

            Geometry.Point b = new Geometry.Point();
            b.SetX(400);
            b.SetY(400);

            Geometry.Point c = new Geometry.Point();
            c.SetX(400);
            c.SetY(50);

            Geometry.Point d = new Geometry.Point();
            d.SetX(50);
            d.SetY(400);

            GraphicsAdaptor ga = new GraphicsAdaptor(Pens.Green);
            GraphicsAdaptorLinable ga2 = new GraphicsAdaptorLinable(Pens.Black,5);

            f.Paint += (sender, e) =>
            {
                Graphics g = e.Graphics;
                VisualLine vl1 = new VisualLine(new Line(a, b), ga);
                vl1.Draw(g);
                VisualLine vl2 = new VisualLine(new Line(c, d), ga2);
                vl2.Draw(g);
                VisualBezier vb = new VisualBezier(new Bezier(a, c, d, b), ga2);
                vb.Draw(g);
            };


            //Application.Run(f);
            Application.Run(f2);
            


        }

    }
}