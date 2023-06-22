using DayzMapsLoader.Core.Helpers.WebpDecoder.LibwebpFunctions;
using DayzMapsLoader.Core.Helpers.WebpDecoder.LibwebpStructs;
using DayzMapsLoader.Core.Helpers.WebpDecoder.Predefined;

using System.Drawing;
using System.Drawing.Imaging;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;

namespace DayzMapsLoader.Core.Helpers.WebpDecoder;

internal sealed class WebP : IDisposable
{
    private static bool _wasDllInstance;

    public WebP()
    {
        if (_wasDllInstance)
            return;

        string assemblyDirectory = Path.GetDirectoryName(Assembly.GetExecutingAssembly().Location)!;
        string directory = Path.Combine(assemblyDirectory, "Helpers", "WebpDecoder");

        LoadDllFile(directory, "libwebp_x64.dll");
        LoadDllFile(directory, "libwebp_x86.dll");

        _wasDllInstance = true;
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
        catch (Exception)
        {
            throw;
        }
        finally
        {
            //Unlock the pixels
            if (bmpData != null)
                bmp?.UnlockBits(bmpData);

            //Free memory
            if (pinnedWebP.IsAllocated)
                pinnedWebP.Free();
        }
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
        VP8StatusCode? result;
        GCHandle pinnedWebP = GCHandle.Alloc(rawWebP, GCHandleType.Pinned);

        try
        {
            IntPtr ptrRawWebP = pinnedWebP.AddrOfPinnedObject();

            WebPBitstreamFeatures features = new();
            result = UnsafeNativeMethods.WebPGetFeatures(ptrRawWebP, rawWebP.Length, ref features);

            if (result != 0)
                throw new Exception(result.ToString());

            width = features.Width;
            height = features.Height;

            has_alpha = features.Has_alpha == 1;

            has_animation = features.Has_animation == 1;

            format = features.Format switch
            {
                1 => "lossy",
                2 => "lossless",
                _ => "undefined",
            };
        }
        catch (Exception ex)
        {
            throw new Exception(ex.Message + "\r\nIn WebP.GetInfo");
        }
        finally
        {
            //Free memory
            if (pinnedWebP.IsAllocated)
                pinnedWebP.Free();
        }
    }
    
    
    public static Bitmap RawWebpToBitmap(byte[] bytes)
    {
        using WebP webp = new();

        return webp.Decode(bytes);
    }

    public void Dispose()
    {
        GC.SuppressFinalize(this);
    }

    private void LoadDllFile(string dllfolder, string libname)
    {
        var currentpath = new StringBuilder(255);

        UnsafeNativeMethods.GetDllDirectory(currentpath.Length, currentpath);

        UnsafeNativeMethods.SetDllDirectory(dllfolder);

        UnsafeNativeMethods.LoadLibrary(libname);

        UnsafeNativeMethods.SetDllDirectory(currentpath.ToString());
    }
}