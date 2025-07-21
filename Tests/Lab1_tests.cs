using System;
using DefaultNamespace;
using Xunit;

namespace Tests
{
    /// <summary>
/// Contains unit tests for the train and route simulation classes.
/// </summary>
public class XTests
{
    /// <summary>
    /// Test scenario 1: Verifies that a train can successfully traverse a route consisting of a force magnetic path followed by a regular magnetic path when the end speed limit is high.
    /// Expected result: Success.
    /// </summary>
    [Fact]
    public void FirstTest()
    {
        var train = new Train(3000, 100, 2);
        var forcePath = new ForceMagneticPath(100, 100);
        var regPath = new RegularMagneticPath(100);

        RouteSection[] temp = { forcePath, regPath };

        var route = new Route(temp, 1000000);
        TravelResultDto result = route.ProcessRouting(train);
        Assert.True(result.Success);
    }

    /// <summary>
    /// Test scenario 2: Verifies that a train fails to stop at the end of the route when the end speed limit is very low.
    /// Expected result: Failure.
    /// </summary>
    [Fact]
    public void SecondTest()
    {
        var train = new Train(3000, 100, 2);
        var forcePath = new ForceMagneticPath(100, 100);
        var regPath = new RegularMagneticPath(100);

        RouteSection[] temp = { forcePath, regPath };

        var route = new Route(temp, 1);
        TravelResultDto result = route.ProcessRouting(train);
        Assert.False(result.Success);
    }

    /// <summary>
    /// Test scenario 3: Verifies that a train can successfully traverse a route that includes a station with no speed limit issues.
    /// Expected result: Success.
    /// </summary>
    [Fact]
    public void ThirdTest()
    {
        var train = new Train(3000, 100, 2);
        var forcePath = new ForceMagneticPath(100, 100);
        var regPath = new RegularMagneticPath(100);
        var station = new Station(1000000, 0.5);

        RouteSection[] temp = { forcePath, regPath, station, regPath };

        var route = new Route(temp, 1000000);
        TravelResultDto result = route.ProcessRouting(train);
        Assert.True(result.Success);
    }

    /// <summary>
    /// Test scenario 4: Verifies that a train fails to stop at a station when approaching at a speed exceeding the station's maximum allowed speed.
    /// Expected result: Failure.
    /// </summary>
    [Fact]
    public void FourthTest()
    {
        var train = new Train(3000, 100, 2);
        var forcePath = new ForceMagneticPath(100, 100);
        var regPath = new RegularMagneticPath(100);
        var station = new Station(1, 0.5); // Station with low max allowed speed
        RouteSection[] temp = { forcePath, station, regPath };
        var route = new Route(temp, 1000000);
        TravelResultDto result = route.ProcessRouting(train);
        Assert.False(result.Success);
    }

    /// <summary>
    /// Test scenario 5: Verifies that a train fails to stop at the end of the route when the end speed limit is very low, even after passing through a station successfully.
    /// Expected result: Failure.
    /// </summary>
    [Fact]
    public void FifthTest()
    {
        var train = new Train(3000, 100, 2);
        var forcePath = new ForceMagneticPath(100, 100);
        var regPath = new RegularMagneticPath(100);
        var station = new Station(1000000, 0.5);

        RouteSection[] temp = { forcePath, regPath, station, regPath };

        var route = new Route(temp, 1); // End speed limit is too low
        TravelResultDto result = route.ProcessRouting(train);
        Assert.False(result.Success);
    }

    /// <summary>
    /// Test scenario 6: Simulates a complex route where the train accelerates above the station's speed limit, decelerates before the station, passes the station, accelerates above the route's speed limit, and then decelerates before the end.
    /// Expected result: Success.
    /// </summary>
    [Fact]
    public void SixthTest()
    {
        var train = new Train(3000, 1000, 0.1);

        var forcePath1 = new ForceMagneticPath(1000, 1000); // Accelerate above station speed limit
        var regPath1 = new RegularMagneticPath(500);
        var forcePath2 = new ForceMagneticPath(800, -1000); // Decelerate to station speed limit
        var station = new Station(100, 0.5); // Station with speed limit
        var regPath2 = new RegularMagneticPath(500);
        var forcePath3 = new ForceMagneticPath(1200, 1000); // Accelerate above route speed limit
        var regPath3 = new RegularMagneticPath(500);
        var forcePath4 = new ForceMagneticPath(1000, -1000); // Decelerate to route speed limit

        RouteSection[] routeSections =
        {
            forcePath1, regPath1, forcePath2, station,
            regPath2, forcePath3, regPath3, forcePath4,
        };
        var route = new Route(routeSections, 80); // Route speed limit

        TravelResultDto result = route.ProcessRouting(train);
        Assert.True(result.Success);
    }

    /// <summary>
    /// Test scenario 7: Verifies that a train fails to traverse a regular magnetic path without initial speed or acceleration.
    /// Expected result: Failure.
    /// </summary>
    [Fact]
    public void SeventhTest()
    {
        var train = new Train(3000, 100, 2);
        var regPath = new RegularMagneticPath(100);

        RouteSection[] temp = { regPath };

        var route = new Route(temp, 10000);
        TravelResultDto result = route.ProcessRouting(train);
        Assert.False(result.Success);
    }

    /// <summary>
    /// Test scenario 8: Verifies that a train fails to traverse a route where the applied forces cancel each other out, resulting in insufficient speed to complete the route.
    /// Expected result: Failure.
    /// </summary>
    [Fact]
    public void EightTest()
    {
        var train = new Train(3000, 100, 2);
        var forcePath = new ForceMagneticPath(100, 100);
        var unForcePath = new ForceMagneticPath(100, -100);

        RouteSection[] temp = { forcePath, unForcePath };

        var route = new Route(temp, 10000);
        TravelResultDto result = route.ProcessRouting(train);
        Assert.False(result.Success);
    }
}
}