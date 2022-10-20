// File: ModView.cs
// Project: ModLoader
// Author: Akasus (akasus.nevelas@gmail.com)
// Description:

using System.Data;
using System.Timers;
using ModLoader.Themes;
using Terminal.Gui;
using Terminal.Gui.Graphs;

namespace ModLoader.Views;

public class ModView : TabView.Tab
{
    private ScrollView modListView;
    public ModView(string title, Func<IEnumerable<Mod>> modRequest)
    {
        Text = title;
        var mods = modRequest.Invoke().Select(e => new ModEntryView(e)).ToArray();
     
        modListView = new ScrollView()
        {
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            Y = Pos.Center(),
            X = Pos.Center(),
            ColorScheme = ColorThemes.DefaultScheme,
            Border = new Border
            {
                BorderStyle = BorderStyle.Rounded
            },
            AutoSize = true,
        };
        modListView.DrawContent += rect => modListView.ContentSize = new Size(rect.Width, 5 * mods.Length);

        View = modListView;
        //var lineView = new LineView(Orientation.Vertical)
        //{
        //    Width = Dim.Fill(),
        //    Height = Dim.Fill(),
        //};
        modListView.Add(mods);
        //lineView.Add(mods);
        for (int i = 1; i < mods.Count(); i++)
        {
            mods[i].Y = Pos.Bottom(mods[i-1]);
        }

        //modListView.Add(new Label("Lala")
        //{
        //    Height = Dim.Fill(),
        //    Width = Dim.Fill(),
        //});
    }
}


public class ModEntryView : Window
{
    private Button downloadButton;
    private Button enableButton;
    private Button removeButton;

    private Label modName;
    private Label modDescription;
    private Label modVersion;
    private Label modAuthor;
    private ProgressBar downloadProgress;

    public ModEntryView(Mod mod)
    {
        AutoSize = true;
        Width = Dim.Fill(1);
        Height = Dim.Sized(6);
        ColorScheme = ColorThemes.DefaultScheme;
        Border = new Border
        {
            BorderStyle = BorderStyle.Rounded
        };
        modName = new Label(mod.Name)
        {
            X = 1,
        };
        modVersion = new Label(mod.Manifest.Version.ToString())
        {
            X = Pos.Center()
        };
        modAuthor = new Label(mod.Manifest.Author)
        {
            X = Pos.AnchorEnd() - mod.Manifest.Author.Length - 3,
        };
        modDescription = new Label(mod.Description)
        {
            Y = Pos.Bottom(modName),
            X = 2,
            Width = Dim.Fill(5),
            Height = 2,
        };
        Add(modName,modVersion,modAuthor,modDescription);
        
        downloadButton = new Button("Download")
        {
            Y = Pos.Bottom(modDescription),
        };
        downloadProgress = new ProgressBar()
        {
            Y = Pos.Bottom(modDescription),
            Width = Dim.Percent(25),
        };
        downloadProgress.ProgressBarStyle = ProgressBarStyle.MarqueeContinuous;
        var f = 0.0f;
        System.Timers.Timer timer = new System.Timers.Timer();
        timer.AutoReset = true;
        timer.Interval = 20;
        timer.Elapsed += (sender, args) =>
        {
            f += 0.02f;
            if (f < 1)
                downloadProgress.Fraction = f;
            else
            {
                timer.Stop();
                Remove(downloadProgress);
                downloadButton.Text = "Remove";
                Add(downloadButton);
            }
        };

        downloadButton.Clicked += () =>
        {
            Remove(downloadButton);
            Add(downloadProgress);
            
            timer.Start();
        };

        Add(downloadButton);

    }
}