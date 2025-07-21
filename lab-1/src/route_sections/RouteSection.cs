namespace DefaultNamespace;

/// <summary>
/// Represents an abstract section of a route that a train can traverse.
/// </summary>
public abstract class RouteSection
{
    /// <summary>
    /// Gets or sets the time taken to traverse this section.
    /// </summary>
    public double Time { get; protected set; } = 0;

    /// <summary>
    /// Processes the train passing through this section of the route.
    /// </summary>
    /// <param name="train">The train that is traversing the section.</param>
    /// <returns>
    /// A tuple where the first item indicates success (true if the train successfully traverses the section, false otherwise),
    /// and the second item is the time taken to traverse this section.
    /// </returns>
    public abstract TravelResultDto ProcessSection(Train train);
}