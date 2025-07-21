using System;

namespace DefaultNamespace;

/// <summary>
/// Class of train.
/// </summary>
public class Train
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Train"/> class.
    /// </summary>
    /// <param name="mass"> Mass.</param>
    /// <param name="maxForce"> Max Force.</param>
    /// <param name="precision">precision.</param>
    /// <exception cref="ArgumentException">Then arguments are negative.</exception>
    public Train(double mass, double maxForce, double precision)
    {
        if (mass <= 0)
        {
            throw new ArgumentException("Mass should be positive");
        }

        if (maxForce < 0)
        {
            throw new ArgumentException("Max force should be positive");
        }

        if (precision <= 0)
        {
            throw new ArgumentException("precision should be positive");
        }

        Mass = mass;
        MaxForce = maxForce;
        Precision = precision;
    }

    /// <summary>
    /// Gets Mass of train.
    /// </summary>
    public double Mass { get; }

    /// <summary>
    /// Gets MaxForce.
    /// </summary>
    public double MaxForce { get; }

    /// <summary>
    /// Gets precision.
    /// </summary>
    public double Precision { get; }

    /// <summary>
    /// Gets Speed.
    /// </summary>
    public double Speed { get; private set; } = 0;

    /// <summary>
    /// Gets Acceleration.
    /// </summary>
    public double Acceleration { get; private set; } = 0;

    /// <summary>
    /// Tryes to apply force to train.
    /// </summary>
    /// <param name="force">force.</param>
    /// <returns>success/no success.</returns>
    public bool TryApplyForce(double force)
    {
        if (Math.Abs(force) > this.MaxForce)
        {
            return false;
        }

        this.Acceleration = force / this.Mass;
        return true;
    }

    /// <summary>
    /// Calculate time for travel distance.
    /// </summary>
    /// <param name="distance">distance to travel.</param>
    /// <returns>success / time.</returns>
    public TravelResultDto CalculateTravelTime(double distance)
    {
        double totalTime = 0;
        double remainingDistance = distance;

        while (remainingDistance > 0)
        {
            double newSpeed = this.Speed + (this.Acceleration * this.Precision);

            if (newSpeed <= 0)
            {
                return new TravelResultDto(false, totalTime);
            }

            double newDist = newSpeed * this.Precision;
            remainingDistance -= newDist;
            totalTime += this.Precision;
            this.Speed = newSpeed;
        }

        return new TravelResultDto(true, totalTime);
    }

    /// <summary>
    /// Stop at end.
    /// </summary>
    /// <param name="maxAllowedSpeed">Max allowed speed.</param>
    /// <returns>success/no success.</returns>
    public bool TryStopAtEnd(double maxAllowedSpeed)
    {
        if (this.Speed > maxAllowedSpeed)
        {
            return false;
        }

        this.Speed = 0;
        return true;
    }
}