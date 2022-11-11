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
using DayzMapsLoader.Application.Helpers.WebpDecoder.LibwebpFunctions;
using DayzMapsLoader.Application.Helpers.WebpDecoder.LibwebpStructs;
using DayzMapsLoader.Application.Helpers.WebpDecoder.Predefined;
using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace DayzMapsLoader.Application.Helpers.WebpDecoder
{
    internal sealed class WebP : IDisposable
    {
        private const int WEBP_MAX_DIMENSION = 16383;

        private static bool _wasDllInstance;

        public WebP()
        {
            if (_wasDllInstance == true)
            {
                return;
            }

            string direcotory = Assembly.GetExecutingAssembly().Location;
            direcotory = Path.GetDirectoryName(direcotory)!;

            LoadDllFile(direcotory, "libwebp_x64.dll");
            LoadDllFile(direcotory, "libwebp_x86.dll");

            _wasDllInstance = true;
        }

        private void LoadDllFile(string dllfolder, string libname)
        {
            var currentpath = new StringBuilder(255);

            UnsafeNativeMethods.GetDllDirectory(currentpath.Length, currentpath);

            UnsafeNativeMethods.SetDllDirectory(dllfolder);

            UnsafeNativeMethods.LoadLibrary(libname);

            UnsafeNativeMethods.SetDllDirectory(currentpath.ToString());
        }

        #region | Public Decode Functions |

        /// <summary>
        /// Read a WebP file
        /// </summary>
        /// <param name="pathFileName"> WebP file to load </param>
        /// <returns> Bitmap with the WebP image </returns>
        public Bitmap Load(string pathFileName)
        {
            try
            {
                byte[] rawWebP = File.ReadAllBytes(pathFileName);

                return Decode(rawWebP);
            }
            catch (Exception ex) { throw new Exception(ex.Message + "\r\nIn WebP.Load"); }
        }

        /// <summary>
        /// Decode a WebP image
        /// </summary>
        /// <param name="rawWebP"> The data to uncompress </param>
        /// <returns> Bitmap with the WebP image </returns>
        public Bitmap Decode(byte[] rawWebP)
        {
            Bitmap? bmp = null;
            BitmapData? bmpData = null;
            GCHandle pinnedWebP = GCHandle.Alloc(rawWebP, GCHandleType.Pinned);

            try
            {
                //Get image width and height
                GetInfo(rawWebP, out int imgWidth, out int imgHeight, out bool hasAlpha, out bool hasAnimation, out string format);

                //Create a BitmapData and Lock all pixels to be written
                if (hasAlpha)
                    bmp = new Bitmap(imgWidth, imgHeight, PixelFormat.Format32bppArgb);
                else
                    bmp = new Bitmap(imgWidth, imgHeight, PixelFormat.Format24bppRgb);
                bmpData = bmp.LockBits(new Rectangle(0, 0, imgWidth, imgHeight), ImageLockMode.WriteOnly, bmp.PixelFormat);

                //Uncompress the image
                int outputSize = bmpData.Stride * imgHeight;
                IntPtr ptrData = pinnedWebP.AddrOfPinnedObject();
                if (bmp.PixelFormat == PixelFormat.Format24bppRgb)
                    UnsafeNativeMethods.WebPDecodeBGRInto(ptrData, rawWebP.Length, bmpData.Scan0, outputSize, bmpData.Stride);
                else
                    UnsafeNativeMethods.WebPDecodeBGRAInto(ptrData, rawWebP.Length, bmpData.Scan0, outputSize, bmpData.Stride);

                return bmp;
            }
            catch (Exception) { throw; }
            finally
            {
                //Unlock the pixels
                if (bmpData != null)
                    bmp.UnlockBits(bmpData);

                //Free memory
                if (pinnedWebP.IsAllocated)
                    pinnedWebP.Free();
            }
        }

        /// <summary>
        /// Decode a WebP image
        /// </summary>
        /// <param name="rawWebP"> the data to uncompress </param>
        /// <param name="options"> Options for advanced decode </param>
        /// <returns> Bitmap with the WebP image </returns>
        public Bitmap Decode(byte[] rawWebP, WebPDecoderOptions options, PixelFormat pixelFormat = PixelFormat.DontCare)
        {
            GCHandle pinnedWebP = GCHandle.Alloc(rawWebP, GCHandleType.Pinned);
            Bitmap bmp = null;
            BitmapData bmpData = null;
            VP8StatusCode result;
            try
            {
                WebPDecoderConfig config = new WebPDecoderConfig();
                if (UnsafeNativeMethods.WebPInitDecoderConfig(ref config) == 0)
                {
                    throw new Exception("WebPInitDecoderConfig failed. Wrong version?");
                }
                // Read the .webp input file information
                IntPtr ptrRawWebP = pinnedWebP.AddrOfPinnedObject();
                int height;
                int width;
                if (options.use_scaling == 0)
                {
                    result = UnsafeNativeMethods.WebPGetFeatures(ptrRawWebP, rawWebP.Length, ref config.input);
                    if (result != VP8StatusCode.VP8_STATUS_OK)
                        throw new Exception("Failed WebPGetFeatures with error " + result);

                    //Test cropping values
                    if (options.use_cropping == 1)
                    {
                        if (options.crop_left + options.crop_width > config.input.Width || options.crop_top + options.crop_height > config.input.Height)
                            throw new Exception("Crop options exceeded WebP image dimensions");
                        width = options.crop_width;
                        height = options.crop_height;
                    }
                }
                else
                {
                    width = options.scaled_width;
                    height = options.scaled_height;
                }

                config.options.bypass_filtering = options.bypass_filtering;
                config.options.no_fancy_upsampling = options.no_fancy_upsampling;
                config.options.use_cropping = options.use_cropping;
                config.options.crop_left = options.crop_left;
                config.options.crop_top = options.crop_top;
                config.options.crop_width = options.crop_width;
                config.options.crop_height = options.crop_height;
                config.options.use_scaling = options.use_scaling;
                config.options.scaled_width = options.scaled_width;
                config.options.scaled_height = options.scaled_height;
                config.options.use_threads = options.use_threads;
                config.options.dithering_strength = options.dithering_strength;
                config.options.flip = options.flip;
                config.options.alpha_dithering_strength = options.alpha_dithering_strength;

                //Create a BitmapData and Lock all pixels to be written
                if (config.input.Has_alpha == 1)
                {
                    config.output.colorspace = WEBP_CSP_MODE.MODE_bgrA;
                    bmp = new Bitmap(config.input.Width, config.input.Height, PixelFormat.Format32bppArgb);
                }
                else
                {
                    config.output.colorspace = WEBP_CSP_MODE.MODE_BGR;
                    bmp = new Bitmap(config.input.Width, config.input.Height, PixelFormat.Format24bppRgb);
                }
                bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.WriteOnly, bmp.PixelFormat);

                // Specify the output format
                config.output.u.RGBA.rgba = bmpData.Scan0;
                config.output.u.RGBA.stride = bmpData.Stride;
                config.output.u.RGBA.size = (UIntPtr)(bmp.Height * bmpData.Stride);
                config.output.height = bmp.Height;
                config.output.width = bmp.Width;
                config.output.is_external_memory = 1;

                // Decode
                result = UnsafeNativeMethods.WebPDecode(ptrRawWebP, rawWebP.Length, ref config);
                if (result != VP8StatusCode.VP8_STATUS_OK)
                {
                    throw new Exception("Failed WebPDecode with error " + result);
                }
                UnsafeNativeMethods.WebPFreeDecBuffer(ref config.output);

                return bmp;
            }
            catch (Exception ex) { throw new Exception(ex.Message + "\r\nIn WebP.Decode"); }
            finally
            {
                //Unlock the pixels
                if (bmpData != null)
                    bmp.UnlockBits(bmpData);

                //Free memory
                if (pinnedWebP.IsAllocated)
                    pinnedWebP.Free();
            }
        }

        /// <summary>
        /// Get Thumbnail from webP in mode faster/low quality
        /// </summary>
        /// <param name="rawWebP"> The data to uncompress </param>
        /// <param name="width"> Wanted width of thumbnail </param>
        /// <param name="height"> Wanted height of thumbnail </param>
        /// <returns> Bitmap with the WebP thumbnail in 24bpp </returns>
        public Bitmap GetThumbnailFast(byte[] rawWebP, int width, int height)
        {
            GCHandle pinnedWebP = GCHandle.Alloc(rawWebP, GCHandleType.Pinned);
            Bitmap bmp = null;
            BitmapData bmpData = null;

            try
            {
                WebPDecoderConfig config = new WebPDecoderConfig();
                if (UnsafeNativeMethods.WebPInitDecoderConfig(ref config) == 0)
                    throw new Exception("WebPInitDecoderConfig failed. Wrong version?");

                // Set up decode options
                config.options.bypass_filtering = 1;
                config.options.no_fancy_upsampling = 1;
                config.options.use_threads = 1;
                config.options.use_scaling = 1;
                config.options.scaled_width = width;
                config.options.scaled_height = height;

                // Create a BitmapData and Lock all pixels to be written
                bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                bmpData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, bmp.PixelFormat);

                // Specify the output format
                config.output.colorspace = WEBP_CSP_MODE.MODE_BGR;
                config.output.u.RGBA.rgba = bmpData.Scan0;
                config.output.u.RGBA.stride = bmpData.Stride;
                config.output.u.RGBA.size = (UIntPtr)(height * bmpData.Stride);
                config.output.height = height;
                config.output.width = width;
                config.output.is_external_memory = 1;

                // Decode
                IntPtr ptrRawWebP = pinnedWebP.AddrOfPinnedObject();
                VP8StatusCode result = UnsafeNativeMethods.WebPDecode(ptrRawWebP, rawWebP.Length, ref config);
                if (result != VP8StatusCode.VP8_STATUS_OK)
                    throw new Exception("Failed WebPDecode with error " + result);

                UnsafeNativeMethods.WebPFreeDecBuffer(ref config.output);

                return bmp;
            }
            catch (Exception ex) { throw new Exception(ex.Message + "\r\nIn WebP.Thumbnail"); }
            finally
            {
                //Unlock the pixels
                if (bmpData != null)
                    bmp.UnlockBits(bmpData);

                //Free memory
                if (pinnedWebP.IsAllocated)
                    pinnedWebP.Free();
            }
        }

        /// <summary>
        /// Thumbnail from webP in mode slow/high quality
        /// </summary>
        /// <param name="rawWebP"> The data to uncompress </param>
        /// <param name="width"> Wanted width of thumbnail </param>
        /// <param name="height"> Wanted height of thumbnail </param>
        /// <returns> Bitmap with the WebP thumbnail </returns>
        public Bitmap GetThumbnailQuality(byte[] rawWebP, int width, int height)
        {
            GCHandle pinnedWebP = GCHandle.Alloc(rawWebP, GCHandleType.Pinned);
            Bitmap bmp = null;
            BitmapData bmpData = null;

            try
            {
                WebPDecoderConfig config = new WebPDecoderConfig();
                if (UnsafeNativeMethods.WebPInitDecoderConfig(ref config) == 0)
                    throw new Exception("WebPInitDecoderConfig failed. Wrong version?");

                IntPtr ptrRawWebP = pinnedWebP.AddrOfPinnedObject();
                VP8StatusCode result = UnsafeNativeMethods.WebPGetFeatures(ptrRawWebP, rawWebP.Length, ref config.input);
                if (result != VP8StatusCode.VP8_STATUS_OK)
                    throw new Exception("Failed WebPGetFeatures with error " + result);

                // Set up decode options
                config.options.bypass_filtering = 0;
                config.options.no_fancy_upsampling = 0;
                config.options.use_threads = 1;
                config.options.use_scaling = 1;
                config.options.scaled_width = width;
                config.options.scaled_height = height;

                //Create a BitmapData and Lock all pixels to be written
                if (config.input.Has_alpha == 1)
                {
                    config.output.colorspace = WEBP_CSP_MODE.MODE_bgrA;
                    bmp = new Bitmap(width, height, PixelFormat.Format32bppArgb);
                }
                else
                {
                    config.output.colorspace = WEBP_CSP_MODE.MODE_BGR;
                    bmp = new Bitmap(width, height, PixelFormat.Format24bppRgb);
                }
                bmpData = bmp.LockBits(new Rectangle(0, 0, width, height), ImageLockMode.WriteOnly, bmp.PixelFormat);

                // Specify the output format
                config.output.u.RGBA.rgba = bmpData.Scan0;
                config.output.u.RGBA.stride = bmpData.Stride;
                config.output.u.RGBA.size = (UIntPtr)(height * bmpData.Stride);
                config.output.height = height;
                config.output.width = width;
                config.output.is_external_memory = 1;

                // Decode
                result = UnsafeNativeMethods.WebPDecode(ptrRawWebP, rawWebP.Length, ref config);
                if (result != VP8StatusCode.VP8_STATUS_OK)
                    throw new Exception("Failed WebPDecode with error " + result);

                UnsafeNativeMethods.WebPFreeDecBuffer(ref config.output);

                return bmp;
            }
            catch (Exception ex) { throw new Exception(ex.Message + "\r\nIn WebP.Thumbnail"); }
            finally
            {
                //Unlock the pixels
                if (bmpData != null)
                    bmp.UnlockBits(bmpData);

                //Free memory
                if (pinnedWebP.IsAllocated)
                    pinnedWebP.Free();
            }
        }

        #endregion | Public Decode Functions |

        #region | Public Encode Functions |

        /// <summary>
        /// Save bitmap to file in WebP format
        /// </summary>
        /// <param name="bmp"> Bitmap with the WebP image </param>
        /// <param name="pathFileName"> The file to write </param>
        /// <param name="quality">
        /// Between 0 (lower quality, lowest file size) and 100 (highest quality, higher file size)
        /// </param>
        public void Save(Bitmap bmp, string pathFileName, int quality = 75)
        {
            byte[] rawWebP;

            try
            {
                //Encode in webP format
                rawWebP = EncodeLossy(bmp, quality);

                //Write webP file
                File.WriteAllBytes(pathFileName, rawWebP);
            }
            catch (Exception ex) { throw new Exception(ex.Message + "\r\nIn WebP.Save"); }
        }

        /// <summary>
        /// Lossy encoding bitmap to WebP (Simple encoding API)
        /// </summary>
        /// <param name="bmp"> Bitmap with the image </param>
        /// <param name="quality">
        /// Between 0 (lower quality, lowest file size) and 100 (highest quality, higher file size)
        /// </param>
        /// <returns> Compressed data </returns>
        public byte[] EncodeLossy(Bitmap bmp, int quality = 75)
        {
            //test bmp
            if (bmp.Width == 0 || bmp.Height == 0)
                throw new ArgumentException("Bitmap contains no data.", "bmp");
            if (bmp.Width > WEBP_MAX_DIMENSION || bmp.Height > WEBP_MAX_DIMENSION)
                throw new NotSupportedException("Bitmap's dimension is too large. Max is " + WEBP_MAX_DIMENSION + "x" + WEBP_MAX_DIMENSION + " pixels.");
            if (bmp.PixelFormat != PixelFormat.Format24bppRgb && bmp.PixelFormat != PixelFormat.Format32bppArgb)
                throw new NotSupportedException("Only support Format24bppRgb and Format32bppArgb pixelFormat.");

            BitmapData bmpData = null;
            IntPtr unmanagedData = IntPtr.Zero;

            try
            {
                int size;

                //Get bmp data
                bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);

                //Compress the bmp data
                if (bmp.PixelFormat == PixelFormat.Format24bppRgb)
                    size = UnsafeNativeMethods.WebPEncodeBGR(bmpData.Scan0, bmp.Width, bmp.Height, bmpData.Stride, quality, out unmanagedData);
                else
                    size = UnsafeNativeMethods.WebPEncodeBGRA(bmpData.Scan0, bmp.Width, bmp.Height, bmpData.Stride, quality, out unmanagedData);
                if (size == 0)
                    throw new Exception("Can´t encode WebP");

                //Copy image compress data to output array
                byte[] rawWebP = new byte[size];
                Marshal.Copy(unmanagedData, rawWebP, 0, size);

                return rawWebP;
            }
            catch (Exception ex) { throw new Exception(ex.Message + "\r\nIn WebP.EncodeLossly"); }
            finally
            {
                //Unlock the pixels
                if (bmpData != null)
                    bmp.UnlockBits(bmpData);

                //Free memory
                if (unmanagedData != IntPtr.Zero)
                    UnsafeNativeMethods.WebPFree(unmanagedData);
            }
        }

        /// <summary>
        /// Lossy encoding bitmap to WebP (Advanced encoding API)
        /// </summary>
        /// <param name="bmp"> Bitmap with the image </param>
        /// <param name="quality">
        /// Between 0 (lower quality, lowest file size) and 100 (highest quality, higher file size)
        /// </param>
        /// <param name="speed"> Between 0 (fastest, lowest compression) and 9 (slower, best compression) </param>
        /// <returns> Compressed data </returns>
        public byte[] EncodeLossy(Bitmap bmp, int quality, int speed, bool info = false)
        {
            //Initialize configuration structure
            WebPConfig config = new WebPConfig();

            //Set compression parameters
            if (UnsafeNativeMethods.WebPConfigInit(ref config, WebPPreset.WEBP_PRESET_DEFAULT, 75) == 0)
                throw new Exception("Can´t configure preset");

            // Add additional tuning:
            config.method = speed;
            if (config.method > 6)
                config.method = 6;
            config.quality = quality;
            config.autofilter = 1;
            config.pass = speed + 1;
            config.segments = 4;
            config.partitions = 3;
            config.thread_level = 1;
            config.alpha_quality = quality;
            config.alpha_filtering = 2;
            config.use_sharp_yuv = 1;

            if (UnsafeNativeMethods.WebPGetDecoderVersion() > 1082)     //Old version does not support preprocessing 4
            {
                config.preprocessing = 4;
                config.use_sharp_yuv = 1;
            }
            else
                config.preprocessing = 3;

            return AdvancedEncode(bmp, config, info);
        }

        /// <summary>
        /// Lossless encoding bitmap to WebP (Simple encoding API)
        /// </summary>
        /// <param name="bmp"> Bitmap with the image </param>
        /// <returns> Compressed data </returns>
        public byte[] EncodeLossless(Bitmap bmp)
        {
            //test bmp
            if (bmp.Width == 0 || bmp.Height == 0)
                throw new ArgumentException("Bitmap contains no data.", "bmp");
            if (bmp.Width > WEBP_MAX_DIMENSION || bmp.Height > WEBP_MAX_DIMENSION)
                throw new NotSupportedException("Bitmap's dimension is too large. Max is " + WEBP_MAX_DIMENSION + "x" + WEBP_MAX_DIMENSION + " pixels.");
            if (bmp.PixelFormat != PixelFormat.Format24bppRgb && bmp.PixelFormat != PixelFormat.Format32bppArgb)
                throw new NotSupportedException("Only support Format24bppRgb and Format32bppArgb pixelFormat.");

            BitmapData bmpData = null;
            IntPtr unmanagedData = IntPtr.Zero;
            try
            {
                //Get bmp data
                bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);

                //Compress the bmp data
                int size;
                if (bmp.PixelFormat == PixelFormat.Format24bppRgb)
                    size = UnsafeNativeMethods.WebPEncodeLosslessBGR(bmpData.Scan0, bmp.Width, bmp.Height, bmpData.Stride, out unmanagedData);
                else
                    size = UnsafeNativeMethods.WebPEncodeLosslessBGRA(bmpData.Scan0, bmp.Width, bmp.Height, bmpData.Stride, out unmanagedData);

                //Copy image compress data to output array
                byte[] rawWebP = new byte[size];
                Marshal.Copy(unmanagedData, rawWebP, 0, size);

                return rawWebP;
            }
            catch (Exception ex) { throw new Exception(ex.Message + "\r\nIn WebP.EncodeLossless (Simple)"); }
            finally
            {
                //Unlock the pixels
                if (bmpData != null)
                    bmp.UnlockBits(bmpData);

                //Free memory
                if (unmanagedData != IntPtr.Zero)
                    UnsafeNativeMethods.WebPFree(unmanagedData);
            }
        }

        /// <summary>
        /// Lossless encoding image in bitmap (Advanced encoding API)
        /// </summary>
        /// <param name="bmp"> Bitmap with the image </param>
        /// <param name="speed"> Between 0 (fastest, lowest compression) and 9 (slower, best compression) </param>
        /// <returns> Compressed data </returns>
        public byte[] EncodeLossless(Bitmap bmp, int speed)
        {
            //Initialize configuration structure
            WebPConfig config = new WebPConfig();

            //Set compression parameters
            if (UnsafeNativeMethods.WebPConfigInit(ref config, WebPPreset.WEBP_PRESET_DEFAULT, (speed + 1) * 10) == 0)
                throw new Exception("Can´t config preset");

            //Old version of DLL does not support info and WebPConfigLosslessPreset
            if (UnsafeNativeMethods.WebPGetDecoderVersion() > 1082)
            {
                if (UnsafeNativeMethods.WebPConfigLosslessPreset(ref config, speed) == 0)
                    throw new Exception("Can´t configure lossless preset");
            }
            else
            {
                config.lossless = 1;
                config.method = speed;
                if (config.method > 6)
                    config.method = 6;
                config.quality = (speed + 1) * 10;
            }
            config.pass = speed + 1;
            config.thread_level = 1;
            config.alpha_filtering = 2;
            config.use_sharp_yuv = 1;
            config.exact = 0;

            return AdvancedEncode(bmp, config, false);
        }

        /// <summary>
        /// Near lossless encoding image in bitmap
        /// </summary>
        /// <param name="bmp"> Bitmap with the image </param>
        /// <param name="quality">
        /// Between 0 (lower quality, lowest file size) and 100 (highest quality, higher file size)
        /// </param>
        /// <param name="speed"> Between 0 (fastest, lowest compression) and 9 (slower, best compression) </param>
        /// <returns> Compress data </returns>
        public byte[] EncodeNearLossless(Bitmap bmp, int quality, int speed = 9)
        {
            //test DLL version
            if (UnsafeNativeMethods.WebPGetDecoderVersion() <= 1082)
                throw new Exception("This DLL version not support EncodeNearLossless");

            //Inicialize config struct
            WebPConfig config = new WebPConfig();

            //Set compression parameters
            if (UnsafeNativeMethods.WebPConfigInit(ref config, WebPPreset.WEBP_PRESET_DEFAULT, (speed + 1) * 10) == 0)
                throw new Exception("Can´t configure preset");
            if (UnsafeNativeMethods.WebPConfigLosslessPreset(ref config, speed) == 0)
                throw new Exception("Can´t configure lossless preset");
            config.pass = speed + 1;
            config.near_lossless = quality;
            config.thread_level = 1;
            config.alpha_filtering = 2;
            config.use_sharp_yuv = 1;
            config.exact = 0;

            return AdvancedEncode(bmp, config, false);
        }

        #endregion | Public Encode Functions |

        #region | Another Public Functions |

        /// <summary>
        /// Get the libwebp version
        /// </summary>
        /// <returns> Version of library </returns>
        public string GetVersion()
        {
            try
            {
                uint v = (uint)UnsafeNativeMethods.WebPGetDecoderVersion();
                var revision = v % 256;
                var minor = (v >> 8) % 256;
                var major = (v >> 16) % 256;
                return major + "." + minor + "." + revision;
            }
            catch (Exception ex) { throw new Exception(ex.Message + "\r\nIn WebP.GetVersion"); }
        }

        /// <summary>
        /// Get info of WEBP data
        /// </summary>
        /// <param name="rawWebP"> The data of WebP </param>
        /// <param name="width"> width of image </param>
        /// <param name="height"> height of image </param>
        /// <param name="has_alpha"> Image has alpha channel </param>
        /// <param name="has_animation"> Image is a animation </param>
        /// <param name="format"> Format of image: 0 = undefined (/mixed), 1 = lossy, 2 = lossless </param>
        public void GetInfo(byte[] rawWebP, out int width, out int height, out bool has_alpha, out bool has_animation, out string format)
        {
            VP8StatusCode result;
            GCHandle pinnedWebP = GCHandle.Alloc(rawWebP, GCHandleType.Pinned);

            try
            {
                IntPtr ptrRawWebP = pinnedWebP.AddrOfPinnedObject();

                WebPBitstreamFeatures features = new WebPBitstreamFeatures();
                result = UnsafeNativeMethods.WebPGetFeatures(ptrRawWebP, rawWebP.Length, ref features);

                if (result != 0)
                    throw new Exception(result.ToString());

                width = features.Width;
                height = features.Height;
                if (features.Has_alpha == 1) has_alpha = true; else has_alpha = false;
                if (features.Has_animation == 1) has_animation = true; else has_animation = false;
                switch (features.Format)
                {
                    case 1:
                        format = "lossy";
                        break;

                    case 2:
                        format = "lossless";
                        break;

                    default:
                        format = "undefined";
                        break;
                }
            }
            catch (Exception ex) { throw new Exception(ex.Message + "\r\nIn WebP.GetInfo"); }
            finally
            {
                //Free memory
                if (pinnedWebP.IsAllocated)
                    pinnedWebP.Free();
            }
        }

        /// <summary>
        /// Compute PSNR, SSIM or LSIM distortion metric between two pictures. Warning: this
        /// function is rather CPU-intensive
        /// </summary>
        /// <param name="source"> Picture to measure </param>
        /// <param name="reference"> Reference picture </param>
        /// <param name="metric_type"> 0 = PSNR, 1 = SSIM, 2 = LSIM </param>
        /// <returns> dB in the Y/U/V/Alpha/All order </returns>
        public float[] GetPictureDistortion(Bitmap source, Bitmap reference, int metric_type)
        {
            WebPPicture wpicSource = new WebPPicture();
            WebPPicture wpicReference = new WebPPicture();
            BitmapData sourceBmpData = null;
            BitmapData referenceBmpData = null;
            float[] result = new float[5];
            GCHandle pinnedResult = GCHandle.Alloc(result, GCHandleType.Pinned);

            try
            {
                if (source == null)
                    throw new Exception("Source picture is void");
                if (reference == null)
                    throw new Exception("Reference picture is void");
                if (metric_type > 2)
                    throw new Exception("Bad metric_type. Use 0 = PSNR, 1 = SSIM, 2 = LSIM");
                if (source.Width != reference.Width || source.Height != reference.Height)
                    throw new Exception("Source and Reference pictures have different dimensions");

                // Setup the source picture data, allocating the bitmap, width and height
                sourceBmpData = source.LockBits(new Rectangle(0, 0, source.Width, source.Height), ImageLockMode.ReadOnly, source.PixelFormat);
                wpicSource = new WebPPicture();
                if (UnsafeNativeMethods.WebPPictureInitInternal(ref wpicSource) != 1)
                    throw new Exception("Can´t initialize WebPPictureInit");
                wpicSource.width = (int)source.Width;
                wpicSource.height = (int)source.Height;

                //Put the source bitmap componets in wpic
                if (sourceBmpData.PixelFormat == PixelFormat.Format32bppArgb)
                {
                    wpicSource.use_argb = 1;
                    if (UnsafeNativeMethods.WebPPictureImportBGRA(ref wpicSource, sourceBmpData.Scan0, sourceBmpData.Stride) != 1)
                        throw new Exception("Can´t allocate memory in WebPPictureImportBGR");
                }
                else
                {
                    wpicSource.use_argb = 0;
                    if (UnsafeNativeMethods.WebPPictureImportBGR(ref wpicSource, sourceBmpData.Scan0, sourceBmpData.Stride) != 1)
                        throw new Exception("Can´t allocate memory in WebPPictureImportBGR");
                }

                // Setup the reference picture data, allocating the bitmap, width and height
                referenceBmpData = reference.LockBits(new Rectangle(0, 0, reference.Width, reference.Height), ImageLockMode.ReadOnly, reference.PixelFormat);
                wpicReference = new WebPPicture();
                if (UnsafeNativeMethods.WebPPictureInitInternal(ref wpicReference) != 1)
                    throw new Exception("Can´t initialize WebPPictureInit");
                wpicReference.width = (int)reference.Width;
                wpicReference.height = (int)reference.Height;
                wpicReference.use_argb = 1;

                //Put the source bitmap contents in WebPPicture instance
                if (sourceBmpData.PixelFormat == PixelFormat.Format32bppArgb)
                {
                    wpicSource.use_argb = 1;
                    if (UnsafeNativeMethods.WebPPictureImportBGRA(ref wpicReference, referenceBmpData.Scan0, referenceBmpData.Stride) != 1)
                        throw new Exception("Can´t allocate memory in WebPPictureImportBGR");
                }
                else
                {
                    wpicSource.use_argb = 0;
                    if (UnsafeNativeMethods.WebPPictureImportBGR(ref wpicReference, referenceBmpData.Scan0, referenceBmpData.Stride) != 1)
                        throw new Exception("Can´t allocate memory in WebPPictureImportBGR");
                }

                //Measure
                IntPtr ptrResult = pinnedResult.AddrOfPinnedObject();
                if (UnsafeNativeMethods.WebPPictureDistortion(ref wpicSource, ref wpicReference, metric_type, ptrResult) != 1)
                    throw new Exception("Can´t measure.");
                return result;
            }
            catch (Exception ex) { throw new Exception(ex.Message + "\r\nIn WebP.GetPictureDistortion"); }
            finally
            {
                //Unlock the pixels
                if (sourceBmpData != null)
                    source.UnlockBits(sourceBmpData);
                if (referenceBmpData != null)
                    reference.UnlockBits(referenceBmpData);

                //Free memory
                if (wpicSource.argb != IntPtr.Zero)
                    UnsafeNativeMethods.WebPPictureFree(ref wpicSource);
                if (wpicReference.argb != IntPtr.Zero)
                    UnsafeNativeMethods.WebPPictureFree(ref wpicReference);
                //Free memory
                if (pinnedResult.IsAllocated)
                    pinnedResult.Free();
            }
        }

        #endregion | Another Public Functions |

        #region | Private Methods |

        private delegate int MyWriterDelegate([InAttribute()] IntPtr data, UIntPtr data_size, ref WebPPicture picture);

        /// <summary>
        /// Encoding image using Advanced encoding API
        /// </summary>
        /// <param name="bmp"> Bitmap with the image </param>
        /// <param name="config"> Configuration for encode </param>
        /// <param name="info"> True if need encode info. </param>
        /// <returns> Compressed data </returns>
        private byte[] AdvancedEncode(Bitmap bmp, WebPConfig config, bool info)
        {
            byte[] rawWebP = null;
            byte[] dataWebp = null;
            WebPPicture wpic = new WebPPicture();
            BitmapData bmpData = null;
            WebPAuxStats stats = new WebPAuxStats();
            IntPtr ptrStats = IntPtr.Zero;
            GCHandle pinnedArrayHandle = new GCHandle();
            int dataWebpSize;
            try
            {
                //Validate the configuration
                if (UnsafeNativeMethods.WebPValidateConfig(ref config) != 1)
                    throw new Exception("Bad configuration parameters");

                //test bmp
                if (bmp.Width == 0 || bmp.Height == 0)
                    throw new ArgumentException("Bitmap contains no data.", "bmp");
                if (bmp.Width > WEBP_MAX_DIMENSION || bmp.Height > WEBP_MAX_DIMENSION)
                    throw new NotSupportedException("Bitmap's dimension is too large. Max is " + WEBP_MAX_DIMENSION + "x" + WEBP_MAX_DIMENSION + " pixels.");
                if (bmp.PixelFormat != PixelFormat.Format24bppRgb && bmp.PixelFormat != PixelFormat.Format32bppArgb)
                    throw new NotSupportedException("Only support Format24bppRgb and Format32bppArgb pixelFormat.");

                // Setup the input data, allocating a the bitmap, width and height
                bmpData = bmp.LockBits(new Rectangle(0, 0, bmp.Width, bmp.Height), ImageLockMode.ReadOnly, bmp.PixelFormat);
                if (UnsafeNativeMethods.WebPPictureInitInternal(ref wpic) != 1)
                    throw new Exception("Can´t initialize WebPPictureInit");
                wpic.width = (int)bmp.Width;
                wpic.height = (int)bmp.Height;
                wpic.use_argb = 1;

                if (bmp.PixelFormat == PixelFormat.Format32bppArgb)
                {
                    //Put the bitmap componets in wpic
                    int result = UnsafeNativeMethods.WebPPictureImportBGRA(ref wpic, bmpData.Scan0, bmpData.Stride);
                    if (result != 1)
                        throw new Exception("Can´t allocate memory in WebPPictureImportBGRA");
                    wpic.colorspace = (uint)WEBP_CSP_MODE.MODE_bgrA;
                    dataWebpSize = bmp.Width * bmp.Height * 32;
                    dataWebp = new byte[bmp.Width * bmp.Height * 32];                //Memory for WebP output
                }
                else
                {
                    //Put the bitmap contents in WebPPicture instance
                    int result = UnsafeNativeMethods.WebPPictureImportBGR(ref wpic, bmpData.Scan0, bmpData.Stride);
                    if (result != 1)
                        throw new Exception("Can´t allocate memory in WebPPictureImportBGR");
                    dataWebpSize = bmp.Width * bmp.Height * 24;
                }

                //Set up statistics of compression
                if (info)
                {
                    stats = new WebPAuxStats();
                    ptrStats = Marshal.AllocHGlobal(Marshal.SizeOf(stats));
                    Marshal.StructureToPtr(stats, ptrStats, false);
                    wpic.stats = ptrStats;
                }

                //Memory for WebP output
                if (dataWebpSize > 2147483591)
                    dataWebpSize = 2147483591;
                dataWebp = new byte[bmp.Width * bmp.Height * 32];
                pinnedArrayHandle = GCHandle.Alloc(dataWebp, GCHandleType.Pinned);
                IntPtr initPtr = pinnedArrayHandle.AddrOfPinnedObject();
                wpic.custom_ptr = initPtr;

                //Set up a byte-writing method (write-to-memory, in this case)
                UnsafeNativeMethods.OnCallback = new UnsafeNativeMethods.WebPMemoryWrite(MyWriter);
                wpic.writer = Marshal.GetFunctionPointerForDelegate(UnsafeNativeMethods.OnCallback);

                //compress the input samples
                if (UnsafeNativeMethods.WebPEncode(ref config, ref wpic) != 1)
                    throw new Exception("Encoding error: " + ((WebPEncodingError)wpic.error_code).ToString());

                //Remove OnCallback
                UnsafeNativeMethods.OnCallback = null;

                //Unlock the pixels
                bmp.UnlockBits(bmpData);
                bmpData = null;

                //Copy webpData to rawWebP
                int size = (int)((long)wpic.custom_ptr - (long)initPtr);
                rawWebP = new byte[size];
                Array.Copy(dataWebp, rawWebP, size);

                //Remove compression data
                pinnedArrayHandle.Free();
                dataWebp = null;

                return rawWebP;
            }
            catch (Exception ex) { throw new Exception(ex.Message + "\r\nIn WebP.AdvancedEncode"); }
            finally
            {
                //Free temporal compress memory
                if (pinnedArrayHandle.IsAllocated)
                {
                    pinnedArrayHandle.Free();
                }

                //Free statistics memory
                if (ptrStats != IntPtr.Zero)
                {
                    Marshal.FreeHGlobal(ptrStats);
                }

                //Unlock the pixels
                if (bmpData != null)
                {
                    bmp.UnlockBits(bmpData);
                }

                //Free memory
                if (wpic.argb != IntPtr.Zero)
                {
                    UnsafeNativeMethods.WebPPictureFree(ref wpic);
                }
            }
        }

        private int MyWriter([InAttribute()] IntPtr data, UIntPtr data_size, ref WebPPicture picture)
        {
            UnsafeNativeMethods.CopyMemory(picture.custom_ptr, data, (uint)data_size);
            //picture.custom_ptr = IntPtr.Add(picture.custom_ptr, (int)data_size);   //Only in .NET > 4.0
            picture.custom_ptr = new IntPtr(picture.custom_ptr.ToInt64() + (int)data_size);
            return 1;
        }

        #endregion | Private Methods |

        #region | Destruction |

        /// <summary>
        /// Free memory
        /// </summary>
        public void Dispose()
        {
            GC.SuppressFinalize(this);
        }

        #endregion | Destruction |
    }
}