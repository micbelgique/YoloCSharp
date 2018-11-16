using System.Collections.Generic;
using OpenCvSharp;

namespace YoloCSharp
{
    /// <summary>
    /// Represents a Darknet detector which can be used to analyze multiple image sources
    /// </summary>
    public class Darknet
    {
        private readonly string _cfgPath;
        private readonly string _weightPath;
        private readonly int _gpuId;

        /// <summary>
        /// Creates a new detector instance and loading the data into it
        /// </summary>
        /// <param name="cfgPath">The *.cfg file</param>
        /// <param name="weightPath">The *.weights file</param>
        /// <param name="gpuId">The index of the CUDA compatible GPU</param>
        public Darknet(string cfgPath, string weightPath, int gpuId = 0)
        {
            _cfgPath = cfgPath;
            _weightPath = weightPath;
            _gpuId = gpuId;

            Setup();
        }

        /// <summary>
        /// Used to initialize the Darknet DNN
        /// </summary>
        private void Setup()
        {
            Wrapper.Init(_cfgPath, _weightPath, _gpuId);
        }

        /// <summary>
        /// Used to detect classes on a opencv Mat object, as a list of NetResult
        /// </summary>
        /// <param name="mat">The frame to analyze</param>
        /// <param name="thresh">Threshold at which a class should be confirmed</param>
        /// <param name="useMean">Unknonwn parameter</param>
        /// <returns></returns>
        public unsafe List<NetResult> Detect(Mat mat, float thresh = 0.2f, bool useMean = false)
        {
            Wrapper.Detect(mat.Data, out var elems, out var resultSize, mat.Rows, mat.Cols, thresh, useMean);

            var results = new List<NetResult>(resultSize);
            for(var i = 0; i < resultSize; i++)
            {
                results.Add(new NetResult(elems[i].X, elems[i].Y, elems[i].W, elems[i].H, elems[i].Prob, elems[i].ObjId, elems[i].TrackId, elems[i].FramesCounter));
            }

            return results;
        }

        /// <summary>
        /// Closes the Darknet DNN
        /// </summary>
        public void Close()
        {
            Wrapper.Close();
        }
    }
}
