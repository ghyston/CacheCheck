using Microsoft.AspNetCore.Mvc;
using SixLabors.ImageSharp;
using SixLabors.ImageSharp.Drawing;
using SixLabors.ImageSharp.Drawing.Processing;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

public class ImageController : Controller
{
    private int height => 480;
    private int width => 640;
    private Random rnd = new Random();

    [ResponseCache(Duration=120)]
    [HttpGet("/Create/{count}")]
    public IActionResult Create(int count)
    {
        using var image = new Image<Rgba32>(width, height);
        MemoryStream memoryStream = new();

        image.Mutate(imageContext =>
        {
            imageContext.BackgroundColor(Color.White);

            for (int i = 0; i < count; i++)
            {
                int x = rnd.Next(1, width);
                int y = rnd.Next(1, height);

                var yourPolygon = new Star(
                    x: x,
                    y: y,
                    prongs: 5,
                    innerRadii: 20.0f,
                    outerRadii: 30.0f);

                imageContext.Fill(Color.Red, yourPolygon);
            }
        });

        image.SaveAsJpeg(memoryStream);

        return File(memoryStream.ToArray(), "image/jpeg", $"result.jpeg"); // TODO: Why passing stream directly not working?
    }
}