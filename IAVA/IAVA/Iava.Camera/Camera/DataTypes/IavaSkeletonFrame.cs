using System.Linq;
using Iava.Core.Math;
using Microsoft.Kinect;
using System;

namespace Iava.Input.Camera {

    public sealed class IavaSkeletonFrame : IDisposable {

        #region Public Properties

        /// <summary>
        /// Gets the fist active skeleton, in any, in the frame
        /// </summary>
        public IavaSkeleton ActiveSkeleton { get { return GetActiveSkeleton(); } }

        public Tuple<float, float, float, float> FloorClipPlane { get; set; }

        public int FrameNumber { get; set; }

        public IavaSkeleton[] Skeletons { get; set; }

        public long Timestamp { get; set; }

        #endregion Public Properties

        #region Private Methods

        private IavaSkeleton GetActiveSkeleton() {
            // Check all the skeleton slots
            for (int i = 0; i < Skeletons.Count(); i++) {
                if (Skeletons[i] != null) {
                    if (Skeletons[i].Position.X != 0 &&
                        Skeletons[i].Position.Y != 0 &&
                        Skeletons[i].Position.Z != 0) {
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

        public static bool operator ==(IavaSkeletonFrame skeletonFrame1, IavaSkeletonFrame skeletonFrame2) {
            // If both are null, or are same instance, return true.
            if (Object.ReferenceEquals(skeletonFrame1, skeletonFrame2)) { return true; }

            // If just one is null, return false.
            if (((object)skeletonFrame1 == null) || ((object)skeletonFrame2 == null)) { return false; }

            return (skeletonFrame1.ActiveSkeleton == skeletonFrame2.ActiveSkeleton && 
                    skeletonFrame1.FloorClipPlane == skeletonFrame2.FloorClipPlane &&
                    skeletonFrame1.FrameNumber == skeletonFrame2.FrameNumber &&
                    skeletonFrame1.Skeletons == skeletonFrame2.Skeletons &&
                    skeletonFrame1.Timestamp == skeletonFrame2.Timestamp);
        }

        public static bool operator !=(IavaSkeletonFrame skeletonFrame1, IavaSkeletonFrame skeletonFrame2) {
            // If both are null, or are same instance, return false.
            if (Object.ReferenceEquals(skeletonFrame1, skeletonFrame2)) { return false; }

            // If just one is null, return true.
            if (((object)skeletonFrame1 == null) || ((object)skeletonFrame2 == null)) { return true; }

            return (skeletonFrame1.ActiveSkeleton != skeletonFrame2.ActiveSkeleton ||
                    skeletonFrame1.FloorClipPlane != skeletonFrame2.FloorClipPlane ||
                    skeletonFrame1.FrameNumber != skeletonFrame2.FrameNumber ||
                    skeletonFrame1.Skeletons != skeletonFrame2.Skeletons ||
                    skeletonFrame1.Timestamp != skeletonFrame2.Timestamp);
        }

        public override bool Equals(object obj) {
            // If parameter is null return false.
            if (obj == null) { return false; }

            // If parameter cannot be cast, return false.
            IavaSkeletonFrame skeletonFrame = (IavaSkeletonFrame)obj;
            if ((Object)skeletonFrame == null) { return false; }

            // Do a field by field comparison
            return (skeletonFrame.ActiveSkeleton == this.ActiveSkeleton &&
                    skeletonFrame.FloorClipPlane == this.FloorClipPlane &&
                    skeletonFrame.FrameNumber == this.FrameNumber &&
                    skeletonFrame.Skeletons == this.Skeletons &&
                    skeletonFrame.Timestamp == this.Timestamp);
        }

        public static explicit operator IavaSkeletonFrame(SkeletonFrame value) {
            if (value == null) { return null; }

            IavaSkeletonFrame skeletonFrame = new IavaSkeletonFrame()
            {
                FloorClipPlane = value.FloorClipPlane,
                FrameNumber = value.FrameNumber,
                Skeletons = new IavaSkeleton[value.SkeletonArrayLength],
                Timestamp = value.Timestamp
            };

            // Copy the Skeletons
            Skeleton[] kinectSkeletons = new Skeleton[value.SkeletonArrayLength];
            value.CopySkeletonDataTo(kinectSkeletons);

            // Convert the array
            for (int i = 0; i < kinectSkeletons.Length; i++) {
                skeletonFrame.Skeletons[i] = (IavaSkeleton)kinectSkeletons[i];
            }

            return skeletonFrame;
        }

        #endregion Operator Overloads

        #region IDisposable Members

        public void Dispose() {
            throw new NotImplementedException();
        }

        #endregion
    }
}
