using System;
using System.IO;
using System.Text.RegularExpressions;

namespace FaceDetection
{
    public class Validators
    {
        public bool IsValid { get; private set; }

        public Validators()
        {
            IsValid = false;
        }

        public void PersonGroup(string personGroupId)
        {
            if (!new Regex(@"^[a-z0-9_-]{1,64}$").IsMatch(personGroupId))
            {
                string error = "The person group ID that you intend to create contains invalid characters.";
                IsValid = false;
                throw new InvalidOperationException(error);
            }
            else
                IsValid = true;
        }

        public void PersonName(string name)
        {
            if (!new Regex(@".{1,128}").IsMatch(name))
            {
                string error = "The person's name is too long.";
                IsValid = false;
                throw new InvalidOperationException(error);
            }
            else
                IsValid = true;
        }

        public void PersonId(string id)
        {
            if (!new Regex(@"^[a-z0-9]{8}[-][a-z0-9]{4}[-][a-z0-9]{4}[-][a-z0-9]{4}[-][a-z0-9]{12}$").IsMatch(id))
            {
                string error = "The person's ID is invalid.";
                IsValid = false;
                throw new InvalidOperationException(error);
            }
            else
                IsValid = true;
        }

        public void ImagePath(string filePath)
        {
            if (!File.Exists(filePath))
            {
                string error = "The image that you provided does not exist.";
                IsValid = false;
                throw new InvalidOperationException(error);
            }
            else
                IsValid = true;
        }
    }
}
