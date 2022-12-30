/////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// Wrapper for WebP format in C#. (MIT) Jose M. Piñeiro
/////////////////////////////////////////////////////////////////////////////////////////////////////////////
/// Decode Functions:
/// Bitmap Load(string pathFileName) - Load a WebP file in bitmap.
/// Bitmap Decode(byte[] rawWebP) - Decode WebP data (rawWebP) to bitmap.
/// Bitmap Decode(byte[] rawWebP, WebPDecoderOptions options) - Decode WebP data (rawWebP) to bitmap using 'options'.
/// Bitmap GetThumbnailFast(byte[] rawWebP, int width, int height) - Get a thumbnail from WebP data (rawWebP) with dimensions 'width x height'. Fast mode.
/// Bitmap GetThumbnailQuality(byte[] rawWebP, int width, int height) - Fast get a thumbnail from WebP data (rawWebP) with dimensions 'width x height'. Quality mode.
///
/// Encode Functions:
/// Save(Bitmap bmp, string pathFileName, int quality) - Save bitmap with quality lost to WebP file. Opcionally select 'quality'.
/// byte[] EncodeLossy(Bitmap bmp, int quality) - Encode bitmap with quality lost to WebP byte array. Opcionally select 'quality'.
/// byte[] EncodeLossy(Bitmap bmp, int quality, int speed, bool info) - Encode bitmap with quality lost to WebP byte array. Select 'quality', 'speed' and optionally select 'info'.
/// byte[] EncodeLossless(Bitmap bmp) - Encode bitmap without quality lost to WebP byte array.
/// byte[] EncodeLossless(Bitmap bmp, int speed, bool info = false) - Encode bitmap without quality lost to WebP byte array. Select 'speed'.
/// byte[] EncodeNearLossless(Bitmap bmp, int quality, int speed = 9, bool info = false) - Encode bitmap with a near lossless method to WebP byte array. Select 'quality', 'speed' and optionally select 'info'.
///
/// Another functions:
/// string GetVersion() - Get the library version
/// GetInfo(byte[] rawWebP, out int width, out int height, out bool has_alpha, out bool has_animation, out string format) - Get information of WEBP data
/// float[] PictureDistortion(Bitmap source, Bitmap reference, int metric_type) - Get PSNR, SSIM or LSIM distortion metric between two pictures
/////////////////////////////////////////////////////////////////////////////////////////////////////////////
using DayzMapsLoader.Application.Managers.MergerSquareImages.Helpers.WebpDecoder.LibwebpStructs;
using DayzMapsLoader.Application.Managers.MergerSquareImages.Helpers.WebpDecoder.Predefined;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace DayzMapsLoader.Application.Managers.MergerSquareImages.Helpers.WebpDecoder.LibwebpFunctions
{
    [SuppressUnmanagedCodeSecurity]
    internal sealed class UnsafeNativeMethods
    {
        internal static WebPMemoryWrite OnCallback;

        private static readonly int WEBP_DECODER_ABI_VERSION = 0x0208;

        /// <summary>
        /// The writer type for output compress data
        /// </summary>
        /// <param name="data"> Data returned </param>
        /// <param name="data_size"> Size of data returned </param>
        /// <param name="wpic"> Picture structure </param>
        /// <returns> </returns>
        [UnmanagedFunctionPointer(CallingConvention.Cdecl)]
        internal delegate int WebPMemoryWrite([In()] IntPtr data, UIntPtr data_size, ref WebPPicture wpic);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern bool SetDllDirectory(string lpPathName);

        [DllImport("kernel32.dll", SetLastError = true)]
        public static extern int GetDllDirectory(int bufsize, StringBuilder buf);

        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr LoadLibrary(string librayName);

        [DllImport("kernel32.dll", EntryPoint = "CopyMemory", SetLastError = false)]
        internal static extern void CopyMemory(IntPtr dest, IntPtr src, uint count);

        /// <summary>
        /// This function will initialize the configuration according to a predefined set of
        /// parameters (referred to by 'preset') and a given quality factor
        /// </summary>
        /// <param name="config"> The WebPConfig structure </param>
        /// <param name="preset"> Type of image </param>
        /// <param name="quality"> Quality of compression </param>
        /// <returns> 0 if error </returns>
        internal static int WebPConfigInit(ref WebPConfig config, WebPPreset preset, float quality)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPConfigInitInternal_x86(ref config, preset, quality, WEBP_DECODER_ABI_VERSION);

                case 8:
                    return WebPConfigInitInternal_x64(ref config, preset, quality, WEBP_DECODER_ABI_VERSION);

                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        /// <summary>
        /// Get info of WepP image
        /// </summary>
        /// <param name="rawWebP"> Bytes[] of WebP image </param>
        /// <param name="data_size"> Size of rawWebP </param>
        /// <param name="features"> Features of WebP image </param>
        /// <returns> VP8StatusCode </returns>
        internal static VP8StatusCode WebPGetFeatures(IntPtr rawWebP, int data_size, ref WebPBitstreamFeatures features)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPGetFeaturesInternal_x86(rawWebP, (UIntPtr)data_size, ref features, WEBP_DECODER_ABI_VERSION);

                case 8:
                    return WebPGetFeaturesInternal_x64(rawWebP, (UIntPtr)data_size, ref features, WEBP_DECODER_ABI_VERSION);

                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        /// <summary>
        /// Activate the lossless compression mode with the desired efficiency
        /// </summary>
        /// <param name="config"> The WebPConfig struct </param>
        /// <param name="level">
        /// between 0 (fastest, lowest compression) and 9 (slower, best compression)
        /// </param>
        /// <returns> 0 in case of parameter error </returns>
        internal static int WebPConfigLosslessPreset(ref WebPConfig config, int level)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPConfigLosslessPreset_x86(ref config, level);

                case 8:
                    return WebPConfigLosslessPreset_x64(ref config, level);

                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        /// <summary>
        /// Check that configuration is non-NULL and all configuration parameters are within their
        /// valid ranges
        /// </summary>
        /// <param name="config"> The WebPConfig structure </param>
        /// <returns> 1 if configuration is OK </returns>
        internal static int WebPValidateConfig(ref WebPConfig config)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPValidateConfig_x86(ref config);

                case 8:
                    return WebPValidateConfig_x64(ref config);

                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        /// <summary>
        /// Initialize the WebPPicture structure checking the DLL version
        /// </summary>
        /// <param name="wpic"> The WebPPicture structure </param>
        /// <returns> 1 if not error </returns>
        internal static int WebPPictureInitInternal(ref WebPPicture wpic)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPPictureInitInternal_x86(ref wpic, WEBP_DECODER_ABI_VERSION);

                case 8:
                    return WebPPictureInitInternal_x64(ref wpic, WEBP_DECODER_ABI_VERSION);

                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        /// <summary>
        /// Colorspace conversion function to import RGB samples
        /// </summary>
        /// <param name="wpic"> The WebPPicture structure </param>
        /// <param name="bgr"> Point to BGR data </param>
        /// <param name="stride"> stride of BGR data </param>
        /// <returns> Returns 0 in case of memory error. </returns>
        internal static int WebPPictureImportBGR(ref WebPPicture wpic, IntPtr bgr, int stride)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPPictureImportBGR_x86(ref wpic, bgr, stride);

                case 8:
                    return WebPPictureImportBGR_x64(ref wpic, bgr, stride);

                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        /// <summary>
        /// Color-space conversion function to import RGB samples
        /// </summary>
        /// <param name="wpic"> The WebPPicture structure </param>
        /// <param name="bgra"> Point to BGRA data </param>
        /// <param name="stride"> stride of BGRA data </param>
        /// <returns> Returns 0 in case of memory error. </returns>
        internal static int WebPPictureImportBGRA(ref WebPPicture wpic, IntPtr bgra, int stride)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPPictureImportBGRA_x86(ref wpic, bgra, stride);

                case 8:
                    return WebPPictureImportBGRA_x64(ref wpic, bgra, stride);

                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        /// <summary>
        /// Color-space conversion function to import RGB samples
        /// </summary>
        /// <param name="wpic"> The WebPPicture structure </param>
        /// <param name="bgr"> Point to BGR data </param>
        /// <param name="stride"> stride of BGR data </param>
        /// <returns> Returns 0 in case of memory error. </returns>
        internal static int WebPPictureImportBGRX(ref WebPPicture wpic, IntPtr bgr, int stride)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPPictureImportBGRX_x86(ref wpic, bgr, stride);

                case 8:
                    return WebPPictureImportBGRX_x64(ref wpic, bgr, stride);

                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        /// <summary>
        /// Compress to WebP format
        /// </summary>
        /// <param name="config"> The configuration structure for compression parameters </param>
        /// <param name="picture"> 'picture' hold the source samples in both YUV(A) or ARGB input </param>
        /// <returns>
        /// Returns 0 in case of error, 1 otherwise. In case of error, picture-&gt;error_code is
        /// updated accordingly.
        /// </returns>
        internal static int WebPEncode(ref WebPConfig config, ref WebPPicture picture)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPEncode_x86(ref config, ref picture);

                case 8:
                    return WebPEncode_x64(ref config, ref picture);

                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        /// <summary>
        /// Release the memory allocated by WebPPictureAlloc() or WebPPictureImport*() Note that
        /// this function does _not_ free the memory used by the 'picture' object itself. Besides
        /// memory (which is reclaimed) all other fields of 'picture' are preserved
        /// </summary>
        /// <param name="picture"> Picture structure </param>
        internal static void WebPPictureFree(ref WebPPicture picture)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    WebPPictureFree_x86(ref picture);
                    break;

                case 8:
                    WebPPictureFree_x64(ref picture);
                    break;

                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        /// <summary>
        /// Validate the WebP image header and retrieve the image height and width. Pointers *width
        /// and *height can be passed NULL if deemed irrelevant
        /// </summary>
        /// <param name="data"> Pointer to WebP image data </param>
        /// <param name="data_size">
        /// This is the size of the memory block pointed to by data containing the image data
        /// </param>
        /// <param name="width"> The range is limited currently from 1 to 16383 </param>
        /// <param name="height"> The range is limited currently from 1 to 16383 </param>
        /// <returns>
        /// 1 if success, otherwise error code returned in the case of (a) formatting error(s).
        /// </returns>
        internal static int WebPGetInfo(IntPtr data, int data_size, out int width, out int height)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPGetInfo_x86(data, (UIntPtr)data_size, out width, out height);

                case 8:
                    return WebPGetInfo_x64(data, (UIntPtr)data_size, out width, out height);

                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        /// <summary>
        /// Decode WEBP image pointed to by *data and returns BGR samples into a preallocated buffer
        /// </summary>
        /// <param name="data"> Pointer to WebP image data </param>
        /// <param name="data_size">
        /// This is the size of the memory block pointed to by data containing the image data
        /// </param>
        /// <param name="output_buffer"> Pointer to decoded WebP image </param>
        /// <param name="output_buffer_size"> Size of allocated buffer </param>
        /// <param name="output_stride"> Specifies the distance between scan lines </param>
        internal static void WebPDecodeBGRInto(IntPtr data, int data_size, IntPtr output_buffer, int output_buffer_size, int output_stride)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    if (WebPDecodeBGRInto_x86(data, (UIntPtr)data_size, output_buffer, output_buffer_size, output_stride) == null)
                        throw new InvalidOperationException("Can not decode WebP");
                    break;

                case 8:
                    if (WebPDecodeBGRInto_x64(data, (UIntPtr)data_size, output_buffer, output_buffer_size, output_stride) == null)
                        throw new InvalidOperationException("Can not decode WebP");
                    break;

                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        /// <summary>
        /// Decode WEBP image pointed to by *data and returns BGRA samples into a preallocated buffer
        /// </summary>
        /// <param name="data"> Pointer to WebP image data </param>
        /// <param name="data_size">
        /// This is the size of the memory block pointed to by data containing the image data
        /// </param>
        /// <param name="output_buffer"> Pointer to decoded WebP image </param>
        /// <param name="output_buffer_size"> Size of allocated buffer </param>
        /// <param name="output_stride"> Specifies the distance between scan lines </param>
        internal static void WebPDecodeBGRAInto(IntPtr data, int data_size, IntPtr output_buffer, int output_buffer_size, int output_stride)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    if (WebPDecodeBGRAInto_x86(data, (UIntPtr)data_size, output_buffer, output_buffer_size, output_stride) == null)
                        throw new InvalidOperationException("Can not decode WebP");
                    break;

                case 8:
                    if (WebPDecodeBGRAInto_x64(data, (UIntPtr)data_size, output_buffer, output_buffer_size, output_stride) == null)
                        throw new InvalidOperationException("Can not decode WebP");
                    break;

                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        /// <summary>
        /// Decode WEBP image pointed to by *data and returns ARGB samples into a preallocated buffer
        /// </summary>
        /// <param name="data"> Pointer to WebP image data </param>
        /// <param name="data_size">
        /// This is the size of the memory block pointed to by data containing the image data
        /// </param>
        /// <param name="output_buffer"> Pointer to decoded WebP image </param>
        /// <param name="output_buffer_size"> Size of allocated buffer </param>
        /// <param name="output_stride"> Specifies the distance between scan lines </param>
        internal static void WebPDecodeARGBInto(IntPtr data, int data_size, IntPtr output_buffer, int output_buffer_size, int output_stride)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    if (WebPDecodeARGBInto_x86(data, (UIntPtr)data_size, output_buffer, output_buffer_size, output_stride) == null)
                        throw new InvalidOperationException("Can not decode WebP");
                    break;

                case 8:
                    if (WebPDecodeARGBInto_x64(data, (UIntPtr)data_size, output_buffer, output_buffer_size, output_stride) == null)
                        throw new InvalidOperationException("Can not decode WebP");
                    break;

                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        /// <summary>
        /// Initialize the configuration as empty. This function must always be called first, unless
        /// WebPGetFeatures() is to be called
        /// </summary>
        /// <param name="webPDecoderConfig"> Configuration structure </param>
        /// <returns> False in case of mismatched version. </returns>
        internal static int WebPInitDecoderConfig(ref WebPDecoderConfig webPDecoderConfig)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPInitDecoderConfigInternal_x86(ref webPDecoderConfig, WEBP_DECODER_ABI_VERSION);

                case 8:
                    return WebPInitDecoderConfigInternal_x64(ref webPDecoderConfig, WEBP_DECODER_ABI_VERSION);

                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        /// <summary>
        /// Decodes the full data at once, taking configuration into account
        /// </summary>
        /// <param name="data"> WebP raw data to decode </param>
        /// <param name="data_size"> Size of WebP data </param>
        /// <param name="webPDecoderConfig"> Configuration structure </param>
        /// <returns> VP8_STATUS_OK if the decoding was successful </returns>
        internal static VP8StatusCode WebPDecode(IntPtr data, int data_size, ref WebPDecoderConfig webPDecoderConfig)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPDecode_x86(data, (UIntPtr)data_size, ref webPDecoderConfig);

                case 8:
                    return WebPDecode_x64(data, (UIntPtr)data_size, ref webPDecoderConfig);

                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        /// <summary>
        /// Free any memory associated with the buffer. Must always be called last. Doesn't free the
        /// 'buffer' structure itself
        /// </summary>
        /// <param name="buffer"> WebPDecBuffer </param>
        internal static void WebPFreeDecBuffer(ref WebPDecBuffer buffer)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    WebPFreeDecBuffer_x86(ref buffer);
                    break;

                case 8:
                    WebPFreeDecBuffer_x64(ref buffer);
                    break;

                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        /// <summary>
        /// Lossy encoding images
        /// </summary>
        /// <param name="bgr"> Pointer to BGR image data </param>
        /// <param name="width"> The range is limited currently from 1 to 16383 </param>
        /// <param name="height"> The range is limited currently from 1 to 16383 </param>
        /// <param name="stride"> Specifies the distance between scanlines </param>
        /// <param name="quality_factor">
        /// Ranges from 0 (lower quality) to 100 (highest quality). Controls the loss and quality
        /// during compression
        /// </param>
        /// <param name="output"> output_buffer with WebP image </param>
        /// <returns> Size of WebP Image or 0 if an error occurred </returns>
        internal static int WebPEncodeBGR(IntPtr bgr, int width, int height, int stride, float quality_factor, out IntPtr output)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPEncodeBGR_x86(bgr, width, height, stride, quality_factor, out output);

                case 8:
                    return WebPEncodeBGR_x64(bgr, width, height, stride, quality_factor, out output);

                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        /// <summary>
        /// Lossy encoding images
        /// </summary>
        /// <param name="bgr"> Pointer to BGRA image data </param>
        /// <param name="width"> The range is limited currently from 1 to 16383 </param>
        /// <param name="height"> The range is limited currently from 1 to 16383 </param>
        /// <param name="stride"> Specifies the distance between scan lines </param>
        /// <param name="quality_factor">
        /// Ranges from 0 (lower quality) to 100 (highest quality). Controls the loss and quality
        /// during compression
        /// </param>
        /// <param name="output"> output_buffer with WebP image </param>
        /// <returns> Size of WebP Image or 0 if an error occurred </returns>
        internal static int WebPEncodeBGRA(IntPtr bgra, int width, int height, int stride, float quality_factor, out IntPtr output)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPEncodeBGRA_x86(bgra, width, height, stride, quality_factor, out output);

                case 8:
                    return WebPEncodeBGRA_x64(bgra, width, height, stride, quality_factor, out output);

                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        /// <summary>
        /// Lossless encoding images pointed to by *data in WebP format
        /// </summary>
        /// <param name="bgr"> Pointer to BGR image data </param>
        /// <param name="width"> The range is limited currently from 1 to 16383 </param>
        /// <param name="height"> The range is limited currently from 1 to 16383 </param>
        /// <param name="stride"> Specifies the distance between scan lines </param>
        /// <param name="output"> output_buffer with WebP image </param>
        /// <returns> Size of WebP Image or 0 if an error occurred </returns>
        internal static int WebPEncodeLosslessBGR(IntPtr bgr, int width, int height, int stride, out IntPtr output)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPEncodeLosslessBGR_x86(bgr, width, height, stride, out output);

                case 8:
                    return WebPEncodeLosslessBGR_x64(bgr, width, height, stride, out output);

                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        /// <summary>
        /// Lossless encoding images pointed to by *data in WebP format
        /// </summary>
        /// <param name="bgra"> Pointer to BGRA image data </param>
        /// <param name="width"> The range is limited currently from 1 to 16383 </param>
        /// <param name="height"> The range is limited currently from 1 to 16383 </param>
        /// <param name="stride"> Specifies the distance between scan lines </param>
        /// <param name="output"> output_buffer with WebP image </param>
        /// <returns> Size of WebP Image or 0 if an error occurred </returns>
        internal static int WebPEncodeLosslessBGRA(IntPtr bgra, int width, int height, int stride, out IntPtr output)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPEncodeLosslessBGRA_x86(bgra, width, height, stride, out output);

                case 8:
                    return WebPEncodeLosslessBGRA_x64(bgra, width, height, stride, out output);

                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        /// <summary>
        /// Releases memory returned by the WebPEncode
        /// </summary>
        /// <param name="p"> Pointer to memory </param>
        internal static void WebPFree(IntPtr p)
        {
            switch (IntPtr.Size)
            {
                case 4:
                    WebPFree_x86(p);
                    break;

                case 8:
                    WebPFree_x64(p);
                    break;

                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        /// <summary>
        /// Get the WebP version library
        /// </summary>
        /// <returns>
        /// 8bits for each of major/minor/revision packet in integer. E.g: v2.5.7 is 0x020507
        /// </returns>
        internal static int WebPGetDecoderVersion()
        {
            switch (IntPtr.Size)
            {
                case 4:
                    return WebPGetDecoderVersion_x86();

                case 8:
                    return WebPGetDecoderVersion_x64();

                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        /// <summary>
        /// Compute PSNR, SSIM or LSIM distortion metric between two pictures
        /// </summary>
        /// <param name="srcPicture"> Picture to measure </param>
        /// <param name="refPicture"> Reference picture </param>
        /// <param name="metric_type"> 0 = PSNR, 1 = SSIM, 2 = LSIM </param>
        /// <param name="pResult"> dB in the Y/U/V/Alpha/All order </param>
        /// <returns> False in case of error (the two pictures don't have same dimension, ...) </returns>
        internal static int WebPPictureDistortion(ref WebPPicture srcPicture, ref WebPPicture refPicture, int metric_type, IntPtr pResult)
        {
            return IntPtr.Size switch
            {
                4 => WebPPictureDistortion_x86(ref srcPicture, ref refPicture, metric_type, pResult),
                8 => WebPPictureDistortion_x64(ref srcPicture, ref refPicture, metric_type, pResult),
                _ => throw new InvalidOperationException("Invalid platform. Can not find proper function"),
            };
        }

        [DllImport("libwebp_x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPConfigInitInternal")]
        private static extern int WebPConfigInitInternal_x86(ref WebPConfig config, WebPPreset preset, float quality, int WEBP_DECODER_ABI_VERSION);

        [DllImport("libwebp_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPConfigInitInternal")]
        private static extern int WebPConfigInitInternal_x64(ref WebPConfig config, WebPPreset preset, float quality, int WEBP_DECODER_ABI_VERSION);

        [DllImport("libwebp_x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPGetFeaturesInternal")]
        private static extern VP8StatusCode WebPGetFeaturesInternal_x86([In()] IntPtr rawWebP, UIntPtr data_size, ref WebPBitstreamFeatures features, int WEBP_DECODER_ABI_VERSION);

        [DllImport("libwebp_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPGetFeaturesInternal")]
        private static extern VP8StatusCode WebPGetFeaturesInternal_x64([In()] IntPtr rawWebP, UIntPtr data_size, ref WebPBitstreamFeatures features, int WEBP_DECODER_ABI_VERSION);

        [DllImport("libwebp_x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPConfigLosslessPreset")]
        private static extern int WebPConfigLosslessPreset_x86(ref WebPConfig config, int level);

        [DllImport("libwebp_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPConfigLosslessPreset")]
        private static extern int WebPConfigLosslessPreset_x64(ref WebPConfig config, int level);

        [DllImport("libwebp_x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPValidateConfig")]
        private static extern int WebPValidateConfig_x86(ref WebPConfig config);

        [DllImport("libwebp_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPValidateConfig")]
        private static extern int WebPValidateConfig_x64(ref WebPConfig config);

        [DllImport("libwebp_x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureInitInternal")]
        private static extern int WebPPictureInitInternal_x86(ref WebPPicture wpic, int WEBP_DECODER_ABI_VERSION);

        [DllImport("libwebp_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureInitInternal")]
        private static extern int WebPPictureInitInternal_x64(ref WebPPicture wpic, int WEBP_DECODER_ABI_VERSION);

        [DllImport("libwebp_x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureImportBGR")]
        private static extern int WebPPictureImportBGR_x86(ref WebPPicture wpic, IntPtr bgr, int stride);

        [DllImport("libwebp_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureImportBGR")]
        private static extern int WebPPictureImportBGR_x64(ref WebPPicture wpic, IntPtr bgr, int stride);

        [DllImport("libwebp_x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureImportBGRA")]
        private static extern int WebPPictureImportBGRA_x86(ref WebPPicture wpic, IntPtr bgra, int stride);

        [DllImport("libwebp_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureImportBGRA")]
        private static extern int WebPPictureImportBGRA_x64(ref WebPPicture wpic, IntPtr bgra, int stride);

        [DllImport("libwebp_x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureImportBGRX")]
        private static extern int WebPPictureImportBGRX_x86(ref WebPPicture wpic, IntPtr bgr, int stride);

        [DllImport("libwebp_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureImportBGRX")]
        private static extern int WebPPictureImportBGRX_x64(ref WebPPicture wpic, IntPtr bgr, int stride);

        [DllImport("libwebp_x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPEncode")]
        private static extern int WebPEncode_x86(ref WebPConfig config, ref WebPPicture picture);

        [DllImport("libwebp_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPEncode")]
        private static extern int WebPEncode_x64(ref WebPConfig config, ref WebPPicture picture);

        [DllImport("libwebp_x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureFree")]
        private static extern void WebPPictureFree_x86(ref WebPPicture wpic);

        [DllImport("libwebp_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureFree")]
        private static extern void WebPPictureFree_x64(ref WebPPicture wpic);

        [DllImport("libwebp_x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPGetInfo")]
        private static extern int WebPGetInfo_x86([In()] IntPtr data, UIntPtr data_size, out int width, out int height);

        [DllImport("libwebp_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPGetInfo")]
        private static extern int WebPGetInfo_x64([In()] IntPtr data, UIntPtr data_size, out int width, out int height);

        [DllImport("libwebp_x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPDecodeBGRInto")]
        private static extern IntPtr WebPDecodeBGRInto_x86([In()] IntPtr data, UIntPtr data_size, IntPtr output_buffer, int output_buffer_size, int output_stride);

        [DllImport("libwebp_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPDecodeBGRInto")]
        private static extern IntPtr WebPDecodeBGRInto_x64([In()] IntPtr data, UIntPtr data_size, IntPtr output_buffer, int output_buffer_size, int output_stride);

        [DllImport("libwebp_x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPDecodeBGRAInto")]
        private static extern IntPtr WebPDecodeBGRAInto_x86([In()] IntPtr data, UIntPtr data_size, IntPtr output_buffer, int output_buffer_size, int output_stride);

        [DllImport("libwebp_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPDecodeBGRAInto")]
        private static extern IntPtr WebPDecodeBGRAInto_x64([In()] IntPtr data, UIntPtr data_size, IntPtr output_buffer, int output_buffer_size, int output_stride);

        [DllImport("libwebp_x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPDecodeARGBInto")]
        private static extern IntPtr WebPDecodeARGBInto_x86([In()] IntPtr data, UIntPtr data_size, IntPtr output_buffer, int output_buffer_size, int output_stride);

        [DllImport("libwebp_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPDecodeARGBInto")]
        private static extern IntPtr WebPDecodeARGBInto_x64([In()] IntPtr data, UIntPtr data_size, IntPtr output_buffer, int output_buffer_size, int output_stride);

        [DllImport("libwebp_x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPInitDecoderConfigInternal")]
        private static extern int WebPInitDecoderConfigInternal_x86(ref WebPDecoderConfig webPDecoderConfig, int WEBP_DECODER_ABI_VERSION);

        [DllImport("libwebp_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPInitDecoderConfigInternal")]
        private static extern int WebPInitDecoderConfigInternal_x64(ref WebPDecoderConfig webPDecoderConfig, int WEBP_DECODER_ABI_VERSION);

        [DllImport("libwebp_x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPDecode")]
        private static extern VP8StatusCode WebPDecode_x86(IntPtr data, UIntPtr data_size, ref WebPDecoderConfig config);

        [DllImport("libwebp_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPDecode")]
        private static extern VP8StatusCode WebPDecode_x64(IntPtr data, UIntPtr data_size, ref WebPDecoderConfig config);

        [DllImport("libwebp_x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPFreeDecBuffer")]
        private static extern void WebPFreeDecBuffer_x86(ref WebPDecBuffer buffer);

        [DllImport("libwebp_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPFreeDecBuffer")]
        private static extern void WebPFreeDecBuffer_x64(ref WebPDecBuffer buffer);

        [DllImport("libwebp_x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPEncodeBGR")]
        private static extern int WebPEncodeBGR_x86([In()] IntPtr bgr, int width, int height, int stride, float quality_factor, out IntPtr output);

        [DllImport("libwebp_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPEncodeBGR")]
        private static extern int WebPEncodeBGR_x64([In()] IntPtr bgr, int width, int height, int stride, float quality_factor, out IntPtr output);

        [DllImport("libwebp_x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPEncodeBGRA")]
        private static extern int WebPEncodeBGRA_x86([In()] IntPtr bgra, int width, int height, int stride, float quality_factor, out IntPtr output);

        [DllImport("libwebp_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPEncodeBGRA")]
        private static extern int WebPEncodeBGRA_x64([In()] IntPtr bgra, int width, int height, int stride, float quality_factor, out IntPtr output);

        [DllImport("libwebp_x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPEncodeLosslessBGR")]
        private static extern int WebPEncodeLosslessBGR_x86([In()] IntPtr bgr, int width, int height, int stride, out IntPtr output);

        [DllImport("libwebp_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPEncodeLosslessBGR")]
        private static extern int WebPEncodeLosslessBGR_x64([In()] IntPtr bgr, int width, int height, int stride, out IntPtr output);

        [DllImport("libwebp_x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPEncodeLosslessBGRA")]
        private static extern int WebPEncodeLosslessBGRA_x86([In()] IntPtr bgra, int width, int height, int stride, out IntPtr output);

        [DllImport("libwebp_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPEncodeLosslessBGRA")]
        private static extern int WebPEncodeLosslessBGRA_x64([In()] IntPtr bgra, int width, int height, int stride, out IntPtr output);

        [DllImport("libwebp_x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPFree")]
        private static extern void WebPFree_x86(IntPtr p);

        [DllImport("libwebp_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPFree")]
        private static extern void WebPFree_x64(IntPtr p);

        [DllImport("libwebp_x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPGetDecoderVersion")]
        private static extern int WebPGetDecoderVersion_x86();

        [DllImport("libwebp_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPGetDecoderVersion")]
        private static extern int WebPGetDecoderVersion_x64();

        [DllImport("libwebp_x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureDistortion")]
        private static extern int WebPPictureDistortion_x86(ref WebPPicture srcPicture, ref WebPPicture refPicture, int metric_type, IntPtr pResult);

        [DllImport("libwebp_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPPictureDistortion")]
        private static extern int WebPPictureDistortion_x64(ref WebPPicture srcPicture, ref WebPPicture refPicture, int metric_type, IntPtr pResult);
    }
}