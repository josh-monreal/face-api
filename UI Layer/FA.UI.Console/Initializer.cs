using FA.Business.Core;
using FA.Business.DTOs;
using FA.Business.DTOs.DetectedFace;
using FaceDetection;
using System;
using System.Collections.Generic;
using System.Reflection;
using System.Threading.Tasks;
using static FaceDetection.Enumerations;
using static System.Console;

namespace FA.UI.Console
{
    public class Initializer
    {
        private bool _isCompleted;
        private readonly List<Task> _taskList;

        private readonly IPersonGroupLogic _personGroupLogic;
        private readonly IPersonLogic _personLogic;
        private readonly IFaceLogic _faceLogic;

        public Initializer(IPersonGroupLogic personGroupLogic,
            IPersonLogic personLogic,
            IFaceLogic faceLogic)
        {
            _personGroupLogic = personGroupLogic;
            _personLogic = personLogic;
            _faceLogic = faceLogic;

            _isCompleted = false;
            _taskList = new List<Task>();
        }

        private void Initialize()
        {
            SetWindowSize(200, 50);

            var assemblyVersion = Assembly.GetExecutingAssembly()
                .GetName()
                .Version
                .ToString();

            WriteLine("\n\tFaceAPI Application v. {0}", assemblyVersion);
        }

        private void ExecuteAPIOption(int option)
        {
            switch (option)
            {
                case (int)APIOption.PersonGroupCreation: ExecutePersonGroupCreation(); break;
                case (int)APIOption.PersonGroupTraining: ExecutePersonGroupTraining(); break;
                case (int)APIOption.PersonCreation: ExecutePersonCreation(); break;
                case (int)APIOption.PersonFaceCreation: ExecutePersonFaceCreation(); break;
                case (int)APIOption.FaceDetection: ExecuteFaceDetection(); break;
                case (int)APIOption.FaceIdentification: ExecuteFaceIdentification(); break;
                default: break;
            }
        }

        private void ExecutePersonGroupCreation()
        {
            WriteLine("\n\tYou have selected the option for creating a person-group.");
            WriteLine("\t\t* Use numbers, lower case letters, '-' and '_'. The maximum length of the personGroupId is 64.");

            Write("\n\n\tEnter an ID for the group that you wish to create: ");
            string personGroupId = ReadLine();
            Validators.PersonGroup(personGroupId);

            _taskList.Add(Task.Factory.StartNew(() =>
            {
                try
                {
                    var result = _personGroupLogic.Create(personGroupId);
                    WriteLine("\t" + result.Message);
                }

                catch (Exception exception)
                {
                    WriteLine($"\n\tError Message: { exception.Message }");
                    WriteLine($"\n\tInner Exception: { exception.InnerException }");
                    WriteLine($"\n\tStackTrace: { exception.StackTrace }");
                    ReadLine();
                }
            }));
        }

        private void ExecutePersonGroupTraining()
        {
            WriteLine("\n\tYou have selected the option for training your person-group.");

            Write("\n\n\tEnter the ID of the person-group that you would like to train: ");
            string personGroupId = ReadLine();
            Validators.PersonGroup(personGroupId);

            _taskList.Add(Task.Factory.StartNew(() =>
            {
                try
                {
                    var result = _personGroupLogic.Train(personGroupId);
                    WriteLine("\t" + result.Message);
                }

                catch (Exception exception)
                {
                    WriteLine($"\n\tError Message: { exception.Message }");
                    WriteLine($"\n\tInner Exception: { exception.InnerException }");
                    WriteLine($"\n\tStackTrace: { exception.StackTrace }");
                    ReadLine();
                }
            }));
        }

        private void ExecutePersonCreation()
        {
            WriteLine("\n\tYou have selected the option for creating a person.");
            WriteLine("\t\t* The maximum length of the person's name is 128.");

            Write("\n\n\tEnter the person group for which you would like to add the person to: ");
            string personGroupId = ReadLine();
            Validators.PersonGroup(personGroupId);

            Write("\n\n\tEnter the name of the person that you would like to add: ");
            string personName = ReadLine();
            Validators.PersonName(personName);

            _taskList.Add(Task.Factory.StartNew(() =>
            {
                try
                {
                    var result = _personLogic.Create(personGroupId, personName);
                    WriteLine("\t" + result.Message);

                    var person = (PersonDto)result.Data;

                    if (person != null) WriteLine("\tPerson ID: " + person.PersonId);
                }

                catch (Exception exception)
                {
                    WriteLine($"\n\tError Message: { exception.Message }");
                    WriteLine($"\n\tInner Exception: { exception.InnerException }");
                    WriteLine($"\n\tStackTrace: { exception.StackTrace }");
                    ReadLine();
                }
            }));
        }

        private void ExecutePersonFaceCreation()
        {
            WriteLine("\n\tYou have selected the option for adding a face data to a person.");
            WriteLine("\t\t* Valid image size is from 1KB to 6MB.");

            Write("\n\n\tEnter the person group for which you would like to add the face data to: ");
            string personGroupId = ReadLine();
            Validators.PersonGroup(personGroupId);

            Write("\n\n\tEnter the ID of the person for which you would like to add the face data to: ");
            string personId = ReadLine();
            Validators.PersonId(personId);

            Write("\n\n\tEnter the file path of the image that you would like to add to {0}: ", personId);
            string imagePath = ReadLine();
            Validators.ImagePath(imagePath);

            _taskList.Add(Task.Factory.StartNew(() =>
            {
                try
                {
                    var result = _personLogic.AddFace(personGroupId,
                        personId,
                        imagePath);
                    WriteLine("\t" + result.Message);

                    var persistedFace = (PersistedFaceDto)result.Data;

                    if (persistedFace != null) WriteLine("\tPersisted Face ID: " + persistedFace.PersistedFaceId);
                }

                catch (Exception exception)
                {
                    WriteLine($"\n\tError Message: { exception.Message }");
                    WriteLine($"\n\tInner Exception: { exception.InnerException }");
                    WriteLine($"\n\tStackTrace: { exception.StackTrace }");
                    ReadLine();
                }
            }));
        }

        private void ExecuteFaceDetection()
        {
            WriteLine("\n\tYou have selected the option for detecting faces.");

            Write("\n\n\tEnter the path to an image with faces that you wish to analyze: ");
            string imagePath = ReadLine();
            Validators.ImagePath(imagePath);

            _taskList.Add(Task.Factory.StartNew(() =>
            {
                try
                {
                    var result = _faceLogic.DetectFace(imagePath);
                    WriteLine("\t" + result.Message);

                    var detectedFace = (DetectedFaceDto[])result.Data;

                    if (detectedFace != null) WriteLine("\tFace ID: " + detectedFace[0].FaceId);
                }

                catch (Exception exception)
                {
                    WriteLine($"\n\tError Message: { exception.Message }");
                    WriteLine($"\n\tInner Exception: { exception.InnerException }");
                    WriteLine($"\n\tStackTrace: { exception.StackTrace }");
                    ReadLine();
                }
            }));
        }

        private void ExecuteFaceIdentification()
        {
            WriteLine("\n\tYou have selected the option for identifying a face.");

            Write("\n\n\tEnter the face ID of the face that you would like to identify: ");
            string faceId = ReadLine();

            Write("\n\n\tEnter the person group ID associated to the face ID: ");
            string personGroupId = ReadLine();

            _taskList.Add(Task.Factory.StartNew(() =>
            {
                var result = _faceLogic.IdentifyFace(new List<string> { faceId }, personGroupId);

                if (result != null) WriteLine("\t{0}", result);
            }));
        }

        public void Start()
        {
            try
            {
                Initialize();

                do
                {
                    WriteLine("\n\tKindly choose from the following options:\n");

                    foreach (APIOption apiOption in Enum.GetValues(typeof(APIOption)))
                        WriteLine("\t\t[{0}] {1}", (int)apiOption, apiOption.ToString());

                    Write("\n\tOption: ");
                    var option = int.Parse(ReadLine());
                    ExecuteAPIOption(option);

                    WriteLine("\n\tWaiting for the results...");
                    WriteLine("\n\t\t - If you would like to execute other options while waiting please press the 'Enter' key.");
                    WriteLine("\t\t - If you would like to exit the application please press the 'Esc' key.\n");

                    var key = ReadKey();

                    if (key.Key == ConsoleKey.Escape)
                    {
                        _isCompleted = true;
                        Task.WaitAll(_taskList.ToArray());
                    }
                    else
                    {
                        Clear();
                        _isCompleted = false;
                    }
                } while (!_isCompleted);
            }

            catch (Exception exception)
            {
                WriteLine($"\n\tError Message: { exception.Message }");
                WriteLine($"\n\tInner Exception: { exception.InnerException }");
                WriteLine($"\n\tStackTrace: { exception.StackTrace }");
                ReadLine();
            }
        }
    }
}
