using ERNI.Software.Net.Attributes;

namespace FaceDetection
{
    public class Enumerations
    {
        public enum APIOption
        {
            [Title("Person Group Creation")]
            PersonGroupCreation = 1,

            [Title("Person Group Training")]
            PersonGroupTraining = 2,

            [Title("Person Creation")]
            PersonCreation = 3,

            [Title("Person Face Creation")]
            PersonFaceCreation = 4,

            [Title("Face Detection")]
            FaceDetection = 5,

            [Title("Face Identification")]
            FaceIdentification = 6,
        }
    }
}
