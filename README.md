[![Build Status](https://dev.azure.com/joshmonreal/face-api/_apis/build/status/Continuous%20Integration%20-%20master?branchName=master)](https://dev.azure.com/joshmonreal/face-api/_build/latest?definitionId=6&branchName=master)

# Face API
This project is a proof-of-concept console application that uses Microsoft Cognitive Services - particularly **Face API**. The following are the frameworks/technologies used in creating this.

- .NET Framework 4.6.1
- .NET Framework 4.6.1
- Unity 5.8.11
- NUnit 3.11.0
- Moq 4.10.0
- Newtonsoft.Json 11.0.2
- Microsoft Azure KeyVault 3.0.3
- Microsoft Azure Services App Authentication 1.0.3

## Getting Started
### Prerequisites
- In order to view the source code you should install **Visual Studio** on your computer. You may refer to this [**LINK**](https://visualstudio.microsoft.com/) to download the software. Afterwards, please ensure that you have .NET Framework **4.6.1** or later installed on your machine.

- You must also have an **API key** in order to use the RESTful APIs of Azure Cognitive Services. To purchase one you may refer to this [**LINK**](https://azure.microsoft.com/en-us/services/cognitive-services/face/). Make sure you already have an Azure account prior to your purchase.

- This application currently does not contain methods for getting, updating, and deleting items (i.e. person-group, person, and face). These have been created for the meantime in [**Postman**](https://www.getpostman.com/) which can be accessed by clicking on the button below. You may need to install Postman on your computer to use the APIs.

&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;&nbsp;[![Run in Postman](https://run.pstmn.io/button.svg)](https://app.getpostman.com/run-collection/4529d7f3b3879c775c27)

- Once you have the subscription key, put it inside the `App.config` file

``` csharp
<?xml version="1.0" encoding="utf-8"?>
<configuration>
  <appSettings>
    <add key="SubscriptionKey" value="ENTER_KEY_HERE" />
  </appSettings>
</configuration>
```

## Running the Tests
In order to run the unit tests you may use the Test Explorer of Visual Studio. However, for the integration tests you will not be able to run them. Only the build pipeline of the master branch will be able to do so.

- In Visual Studio click **Test**
- Click **Windows**
- Click **Test Explorer**
