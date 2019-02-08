[![Build Status](https://dev.azure.com/joshmonreal/face-api/_apis/build/status/Continuous%20Integration)](https://dev.azure.com/joshmonreal/face-api/_build/latest?definitionId=6)

# Face API
This project is a proof-of-concept console application that uses Microsoft Cognitive Services - particularly **Face API**.

- .NET Framework 4.6.1 - Console application
- .NET Framework 4.6.1 - Class libraries
- Unity 5.8.11 - Dependency injection framework
- NUnit 3.11.0 - Unit testing framework
- Moq 4.10.0 - Mocking framework
- Newtonsoft.Json 11.0.2 - JSON serializer and deserializer

### Prerequisites
- In order to view the source code you should install **Visual Studio** on your computer. You may refer to this [**LINK**](https://visualstudio.microsoft.com/) to download the software. Afterwards, please ensure that you have .NET Framework 4.6.1 or later installed on your machine.

- You must also have an **API key** in order to use the RESTful APIs of Azure Cognitive Services. To purchase one you may refer to this [**LINK**](https://azure.microsoft.com/en-us/services/cognitive-services/face/). Make sure you already have an Azure account prior to your purchase.

- This application currently does not contain methods for getting, updating, and deleting items (i.e. person-group, person, and face). These have been created for the meantime in [**Postman**](https://www.getpostman.com/) which can be accessed by clicking on the button below. You may need to install Postman on your computer to use the APIs. Note that you need an API key as well.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[![Run in Postman](https://run.pstmn.io/button.svg)](https://www.getpostman.com/collections/b4c308aee8bad1de1af5)
