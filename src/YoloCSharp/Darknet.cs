using System;
using System.Collections.Generic;
using OpenCvSharp;

namespace YoloCSharp
{
    /// <summary>
    /// Represents a Darknet detector which can be used to analyze multiple image sources
    /// </summary>
    public class Darknet : IDisposable
    {
        /// <summary>
        /// Creates a new detector instance and loading the data into it
        /// </summary>
        /// <param name="cfgFile">The *.cfg file</param>
        /// <param name="weightFile">The *.weights file</param>
        /// <param name="gpuId">The index of the CUDA compatible GPU</param>
        public Darknet(string cfgFile, string weightFile, int gpuId = 0)
        {
            Wrapper.Init(cfgFile, weightFile, gpuId);
        }

        /// <summary>
        /// Used to detect classes on a opencv Mat object, as a list of NetResult
        /// </summary>
        /// <param name="mat">The frame to analyze</param>
        /// <param name="thresh">Threshold at which a class should be confirmed</param>
        /// <param name="useMean">Unknonwn parameter</param>
        /// <returns></returns>
        public unsafe List<YoloResult> Detect(Mat mat, float thresh = 0.2f, bool useMean = false)
        {
            Wrapper.Detect(mat.Data, mat.Rows, mat.Cols, thresh, useMean, out var elems, out var elemsSize);

            var results = new List<YoloResult>(elemsSize);
            for(var i = 0; i < elemsSize; i++)
            {
                results.Add(new YoloResult(elems[i].X, elems[i].Y, elems[i].W, elems[i].H, elems[i].Prob, elems[i].ObjId, elems[i].TrackId, elems[i].FramesCounter));
            }

            return results;
        }

        public void Dispose()
        {
            Wrapper.Close();
        }
    }
}
