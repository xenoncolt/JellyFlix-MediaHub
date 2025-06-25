using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace JellyFlix_MediaHub.Utils
{
    internal class App
    {
        private static Form active_form;

        public static void Show(Form new_form, Form old_form = null)
        {
            Form to_close = old_form ?? active_form;

            new_form.FormClosed += OnFormClosed;
            new_form.Show();

            active_form = new_form;

            if (to_close != null & !to_close.IsDisposed)
            {
                to_close.Hide();
            }
        }

        private static void OnFormClosed(object sender, FormClosedEventArgs e)
        {
            if ( sender == active_form)
            {
                Application.Exit();
            }
        }
    }
}
