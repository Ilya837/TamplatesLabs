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

            Form d = new Dialog();

            Application.Run(d);
            
        }

    }
}