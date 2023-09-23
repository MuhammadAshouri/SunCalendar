using System;
using System.Globalization;
using System.Windows;

namespace SunCalendar;

public partial class MainWindow
{
    private DateTime LastTime;
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
        if (LastTime.DayOfYear == now.DayOfYear) return;
        LastTime = now;
        FillFields(now);
    }

    private void FillFields(DateTime now)
    {
        var cal = new PersianCalendar();
        var dayShamsi = cal.GetDayOfMonth(now);
        var monthShamsi = cal.GetMonth(now);
        var dayShamsiStr = dayShamsi.CompareTo(10) >= 0 ? dayShamsi.ToString() : "0" + dayShamsi;
        var monthShamsiStr = monthShamsi.CompareTo(10) >= 0 ? monthShamsi.ToString() : "0" + monthShamsi;
        TodayShamsiDay.Text = dayShamsiStr;
        TodayShamsiMonth.Text = monthShamsiStr;
        TodayShamsiYear.Text = cal.GetYear(now).ToString();
        
        var day = now.Day.CompareTo(10) >= 0 ? now.Day.ToString() : "0" + now.Day;
        var month = now.Month.CompareTo(10) >= 0 ? now.Month.ToString() : "0" + now.Month;
        TodayMiladi.Text = $"{now.Year}/{month}/{day}";
        
        var hijri = new HijriCalendar();
        var dayHijri = hijri.GetDayOfMonth(now);
        var monthHijri = hijri.GetMonth(now);
        var dayHijriStr = dayHijri.CompareTo(10) >= 0 ? dayHijri.ToString() : "0" + dayHijri;
        var monthHijriStr = monthHijri.CompareTo(10) >= 0 ? monthHijri.ToString() : "0" + monthHijri;
        TodayHijri.Text = $"{hijri.GetYear(now)}/{monthHijriStr}/{dayHijriStr}";
        
        TodayName.Text = now.DayOfWeek.ToString();
    }
}