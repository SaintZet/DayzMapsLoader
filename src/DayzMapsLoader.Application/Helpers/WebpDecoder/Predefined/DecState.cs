namespace DayzMapsLoader.Application.Helpers.WebpDecoder.Predefined;

/// <summary>
/// Decoding states. State normally flows as:
/// WEBP_HEADER-&gt;VP8_HEADER-&gt;VP8_PARTS0-&gt;VP8_DATA-&gt;DONE for a lossy image, and
/// WEBP_HEADER-&gt;VP8L_HEADER-&gt;VP8L_DATA-&gt;DONE for a lossless image. If there is any error
/// the decoder goes into state ERROR.
/// </summary>
internal enum DecState
{
    STATE_WEBP_HEADER,  // All the data before that of the VP8/VP8L chunk.
    STATE_VP8_HEADER,   // The VP8 Frame header (within the VP8 chunk).
    STATE_VP8_PARTS0,
    STATE_VP8_DATA,
    STATE_VP8L_HEADER,
    STATE_VP8L_DATA,
    STATE_DONE,
    STATE_ERROR
};