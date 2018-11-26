using System;
using System.IO;
using OpenCvSharp;
using YoloCSharp;

namespace SimpleConsoleYoloV3
{
    public class Program
    {
        public static void Main(string[] args)
        {
            // Initialise darknet with official yolov3 model
            using (var darknet = new Darknet("data/yolov3.cfg", "data/yolov3.weights"))
            {
                // Load all classnames to make the relationship between yolov3 model id and real names of objects
                var classNames = File.ReadAllLines("data/coco.names");

                // Open dog.jpg and convert it to an OpenCV Mat Object
                var image = new Mat("data/dog.jpg");

                // Try to detect all objects that model know with a thresh of 30%
                var results = darknet.Detect(image, 0.3f);

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
}