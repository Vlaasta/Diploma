using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace diplom
{
    internal static class Program
    {
        [STAThread]
        private static void Main()
        {
            JsonProcessing.LoadSettings();
            Task.Run(() => SaveUrlController.StartHttpServerAsync());

            Thread formThread = new Thread(() =>
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            });

            formThread.SetApartmentState(ApartmentState.STA);
            formThread.IsBackground = true;
            formThread.Start();

            //CohereClient.CanSendRequest();

            Task.Run(ActivityMonitoring.MainLoopAsync).Wait();
        }
    }
}








































