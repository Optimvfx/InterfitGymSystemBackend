using CLL.ControllersLogic;
using CLL.ControllersLogic.Interface;

namespace TestsLayer.ControllersLogic;

public abstract class VisitsLogicTests
{
    private IVisitsLogic _visitsLogic;
    
    [OneTimeSetUp]
    public void OneTimeSetUp()
    {
        
    }
    
    [SetUp]
    public void Setup()
    {
    }

    [Test]
    public void EnterTest()
    {
        
        _visitsLogic.Register()
    }

    [TearDown]
    public void TearDown()
    {
        
    }
}