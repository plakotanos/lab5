using System;
using Avalonia.Media;
using Lab3.Models;
using Microsoft.EntityFrameworkCore.Query;
using ReactiveUI;

namespace Lab5.ViewModels;

public class StepViewModel : ViewModelBase
{
    private IBrush _color = Brushes.Black;

    private Step _step = null!;

    public PointViewModel Start { get; }
    public PointViewModel End { get; }

    public IBrush Color
    {
        get => _color;
        set => this.RaiseAndSetIfChanged(ref _color, value);
    }

    public Step Value
    {
        get => _step;
        set
        {
            this.RaiseAndSetIfChanged(ref _step, value);
            Start.X = value.StartX;
            Start.Y = value.StartY;
            End.X = value.EndX;
            End.Y = value.EndY;

            Color = value.Color switch
            {
                PenColor.Black => Brushes.Black,
                PenColor.Green => Brushes.Green,
                _ => Brushes.Black
            };
        }
    }

    public StepViewModel(Step step)
    {
        Start = new(step.StartX, step.StartY);
        End = new(step.EndX, step.EndY);
        Value = step;
    }
}