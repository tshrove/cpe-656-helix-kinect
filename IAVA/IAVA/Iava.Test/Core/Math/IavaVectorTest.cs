using Iava.Core.Math;
using Microsoft.Kinect;
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
            SkeletonPoint vector = new SkeletonPoint();
            vector.X = 2.8f;
            vector.Y = -99.7f;
            vector.Z = -5;

            // Explicitly cast to the Iava equivalent
            IavaSkeletonPoint iavaVector = (IavaSkeletonPoint)vector;
            Assert.AreEqual(vector.X, iavaVector.X);
            Assert.AreEqual(vector.Y, iavaVector.Y);
            Assert.AreEqual(vector.Z, iavaVector.Z);

            // Test object as a whole
            Assert.AreEqual((IavaSkeletonPoint)vector, iavaVector);
        }

        /// <summary>
        ///A test for Equality Operation
        ///</summary>
        [TestMethod()]
        public void IavaVectorEqualityTest() {
            IavaSkeletonPoint vector1 = new IavaSkeletonPoint() { X = 0.5, Y = 0.0, Z = -2.0 };
            IavaSkeletonPoint vector2 = new IavaSkeletonPoint() { X = 0.5, Y = 0.0, Z = -2.0 };

            // Make sure both vectors are equal
            Assert.IsTrue(vector1 == vector2);
            Assert.AreEqual(vector1, vector2);

            // Make sure both vectors are not equal
            Assert.IsFalse(vector1 == IavaSkeletonPoint.Zero);
            Assert.AreNotEqual(vector1, IavaSkeletonPoint.Zero);
        }

        /// <summary>
        ///A test for Inequality Operation
        ///</summary>
        [TestMethod()]
        public void IavaVectorInequalityTest() {
            IavaSkeletonPoint vector1 = new IavaSkeletonPoint() { X = 0.5, Y = 0.0, Z = -2.0 };
            IavaSkeletonPoint vector2 = new IavaSkeletonPoint() { X = 2.1, Y = 8.9, Z = 0.0 };
            IavaSkeletonPoint vector3 = new IavaSkeletonPoint() { X = 2.1, Y = 8.9, Z = 0.0 };

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
            IavaSkeletonPoint vector1 = new IavaSkeletonPoint() { X = 0.5, Y = 0.0, Z = -2.0 };
            IavaSkeletonPoint vector2 = new IavaSkeletonPoint() { X = -0.5, Y = 0.0, Z = 2.0 };
            IavaSkeletonPoint vector3 = new IavaSkeletonPoint() { X = -1.6, Y = -8.9, Z = -2.0 };
            IavaSkeletonPoint vector4 = new IavaSkeletonPoint() { X = -1.1, Y = -8.9, Z = -4.0 };

            // Make sure that vector1 + vector2 is IavaSkeletonPoint.Zero
            Assert.AreEqual(IavaSkeletonPoint.Zero, vector1 + vector2);

            // Make sure vector1 + vector3 is vector4
            Assert.AreEqual(vector4, vector1 + vector3);
        }

        /// <summary>
        ///A test for Subtraction Operation
        ///</summary>
        [TestMethod()]
        public void IavaVectorSubtractionTest() {
            IavaSkeletonPoint vector1 = new IavaSkeletonPoint() { X = 0.5, Y = 0.0, Z = -2.0 };
            IavaSkeletonPoint vector2 = new IavaSkeletonPoint() { X = 2.1, Y = 8.9, Z = 0.0 };
            IavaSkeletonPoint vector3 = new IavaSkeletonPoint() { X = -1.6, Y = -8.9, Z = -2.0 };

            // Make sure that vector1 - vector2 is IavaSkeletonPoint.Zero
            Assert.AreEqual(IavaSkeletonPoint.Zero, vector1 - vector1);

            // Make sure vector1 - vector2 is vector3
            Assert.AreEqual(vector3, vector1 - vector2);
        }

        /// <summary>
        ///A test for X
        ///</summary>
        [TestMethod()]
        public void IavaVectorXTest() {
            IavaSkeletonPoint vector = new IavaSkeletonPoint();

            vector.X = 2;

            Assert.AreEqual(2, vector.X);
        }

        /// <summary>
        ///A test for Y
        ///</summary>
        [TestMethod()]
        public void IavaVectorYTest() {
            IavaSkeletonPoint vector = new IavaSkeletonPoint();

            vector.Y = -5.987;

            Assert.AreEqual(-5.987, vector.Y);
        }

        /// <summary>
        ///A test for Z
        ///</summary>
        [TestMethod()]
        public void IavaVectorZTest() {
            IavaSkeletonPoint vector = new IavaSkeletonPoint();

            vector.Z = 0.489;

            Assert.AreEqual(0.489, vector.Z);
        }

        /// <summary>
        ///A test for Zero
        ///</summary>
        [TestMethod()]
        public void IavaVectorZeroTest() {
            IavaSkeletonPoint zero =  IavaSkeletonPoint.Zero;

            Assert.AreEqual(0.0, zero.X);
            Assert.AreEqual(0.0, zero.Y);
            Assert.AreEqual(0.0, zero.Z);
        }
    }
}
