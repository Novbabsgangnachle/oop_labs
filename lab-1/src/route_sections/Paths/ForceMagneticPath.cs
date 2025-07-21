using System;

namespace DefaultNamespace;

/// <summary>
/// Represents a force magnetic path that applies a constant force to the train throughout its length.
/// </summary>
public class ForceMagneticPath : Path
{
    /// <summary>
    /// Initializes a new instance of the <see cref="ForceMagneticPath"/> class with the specified length and force.
    /// </summary>
    /// <param name="length">The length of the path.</param>
    /// <param name="force">The force applied to the train throughout the path.</param>
    public ForceMagneticPath(double length, double force)
        : base(length)
    {
        this.Force = force;
    }

    /// <summary>
    /// Gets the force applied to the train throughout the path.
    /// </summary>
    private double Force { get; }

    /// <summary>
    /// Processes the train passing through this section of the route by applying force and calculating the travel time.
    /// </summary>
    /// <param name="train">The train that is passing through the path section.</param>
    /// <returns>
    /// A tuple where the first item indicates success (true if successful, false otherwise),
    /// and the second item is the time taken to pass through this section.
    /// </returns>
    public override TravelResultDto ProcessSection(Train train)
    {
        if (train == null)
        {
            throw new ArgumentNullException(nameof(train), "Train cannot be null");
        }

        if (!train.TryApplyForce(this.Force))
        {
            return new TravelResultDto(false, 0);
        }

        TravelResultDto res = train.CalculateTravelTime(this.Length);
        this.Time = res.TotalTime;
        return res;
    }
}