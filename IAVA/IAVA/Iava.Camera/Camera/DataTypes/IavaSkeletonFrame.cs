using System.Linq;
using Iava.Core.Math;
using Microsoft.Research.Kinect.Nui;
using Iava.Core.Math;

namespace Iava.Input.Camera {

    public sealed class IavaSkeletonFrame {

        #region Public Properties

        /// <summary>
        /// Gets the fist active skeleton, in any, in the frame
        /// </summary>
        public IavaSkeletonData ActiveSkeleton { get { return GetActiveSkeleton(); } }

        public IavaVector FloorClipPlane { get; set; }

        public int FrameNumber { get; set; }

        public IavaVector NormalToGravity { get; set; }

        public IavaSkeletonFrameQuality Quality { get; set; }

        public IavaSkeletonData[] Skeletons { get; set; }

        public long TimeStamp { get; set; }

        #endregion Public Properties

        #region Private Methods

        private IavaSkeletonData GetActiveSkeleton() {
            // Check the last known active skeleton first...
            //if (Skeletons[_skeletonIndex] != null) { return Skeletons[_skeletonIndex]; }

            // Check all the skeleton slots
            for (int i = 0; i < Skeletons.Count(); i++) {
                if (Skeletons[i] != null) {
                    if (Skeletons[i].Position.W != 0 && Skeletons[i].Position.X != 0 &&
                        Skeletons[i].Position.Y != 0 && Skeletons[i].Position.Z != 0) {
                        _skeletonIndex = i;
                        return Skeletons[i];
                    }
                }
            }

            // If we get here we didn't find anything
            _skeletonIndex = 0;
            return null;
        }

        #endregion Private Methods
        
        #region Private Fields

        private int _skeletonIndex = 0;

        #endregion Private Fields

        #region Operator Overloads

        public static explicit operator IavaSkeletonFrame(SkeletonFrame value) {
            if (value == null) { return null; }

            IavaSkeletonFrame skeletonFrame = new IavaSkeletonFrame()
            {
                FloorClipPlane = (IavaVector)value.FloorClipPlane,
                FrameNumber = value.FrameNumber,
                NormalToGravity = (IavaVector)value.NormalToGravity,
                Quality = (IavaSkeletonFrameQuality)value.Quality,
                Skeletons = new IavaSkeletonData[value.Skeletons.Length],
                TimeStamp = value.TimeStamp
            };

            // Copy and convert the array
            for (int i = 0; i < skeletonFrame.Skeletons.Length; i++) {
                skeletonFrame.Skeletons[i] = (IavaSkeletonData)value.Skeletons[i];
            }

            return skeletonFrame;
        }

        #endregion Operator Overloads
    }
}