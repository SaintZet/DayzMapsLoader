using System.Runtime.InteropServices;

namespace DayzMapsLoader.Application.Managers.MergerSquareImages.Helpers.WebpDecoder.LibwebpStructs;

/// <summary>
/// Main exchange structure (input samples, output bytes, statistics)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct WebPPicture
{
    /// <summary>
    /// Main flag for encoder selecting between ARGB or YUV input. Recommended to use ARGB input
    /// (*argb, argb_stride) for lossless, and YUV input (*y, *u, *v, etc.) for lossy
    /// </summary>
    public int use_argb;
    /// <summary>
    /// Color-space: should be YUV420 for now (=Y'CbCr). Value = 0
    /// </summary>
    public uint colorspace;
    /// <summary>
    /// Width of picture (less or equal to WEBP_MAX_DIMENSION)
    /// </summary>
    public int width;
    /// <summary>
    /// Height of picture (less or equal to WEBP_MAX_DIMENSION)
    /// </summary>
    public int height;
    /// <summary>
    /// Pointer to luma plane
    /// </summary>
    public IntPtr y;
    /// <summary>
    /// Pointer to chroma U plane
    /// </summary>
    public IntPtr u;
    /// <summary>
    /// Pointer to chroma V plane
    /// </summary>
    public IntPtr v;
    /// <summary>
    /// Luma stride
    /// </summary>
    public int y_stride;
    /// <summary>
    /// Chroma stride
    /// </summary>
    public int uv_stride;
    /// <summary>
    /// Pointer to the alpha plane
    /// </summary>
    public IntPtr a;
    /// <summary>
    /// stride of the alpha plane
    /// </summary>
    public int a_stride;
    /// <summary>
    /// Padding for later use
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.U4)]
    private readonly uint[] pad1;
    /// <summary>
    /// Pointer to ARGB (32 bit) plane
    /// </summary>
    public IntPtr argb;
    /// <summary>
    /// This is stride in pixels units, not bytes
    /// </summary>
    public int argb_stride;
    /// <summary>
    /// Padding for later use
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 3, ArraySubType = UnmanagedType.U4)]
    private readonly uint[] pad2;
    /// <summary>
    /// Byte-emission hook, to store compressed bytes as they are ready
    /// </summary>
    public IntPtr writer;
    /// <summary>
    /// Can be used by the writer
    /// </summary>
    public IntPtr custom_ptr;
    // map for extra information (only for lossy compression mode)
    /// <summary>
    /// 1: intra type, 2: segment, 3: quant, 4: intra-16 prediction mode, 5: chroma prediction mode,
    /// 6: bit cost, 7: distortion
    /// </summary>
    public int extra_info_type;
    /// <summary>
    /// If not NULL, points to an array of size ((width + 15) / 16) * ((height + 15) / 16) that will
    /// be filled with a macroblock map, depending on extra_info_type
    /// </summary>
    public IntPtr extra_info;
    /// <summary>
    /// Pointer to side statistics (updated only if not NULL)
    /// </summary>
    public IntPtr stats;
    /// <summary>
    /// Error code for the latest error encountered during encoding
    /// </summary>
    public uint error_code;
    /// <summary>
    /// If not NULL, report progress during encoding
    /// </summary>
    public IntPtr progress_hook;
    /// <summary>
    /// This field is free to be set to any value and used during callbacks (like progress-report e.g.)
    /// </summary>
    public IntPtr user_data;
    /// <summary>
    /// Padding for later use
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 13, ArraySubType = UnmanagedType.U4)]
    private readonly uint[] pad3;
    /// <summary>
    /// Row chunk of memory for YUVA planes
    /// </summary>
    private readonly IntPtr memory_;
    /// <summary>
    /// Row chunk of memory for ARGB planes
    /// </summary>
    private readonly IntPtr memory_argb_;
    /// <summary>
    /// Padding for later use
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.U4)]
    private readonly uint[] pad4;
};