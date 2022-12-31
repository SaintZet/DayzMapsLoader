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
using DayzMapsLoader.Application.Helpers.WebpDecoder.LibwebpStructs;
using DayzMapsLoader.Application.Helpers.WebpDecoder.Predefined;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;

namespace DayzMapsLoader.Application.Helpers.WebpDecoder.LibwebpFunctions
{
    [SuppressUnmanagedCodeSecurity]
    internal sealed class UnsafeNativeMethods
    {
        private const int WebpDecoderAbiVersion = 0x0208;

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern bool SetDllDirectory(string lpPathName);

        [DllImport("kernel32.dll", SetLastError = true, CharSet = CharSet.Unicode)]
        public static extern int GetDllDirectory(int bufsize, StringBuilder buf);

        [DllImport("kernel32", CharSet = CharSet.Unicode, SetLastError = true)]
        public static extern IntPtr LoadLibrary(string librayName);

        /// <summary>
        /// Get info of WepP image
        /// </summary>
        /// <param name="rawWebP"> Bytes[] of WebP image </param>
        /// <param name="data_size"> Size of rawWebP </param>
        /// <param name="features"> Features of WebP image </param>
        /// <returns> VP8StatusCode </returns>
        internal static VP8StatusCode WebPGetFeatures(IntPtr rawWebP, int data_size, ref WebPBitstreamFeatures features)
        {
            return IntPtr.Size switch
            {
                4 => WebPGetFeaturesInternal_x86(rawWebP, (UIntPtr)data_size, ref features, WebpDecoderAbiVersion),
                8 => WebPGetFeaturesInternal_x64(rawWebP, (UIntPtr)data_size, ref features, WebpDecoderAbiVersion),
                _ => throw new InvalidOperationException("Invalid platform. Can not find proper function"),
            };
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
                    if (WebPDecodeBGRInto_x86(data, (UIntPtr)data_size, output_buffer, output_buffer_size, output_stride) == default)
                        throw new InvalidOperationException("Can't decode WebP");
                    break;

                case 8:
                    if (WebPDecodeBGRInto_x64(data, (UIntPtr)data_size, output_buffer, output_buffer_size, output_stride) == default)
                        throw new InvalidOperationException("Can't decode WebP");
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
                    if (WebPDecodeBGRAInto_x86(data, (UIntPtr)data_size, output_buffer, output_buffer_size, output_stride) == default)
                        throw new InvalidOperationException("Can't decode WebP");
                    break;

                case 8:
                    if (WebPDecodeBGRAInto_x64(data, (UIntPtr)data_size, output_buffer, output_buffer_size, output_stride) == default)
                        throw new InvalidOperationException("Can't decode WebP");
                    break;

                default:
                    throw new InvalidOperationException("Invalid platform. Can not find proper function");
            }
        }

        [DllImport("libwebp_x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPGetFeaturesInternal")]
        private static extern VP8StatusCode WebPGetFeaturesInternal_x86([In()] IntPtr rawWebP, UIntPtr data_size, ref WebPBitstreamFeatures features, int WEBP_DECODER_ABI_VERSION);

        [DllImport("libwebp_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPGetFeaturesInternal")]
        private static extern VP8StatusCode WebPGetFeaturesInternal_x64([In()] IntPtr rawWebP, UIntPtr data_size, ref WebPBitstreamFeatures features, int WEBP_DECODER_ABI_VERSION);

        [DllImport("libwebp_x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPDecodeBGRInto")]
        private static extern IntPtr WebPDecodeBGRInto_x86([In()] IntPtr data, UIntPtr data_size, IntPtr output_buffer, int output_buffer_size, int output_stride);

        [DllImport("libwebp_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPDecodeBGRInto")]
        private static extern IntPtr WebPDecodeBGRInto_x64([In()] IntPtr data, UIntPtr data_size, IntPtr output_buffer, int output_buffer_size, int output_stride);

        [DllImport("libwebp_x86.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPDecodeBGRAInto")]
        private static extern IntPtr WebPDecodeBGRAInto_x86([In()] IntPtr data, UIntPtr data_size, IntPtr output_buffer, int output_buffer_size, int output_stride);

        [DllImport("libwebp_x64.dll", CallingConvention = CallingConvention.Cdecl, EntryPoint = "WebPDecodeBGRAInto")]
        private static extern IntPtr WebPDecodeBGRAInto_x64([In()] IntPtr data, UIntPtr data_size, IntPtr output_buffer, int output_buffer_size, int output_stride);
    }
}