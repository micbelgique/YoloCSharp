using System;
using System.Collections.Generic;
using OpenCvSharp;

namespace YoloCSharp
{
    /// <inheritdoc />
    /// <summary>
    /// Provides a Darknet detector which can be used to analyze image and find all detectable objects inside
    /// </summary>
    public class Darknet : IDisposable
    {
        /// <summary>
        /// Initializes a new Darknet detector with the <paramref name="cfgFile"/> and the <paramref name="weightFile"/>
        /// </summary>
        /// <param name="cfgFile">The *.cfg file</param>
        /// <param name="weightFile">The *.weights file</param>
        /// <param name="gpuId">The index of a CUDA compatible GPU</param>
        public Darknet(string cfgFile, string weightFile, int gpuId = 0)
        {
            Wrapper.Init(cfgFile, weightFile, gpuId);
        }

        /// <summary>
        /// Use to detect objects on an OpenCV Mat object and return all found objects
        /// </summary>
        /// <returns>List of <see cref="YoloResult">YoloResult</see></returns>
        /// <param name="mat">The frame to analyze</param>
        /// <param name="thresh">Threshold at which an object should be confirmed</param>
        /// <param name="useMean">Unknown parameter</param>
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

        /// <inheritdoc />
        /// <summary>
        /// Dispose method to close the darknet process 
        /// </summary>
        public void Dispose()
        {
            Wrapper.Close();
        }
    }
}
