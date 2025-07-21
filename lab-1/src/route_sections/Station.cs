using System;

namespace DefaultNamespace;

/// <summary>
/// Represents a station section in the route where the train stops for passenger boarding and alighting.
/// The station imposes a maximum allowed speed for incoming trains and includes a workload factor that affects the stop duration.
/// </summary>
public class Station : RouteSection
{
    private const double StopTimeMultiplier = 2;

    /// <summary>
    /// Initializes a new instance of the <see cref="Station"/> class with the specified maximum allowed speed and workload factor.
    /// </summary>
    /// <param name="maxAllowedSpeed">The maximum allowed speed for a train entering the station.</param>
    /// <param name="factor">The workload factor influencing the duration of the stop (value between 0 and 1).</param>
    /// <exception cref="ArgumentException">
    /// Thrown when <paramref name="maxAllowedSpeed"/> is less than or equal to zero,
    /// or when <paramref name="factor"/> is outside the range of 0 to 1.
    /// </exception>
    public Station(double maxAllowedSpeed, double factor)
    {
        if (maxAllowedSpeed <= 0 || factor < 0 || factor > 1)
        {
            throw new ArgumentException("Invalid arguments");
        }

        this.MaxAllowedSpeed = maxAllowedSpeed;
        this.WorkloadFactor = factor;
    }

    /// <summary>
    /// Gets the maximum allowed speed for a train entering the station.
    /// </summary>
    private double MaxAllowedSpeed { get; }

    /// <summary>
    /// Gets the workload factor that affects the duration of the stop at the station.
    /// </summary>
    private double WorkloadFactor { get; }

    /// <summary>
    /// Processes the train passing through the station, determining if it can stop and calculating the time spent.
    /// </summary>
    /// <param name="train">The train attempting to pass through the station.</param>
    /// <returns>
    /// A tuple where the first item indicates success (true if the train successfully stops at the station, false otherwise),
    /// and the second item is the time taken for the stop, including boarding and alighting, influenced by the workload factor.
    /// </returns>
    public override TravelResultDto ProcessSection(Train train)
    {
        if (train == null)
        {
            throw new ArgumentNullException(nameof(train), "Train cannot be null");
        }

        if (train.Speed > this.MaxAllowedSpeed)
        {
            return new TravelResultDto(false, 0);
        }

        train.TryApplyForce(0);

        var res = new TravelResultDto(true, (this.WorkloadFactor + 1) * StopTimeMultiplier);
        this.Time = res.TotalTime;
        return res;
    }
}