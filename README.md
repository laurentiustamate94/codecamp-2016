# CodeCamp 2016

Today, 19.11.2016, Laurențiu Stamate was invited to host a presentation at [CodeCamp 2016](http://cluj.codecamp.ro/) in Cluj on [Microsoft Cognitive Services](https://www.microsoft.com/cognitive-services/en-us/apis) (previously known as Project Oxford) and how easy it is to implement an application that uses Computer Vision and Speech APIs in [Universal Windows Platform](https://laurentiu.microsoft.pub.ro/2016/04/03/hello-universal-windows-platform/). This is a deep technical presentation about Cognitive Services and Universal Windows Platform development in C#. If you are new to these concepts or to programming in general, you can check [this repository](https://github.com/microsoft-dx/csharp-fundamentals) full with basic examples of programming in C# or [this repository](https://github.com/microsoft-dx/uwp-fundamentals) full with basic examples in Universal Windows Platform development.

## Microsoft Cognitive Services in details

> Microsoft Cognitive Services let you build apps with powerful algorithms using just a few lines of code. They work across devices and platforms such as iOS, Android, and Windows, keep improving, and are easy to set up.
> 
> [From the official documentation](https://www.microsoft.com/cognitive-services)

[![cognitive-services-stack](https://laurentiu.microsoft.pub.ro/wp-content/uploads/sites/3/2016/11/cognitive-services-stack.png)](https://laurentiu.microsoft.pub.ro/wp-content/uploads/sites/3/2016/11/cognitive-services-stack.png)

Here we see the full stack that Microsoft provides as Cognitive Services. We have **Vision APIs** (analyzes images and gives your relevant information about them), **Speech APIs** (create sounds based on text and text based on sound), **Language APIs** (offer understanding of writing material and intents, **Knowledge APIs** (structure data to better understand client needs and explore more possibilities), **Search APIs** (gives you smart, organized and on topic data based on what you query).

## Let's talk code

We will be using [Computer Vision](https://www.microsoft.com/cognitive-services/en-us/computer-vision-api), [Emotion](https://www.microsoft.com/cognitive-services/en-us/emotion-api) and [Face](https://www.microsoft.com/cognitive-services/en-us/face-api) from **Vision APIs** and [Bing Speech](https://www.microsoft.com/cognitive-services/en-us/speech-api) from **Speech APIs** in this demo. I've forked my own versions of [Vision](https://github.com/laurentiustamate94/Cognitive-Vision-Windows), [Emotion](https://github.com/laurentiustamate94/Cognitive-Emotion-Windows) and [Face](https://github.com/laurentiustamate94/Cognitive-Face-Windows) APIs because I want to include actual code and not use the NuGets provided (that is a personal choice 😎).

The solution is made of two projects - **CodeCamp2016** (in which all the magic happens) and **ProjectOxford** (class library project containing all the SDKs from each API). If you analyze the class library project in depth, you will find out that the SDKs themselves are no more than a WebRequest done in an elderly manner (Event-Based Asynchronous Programming -- if you are interested in this topic, you can check [this blog post](https://laurentiu.microsoft.pub.ro/2016/08/14/from-eap-to-tap-in-csharp/)).

The **CodeCamp2016** project is interfaced in 3 big components: **Storage**, **ImageSource** and **ImageProcessing**. I've done this to make it easy, in the future, to change or extend the functionalities of the application.

The `IStorage` interface is implemented by the `LocalPhotoStorage` which makes use of the [UWP storage APIs](https://msdn.microsoft.com/en-us/windows/uwp/files/quickstart-reading-and-writing-files) and saves the picture from the media device in the Pictures folder of the current user.

The `IImageSource` interface is implemented by the `LocalCameraImageSource` which makes use of the [UWP audio-video](https://msdn.microsoft.com/en-us/windows/uwp/audio-video-camera/index) APIs.

The `IImageProcessing` interface is implemented by the `ProjectOxford` partial class to keep the code separated by each API and to improve consistency. This class contains the actual service clients properties (you guessed it **interfaced 🤓**)which I instantiate in the constructor (I could have used Dependency Injection to remove this step -- if you are interested in this topic you can check <del>this blog post</del>).

The `ProjectOxford.Emotion` is a wrapper over the the `EmotionServiceClient` in which I implemented, using `IStorage` and `IImageSource`, to save the current photo taken and feed it to the Emotion API, after which I get the highest emotion score, display the emotion to the user and feed it to Speech API.

The `ProjectOxford.Face` is a wrapper over the the `FaceServiceClient` in which I implemented, using `IStorage` and `IImageSource`, to save the current photo taken and feed it to the Face API to recognize a face and get facial features, after which I check if the face is familiar by feeding two photos into Face API, display the result to the user and feed it to Speech API.

The `ProjectOxford.Vision` is a wrapper over the the `VisionServiceClient` in which I implemented, using `IStorage` and `IImageSource`, to save the current photo taken and feed it to the Vision API to recognize a text or dominant foreground color (which I use as "get shirt color" 😎), display the result to the user and feed it to Speech API.

The UI is done in XAML in the `MainPage.xaml` file. It basically has five [buttons](https://msdn.microsoft.com/library/windows/apps/Windows.UI.Xaml.Controls.Button), a [textbox](https://msdn.microsoft.com/en-us/library/windows/apps/windows.ui.xaml.controls.textbox.aspx) and a [media element](https://msdn.microsoft.com/en-us/library/windows/apps/mt187272.aspx) in which we load the image source. If you want to learn more about the XAML language or the Universal Windows Platform, you can check out this blog post and this repository. In the `MainPage.xaml.cs` we have the implementation for each buttons which bassically are calls of the methods we defined in our interfaces. We also have the Speech API, implemented using [SpeechSynthesizer](https://msdn.microsoft.com/en-us/library/windows/apps/windows.media.speechsynthesis.speechsynthesizer.aspx) which creates a stream of sound from a string.

[![ui-demo](https://laurentiu.microsoft.pub.ro/wp-content/uploads/sites/3/2016/11/ui-demo.png)](https://laurentiu.microsoft.pub.ro/wp-content/uploads/sites/3/2016/11/ui-demo.png)

For your application to function, you will need API keys provided from the [Cognitive Services portal](https://www.microsoft.com/cognitive-services/en-us/sign-up) for each API you want to use. I have created a resource file called `AppSettings.resw` in which to store all the necesarry API keys and load them into the code without hardcoding anything. I've also setted **Microphone**, **Pictures Library** and **Webcam** capabilities in the `Package.appxmanifest`.

##  In conclusion

As you may have observed, a bit of C# coding knowledge is required. For a better understanding of how powerful the C# language really is, you can check out [this repository](https://github.com/microsoft-dx/csharp-fundamentals/) full with basic C# projects. If you want to go deeply into advanced C# topics, you can check out [this repository](https://github.com/microsoft-dx/advanced-csharp). Stay tuned on this blog (and star the [microsoft-dx organization](https://github.com/microsoft-dx/)) to emerge in the beautiful world of “there’s an app for that”.

Good to have resource: [[Channel9] Give Your Apps a Human Side](https://channel9.msdn.com/Events/Build/2016/B878)

Good to have resource: [[Channel9] Build smarter and more engaging experiences](https://channel9.msdn.com/Events/Build/2016/B855)
