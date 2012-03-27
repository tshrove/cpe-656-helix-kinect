using System.Threading;
using Iava.Input.Camera;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Moq;

namespace Iava.Test.Camera {

    /// <summary>
    ///This is a test class for CameraTest and is intended
    ///to contain all CameraTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CameraTest {

        [TestMethod()]
        public void CameraImageFrameReadyTest() {
            // Create a mock runtime
            var mockImageFrame = SetupMockColorImageFrame();
            camera = new IavaCamera(mockImageFrame.Object);

            bool eventFired = false;

            // Subscribe to our Camera ImageFrame Ready Event
            IavaCamera.ImageFrameReady += (param1, param2) => eventFired = true;

            // Raise the IRuntime ColorImageFrameReady Event
            mockImageFrame.Raise(m => m.ColorImageFrameReady += null, IavaColorImageFrameReadyEventArgs.Empty);
            Thread.Sleep(50);

            Assert.IsTrue(eventFired);
        }

        [TestMethod()]
        public void CameraSkeletonReadyTest() {
            // Create a mock runtime
            var mockImageFrame = SetupMockColorImageFrame();
            camera = new IavaCamera(mockImageFrame.Object);

            bool eventFired = false;

            // Subscribe to our Camera SkeletonReady Ready Event
            IavaCamera.SkeletonReady += (param1, param2) => eventFired = true;

            // Raise the IRuntime SkeletonReady Event
            mockImageFrame.Raise(m => m.SkeletonReady += null, IavaSkeletonEventArgs.Empty);
            Thread.Sleep(50);

            Assert.IsTrue(eventFired);
        }

        [TestMethod()]
        public void CameraSkeletonFrameReadyTest() {
            // Create a mock runtime
            var mockImageFrame = SetupMockColorImageFrame();
            camera = new IavaCamera(mockImageFrame.Object);

            bool eventFired = false;

            // Subscribe to our Camera ImageFrame Ready Event
            IavaCamera.SkeletonFrameReady += (param1, param2) => eventFired = true;

            // Raise the IRuntime SkeletonFrameReady Event
            mockImageFrame.Raise(m => m.SkeletonFrameReady += null, IavaSkeletonFrameReadyEventArgs.Empty);
            Thread.Sleep(50);

            Assert.IsTrue(eventFired);
        }

        #region Private Methods And Attributes

        /// <summary>
        /// Recognizer object under test.
        /// </summary>
        private IavaCamera camera;

        /// <summary>
        /// Creates and sets up a mock IRuntime.
        /// </summary>
        /// <returns>Mock IRuntime</returns>
        private Mock<IRuntime> SetupMockColorImageFrame() {
            var mockImageFrame = new Mock<IRuntime>(MockBehavior.Strict);

            // Setup methods that need to be called
            mockImageFrame.Setup(m => m.Initialize());

            return mockImageFrame;
        }

        #endregion Private Methods And Attributes
    }
}
