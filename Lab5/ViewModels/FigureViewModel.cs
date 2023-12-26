using System.Collections.ObjectModel;
using System.Linq;
using DynamicData;
using Lab3.Models;
using ReactiveUI;

namespace Lab5.ViewModels;

public class FigureViewModel : ViewModelBase
{
    private Figure _figure = null!;
    
    public Figure Value
    {
        get => _figure;
        set
        {
            this.RaiseAndSetIfChanged(ref _figure, value);
            Steps.Clear();
            Steps.AddRange(from step in value.Steps select new StepViewModel(step));
        }
    }
    public ObservableCollection<StepViewModel> Steps { get; } = new();

    public FigureViewModel(Figure figure)
    {
        Value = figure;
    }
}