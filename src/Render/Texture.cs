namespace Kinetra.Render
{
    public class Texture : IDisposable
    {
        int Handle;

        public Texture(string path)
        {
            Handle = GL.GenTexture();

            Use();

            // Load the image using StbImageSharp
            StbImage.stbi_set_flip_vertically_on_load(1);
            ImageResult image = ImageResult.FromStream(File.OpenRead(path), ColorComponents.RedGreenBlueAlpha);


            // Upload the image data to the GPU
            GL.TexImage2D(
                TextureTarget.Texture2D,   // Target
                0,                         // Level of detail
                PixelInternalFormat.Rgba,  // Internal format
                image.Width, image.Height, // Width and height
                0,                         // Border
                PixelFormat.Rgba,          // Format of the pixel data 
                PixelType.UnsignedByte,    // Data type of the pixel data
                image.Data                 // Pixel data
                );

            // Set texture parameters
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMinFilter, (int)TextureMinFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureMagFilter, (int)TextureMagFilter.Linear);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapS, (int)TextureWrapMode.Repeat);
            GL.TexParameter(TextureTarget.Texture2D, TextureParameterName.TextureWrapT, (int)TextureWrapMode.Repeat);

            GL.GenerateMipmap(GenerateMipmapTarget.Texture2D);
        }

        public void Use()
        {
            GL.BindTexture(TextureTarget.Texture2D, Handle);
        }

        public void Dispose()
        {
            GL.DeleteTexture(Handle);
        }
    }
}
