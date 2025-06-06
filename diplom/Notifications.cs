using System.Drawing; 
using System.Windows.Forms;

namespace diplom
{
    public static class Notifications
    {
        private static NotifyIcon notifyIcon;
        public static bool NotificationsEnabled { get; set; } = true;

        public static void Show(string message)
        {
            if (!NotificationsEnabled)
                return;

            if (notifyIcon == null)
            {
                notifyIcon = new NotifyIcon
                {
                    Icon = SystemIcons.Information,
                    BalloonTipTitle = "Нове сповіщення",
                    BalloonTipIcon = ToolTipIcon.Info,
                    Text = "TimeToTrack",
                    Visible = true
                };
            }
            notifyIcon.Icon = Properties.Resources.Icon;
            notifyIcon.BalloonTipText = message;
            notifyIcon.ShowBalloonTip(3000);
        }

        public static void Dispose()
        {
            notifyIcon?.Dispose();
        }
    }
}
