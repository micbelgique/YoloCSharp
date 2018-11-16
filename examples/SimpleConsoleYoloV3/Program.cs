using OpenCvSharp;
using System;
using System.Collections.Generic;
using System.IO;
using YoloCSharp;

namespace YoloCSharpTest
{
    class Program
    {
        static void Main(string[] args)
        {
            // Initialise darknet with official yolov3 model
            Darknet darknet = new Darknet("data/yolov3.cfg", "data/yolov3.weights");

            // Load all classnames to make the relationship between yolov3 model id and real names of objects
            var classNames = File.ReadAllLines("data/coco.names");

            // Open dog.jpg and convert it to an OpenCV Mat Object
            Mat image = new Mat("data/dog.jpg");

            // Try to detect all objects that model know with a thresh of 30%
            List<NetResult> results = darknet.Detect(image, 0.3f);

            // Print all results found by yolo
            foreach (var result in results)
            {
                Console.WriteLine($"{classNames[result.ObjId]} - {result.Prob * 100}%");
            }

            Console.WriteLine("Press any key to quit the example :");
            Console.ReadLine();
        }
    }
}
