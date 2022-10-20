// File: SetupWizard.cs
// Project: ModLoader
// Author: Akasus (akasus.nevelas@gmail.com)
// Description:

using System.Runtime.CompilerServices;
using System.Text.RegularExpressions;
using ModLoader.DirectoryBullshit;
using ModLoader.Elements;
using Terminal.Gui;
using Terminal.Gui.Graphs;

namespace ModLoader
{

    public class SetupWizard : Wizard
    {
        public SetupWizard()
        {
            Title = "Setup Wizard";
            X = Pos.Center();
            Y = Pos.Center();
            AutoSize = true;
            var ps = Path.DirectorySeparatorChar;

            var LWPath = $"steamapps{ps}common{ps}Logic World{ps}GameData";

            //Search for Steamapps Folder
            //var LWfullPath = DriveInfo.GetDrives().AsParallel()
            //    .First(e => e.RootDirectory.FindDirectories(LWPath, SearchOption.AllDirectories).Any()).RootDirectory
            //    .FindDirectories(LWPath, SearchOption.AllDirectories).First().FullName;

            var firstStep = new PathWizardStep("Specify Repository folder");
            var secondStep = new PathWizardStep("Specify GameData folder",LWPath);

            StepChanging += args =>
            {
                if(args.OldStep == null) return;
                if (args.OldStep.Id == firstStep.Id)
                {
                    Program.Configuration.RepositoryFolder = (args.OldStep as PathWizardStep)?.Path ?? "";
                }

                if (args.OldStep.Id == secondStep.Id)
                {
                    Program.Configuration.GameDataFolder = (args.OldStep as PathWizardStep)?.Path ?? "";
                }
            };
            AddStep(firstStep);
            AddStep(secondStep);
        }
    }


    public class PathWizardStep : Wizard.WizardStep
    {
        public string? Path => TextField.Text.ToString();

        public PathTextField TextField { get; private set; }

        public PathWizardStep(string title, string preview = "") : base(title)
        {
          
            var label = new Label("Path:") { AutoSize = true, Y = 1 };
            TextField = new PathTextField()
            {
                Y = 1,
                X = Pos.Right(label) + 1,
                Width = Dim.Fill() - 1,
                Text = preview
            };
            Add(label,TextField);
        }
    }
}