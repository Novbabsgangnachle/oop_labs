using System;
using System.Collections.Generic;

namespace DefaultNamespace;

/// <summary>
/// Represents a route consisting of multiple route sections that a train can traverse.
/// </summary>
public class Route
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Route"/> class with the specified route sections and maximum allowed speed at the end.
    /// </summary>
    /// <param name="values">An enumerable collection of route sections that make up the route.</param>
    /// <param name="maxAllowedSpeed">The maximum allowed speed at the end of the route.</param>
    public Route(IEnumerable<RouteSection> values, double maxAllowedSpeed)
    {
        if (values == null)
        {
            throw new ArgumentException("Route sections cannot be null or empty", nameof(values));
        }

        if (maxAllowedSpeed <= 0)
        {
            throw new ArgumentException("Max allowed speed must be greater than zero", nameof(maxAllowedSpeed));
        }

        this.Sections = new List<RouteSection>(values).AsReadOnly();
        this.MaxAllowedSpeed = maxAllowedSpeed;
    }

    /// <summary>
    /// Gets the read-only list of route sections that make up the route.
    /// </summary>
    public IReadOnlyList<RouteSection> Sections { get; }

    /// <summary>
    /// Gets the maximum allowed speed at the end of the route.
    /// </summary>
    private double MaxAllowedSpeed { get; }

    /// <summary>
    /// Processes the train through all sections of the route.
    /// </summary>
    /// <param name="train">The train that will traverse the route.</param>
    /// <returns>
    /// A tuple where the first item indicates success (true if the train successfully completes the route, false otherwise),
    /// and the second item is the total time taken to traverse the route up to the point of failure or completion.
    /// </returns>
    public TravelResultDto ProcessRouting(Train train)
    {
        if (train == null)
        {
            throw new ArgumentNullException(nameof(train), "Train cannot be null");
        }

        double totalTime = 0;
        foreach (RouteSection section in this.Sections)
        {
            TravelResultDto tempResult = section.ProcessSection(train);
            if (!tempResult.Success)
            {
                return new TravelResultDto(false, totalTime);
            }

            totalTime += tempResult.TotalTime;
        }

        if (!train.TryStopAtEnd(this.MaxAllowedSpeed))
        {
            return new TravelResultDto(false, totalTime);
        }

        return new TravelResultDto(true, totalTime);
    }

    /// <summary>
    /// Calculates the force required for the train to reach a specified speed over a given distance.
    /// </summary>
    /// <param name="train">The train for which to calculate the required force.</param>
    /// <param name="speed">The target speed to achieve.</param>
    /// <param name="distance">The distance over which the train should reach the target speed.</param>
    /// <returns>The required force to accelerate the train to the specified speed over the given distance.</returns>
    public double GetsForceByNeededSpeed(Train train, double speed, double distance)
    {
        if (distance <= 0)
        {
            throw new ArgumentException("Distance must be greater than zero", nameof(distance));
        }

        if (speed <= 0)
        {
            throw new ArgumentException("Speed must be greater than zero", nameof(speed));
        }

        const double factor = 2;  // Based on the formula F = (m * v^2) / (2 * d)
        return train.Mass * speed * speed / (factor * distance);
    }
}