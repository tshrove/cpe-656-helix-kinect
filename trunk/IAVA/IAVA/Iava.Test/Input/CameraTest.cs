using Iava.Input.Camera;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Iava.Test
{
    
    
    /// <summary>
    ///This is a test class for CameraTest and is intended
    ///to contain all CameraTest Unit Tests
    ///</summary>
    [TestClass()]
    public class CameraTest
    {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext
        {
            get
            {
                return testContextInstance;
            }
            set
            {
                testContextInstance = value;
            }
        }

        #region Additional test attributes
        // 
        //You can use the following additional attributes as you write your tests:
        //
        //Use ClassInitialize to run code before running the first test in the class
        //[ClassInitialize()]
        //public static void MyClassInitialize(TestContext testContext)
        //{
        //}
        //
        //Use ClassCleanup to run code after all tests in a class have run
        //[ClassCleanup()]
        //public static void MyClassCleanup()
        //{
        //}
        //
        //Use TestInitialize to run code before running each test
        //[TestInitialize()]
        //public void MyTestInitialize()
        //{
        //}
        //
        //Use TestCleanup to run code after each test has run
        //[TestCleanup()]
        //public void MyTestCleanup()
        //{
        //}
        //
        #endregion

        /// <summary>
        ///A test for TiltUp
        ///</summary>
        [TestMethod()]
        public void TiltUpTest()
        {
            //TODO: Need to figure out why this is failing
            using (Camera target = new Camera())
            {
                int iExpectedAngle = target.TiltAngle;
                target.TiltUp();
                iExpectedAngle += 1;
                Assert.AreEqual(iExpectedAngle, target.TiltAngle);
            }
        }

        /// <summary>
        ///A test for TiltDown
        ///</summary>
        [TestMethod()]
        public void TiltDownTest()
        {
            //TODO: Need to figure out why this is failing
            using (Camera target = new Camera())
            {
                int iExpectedAngle = target.TiltAngle;
                target.TiltDown();
                iExpectedAngle -= 1;
                Assert.AreEqual(iExpectedAngle, target.TiltAngle);
            }
        }
    }
}
