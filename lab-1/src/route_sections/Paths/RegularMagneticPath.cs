using System;

namespace DefaultNamespace;

/// <summary>
/// Represents a regular magnetic path where the train moves without any additional forces applied.
/// </summary>
/// <remarks>
/// Initializes a new instance of the <see cref="RegularMagneticPath"/> class.
/// </remarks>
/// <param name="length">The length of the path.</param>
public class RegularMagneticPath(double length) : Path(length)
{
    /// <summary>
    /// Processes the train passing through this section by calculating the travel time based on its current speed and acceleration.
    /// </summary>
    /// <param name="train">The train that is passing through the path.</param>
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

        train.TryApplyForce(0);

        TravelResultDto res = train.CalculateTravelTime(this.Length);
        this.Time = res.TotalTime;
        return res;
    }
}