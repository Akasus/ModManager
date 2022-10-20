// File: ModEntryView.cs
// Project: ModLoader
// Author: Akasus (akasus.nevelas@gmail.com)
// Description:

using System.Timers;
using Terminal.Gui;
using Timer = System.Timers.Timer;

namespace ModLoader.Components;

public partial class ModEntryView : Window
{
    private Timer timer;
    private float f = 0;
    public ModEntryView(Mod mod)
    {
        InitializeComponent();
        var f = 0.0f;
        timer = new System.Timers.Timer();
        timer.AutoReset = true;
        timer.Interval = 10;
        timer.Elapsed += TimerOnElapsed;
    }

    private void TimerOnElapsed(object? sender, ElapsedEventArgs e)
    {
        f += 0.02f;
        if (f < 1)
            downloadProgress.Fraction = f;
        else
        {
            timer.Stop();
            Remove(downloadProgress);
            downloadButton.Text = "Remove";
            downloadButton.Visible = true;
        }
    }

    private void OnDownloadButtonClicked()
    {
        downloadButton.Visible = false;
        downloadProgress.Visible = true;
        timer.Start();
    }
}