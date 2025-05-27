using TamplatesLabs4;
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

            Geometry.Point a1 = new Geometry.Point();
            a1.SetX(50);
            a1.SetY(300);

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

            Geometry.Point b2 = new Geometry.Point();
            b2.SetX(400);
            b2.SetY(50);


            GraphicsAdaptor ga = new GraphicsAdaptor(Pens.Green);
            GraphicsAdaptorLinable ga2 = new GraphicsAdaptorLinable(Pens.Black,5);

            f.Paint += (sender, e) =>
            {
                Graphics g = e.Graphics;
                VisualLine vl1 = new VisualLine(new Line(a1, a), ga);
                VisualLine vl2 = new VisualLine(new Line(b, b2), ga2);
                VisualBezier vb = new VisualBezier(new Bezier(a, c, d, b), ga2);

                VisualChain chain = new VisualChain(vl1, vb,ga);
                chain = new VisualChain(chain, vl2,ga);

                chain.Draw(g);


            };


            Application.Run(f);
            


        }

    }
}