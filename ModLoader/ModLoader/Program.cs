



using System.Data;
using System.Runtime.CompilerServices;
using ModLoader.Data;
using ModLoader.Themes;
using ModLoader.Views;
using NStack;
using SUCC;
using Terminal.Gui;


namespace ModLoader;

internal class Program
{
    public static Config Configuration;
    private static DataFile _configDataFile;
    private static Label _programLabel;

    public static void SaveConfig()
    {
        _configDataFile.SaveAsObject(Configuration);
    }


    static void Main(string[] args)
    {
        _configDataFile = new DataFile("ModLoader")
        {
            AutoReload = true
        };
        Configuration = _configDataFile.GetAsObject<Config>();
        Application.Init();
        _programLabel = new Label("Logic World Mod Manager", false)
        {
            X = Pos.Center(),
            Y = 2,
            Width = Dim.Fill(4),
            TextAlignment = TextAlignment.Centered,
            Border = new Border
            {
                Background = Color.Black,
                BorderStyle = BorderStyle.Rounded,
                Padding = new Thickness(1),
            }
        };

        Application.Top.Add(_programLabel);
        ShowSetupWizard();
        ModRepository.Init(Configuration.RepositoryFolder);
        ModFolder.Init(Configuration.GameDataFolder);
        MainMenu();
    }

    public static void ShowSetupWizard()
    {
        //Check if the folders for GameData and the Repo already set
        if (!string.IsNullOrEmpty(Configuration.RepositoryFolder) || !string.IsNullOrEmpty(Configuration.GameDataFolder))
        {
            return;
        }

        var wizard = new Wizard("Logic World Mod Manager");
        wizard.AddStep(new Wizard.WizardStep("Specify Directories")
        {
            Y = Pos.Top(_programLabel) + 3,
            HelpText = "You need to specify a Directory to save the downloaded mods\n" +
                       "and where the GameData Folder from the Logic World Game is! ",
            TextAlignment = TextAlignment.Centered,
            AutoSize = true,
        });
        Application.Run(wizard);

        var RepoDialog = new OpenDialog(
            "Specify Repository folder",
            "Choose a folder where you want to store the mods.\n"
            + "This must not be the GameData folder of the Game",
            null,
            OpenDialog.OpenMode.Directory)
        {
            Y = Pos.Top(_programLabel) + 3,
            CanChooseDirectories = true,
            CanCreateDirectories = true,
            CanChooseFiles = false,
            AllowsMultipleSelection = false,
            DirectoryPath = Configuration.RepositoryFolder ?? "",
        };
        Application.Run(RepoDialog);
        var IsRepoPath = Directory.Exists(RepoDialog.FilePath.ToString());
        Configuration.RepositoryFolder = RepoDialog.FilePath.ToString();
        if (!IsRepoPath)
        {
            var dialog = MessageBox.Query(
                "Path Does Not Exist...",
                "The specified Repo Path does not exist!\n"
                + "Would you like to create it ?",
                0, "Yes", "No");
            if (dialog == 0)
            {
                Directory.CreateDirectory(RepoDialog.FilePath.ToString());
            }
        }


        var ps = Path.DirectorySeparatorChar;
        var GameDataDialog = new OpenDialog(
            "Specify GameData folder", "Enter the GameData Path of Logic World in your Steam Library.\n"
                   + "This should be somewhere like :" + $"steamapps{ps}common{ps}Logic World{ps}GameData",
            null,
            OpenDialog.OpenMode.Directory)
        {
            Y = Pos.Top(_programLabel) + 3,
            CanChooseDirectories = true,
            CanCreateDirectories = true,
            AllowsMultipleSelection = false,
            CanChooseFiles = false,
            DirectoryPath = Configuration.GameDataFolder ?? "",
        };

        Application.Run(GameDataDialog);
        Configuration.GameDataFolder = GameDataDialog.FilePath.ToString();
        SaveConfig();
    }

    public static void MainMenu()
    {
        var window = new Window()
        {
            ColorScheme = ColorThemes.DefaultScheme,
            Y = Pos.Bottom(_programLabel) +1 ,
            Width = Dim.Fill(),
            Height = Dim.Fill(),
            Border = new Border
            {
                BorderStyle = BorderStyle.None
            }
        };
        var tabs = new TabView()
        {
            Width = Dim.Fill(3),
            Height = Dim.Fill(3)
        };
        window.Add(tabs);
        var InstalledTab = new ModView("Installed", ModRepository.GetInstalledMods);
        tabs.AddTab(InstalledTab,true);
        Application.Top.Add(window);
        Application.Run();
    }
}