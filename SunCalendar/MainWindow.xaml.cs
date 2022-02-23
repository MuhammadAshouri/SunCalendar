using System;
using System.Globalization;
using System.Windows;

namespace SunCalendar
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        DateTime LastTime;

        public MainWindow() => InitializeComponent();

        private void WindowLoaded(object sender, RoutedEventArgs e)
        {
            Hide();
            FillFields(DateTime.Now);
        }

        private void CloseClick(object sender, System.Windows.Input.MouseButtonEventArgs e) => Hide();

        private void Window_IsVisibleChanged(object sender, DependencyPropertyChangedEventArgs e)
        {
            var now = DateTime.Now;
            if (LastTime.DayOfYear != now.DayOfYear)
            {
                LastTime = now;
                FillFields(now);
            }
        }

        void FillFields(DateTime now)
        {
            var cal = new PersianCalendar();
            TodayShamsiDay.Text = cal.GetDayOfMonth(now).ToString();
            TodayShamsiMonth.Text = cal.GetMonth(now).ToString();
            TodayShamsiYear.Text = cal.GetYear(now).ToString();

            TodayMiladi.Text = $"{now.Year}/{now.Month}/{now.Day}";
            var hijri = new HijriCalendar();
            TodayHijri.Text = $"{hijri.GetYear(now)}/{hijri.GetMonth(now)}/{hijri.GetDayOfMonth(now)}";

            TodayName.Text = now.DayOfWeek.ToString();
        }
    }
}
