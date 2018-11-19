namespace FA.Data.Entities.Face
{
    public class MicrosoftCognitiveFace
    {
        public string FaceId { get; set; }
        public FaceRectangle FaceRectangle { get; set; }
        public FaceAttributes.FaceAttributes FaceAttributes { get; set; }
    }
}
