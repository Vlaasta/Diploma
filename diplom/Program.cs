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

            // Запуск форми в окремому потоці
            Thread formThread = new Thread(() =>
            {
                Application.EnableVisualStyles();
                Application.SetCompatibleTextRenderingDefault(false);
                Application.Run(new Form1());
            });

            formThread.SetApartmentState(ApartmentState.STA);
            formThread.IsBackground = true;
            formThread.Start();

             CohereClient.CanSendRequest();
           // JsonProcessing.IfWasModifiedToday();
            //CohereClient.RunDailyTask();

            //JsonProcessing.FilterUrlsBySimilarity(CohereClient.outputJsonPath, JsonProcessing.todayUrls, JsonProcessing.filePath2, JsonProcessing.filePath3);

            // Асинхронний запуск основного циклу
            Task.Run(ActivityMonitoring.MainLoopAsync).Wait();
        }
    }
}








































