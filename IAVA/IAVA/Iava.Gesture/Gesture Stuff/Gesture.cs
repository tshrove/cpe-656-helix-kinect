using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.Kinect.Nui;

namespace Iava.Gesture.GestureStuff {
    public class Gesture {
        /// <summary>
        /// The parts that make up this gesture
        /// </summary>
        private IGestureSegment[] gestureParts;

        /// <summary>
        /// The current gesture part that we are matching against
        /// </summary>
        private int currentGesturePart = 0;

        /// <summary>
        /// the number of frames to pause for when a pause is initiated
        /// </summary>
        private int pausedFrameCount = 10;

        /// <summary>
        /// The current frame that we are on
        /// </summary>
        private int frameCount = 0;

        /// <summary>
        /// Are we paused?
        /// </summary>
        private bool paused = false;

        /// <summary>
        /// The type of gesture that this is
        /// </summary>
        private GestureType type;

        /// <summary>
        /// Initializes a new instance of the <see cref="Gesture"/> class.
        /// </summary>
        /// <param name="type">The type of gesture.</param>
        /// <param name="gestureParts">The gesture parts.</param>
        public Gesture(GestureType type, IGestureSegment[] gestureParts) {
            this.gestureParts = gestureParts;
            this.type = type;
        }

        /// <summary>
        /// Occurs when [gesture recognised].
        /// </summary>
        public event EventHandler<GestureEventArgs> GestureRecognised;

        /// <summary>
        /// Updates the gesture.
        /// </summary>
        /// <param name="data">The skeleton data.</param>
        public void UpdateGesture(SkeletonData data) {
            if (this.paused) {
                if (this.frameCount == this.pausedFrameCount) {
                    this.paused = false;
                }

                this.frameCount++;
            }

            GestureResult result = this.gestureParts[this.currentGesturePart].CheckGesture(data);
            if (result == GestureResult.Succeed) {
                if (this.currentGesturePart + 1 < this.gestureParts.Length) {
                    this.currentGesturePart++;
                    this.frameCount = 0;
                    this.pausedFrameCount = 10;
                    this.paused = true;
                }
                else {
                    if (this.GestureRecognised != null) {
                        this.GestureRecognised(this, new GestureEventArgs(this.type, data.TrackingID, data.UserIndex));
                        this.Reset();
                    }
                }
            }
            else if (result == GestureResult.Fail || this.frameCount == 50) {
                this.currentGesturePart = 0;
                this.frameCount = 0;
                this.pausedFrameCount = 5;
                this.paused = true;
            }
            else {
                this.frameCount++;
                this.pausedFrameCount = 5;
                this.paused = true;
            }
        }

        /// <summary>
        /// Resets this instance.
        /// </summary>
        public void Reset() {
            this.currentGesturePart = 0;
            this.frameCount = 0;
            this.pausedFrameCount = 5;
            this.paused = true;
        }
    }
}
