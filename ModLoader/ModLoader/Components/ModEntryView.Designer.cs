// File: ModEntryView.Designer.cs
// Project: ModLoader
// Author: Akasus (akasus.nevelas@gmail.com)
// Description:

using Terminal.Gui;
using ModLoader.Themes;
namespace ModLoader.Components;

public partial class ModEntryView
{
    private Button downloadButton;
    private Button enableButton;
    private Button removeButton;

    private Label modName;
    private Label modDescription;
    private Label modVersion;
    private Label modAuthor;
    private ProgressBar downloadProgress;

    private void InitializeComponent()
    {
        this.AutoSize = true;
        this.Width = Dim.Fill(1);
        this.Height = Dim.Sized(6);
        this.ColorScheme = ColorThemes.DefaultScheme;
        this.Border = new Border();
        this.Border.BorderStyle = BorderStyle.Rounded;


        modName = new Label();
        modName.X = 1;
        
        modVersion = new Label();
        modVersion.X = Pos.Center();
        
        modAuthor = new Label();
        modAuthor.X = Pos.AnchorEnd() - Pos.Function(() => modAuthor.Text.Length) - 3;

        modDescription = new Label();
        modDescription.Y = Pos.Bottom(modName);
        modDescription.X = 2;
        modDescription.Width = Dim.Fill(5);
        modDescription.Height = 2;
        

        downloadButton = new Button("Download");
        downloadButton.Y = Pos.Bottom(modDescription);
        downloadButton.Clicked += OnDownloadButtonClicked;

        downloadProgress = new ProgressBar();
        downloadProgress.Y = Pos.Bottom(modDescription);
        downloadProgress.Width = Dim.Percent(33);
        downloadProgress.Visible = false;
        
        Add(modName, modVersion, modAuthor, modDescription, downloadProgress);
    }
}