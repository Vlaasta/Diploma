using System;
using System.Drawing; // Для іконки
using System.Windows.Forms;

namespace diplom
{
    internal class Notifications : Form
    {
        private NotifyIcon notifyIcon;
        public Notifications()
        {
            InitializeNotification();
        }

        private void InitializeNotification()
        {
            notifyIcon = new NotifyIcon
            {
                Icon = new Icon(SystemIcons.Information, 40, 40), // Іконка сповіщення
                Visible = true,
                BalloonTipTitle = "Нове сповіщення",
                BalloonTipText = "Дякую що запустив мене, сонечко!",
                BalloonTipIcon = ToolTipIcon.Info
            };

            // Показати сповіщення
            notifyIcon.ShowBalloonTip(3000); // Тривалість у мс (3 секунди)
        }

        public void ShowNotification()
        {
            InitializeNotification(); // Виклик приватного методу
        }
    }
}
