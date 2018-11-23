using System;
using System.Runtime.InteropServices;

namespace YoloCSharp
{
    internal static class Wrapper
    {
        private const string DllName = "YoloApi";

        // same as c++ struct bbox_t from yolo_v2_class
        // need to disable check for Field XYZ is never assigned to, and will always have its default value XX
        // because the struct is initialized in the c++ code
        #pragma warning disable 0649
        internal struct BoundingBox
        {
            internal uint X, Y, W, H;
            internal float Prob;
            internal uint ObjId;
            internal uint TrackId;
            internal uint FramesCounter;
        };
        #pragma warning restore 0649

        [DllImport(DllName, EntryPoint = "initDetector", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Init(string cfgFile, string weightFile, int gpuId = 0);
        [DllImport(DllName, EntryPoint = "closeDetector", CallingConvention = CallingConvention.Cdecl)]
        internal static extern void Close();
        [DllImport(DllName, EntryPoint = "detect", CallingConvention = CallingConvention.Cdecl)]
        internal static extern unsafe void Detect(IntPtr matPtr, int matRows, int matCols, float thresh, bool useMean, out BoundingBox* elems, out int elemsSize);
    }
}