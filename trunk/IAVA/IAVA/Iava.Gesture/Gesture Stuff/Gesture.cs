//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
////using Microsoft.Kinect;

//using Iava.Input.Camera;

//namespace Iava.Gesture.GestureStuff {
//    public class IavaGestureStuff {

//        #region Public Events

//        public event EventHandler<GestureEventArgs> GestureRecognized;

//        #endregion Public Events

//        #region Public Properties

//        /// <summary>
//        ///  Gets the segments that makes up the gesture object
//        /// </summary>
//        public List<IGestureSegment> GestureParts { get; private set; }

//        /// <summary>
//        /// Gets the name of the Gesture
//        /// </summary>
//        public string Name { get; private set; }

//        #endregion Public Properties

//        #region Public Methods

//        public void CheckForGesture(IavaSkeleton skeleton) {
//            // NEM: Need to describe what this code is doing...
//            if (_paused) {
//                if (_frameCount == _pausedFrameCount) {
//                    _paused = false;
//                }

//                _frameCount++;
//            }

//            // See if we recognize anything
//            switch (GestureParts[_currentGestureSegment].CheckGesture(skeleton)) {
//                case GestureResult.Succeed:
//                    _currentGestureSegment++;

//                    // We have detected a gesture
//                    if (_currentGestureSegment == GestureParts.Count) {
//                        if (GestureRecognized != null) {
//                            GestureRecognized(this, new GestureEventArgs(Name));
//                        }

//                        // Need to reset the gesture state
//                        Reset();
//                    }

//                    // We have only detected part of a gesture, so prepare to 
//                    // look for the next segment
//                    else {
//                        _frameCount = 0;
//                        _pausedFrameCount = 10;
//                        _paused = true;
//                    }

//                    break;
//                case GestureResult.Pause:
//                    // Don't worry about it
//                    break;
//                case GestureResult.Fail: // intentional fall-thru
//                default:
//                    // We've timed out, start over on detecting the gesture
//                    if (_frameCount >= 50) {
//                        _currentGestureSegment = 0;
//                        _frameCount = 0;
//                    }

//                    else { _frameCount++; }

//                    // Change to the paused state
//                    _pausedFrameCount = 5;
//                    _paused = true;

//                    break;
//            }
//        }

//        public void Reset() {
//            _currentGestureSegment = 0;
//            _frameCount = 0;
//            _pausedFrameCount = 5;
//            _paused = true;
//        }

//        #endregion Public Methods

//        #region Constructors

//        public IavaGestureStuff(string name, List<IGestureSegment> gestureParts) {
//            Name = name;
//            GestureParts = gestureParts;

//            Reset();
//        }

//        #endregion Constructors

//        #region Private Methods

//        private void OnGestureRecognized() {
//            if (GestureRecognized != null) {
//                GestureRecognized(this, new GestureEventArgs(Name));
//            }
//        }

//        #endregion Private Methods

//        #region Private Fields

//        /// <summary>
//        /// Indicates if the current gesture is in the paused state
//        /// </summary>
//        private bool _paused = false;

//        /// <summary>
//        /// Specifies how many frames the gesture has been in the paused state
//        /// </summary>
//        private int _pausedFrameCount = 10;

//        /// <summary>
//        /// Specifies the number of frames we have analyzed for this gesture
//        /// </summary>
//        private int _frameCount = 0;

//        /// <summary>
//        /// Specifies the current gesture segment we are trying to detect
//        /// </summary>
//        private int _currentGestureSegment = 0;

//        #endregion Private Fields
//    }
//}
