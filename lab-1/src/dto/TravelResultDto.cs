namespace DefaultNamespace;

/// <summary>
/// DTO class of travel result.
/// </summary>
public class TravelResultDto
{
    /// <summary>
    /// Initializes a new instance of the <see cref="TravelResult"/> class.
    /// </summary>
    /// <param name="success">success.</param>
    /// <param name="totalTime">totaltime.</param>
    public TravelResultDto(bool success, double totalTime)
    {
        this.Success = success;
        this.TotalTime = totalTime;
    }

    /// <summary>
    /// Gets a value indicating whether.
    /// </summary>
    public bool Success { get; }

    /// <summary>
    /// Gets a value indicating whether.
    /// </summary>
    public double TotalTime { get; }
}