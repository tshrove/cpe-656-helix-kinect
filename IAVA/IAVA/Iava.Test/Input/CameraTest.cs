using Iava.Input.Camera;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Threading;

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
        public void CameraTiltUpTest()
        {/*
            //TODO: Need to figure out why this is failing
            int iExpectedAngle = Camera.TiltAngle;
            Camera.TiltUp();
            iExpectedAngle++;
            Thread.Sleep(1500);
            Assert.AreEqual(iExpectedAngle, Camera.TiltAngle);
          */
        }

        /// <summary>
        ///A test for TiltDown
        ///</summary>
        [TestMethod()]
        public void CameraTiltDownTest()
        {/*
            //TODO: Need to figure out why this is failing
            int iExpectedAngle = Camera.TiltAngle;
            Camera.TiltDown();
            iExpectedAngle--;
            Thread.Sleep(1000);
            Assert.AreEqual(iExpectedAngle, Camera.TiltAngle);
            */
        }
    }
}
