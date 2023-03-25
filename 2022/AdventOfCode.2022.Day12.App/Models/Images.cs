using System;
using System.Windows.Media;
using System.Windows.Media.Imaging;

namespace AdventOfCode._2022.Day12.App.Models;

/// <summary>
/// Container for all image assets
/// </summary>
public static class Images
{
    public static readonly ImageSource Empty = LoadImage("Empty.png");
    public static readonly ImageSource Body = LoadImage("Body.png");
    public static readonly ImageSource Food = LoadImage("Food.png");
    public static readonly ImageSource Head = LoadImage("Head.png");
    public static readonly ImageSource DeadBody = LoadImage("DeadBody.png");
    public static readonly ImageSource DeadHead = LoadImage("DeadHead.png");

    private static ImageSource LoadImage(string filename)
    {
        return new BitmapImage(new Uri($"Assets/{filename}", UriKind.Relative));
    }
}