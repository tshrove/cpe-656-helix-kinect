using Iava.Core.Math;
using Microsoft.Research.Kinect.Nui;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iava.Test.Core.Math {

    /// <summary>
    ///This is a test class for IavaVectorTest and is intended
    ///to contain all IavaVectorTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IavaVectorTest {

        /// <summary>
        ///A test for op_Explicit
        ///</summary>
        [TestMethod()]
        public void IavaVectorExplicitTest() {
            // Init the Kinect object
            Vector vector = new Vector();
            vector.W = 1;
            vector.X = 2.8f;
            vector.Y = -99.7f;
            vector.Z = -5;

            // Explicitly cast to the Iava equivalent
            IavaVector iavaVector = (IavaVector)vector;
            Assert.AreEqual(vector.W, iavaVector.W);
            Assert.AreEqual(vector.X, iavaVector.X);
            Assert.AreEqual(vector.Y, iavaVector.Y);
            Assert.AreEqual(vector.Z, iavaVector.Z);

            // Test object as a whole
            Assert.AreEqual((IavaVector)vector, iavaVector);
        }

        /// <summary>
        ///A test for Equality Operation
        ///</summary>
        [TestMethod()]
        public void IavaVectorEqualityTest() {
            IavaVector vector1 = new IavaVector() { W = 1.0, X = 0.5, Y = 0.0, Z = -2.0 };
            IavaVector vector2 = new IavaVector() { W = 1.0, X = 0.5, Y = 0.0, Z = -2.0 };

            // Make sure both vectors are equal
            Assert.IsTrue(vector1 == vector2);
            Assert.AreEqual(vector1, vector2);

            // Make sure both vectors are not equal
            Assert.IsFalse(vector1 == IavaVector.Zero);
            Assert.AreNotEqual(vector1, IavaVector.Zero);
        }

        /// <summary>
        ///A test for Inequality Operation
        ///</summary>
        [TestMethod()]
        public void IavaVectorInequalityTest() {
            IavaVector vector1 = new IavaVector() { W = 1.0, X = 0.5, Y = 0.0, Z = -2.0 };
            IavaVector vector2 = new IavaVector() { W = 4.0, X = 2.1, Y = 8.9, Z = 0.0 };
            IavaVector vector3 = new IavaVector() { W = 4.0, X = 2.1, Y = 8.9, Z = 0.0 };

            // Make sure both vectors are not equal
            Assert.IsTrue(vector1 != vector2);
            Assert.AreNotEqual(vector1, vector2);

            // Make sure both vectors are equal
            Assert.IsFalse(vector2 != vector3);
        }

        /// <summary>
        ///A test for Addition Operation
        ///</summary>
        [TestMethod()]
        public void IavaVectorAddtionTest() {
            IavaVector vector1 = new IavaVector() { W = 1.0, X = 0.5, Y = 0.0, Z = -2.0 };
            IavaVector vector2 = new IavaVector() { W = -1.0, X = -0.5, Y = 0.0, Z = 2.0 };
            IavaVector vector3 = new IavaVector() { W = -3.0, X = -1.6, Y = -8.9, Z = -2.0 };
            IavaVector vector4 = new IavaVector() { W = -2.0, X = -1.1, Y = -8.9, Z = -4.0 };

            // Make sure that vector1 + vector2 is IavaVector.Zero
            Assert.AreEqual(IavaVector.Zero, vector1 + vector2);

            // Make sure vector1 + vector3 is vector4
            Assert.AreEqual(vector4, vector1 + vector3);
        }

        /// <summary>
        ///A test for Subtraction Operation
        ///</summary>
        [TestMethod()]
        public void IavaVectorSubtractionTest() {
            IavaVector vector1 = new IavaVector() { W = 1.0, X = 0.5, Y = 0.0, Z = -2.0 };
            IavaVector vector2 = new IavaVector() { W = 4.0, X = 2.1, Y = 8.9, Z = 0.0 };
            IavaVector vector3 = new IavaVector() { W = -3.0, X = -1.6, Y = -8.9, Z = -2.0 };

            // Make sure that vector1 - vector2 is IavaVector.Zero
            Assert.AreEqual(IavaVector.Zero, vector1 - vector1);

            // Make sure vector1 - vector2 is vector3
            Assert.AreEqual(vector3, vector1 - vector2);
        }

        /// <summary>
        ///A test for W
        ///</summary>
        [TestMethod()]
        public void IavaVectorWTest() {
            IavaVector vector = new IavaVector();

            vector.W = 1.875;

            Assert.AreEqual(1.875, vector.W);
        }

        /// <summary>
        ///A test for X
        ///</summary>
        [TestMethod()]
        public void IavaVectorXTest() {
            IavaVector vector = new IavaVector();

            vector.X = 2;

            Assert.AreEqual(2, vector.X);
        }

        /// <summary>
        ///A test for Y
        ///</summary>
        [TestMethod()]
        public void IavaVectorYTest() {
            IavaVector vector = new IavaVector();

            vector.Y = -5.987;

            Assert.AreEqual(-5.987, vector.Y);
        }

        /// <summary>
        ///A test for Z
        ///</summary>
        [TestMethod()]
        public void IavaVectorZTest() {
            IavaVector vector = new IavaVector();

            vector.Z = 0.489;

            Assert.AreEqual(0.489, vector.Z);
        }

        /// <summary>
        ///A test for Zero
        ///</summary>
        [TestMethod()]
        public void IavaVectorZeroTest() {
            IavaVector zero =  IavaVector.Zero;

            Assert.AreEqual(0.0, zero.W);
            Assert.AreEqual(0.0, zero.X);
            Assert.AreEqual(0.0, zero.Y);
            Assert.AreEqual(0.0, zero.Z);
        }
    }
}
