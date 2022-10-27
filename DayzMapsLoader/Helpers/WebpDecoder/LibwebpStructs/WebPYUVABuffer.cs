using System.Runtime.InteropServices;

namespace DayzMapsLoader.Helpers.WebpDecoder.LibwebpStructs;

[StructLayout(LayoutKind.Sequential)]
internal struct WebPYUVABuffer
{
    /// <summary>
    /// Pointer to luma samples
    /// </summary>
    public IntPtr y;
    /// <summary>
    /// Pointer to chroma U samples
    /// </summary>
    public IntPtr u;
    /// <summary>
    /// Pointer to chroma V samples
    /// </summary>
    public IntPtr v;
    /// <summary>
    /// Pointer to alpha samples
    /// </summary>
    public IntPtr a;
    /// <summary>
    /// Luma stride
    /// </summary>
    public int y_stride;
    /// <summary>
    /// Chroma U stride
    /// </summary>
    public int u_stride;
    /// <summary>
    /// Chroma V stride
    /// </summary>
    public int v_stride;
    /// <summary>
    /// Alpha stride
    /// </summary>
    public int a_stride;
    /// <summary>
    /// Luma plane size
    /// </summary>
    public UIntPtr y_size;
    /// <summary>
    /// Chroma plane U size
    /// </summary>
    public UIntPtr u_size;
    /// <summary>
    /// Chroma plane V size
    /// </summary>
    public UIntPtr v_size;
    /// <summary>
    /// Alpha plane size
    /// </summary>
    public UIntPtr a_size;
}