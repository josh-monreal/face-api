using System.ComponentModel;

namespace FaceDetection
{
    public class Enumerations
    {
        public enum APIOption
        {
            [Description("Create a Person Group")]
            PersonGroupCreation = 1,

            [Description("Train a Person Group")]
            PersonGroupTraining = 2,

            [Description("Create a Person")]
            PersonCreation = 3,

            [Description("Create a Face For a Person")]
            PersonFaceCreation = 4,

            [Description("Detect a Face")]
            FaceDetection = 5,

            [Description("Identify a Face")]
            FaceIdentification = 6,
        }
    }
}
