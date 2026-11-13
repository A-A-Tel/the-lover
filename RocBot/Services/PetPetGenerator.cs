using SixLabors.ImageSharp;
using SixLabors.ImageSharp.PixelFormats;
using SixLabors.ImageSharp.Processing;

namespace RocBot.Services;

public sealed class PetPetGenerator : IDisposable
{
    private const int CanvasSize = 512;
    private const int AvatarSize = 350;
    private const int AvatarX = 162;
    private const int AvatarY = 162;
    private const double ScalePercentage = 0.07;
    private const int FrameDelay = 6; // Hundredths of a second

    private readonly Image<Rgba32>[] _overlays;

    public PetPetGenerator(IEnumerable<string> overlayPaths)
    {
        _overlays = [.. overlayPaths.Select(Image.Load<Rgba32>)];
        if (_overlays.Length == 0)
            throw new ArgumentException("At least one overlay frame is required.", nameof(overlayPaths));
    }

    public static PetPetGenerator FromDirectory(string directory, int frameCount = 5) =>
        new(Enumerable.Range(1, frameCount).Select(i => Path.Combine(directory, $"frame-{i}.png")));

    public byte[] Generate(Stream avatarStream)
    {
        using var avatar = Image.Load<Rgba32>(avatarStream);
        return Generate(avatar);
    }

    public byte[] Generate(Image<Rgba32> avatar)
    {
        var frames = new List<Image<Rgba32>>(_overlays.Length);

        try
        {
            for (int i = 0; i < _overlays.Length; i++)
            {
                int sign = i % 2 == 0 ? 1 : -1;
                double scaleX = 1.0 + ScalePercentage * sign;
                double scaleY = 1.0 - ScalePercentage * sign;

                int w = (int)Math.Round(AvatarSize * scaleX);
                int h = (int)Math.Round(AvatarSize * scaleY);
                int x = AvatarX + AvatarSize / 2 - w / 2;
                int y = AvatarY + AvatarSize / 2 - h / 2;

                using Image<Rgba32> scaledAvatar = avatar.Clone(ctx => ctx.Resize(w, h));

                Image<Rgba32> frame = new(CanvasSize, CanvasSize, new Rgba32(255, 255, 255, 255));
                frame.Mutate(ctx =>
                {
                    ctx.DrawImage(scaledAvatar, new Point(x, y), 1f);
                    ctx.DrawImage(_overlays[i], new Point(0, 0), 1f);
                });

                frames.Add(frame);
            }

            return ToGif(frames);
        }
        finally
        {
            foreach (var f in frames) f.Dispose();
        }
    }

    private static byte[] ToGif(IReadOnlyList<Image<Rgba32>> frames)
    {
        using Image<Rgba32> gif = frames[0].Clone();

        for (int i = 1; i < frames.Count; i++)
            gif.Frames.AddFrame(frames[i].Frames.RootFrame);

        gif.Metadata.GetGifMetadata().RepeatCount = 0;

        foreach (ImageFrame<Rgba32> frame in gif.Frames)
        {
            frame.Metadata.GetGifMetadata().FrameDelay = FrameDelay;
        }

        using MemoryStream ms = new();
        gif.SaveAsGif(ms);
        return ms.ToArray();
    }

    public void Dispose()
    {
        foreach (Image<Rgba32> o in _overlays) o.Dispose();
    }
}