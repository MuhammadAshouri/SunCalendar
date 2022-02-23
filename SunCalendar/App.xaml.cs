using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Drawing;
using System.Globalization;
using System.Timers;
using System.Windows;

namespace SunCalendar
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        TaskbarIcon tb;
        Timer timer;

        protected override void OnStartup(StartupEventArgs e)
        {
            tb = (TaskbarIcon)FindResource("NotifyIcon");
            CreateTextIcon(GetToday());

            timer = new(GetTimerRemain() * 1000);
            timer.AutoReset = true;
            timer.Elapsed += Ti_Elapsed;
            timer.Start();

            base.OnStartup(e);
        }

        private void Ti_Elapsed(object? sender, ElapsedEventArgs e)
        {
            CreateTextIcon(GetToday());
            timer.Interval = GetTimerRemain() * 1000;
        }

        public void CreateTextIcon(string str)
        {
            Font fontToUse = new("Bungee Inline", 13, System.Drawing.FontStyle.Bold, GraphicsUnit.Pixel);
            Brush brushToUse = new SolidBrush(Color.Aqua);
            Bitmap bitmapText = new(16, 16);
            Graphics g = Graphics.FromImage(bitmapText);

            IntPtr hIcon;

            g.Clear(Color.Transparent);
            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
            g.DrawString(str, fontToUse, brushToUse, -4, -8);
            hIcon = bitmapText.GetHicon();
            tb.Icon = Icon.FromHandle(hIcon);
        }

        public static string FixNumber(int num)
        {
            string con = num.ToString();
            return con.Length == 1 ? $"0{con}" : con;
        }

        public static int GetTimerRemain()
        {
            var startTime = DateTime.Now;
            var aa = startTime.TimeOfDay;
            var total = Convert.ToInt32($"{aa.Hours}{FixNumber(aa.Minutes)}{FixNumber(aa.Seconds)}");
            var toEnd = (235959 - total).ToString();
            return (int)DateTime.Parse("2000/1/1 " + toEnd.Insert(toEnd.Length - 2, ":").Insert(toEnd.Length - 4, ":")).TimeOfDay.TotalSeconds + 2;
        }

        public static string GetToday()
        {
            var day = new PersianCalendar().GetDayOfMonth(DateTime.Today).ToString();
            return day.Length == 1 ? $"0{day}" : day;
        }

        private void TaskbarIcon_DoubleClick(object sender, RoutedEventArgs e) => MainWindow.Show();

        private void MenuItem_Click(object sender, RoutedEventArgs e) => Current.Shutdown();
    }
}
