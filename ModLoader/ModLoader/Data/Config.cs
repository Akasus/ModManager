// File: Config.cs
// Project: ModLoader
// Author: Akasus (akasus.nevelas@gmail.com)
// Description:

using SUCC;

namespace ModLoader.Data;

public class Config
{
    [SaveThis] public string? RepositoryFolder { get; set; } = "";
    [SaveThis] public string? GameDataFolder { get; set; } = "";

}