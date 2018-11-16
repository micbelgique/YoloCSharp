namespace YoloCSharp
{
    public class NetResult
    {
        //(x,y) - top-left corner of bounded box
        public uint X { get; set; } 
        public uint Y { get; set; }
        //(w, h) - width & height of bounded box
        public uint Width { get; set; } 
        public uint Height { get; set; }
        //Confidence - probability that the object was found correctly
        public float Prob { get; set; }
        //Class of object - from range [0, classes-1]
        public uint ObjId { get; set; }
        //Tracking id for video (0 - untracked, 1 - inf - tracked object)
        public uint TrackId { get; set; }
        //Counter of frames on which the object was detected
        public uint FramesCounter { get; set; }

        public NetResult(uint x, uint y, uint width, uint height, float prob, uint objId, uint trackId, uint framesCounter)
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
