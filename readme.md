# YoloCSharp -  Dotnet wrapper  for Yolo

YoloCSharp is a cross platform wrapper of Yolo/Darknet for .Net Standard 2.0.

- Support for Windows/Linux/Docker;
- Compatible with .Net Framework 4.6.1+/.Net Core 2.0;
- GPU Computation with CUDA;
- No CPU Computation available.

## Features

- Easy to use;
- Use Yolo/Darknet library from the repo of [AlexeyAB](https://github.com/AlexeyAB/darknet);
- Use OpenCV 3.3.1 to able to deal with the images or videos (See API);
- Compatible with Yolo v2 and Yolo v3.

## Requirements

1. [CUDA 10.0](https://developer.nvidia.com/cuda-downloads) and [cuDNN 7.4.1](https://developer.nvidia.com/cudnn);
2. **On Linux**, you need to install OpenCV 3.3.1. See your distribution documentation for more information.

## Installation

First, you need to install [OpenCvSharp-AnyCPU](https://github.com/shimat/opencvsharp) in the version **3.3.1.20171117** and then just download the `nuget` from the [official repository](nuget.org) in your project.

**Note:** Due to a [bug](https://github.com/AlexeyAB/darknet/issues/500#issuecomment-375927822) with OpenCV 3.4.1 and later, Yolo is not compatible with them. So these wrapper is only build for the version 3.3.1.

## Examples

All examples are available in the `examples` folder. Don’t hesitate to create new and send them to us.

**Note :** You need to download [yolov3.weights](https://pjreddie.com/media/files/yolov3.weights) and put it in the data folder before test any example. 

## API

**Darknet.cs**

```csharp
/// <summary>
/// Provides a Darknet detector which can be used to analyze image and find all detectable objects inside
/// </summary>
public class Darknet : IDisposable
{
	/// <summary>
	/// Initializes a new Darknet detector with the cfgFile and the weightFile
	/// </summary>
	/// <param name="cfgFile">The *.cfg file</param>
	/// <param name="weightFile">The *.weights file</param>
	/// <param name="gpuId">The index of a CUDA compatible GPU</param>
	public Darknet(string cfgFile, string weightFile, int gpuId = 0);

	/// <summary>
	/// Use to detect objects on an OpenCV Mat object and return all found objects
	/// </summary>
	/// <returns>List of YoloResult</returns>
	/// <param name="mat">The frame to analyze</param>
	/// <param name="thresh">Threshold at which an object should be confirmed</param>
	/// <param name="useMean">Unknown parameter</param>
	public List<YoloResult> Detect(Mat mat, float thresh=0.2f, bool useMean=false);
}
```

**YoloResult.cs**

```csharp
/// <summary>
/// Represents the detection information of an object by Yolo
/// </summary>
public class YoloResult
{
	/// <summary>
	/// X coordinate of the top-left corner of bounded box
	/// </summary>
	public uint X { get; }

	/// <summary>
	/// Y coordinate of the top-left corner of bounded box
	/// </summary>
	public uint Y { get; }

	/// <summary>
	/// Width of bounded box
	/// </summary>
	public uint Width { get; }

	/// <summary>
	/// Height of bounded box
	/// </summary>
	public uint Height { get; }

	/// <summary>
	/// Probability that the object was found correctly
	/// </summary>
	public float Prob { get; }

	/// <summary>
	/// Class of object - from range [0, classes-1]
	/// </summary>
	public uint ObjId { get; }

	/// <summary>
	/// Tracking id for video (0 - untracked, 1 - inf - tracked object)
	/// </summary>
	public uint TrackId { get; }

	/// <summary>
	/// Counter of frames on which the object was detected
	/// </summary>
	public uint FramesCounter { get; }
}
```

