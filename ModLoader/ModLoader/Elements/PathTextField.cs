// File: PathTextField.cs
// Project: ModLoader
// Author: Akasus (akasus.nevelas@gmail.com)
// Description:

using NStack;
using Terminal.Gui;

namespace ModLoader.Elements;

public class PathTextField : TextField
{
    public PathTextField()
    {
        Autocomplete.AllSuggestions = DriveInfo.GetDrives().Select(e => e.Name).ToList();
        //TextChanged += HostcontrolOnTextChanged;
        TextChanging += OnTextChanging;
        Autocomplete.Visible = true;
        AutoSize = true;
    }

    private void OnTextChanging(TextChangingEventArgs obj)
    {
        var path = obj.NewText.ToString();
        if (string.IsNullOrEmpty(path))
        {
            DriveInfo.GetDrives().Select(e => e.Name).ToList();
            return;
        }

        //if (path.Contains(":\\"))
        //{
        //    var lastbit = path.LastIndexOf(Path.DirectorySeparatorChar);
        //    var Root = path.Substring(0, lastbit + 1);
        //    var search = path.Substring(lastbit + 1);
        //    var dirs = Directory.GetDirectories(Root, $"{search}*").ToList();
        //    Autocomplete.AllSuggestions = dirs;
        //    Autocomplete.Visible = true;
        //    Autocomplete.GenerateSuggestions();
        //}
    }
}