namespace DayzMapsLoader.Application.Managers.MergerSquareImages.Helpers.WebpDecoder.Predefined;

/// <summary>
/// Describes the byte-ordering of packed samples in memory
/// </summary>
internal enum WEBP_CSP_MODE
{
    /// <summary>
    /// Byte-order: R,G,B,R,G,B,..
    /// </summary>
    MODE_RGB = 0,
    /// <summary>
    /// Byte-order: R,G,B,A,R,G,B,A,..
    /// </summary>
    MODE_RGBA = 1,
    /// <summary>
    /// Byte-order: B,G,R,B,G,R,..
    /// </summary>
    MODE_BGR = 2,
    /// <summary>
    /// Byte-order: B,G,R,A,B,G,R,A,..
    /// </summary>
    MODE_BGRA = 3,
    /// <summary>
    /// Byte-order: A,R,G,B,A,R,G,B,..
    /// </summary>
    MODE_ARGB = 4,
    /// <summary>
    /// Byte-order: RGB-565: [a4 a3 a2 a1 a0 r5 r4 r3], [r2 r1 r0 g4 g3 g2 g1 g0], ...
    /// WEBP_SWAP_16BITS_CSP is defined, Byte-order: RGB-565: [a4 a3 a2 a1 a0 b5 b4 b3], [b2 b1 b0
    /// g4 g3 g2 g1 g0], ..
    /// </summary>
    MODE_RGBA_4444 = 5,
    /// <summary>
    /// Byte-order: RGB-565: [r4 r3 r2 r1 r0 g5 g4 g3], [g2 g1 g0 b4 b3 b2 b1 b0], ...
    /// WEBP_SWAP_16BITS_CSP is defined, Byte-order: [b3 b2 b1 b0 a3 a2 a1 a0], [r3 r2 r1 r0 g3 g2
    /// g1 g0], ..
    /// </summary>
    MODE_RGB_565 = 6,
    /// <summary>
    /// RGB-premultiplied transparent modes (alpha value is preserved)
    /// </summary>
    MODE_rgbA = 7,
    /// <summary>
    /// RGB-premultiplied transparent modes (alpha value is preserved)
    /// </summary>
    MODE_bgrA = 8,
    /// <summary>
    /// RGB-premultiplied transparent modes (alpha value is preserved)
    /// </summary>
    MODE_Argb = 9,
    /// <summary>
    /// RGB-premultiplied transparent modes (alpha value is preserved)
    /// </summary>
    MODE_rgbA_4444 = 10,
    /// <summary>
    /// YUV 4:2:0
    /// </summary>
    MODE_YUV = 11,
    /// <summary>
    /// YUV 4:2:0
    /// </summary>
    MODE_YUVA = 12,
    /// <summary>
    /// MODE_LAST -&gt; 13
    /// </summary>
    MODE_LAST = 13,
}