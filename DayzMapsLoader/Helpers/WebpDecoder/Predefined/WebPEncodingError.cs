namespace DayzMapsLoader.Helpers.WebpDecoder.Predefined;

/// <summary>
/// Encoding error conditions
/// </summary>
internal enum WebPEncodingError
{
    /// <summary>
    /// No error
    /// </summary>
    VP8_ENC_OK = 0,
    /// <summary>
    /// Memory error allocating objects
    /// </summary>
    VP8_ENC_ERROR_OUT_OF_MEMORY,
    /// <summary>
    /// Memory error while flushing bits
    /// </summary>
    VP8_ENC_ERROR_BITSTREAM_OUT_OF_MEMORY,
    /// <summary>
    /// A pointer parameter is NULL
    /// </summary>
    VP8_ENC_ERROR_NULL_PARAMETER,
    /// <summary>
    /// Configuration is invalid
    /// </summary>
    VP8_ENC_ERROR_INVALID_CONFIGURATION,
    /// <summary>
    /// Picture has invalid width/height
    /// </summary>
    VP8_ENC_ERROR_BAD_DIMENSION,
    /// <summary>
    /// Partition is bigger than 512k
    /// </summary>
    VP8_ENC_ERROR_PARTITION0_OVERFLOW,
    /// <summary>
    /// Partition is bigger than 16M
    /// </summary>
    VP8_ENC_ERROR_PARTITION_OVERFLOW,
    /// <summary>
    /// Error while flushing bytes
    /// </summary>
    VP8_ENC_ERROR_BAD_WRITE,
    /// <summary>
    /// File is bigger than 4G
    /// </summary>
    VP8_ENC_ERROR_FILE_TOO_BIG,
    /// <summary>
    /// Abort request by user
    /// </summary>
    VP8_ENC_ERROR_USER_ABORT,
    /// <summary>
    /// List terminator. Always last
    /// </summary>
    VP8_ENC_ERROR_LAST,
}