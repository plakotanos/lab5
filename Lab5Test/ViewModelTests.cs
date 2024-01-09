using Lab5.ViewModels;

namespace Lab5Test;

public class ViewModelTests
{
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void PointViewModel_SetCoordinates_ConvertsToPxValues()
    {
        var vm = new PointViewModel(0, 0);

        vm.X = 1;
        vm.Y = 1;
        
        Assert.Multiple(() =>
        {
            Assert.That(vm.X, Is.EqualTo(250));
            Assert.That(vm.Y, Is.EqualTo(200));
        });
    }

    [Test]
    public void TurtleViewModel_SetCoordinates_ConvertsToPxValues()
    {
        var vm = new TurtleViewModel(new());

        vm.ActualX = 1;
        vm.ActualY = 1;
        
        Assert.Multiple(() =>
        {
            Assert.That(vm.ActualX, Is.EqualTo(230));
            Assert.That(vm.ActualY, Is.EqualTo(180));
        });
    }
}