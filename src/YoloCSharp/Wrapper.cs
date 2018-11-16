using System;
using System.Runtime.InteropServices;

namespace YoloCSharp
{
    internal static class Wrapper
    {
        internal struct BoundingBox
        {
            public uint X, Y, W, H;
            public float Prob;
            public uint ObjId;
            public uint TrackId;
            public uint FramesCounter;
        };

        [DllImport("YoloApi", EntryPoint = "initDetector", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Init(string cfgFile, string weightFile, int gpuId = 0);
        [DllImport("YoloApi", EntryPoint = "resetDetector", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Close();
        [DllImport("YoloApi", EntryPoint = "detectOpenCV", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe void Detect(IntPtr matPtr, out BoundingBox* elems, out int arraySize, int matRows, int matCols, float thresh = 0.2f, bool useMean = false);
    }
}