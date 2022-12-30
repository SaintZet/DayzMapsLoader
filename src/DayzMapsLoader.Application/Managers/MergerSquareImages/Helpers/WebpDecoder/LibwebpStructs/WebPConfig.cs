using DayzMapsLoader.Application.Managers.MergerSquareImages.Helpers.WebpDecoder.Predefined;
using System.Runtime.InteropServices;

namespace DayzMapsLoader.Application.Managers.MergerSquareImages.Helpers.WebpDecoder.LibwebpStructs;

/// <summary>
/// Compression parameters
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct WebPConfig
{
    /// <summary>
    /// Lossless encoding (0=lossy(default), 1=lossless)
    /// </summary>
    public int lossless;
    /// <summary>
    /// Between 0 (smallest file) and 100 (biggest)
    /// </summary>
    public float quality;
    /// <summary>
    /// Quality/speed trade-off (0=fast, 6=slower-better)
    /// </summary>
    public int method;
    /// <summary>
    /// Hint for image type (lossless only for now)
    /// </summary>
    public WebPImageHint image_hint;
    /// <summary>
    /// If non-zero, set the desired target size in bytes. Takes precedence over the 'compression' parameter
    /// </summary>
    public int target_size;
    /// <summary>
    /// If non-zero, specifies the minimal distortion to try to achieve. Takes precedence over target_size
    /// </summary>
    public float target_PSNR;
    /// <summary>
    /// Maximum number of segments to use, in [1..4]
    /// </summary>
    public int segments;
    /// <summary>
    /// Spatial Noise Shaping. 0=off, 100=maximum
    /// </summary>
    public int sns_strength;
    /// <summary>
    /// Range: [0 = off .. 100 = strongest]
    /// </summary>
    public int filter_strength;
    /// <summary>
    /// Range: [0 = off .. 7 = least sharp]
    /// </summary>
    public int filter_sharpness;
    /// <summary>
    /// Filtering type: 0 = simple, 1 = strong (only used if filter_strength &gt; 0 or auto-filter
    /// &gt; 0)
    /// </summary>
    public int filter_type;
    /// <summary>
    /// Auto adjust filter's strength [0 = off, 1 = on]
    /// </summary>
    public int autofilter;
    /// <summary>
    /// Algorithm for encoding the alpha plane (0 = none, 1 = compressed with WebP lossless).
    /// Default is 1
    /// </summary>
    public int alpha_compression;
    /// <summary>
    /// Predictive filtering method for alpha plane. 0: none, 1: fast, 2: best. Default if 1
    /// </summary>
    public int alpha_filtering;
    /// <summary>
    /// Between 0 (smallest size) and 100 (lossless). Default is 100
    /// </summary>
    public int alpha_quality;
    /// <summary>
    /// Number of entropy-analysis passes (in [1..10])
    /// </summary>
    public int pass;
    /// <summary>
    /// If true, export the compressed picture back. In-loop filtering is not applied
    /// </summary>
    public int show_compressed;
    /// <summary>
    /// Preprocessing filter (0=none, 1=segment-smooth, 2=pseudo-random dithering)
    /// </summary>
    public int preprocessing;
    /// <summary>
    /// Log2(number of token partitions) in [0..3] Default is set to 0 for easier progressive decoding
    /// </summary>
    public int partitions;
    /// <summary>
    /// Quality degradation allowed to fit the 512k limit on prediction modes coding (0: no
    /// degradation, 100: maximum possible degradation)
    /// </summary>
    public int partition_limit;
    /// <summary>
    /// If true, compression parameters will be remapped to better match the expected output size
    /// from JPEG compression. Generally, the output size will be similar but the degradation will
    /// be lower
    /// </summary>
    public int emulate_jpeg_size;
    /// <summary>
    /// If non-zero, try and use multi-threaded encoding
    /// </summary>
    public int thread_level;
    /// <summary>
    /// If set, reduce memory usage (but increase CPU use)
    /// </summary>
    public int low_memory;
    /// <summary>
    /// Near lossless encoding [0 = max loss .. 100 = off (default)]
    /// </summary>
    public int near_lossless;
    /// <summary>
    /// If non-zero, preserve the exact RGB values under transparent area. Otherwise, discard this
    /// invisible RGB information for better compression. The default value is 0
    /// </summary>
    public int exact;
    /// <summary>
    /// Reserved for future lossless feature
    /// </summary>
    public int delta_palettization;
    /// <summary>
    /// If needed, use sharp (and slow) RGB-&gt;YUV conversion
    /// </summary>
    public int use_sharp_yuv;
    /// <summary>
    /// Padding for later use
    /// </summary>
    private readonly int pad1;
    private readonly int pad2;
};