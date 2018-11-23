namespace YoloCSharp
{
    /// <summary>
    /// Represents the detection information of an object by Yolo
    /// </summary>
    public class YoloResult
    {
        /// <value>
        /// X coordinate of the top-left corner of bounded box
        /// </value>
        public uint X { get; }

        /// <value>
        /// Y coordinate of the top-left corner of bounded box
        /// </value>
        public uint Y { get; }

        /// <value>
        /// Width of bounded box
        /// </value>
        public uint Width { get; }

        /// <summary>
        /// Height of bounded box
        /// </summary>
        public uint Height { get; }

        /// <value>
        /// Probability that the object was found correctly
        /// </value>
        public float Prob { get; }

        /// <value>
        /// Class of object - from range [0, classes-1]
        /// </value>
        public uint ObjId { get; }

        /// <value>
        /// Tracking id for video (0 - untracked, 1 - inf - tracked object)
        /// </value>
        public uint TrackId { get; }

        /// <value>
        /// Counter of frames on which the object was detected
        /// </value>
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
