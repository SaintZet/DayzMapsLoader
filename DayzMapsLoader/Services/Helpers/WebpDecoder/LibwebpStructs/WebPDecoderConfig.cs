using System.Runtime.InteropServices;

namespace DayzMapsLoader.Helpers.WebpDecoder.LibwebpStructs;

[StructLayout(LayoutKind.Sequential)]
internal struct WebPDecoderConfig
{
    /// <summary>
    /// Immutable bit stream features (optional)
    /// </summary>
    public WebPBitstreamFeatures input;
    /// <summary>
    /// Output buffer (can point to external memory)
    /// </summary>
    public WebPDecBuffer output;
    /// <summary>
    /// Decoding options
    /// </summary>
    public WebPDecoderOptions options;
}