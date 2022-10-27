using System.Runtime.InteropServices;

namespace DayzMapsLoader.Helpers.WebpDecoder.LibwebpStructs;

/// <summary>
/// Generic structure for describing the output sample buffer
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct WebPRGBABuffer
{
    /// <summary>
    /// Pointer to RGBA samples
    /// </summary>
    public IntPtr rgba;
    /// <summary>
    /// Stride in bytes from one scanline to the next
    /// </summary>
    public int stride;
    /// <summary>
    /// Total size of the RGBA buffer
    /// </summary>
    public UIntPtr size;
}