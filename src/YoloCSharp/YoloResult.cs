namespace YoloCSharp
{
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

        internal YoloResult(uint x, uint y, uint width, uint height, float prob, uint objId, uint trackId, uint framesCounter)
        {
            X = x;
            Y = y;
            Width = width;
            Height = height;
            Prob = prob;
            ObjId = objId;
            TrackId = trackId;
            FramesCounter = framesCounter;
        }
    }
}
