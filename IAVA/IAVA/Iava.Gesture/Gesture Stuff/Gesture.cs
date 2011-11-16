using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Microsoft.Research.Kinect.Nui;

namespace Iava.Gesture.GestureStuff {
    public class Gesture {/*
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
        }*/

        #region Public Events

        public event EventHandler<GestureEventArgs> GestureRecognized;

        #endregion Public Events

        #region Public Properties

        /// <summary>
        ///  Gets the segments that makes up the gesture object
        /// </summary>
        public List<IGestureSegment> GestureParts { get; private set; }

        /// <summary>
        /// Gets the name of the Gesture
        /// </summary>
        public string Name { get; private set; }

        #endregion Public Properties

        #region Public Methods

        public void CheckForGesture(SkeletonData skeleton) {
            // NEM: Need to describe what this code is doing...
            if (_paused) {
                if (_frameCount == _pausedFrameCount) {
                    _paused = false;
                }

                _frameCount++;
            }

            // See if we recognize anything
            switch (GestureParts[_currentGestureSegment].CheckGesture(skeleton)) {
                case GestureResult.Succeed:
                    _currentGestureSegment++;

                    // We have detected a gesture
                    if (_currentGestureSegment == GestureParts.Count) {
                        if (GestureRecognized != null) {
                            GestureRecognized(this, new GestureEventArgs(Name));
                        }

                        // Need to reset the gesture state
                        Reset();
                    }

                    // We have only detected part of a gesture, so prepare to 
                    // look for the next segment
                    else {
                        _frameCount = 0;
                        _pausedFrameCount = 10;
                        _paused = true;
                    }

                    break;
                case GestureResult.Pause:
                    // Don't worry about it
                    break;
                case GestureResult.Fail: // intentional fall-thru
                default:
                    // We've timed out, start over on detecting the gesture
                    if (_frameCount >= 50) {
                        _currentGestureSegment = 0;
                        _frameCount = 0;
                    }

                    else { _frameCount++; }

                    // Change to the paused state
                    _pausedFrameCount = 5;
                    _paused = true;

                    break;
            }
        }

        public void Reset() {
            _currentGestureSegment = 0;
            _frameCount = 0;
            _pausedFrameCount = 5;
            _paused = true;
        }

        #endregion Public Methods

        #region Constructors

        public Gesture(string name, List<IGestureSegment> gestureParts) {
            Name = name;
            GestureParts = gestureParts;

            Reset();
        }

        #endregion Constructors

        #region Private Methods

        private void OnGestureRecognized() {
            if (GestureRecognized != null) {
                GestureRecognized(this, new GestureEventArgs(Name));
            }
        }

        #endregion Private Methods

        #region Private Fields

        /// <summary>
        /// Indicates if the current gesture is in the paused state
        /// </summary>
        private bool _paused = false;

        /// <summary>
        /// Specifies how many frames the gesture has been in the paused state
        /// </summary>
        private int _pausedFrameCount = 10;

        /// <summary>
        /// Specifies the number of frames we have analyzed for this gesture
        /// </summary>
        private int _frameCount = 0;

        /// <summary>
        /// Specifies the current gesture segment we are trying to detect
        /// </summary>
        private int _currentGestureSegment = 0;

        #endregion Private Fields
    }
}
