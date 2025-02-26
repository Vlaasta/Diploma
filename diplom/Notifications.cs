using System;
using System.Drawing; // Для іконки
using System.Windows.Forms;

namespace diplom
{
    internal class Notifications : Form
    {
        private NotifyIcon notifyIcon;
       // public bool notificationState = false;

        public Notifications()
        {
            InitializeNotification();
        }

        private void InitializeNotification()
        {
            notifyIcon = new NotifyIcon
            {
                Icon = SystemIcons.Information,
                BalloonTipTitle = "Нове сповіщення",
                BalloonTipText = "Дякую що запустив мене, сонечко!",
                BalloonTipIcon = ToolTipIcon.Info,
                Visible = true // Поставити видимість в кінці
            };

            notifyIcon.ShowBalloonTip(3000); // Тривалість
        }

        public void ShowNotification()
        {
            InitializeNotification(); // Виклик приватного методу

        }

        public void GetNotificationState()
        {

        }
    }
}
