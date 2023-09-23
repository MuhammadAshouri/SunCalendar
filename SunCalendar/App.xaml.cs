using Hardcodet.Wpf.TaskbarNotification;
using System;
using System.Drawing;
using System.Globalization;
using System.Timers;
using System.Windows;

namespace SunCalendar;

public partial class App
{
    private TaskbarIcon? Tb;
    private Timer? Timer;
        
    protected override void OnStartup(StartupEventArgs e)
    {
        Tb = (TaskbarIcon?)FindResource("NotifyIcon");
        CreateTextIcon(GetToday());

        Timer = new Timer(GetTimerRemain() * 1000);
        Timer.AutoReset = true;
        Timer.Elapsed += Ti_Elapsed;
        Timer.Start();

        base.OnStartup(e);
    }

    private void Ti_Elapsed(object? sender, ElapsedEventArgs e)
    {
        CreateTextIcon(GetToday());
        if (Timer is not null) Timer.Interval = GetTimerRemain() * 1000;
    }

    private void CreateTextIcon(string str)
    {
        Font fontToUse = new("Bungee Inline", 13, System.Drawing.FontStyle.Bold, GraphicsUnit.Pixel);
        Brush brushToUse = new SolidBrush(Color.Aqua);
        Bitmap bitmapText = new(16, 16);
        var g = Graphics.FromImage(bitmapText);

        g.Clear(Color.Transparent);
        g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAlias;
        g.DrawString(str, fontToUse, brushToUse, 0, 0);
        var hIcon = bitmapText.GetHicon();
        if (Tb is not null) Tb.Icon = Icon.FromHandle(hIcon);
    }

    private static string FixNumber(int num)
    {
        var con = num.ToString();
        return con.Length == 1 ? $"0{con}" : con;
    }

    private static int GetTimerRemain()
    {
        var startTime = DateTime.Now;
        var aa = startTime.TimeOfDay;
        var total = Convert.ToInt32($"{aa.Hours}{FixNumber(aa.Minutes)}{FixNumber(aa.Seconds)}");
        var toEnd = (235959 - total).ToString();
        return (int)DateTime.Parse("2000/01/01 " + toEnd.Insert(toEnd.Length - 2, ":").Insert(toEnd.Length - 4, ":")).TimeOfDay.TotalSeconds + 2;
    }

    private static string GetToday()
    {
        var day = new PersianCalendar().GetDayOfMonth(DateTime.Today).ToString();
        return day.Length == 1 ? $"0{day}" : day;
    }

    private void TaskbarIcon_DoubleClick(object sender, RoutedEventArgs e) => MainWindow?.Show();

    private void MenuItem_Click(object sender, RoutedEventArgs e) => Current.Shutdown();
}