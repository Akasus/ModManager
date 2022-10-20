using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Terminal.Gui;
using Attribute = Terminal.Gui.Attribute;

namespace ModLoader.Themes
{
    public class ColorThemes
    {
        public static readonly ColorScheme DefaultScheme = new ColorScheme
        {
            Normal = Attribute.Make(Color.White,Color.Black),
            Disabled = Attribute.Make(Color.DarkGray,Color.Black),
            Focus = Attribute.Make(Color.BrightGreen,Color.DarkGray),
            HotFocus = Attribute.Make(Color.BrightRed,Color.DarkGray),
            HotNormal = Attribute.Make(Color.Red,Color.Black),
        };


        public static readonly ColorScheme BlueScheme = new ColorScheme
        {
            Normal = Attribute.Make(Color.White, Color.Blue),
            Disabled = Attribute.Make(Color.DarkGray, Color.DarkGray),
            Focus = Attribute.Make(Color.BrightGreen, Color.DarkGray),
            HotFocus = Attribute.Make(Color.BrightRed, Color.DarkGray),
            HotNormal = Attribute.Make(Color.Red, Color.Black),
        };
    }
}
