using System;

namespace DefaultNamespace;

/// <summary>
/// Represents an abstract base class for different types of path sections in the route.
/// </summary>
public abstract class Path : RouteSection
{
    /// <summary>
    /// Initializes a new instance of the <see cref="Path"/> class with the specified length.
    /// </summary>
    /// <param name="length">The length of the path section.</param>
    protected Path(double length)
    {
        if (length < 0)
        {
            throw new ArgumentException("Length can`t be negative.");
        }

        this.Length = length;
    }

    /// <summary>
    /// Gets the length of the path section.
    /// </summary>
    protected double Length { get; }
}