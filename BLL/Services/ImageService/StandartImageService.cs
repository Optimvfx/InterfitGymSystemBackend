using Common.Models;
using Microsoft.Extensions.Logging;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Formats.Jpeg;

namespace BLL.Services.ImageService;

public class StandartImageService : IImageService
{
    public Result<Image> TryGetImage(byte[] bytes)
    {
        try
        {
            Image image = Image.Load(bytes);
            return new Result<Image>(image);
        }
        catch (Exception ex)
        {
            return new();
        }
    }

    public Result<byte[]> TryGetBytes(Image image)
    {
        try
        {
            using (MemoryStream memoryStream = new MemoryStream())
            {
                image.Save(memoryStream, new JpegEncoder());
                return new Result<byte[]>(memoryStream.ToArray());
            }
        }
        catch (Exception ex)
        {
            return new Result<byte[]>();
        }
    }
}