using System;
using System.Reflection;
using BindingFlags = System.Reflection.BindingFlags;

namespace ChaosTemplates.Game3D
{
    static class Program
    {
        [STAThread]
        static void Main()
        {
            typeof(System.Globalization.CultureInfo)
                .GetField("s_userDefaultCulture", BindingFlags.NonPublic | BindingFlags.Static)
                .SetValue(null, System.Globalization.CultureInfo.InvariantCulture);

            Environment.CurrentDirectory = System.IO.Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location);

#if OS_WINDOWS
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
#endif

            // TODO: start a game
            Console.WriteLine("Game Start");
        }
    }
}
