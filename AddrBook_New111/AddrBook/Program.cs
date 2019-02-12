using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows.Forms;

namespace AddrBook
{
    public class AltF4Filter : IMessageFilter
    {
        //ALT+F4 키 막기
        public bool PreFilterMessage(ref Message m)
        {
            const int WM_SYSKEYDOWN = 0x0104;
            if (m.Msg == WM_SYSKEYDOWN)
            {
                bool alt = ((int)m.LParam & 0x20000000) != 0;
                if (alt && (m.WParam == new IntPtr((int)Keys.F4)))
                    Console.WriteLine("ALT+F4 Filtering됨.");
                return true; // eat it!                
            }
            return false;
        }
    }

    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        [STAThread]
        static void Main()
        {
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            // Add a message filter, ALT+F4 키막기
            Application.AddMessageFilter(new AltF4Filter()); 

            Application.Run(new FrmLogin());
        }
    }
}
