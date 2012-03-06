using Iava.Core.Math;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;

namespace Iava.Test.Core.Math
{
    
    
    /// <summary>
    ///This is a test class for GeometryTest and is intended
    ///to contain all GeometryTest Unit Tests
    ///</summary>
    [TestClass()]
    public class GeometryTest {


        private TestContext testContextInstance;

        /// <summary>
        ///Gets or sets the test context which provides
        ///information about and functionality for the current test run.
        ///</summary>
        public TestContext TestContext {
            get {
                return testContextInstance;
            }
            set {
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
        ///A test for Magnitude2D
        ///</summary>
        [TestMethod()]
        public void Magnitude2DTest() {
            IavaVector point1 = new IavaVector(); // TODO: Initialize to an appropriate value
            IavaVector point2 = new IavaVector(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = Geometry.Magnitude2D(point1, point2);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Magnitude3D
        ///</summary>
        [TestMethod()]
        public void Magnitude3DTest() {
            IavaVector point1 = new IavaVector(); // TODO: Initialize to an appropriate value
            IavaVector point2 = new IavaVector(); // TODO: Initialize to an appropriate value
            double expected = 0F; // TODO: Initialize to an appropriate value
            double actual;
            actual = Geometry.Magnitude3D(point1, point2);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }

        /// <summary>
        ///A test for Translate
        ///</summary>
        [TestMethod()]
        public void TranslateTest() {
            IavaVector original = new IavaVector(); // TODO: Initialize to an appropriate value
            IavaVector translation = new IavaVector(); // TODO: Initialize to an appropriate value
            IavaVector expected = new IavaVector(); // TODO: Initialize to an appropriate value
            IavaVector actual;
            actual = Geometry.Translate(original, translation);
            Assert.AreEqual(expected, actual);
            Assert.Inconclusive("Verify the correctness of this test method.");
        }
    }
}
