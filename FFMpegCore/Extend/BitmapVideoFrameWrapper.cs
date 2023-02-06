using System;
using System.IO;
using System.Runtime.InteropServices;
using System.Threading;
using System.Threading.Tasks;
using FFMpegCore.Pipes;
using SkiaSharp;

namespace FFMpegCore.Extend
{
    public class BitmapVideoFrameWrapper : IVideoFrame, IDisposable
    {
        public int Width => Source.Width;

        public int Height => Source.Height;

        public string Format { get; private set; }

        public SKBitmap Source { get; private set; }

        public BitmapVideoFrameWrapper(SKBitmap bitmap)
        {
            Source = bitmap ?? throw new ArgumentNullException(nameof(bitmap));
            Format = ConvertStreamFormat(bitmap.ColorType);
        }

        public void Serialize(Stream stream)
        {
            throw new NotImplementedException("Not implemented yet for SkiaSharp");

            //var data = Source.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadOnly, Source.PixelFormat);

            //try
            //{
            //    var buffer = new byte[data.Stride * data.Height];
            //    Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);
            //    stream.Write(buffer, 0, buffer.Length);
            //}
            //finally
            //{
            //    Source.UnlockBits(data);
            //}
        }

        public async Task SerializeAsync(Stream stream, CancellationToken token)
        {
            throw new NotImplementedException("Not implemented yet for SkiaSharp");

            //var data = Source.LockBits(new Rectangle(0, 0, Width, Height), ImageLockMode.ReadOnly, Source.PixelFormat);

            //try
            //{
            //    var buffer = new byte[data.Stride * data.Height];
            //    Marshal.Copy(data.Scan0, buffer, 0, buffer.Length);
            //    await stream.WriteAsync(buffer, 0, buffer.Length, token).ConfigureAwait(false);
            //}
            //finally
            //{
            //    Source.UnlockBits(data);
            //}
        }

        public void Dispose()
        {
            Source.Dispose();
        }

        private static string ConvertStreamFormat(SKColorType fmt)
        {
            switch (fmt)
            {
                case SKColorType.Gray8:
                    return "gray8";
                case SKColorType.Bgra8888:
                    return "bgra";
                case SKColorType.Rgb888x:
                    return "rgb";
                case SKColorType.Rgba8888:
                    return "rgba";
                case SKColorType.Rgb565:
                    return "rgb565";
                default:
                    throw new NotSupportedException($"Not supported pixel format {fmt}");
            }
        }
    }
}
