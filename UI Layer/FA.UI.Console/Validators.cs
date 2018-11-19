using System;
using System.IO;
using System.Text.RegularExpressions;

namespace FaceDetection
{
    public static class Validators
    {
        public static void PersonGroup(string personGroupId)
        {
            if (!new Regex(@"^[a-z0-9_-]{1,64}$").IsMatch(personGroupId))
            {
                string error = "The person group ID that you intend to create contains invalid characters.";
                throw new InvalidOperationException(error);
            };
        }

        public static void PersonName(string name)
        {
            if (!new Regex(@".{1,128}").IsMatch(name))
            {
                string error = "The person's name is too long.";
                throw new InvalidOperationException(error);
            }
        }

        public static void PersonId(string id)
        {
            if(!new Regex(@"^[a-z0-9]{8}[-][a-z0-9]{4}[-][a-z0-9]{4}[-][a-z0-9]{4}[-][a-z0-9]{12}$").IsMatch(id))
            {
                string error = "The person's ID is invalid.";
                throw new InvalidOperationException(error);
            }
        }

        public static void ImagePath(string filePath)
        {
            if (!File.Exists(filePath))
            {
                string error = "The image that you provided does not exist.";
                throw new InvalidOperationException(error);
            }
        }
    }
}
