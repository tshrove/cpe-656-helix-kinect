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

            // Do a field by field comparison
            return (skeletonFrame1.FloorClipPlane.Equals(skeletonFrame2.FloorClipPlane) &&
                    skeletonFrame1.FrameNumber == skeletonFrame2.FrameNumber &&
                    skeletonFrame1.Skeletons.SequenceEqual(skeletonFrame2.Skeletons) &&
                    skeletonFrame1.Timestamp == skeletonFrame2.Timestamp);
        }

        public static bool operator !=(IavaSkeletonFrame skeletonFrame1, IavaSkeletonFrame skeletonFrame2) {
            // If both are null, or are same instance, return false.
            if (Object.ReferenceEquals(skeletonFrame1, skeletonFrame2)) { return false; }

            // If just one is null, return true.
            if (((object)skeletonFrame1 == null) || ((object)skeletonFrame2 == null)) { return true; }

            return (!skeletonFrame1.FloorClipPlane.Equals(skeletonFrame2.FloorClipPlane) ||
                    !skeletonFrame1.FrameNumber.Equals(skeletonFrame2.FrameNumber) ||
                    !skeletonFrame1.Skeletons.SequenceEqual(skeletonFrame2.Skeletons) ||
                    !skeletonFrame1.Timestamp.Equals(skeletonFrame2.Timestamp));
        }

        public override bool Equals(object obj) {
            // If parameter is null return false.
            if (obj == null) { return false; }

            try {
                IavaSkeletonFrame skeletonFrame = (IavaSkeletonFrame)obj;

                // Do a field by field comparison
                return (skeletonFrame.FloorClipPlane.Equals(this.FloorClipPlane) &&
                        skeletonFrame.FrameNumber.Equals(this.FrameNumber) &&
                        skeletonFrame.Skeletons.SequenceEqual(this.Skeletons) &&
                        skeletonFrame.Timestamp.Equals(this.Timestamp));
            }
            // If parameter cannot be cast, return false.
            catch (InvalidCastException) {
                return false;
            }
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
            if (value.SkeletonArrayLength > 0) {
                try {
                    Skeleton[] kinectSkeletons = new Skeleton[value.SkeletonArrayLength];
                    value.CopySkeletonDataTo(kinectSkeletons);

                    // Convert the array
                    for (int i = 0; i < kinectSkeletons.Length; i++) {
                        skeletonFrame.Skeletons[i] = (IavaSkeleton)kinectSkeletons[i];
                    }
                }
                catch (ArgumentNullException) {
                    // Somehow the SkeletonFrame's skeleton array is empty
                    // Since we can't copy it, just return.
                }
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
