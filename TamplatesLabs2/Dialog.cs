using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TamplatesLabs2;
using Svg;
using Visual;
using Geometry;
using System.Security.Cryptography;

namespace TamplatesLabs2
{
    public partial class Dialog : Form
    {
        int Counter = 0;

        Panel panel = new Panel();

        List<Tuple<VisualCurve, AGraphicsAdaptor>> panel1Curves = new List<Tuple<VisualCurve, AGraphicsAdaptor>>();
        List<Tuple<VisualCurve, AGraphicsAdaptor>> panel2Curves = new List<Tuple<VisualCurve, AGraphicsAdaptor>>();
        public Dialog()
        {
            InitializeComponent();
        }

        private void splitContainer1_Panel1_Paint(object sender, PaintEventArgs e)
        {

        }

        private void button1_Click(object sender, EventArgs e)
        {
            SvgDocument svgDoc = new SvgDocument()
            {
                Width = panel1.Width,
                Height = panel1.Height
            };

            SvgGroup group = new SvgGroup();

            var graphics = panel1.CreateGraphics();

            panel1Curves.ForEach(curve =>
            {

                group.Children.Add(curve.Item1.GetSVGElement(curve.Item2));
            });


            svgDoc.Children.Add(group);

            svgDoc.Write("output.svg");


        }

        private void button3_Click(object sender, EventArgs e)
        {
            Counter++;

            Random rand = new Random(DateTime.UtcNow.Millisecond);

            int x1 = rand.Next(100, 400);
            int y1 = rand.Next(100, 400);

            int x2 = rand.Next(100, 400);
            int y2 = rand.Next(100, 400);

            Geometry.Point a = new Geometry.Point();
            a.SetX(x1);
            a.SetY(y1);

            Geometry.Point b = new Geometry.Point();
            b.SetX(x2);
            b.SetY(y2);

            Geometry.Point c = new Geometry.Point();
            c.SetX(x1);
            c.SetY(y2);

            Geometry.Point d = new Geometry.Point();
            d.SetX(x2);
            d.SetY(y1);

            AGraphicsAdaptor ga = new GraphicsAdaptor(Pens.Green);

            Graphics g = panel.CreateGraphics();

            g.Clear(Color.White);
            

            switch (Counter % 4)
            {
                case 0:
                    ga = new GraphicsAdaptorLinable(Pens.Black, 5);
                    break;
                case 1:
                    ga = new GraphicsAdaptorLinable(Pens.Green, 5);
                    break;
                case 2:
                    ga = new GraphicsAdaptor(Pens.Green);
                    break;
                case 3:
                    ga = new GraphicsAdaptor(Pens.Red);
                    break;
            }

            Color Penсolor = Color.FromArgb(
                ga.GetPen().Color.A,
                ga.GetPen().Color.R,
                ga.GetPen().Color.G,
                ga.GetPen().Color.B
                );


            List<Tuple<VisualCurve, AGraphicsAdaptor>> panelCurves;

            if (panel == panel1) panelCurves = panel1Curves;
            else panelCurves = panel2Curves;

            panelCurves.Clear();

            switch (Counter % 3)
            {
                case 0:
                    VisualLine vl1 = new(new Line(a, b), ga);
                    vl1.Draw(g);
                    panelCurves.Add(new Tuple<VisualCurve, AGraphicsAdaptor>(vl1, ga));
                    break;

                case 1:
                    VisualLine vl2 = new VisualLine(new Line(c, d), ga);
                    vl2.Draw(g);
                    panelCurves.Add(new Tuple<VisualCurve, AGraphicsAdaptor>(vl2, ga));
                    break;
                case 2:
                    VisualBezier vb = new VisualBezier(new Bezier(a, c, d, b), ga);

                    vb.Draw(g);
                    panelCurves.Add(new Tuple<VisualCurve, AGraphicsAdaptor>(vb, ga));
                    break;


            }


        }

        private void panel1_MouseClick(object sender, MouseEventArgs e)
        {
            panel = panel1;
        }

        private void panel2_MouseClick(object sender, MouseEventArgs e)
        {
            panel = panel2;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            SvgDocument svgDoc = new SvgDocument()
            {
                Width = panel1.Width,
                Height = panel1.Height
            };

            SvgGroup group = new SvgGroup();

            var graphics = panel1.CreateGraphics();

            panel2Curves.ForEach(curve =>
            {

                group.Children.Add(curve.Item1.GetSVGElement(curve.Item2));
            });


            svgDoc.Children.Add(group);

            svgDoc.Write("output.svg");
        }
    }
}
