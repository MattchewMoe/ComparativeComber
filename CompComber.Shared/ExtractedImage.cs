namespace CompComber.Shared;


    public class ExtractedImage
    {
        public byte[] ImageData { get; set; }  // To hold the raw image bytes
        public string ImageFormat { get; set; }  // To hold the image format (e.g., "image/png")
        public string Caption { get; set; }  // New field


        // TODO: Add more properties to capture additional image metadata if needed
    }

