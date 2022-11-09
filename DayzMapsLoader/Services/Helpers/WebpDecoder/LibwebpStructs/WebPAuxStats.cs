using System.Runtime.InteropServices;

namespace DayzMapsLoader.Helpers.WebpDecoder.LibwebpStructs;

/// <summary>
/// Structure for storing auxiliary statistics (mostly for lossy encoding)
/// </summary>
[StructLayout(LayoutKind.Sequential)]
internal struct WebPAuxStats
{
    /// <summary>
    /// Final size
    /// </summary>
    public int coded_size;
    /// <summary>
    /// Peak-signal-to-noise ratio for Y
    /// </summary>
    public float PSNRY;
    /// <summary>
    /// Peak-signal-to-noise ratio for U
    /// </summary>
    public float PSNRU;
    /// <summary>
    /// Peak-signal-to-noise ratio for V
    /// </summary>
    public float PSNRV;
    /// <summary>
    /// Peak-signal-to-noise ratio for All
    /// </summary>
    public float PSNRALL;
    /// <summary>
    /// Peak-signal-to-noise ratio for Alpha
    /// </summary>
    public float PSNRAlpha;
    /// <summary>
    /// Number of intra4
    /// </summary>
    public int block_count_intra4;
    /// <summary>
    /// Number of intra16
    /// </summary>
    public int block_count_intra16;
    /// <summary>
    /// Number of skipped macro-blocks
    /// </summary>
    public int block_count_skipped;
    /// <summary>
    /// Approximate number of bytes spent for header
    /// </summary>
    public int header_bytes;
    /// <summary>
    /// Approximate number of bytes spent for mode-partition #0
    /// </summary>
    public int mode_partition_0;
    /// <summary>
    /// Approximate number of bytes spent for DC coefficients for segment 0
    /// </summary>
    public int residual_bytes_DC_segments0;
    /// <summary>
    /// Approximate number of bytes spent for AC coefficients for segment 0
    /// </summary>
    public int residual_bytes_AC_segments0;
    /// <summary>
    /// Approximate number of bytes spent for UV coefficients for segment 0
    /// </summary>
    public int residual_bytes_uv_segments0;
    /// <summary>
    /// Approximate number of bytes spent for DC coefficients for segment 1
    /// </summary>
    public int residual_bytes_DC_segments1;
    /// <summary>
    /// Approximate number of bytes spent for AC coefficients for segment 1
    /// </summary>
    public int residual_bytes_AC_segments1;
    /// <summary>
    /// Approximate number of bytes spent for UV coefficients for segment 1
    /// </summary>
    public int residual_bytes_uv_segments1;
    /// <summary>
    /// Approximate number of bytes spent for DC coefficients for segment 2
    /// </summary>
    public int residual_bytes_DC_segments2;
    /// <summary>
    /// Approximate number of bytes spent for AC coefficients for segment 2
    /// </summary>
    public int residual_bytes_AC_segments2;
    /// <summary>
    /// Approximate number of bytes spent for UV coefficients for segment 2
    /// </summary>
    public int residual_bytes_uv_segments2;
    /// <summary>
    /// Approximate number of bytes spent for DC coefficients for segment 3
    /// </summary>
    public int residual_bytes_DC_segments3;
    /// <summary>
    /// Approximate number of bytes spent for AC coefficients for segment 3
    /// </summary>
    public int residual_bytes_AC_segments3;
    /// <summary>
    /// Approximate number of bytes spent for UV coefficients for segment 3
    /// </summary>
    public int residual_bytes_uv_segments3;
    /// <summary>
    /// Number of macro-blocks in segments 0
    /// </summary>
    public int segment_size_segments0;
    /// <summary>
    /// Number of macro-blocks in segments 1
    /// </summary>
    public int segment_size_segments1;
    /// <summary>
    /// Number of macro-blocks in segments 2
    /// </summary>
    public int segment_size_segments2;
    /// <summary>
    /// Number of macro-blocks in segments 3
    /// </summary>
    public int segment_size_segments3;
    /// <summary>
    /// Quantizer values for segment 0
    /// </summary>
    public int segment_quant_segments0;
    /// <summary>
    /// Quantizer values for segment 1
    /// </summary>
    public int segment_quant_segments1;
    /// <summary>
    /// Quantizer values for segment 2
    /// </summary>
    public int segment_quant_segments2;
    /// <summary>
    /// Quantizer values for segment 3
    /// </summary>
    public int segment_quant_segments3;
    /// <summary>
    /// Filtering strength for segment 0 [0..63]
    /// </summary>
    public int segment_level_segments0;
    /// <summary>
    /// Filtering strength for segment 1 [0..63]
    /// </summary>
    public int segment_level_segments1;
    /// <summary>
    /// Filtering strength for segment 2 [0..63]
    /// </summary>
    public int segment_level_segments2;
    /// <summary>
    /// Filtering strength for segment 3 [0..63]
    /// </summary>
    public int segment_level_segments3;
    /// <summary>
    /// Size of the transparency data
    /// </summary>
    public int alpha_data_size;
    /// <summary>
    /// Size of the enhancement layer data
    /// </summary>
    public int layer_data_size;

    // lossless encoder statistics
    /// <summary>
    /// bit0:predictor bit1:cross-color transform bit2:subtract-green bit3:color indexing
    /// </summary>
    public int lossless_features;
    /// <summary>
    /// Number of precision bits of histogram
    /// </summary>
    public int histogram_bits;
    /// <summary>
    /// Precision bits for transform
    /// </summary>
    public int transform_bits;
    /// <summary>
    /// Number of bits for color cache lookup
    /// </summary>
    public int cache_bits;
    /// <summary>
    /// Number of color in palette, if used
    /// </summary>
    public int palette_size;
    /// <summary>
    /// Final lossless size
    /// </summary>
    public int lossless_size;
    /// <summary>
    /// Lossless header (transform, Huffman, etc) size
    /// </summary>
    public int lossless_hdr_size;
    /// <summary>
    /// Lossless image data size
    /// </summary>
    public int lossless_data_size;
    /// <summary>
    /// Padding for later use
    /// </summary>
    [MarshalAs(UnmanagedType.ByValArray, SizeConst = 2, ArraySubType = UnmanagedType.U4)]
    private readonly uint[] pad;
};