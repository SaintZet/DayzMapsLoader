namespace DayzMapsLoader.Application.Helpers.WebpDecoder.Predefined;

/// <summary>
/// Enumeration of the status codes
/// </summary>
internal enum VP8StatusCode
{
    /// <summary>
    /// No error
    /// </summary>
    VP8_STATUS_OK = 0,
    /// <summary>
    /// Memory error allocating objects
    /// </summary>
    VP8_STATUS_OUT_OF_MEMORY,
    /// <summary>
    /// Configuration is invalid
    /// </summary>
    VP8_STATUS_INVALID_PARAM,
    VP8_STATUS_BITSTREAM_ERROR,
    /// <summary>
    /// Configuration is invalid
    /// </summary>
    VP8_STATUS_UNSUPPORTED_FEATURE,
    VP8_STATUS_SUSPENDED,
    /// <summary>
    /// Abort request by user
    /// </summary>
    VP8_STATUS_USER_ABORT,
    VP8_STATUS_NOT_ENOUGH_DATA,
}