using Avalonia;
using ReactiveUI;

namespace Lab5.ViewModels;

public class PointViewModel(double x, double y) : ViewModelBase
{
    public double X
    {
        get => x * 25 + 225;
        set
        {
            this.RaiseAndSetIfChanged(ref x, value);
            this.RaisePropertyChanged(nameof(Point));
        }
    }

    public double Y
    {
        get => -y * 25 + 225;
        set
        {
            this.RaiseAndSetIfChanged(ref y, value);
            this.RaisePropertyChanged(nameof(Point));
        }
    }

    public Point Point
    {
        get => new(X, Y);
    }
}