using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Forms;
using JellyFlix_MediaHub.UI;
using JellyFlix_MediaHub.Data.Handlers;

namespace JellyFlix_MediaHub
{
    internal static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            if (UserHandle.IsUsersDBEmpty())
            {
                Application.Run(new SignUp());
            } else
            {
                Application.Run(new Login());
            }
        }
    }
}
