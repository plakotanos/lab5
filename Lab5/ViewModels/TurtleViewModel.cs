using System;
using System.Collections.ObjectModel;
using System.Linq;
using Avalonia.Media;
using Avalonia.Media.Imaging;
using Avalonia.Platform;
using DynamicData;
using Lab3.Models;
using ReactiveUI;

namespace Lab5.ViewModels;

public class TurtleViewModel : ViewModelBase
{
    private double _actualX;
    private double _actualY;

    private string _rotate = "rotate(0deg)";
    private IImage _image = null!;
    private Turtle _t = null!;

    public Turtle Value
    {
        get => _t;
        set
        {
            this.RaiseAndSetIfChanged(ref _t, value);
            ActualX = value.X;
            ActualY = value.Y;
            Rotate = $"rotate({-value.Direction % 360 + 90}deg)";
            try
            {
                Image = value.Color switch
                {
                    PenColor.Black => new Bitmap(AssetLoader.Open(new Uri("avares://Lab5/Assets/turtle-black.png"))),
                    PenColor.Green => new Bitmap(AssetLoader.Open(new Uri("avares://Lab5/Assets/turtle-green.png"))),
                    _ => null!
                };
            }
            catch
            {
                Image = null!;
            }

            Path.Clear();
            Path.AddRange(
                (
                    from step in value.Path
                    select new StepViewModel(step)
                )
                .Union(
                    from figure in value.Figures
                    from step in figure.Steps
                    select new StepViewModel(step)
                )
            );
        }
    }

    public IImage Image
    {
        get => _image;
        set => this.RaiseAndSetIfChanged(ref _image, value);
    }

    public double ActualX
    {
        get => _actualX;
        set => this.RaiseAndSetIfChanged(ref _actualX, value * 25 + 205);
    }

    public double ActualY
    {
        get => _actualY;
        set => this.RaiseAndSetIfChanged(ref _actualY, -value * 25 + 205);
    }

    public string Rotate
    {
        get => _rotate;
        set => this.RaiseAndSetIfChanged(ref _rotate, value);
    }

    public ObservableCollection<StepViewModel> Path { get; } = new();

    public TurtleViewModel(Turtle t)
    {
        Value = t;
    }
}