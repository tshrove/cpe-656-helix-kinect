using System;
using Iava.Core.Math;
using Microsoft.Kinect;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace Iava.Test.Core.Math {

    /// <summary>
    ///This is a test class for IavaSkeletonPointTest and is intended
    ///to contain all IavaSkeletonPointTest Unit Tests
    ///</summary>
    [TestClass()]
    public class IavaSkeletonPointTest {

        /// <summary>
        ///A test for Explicit Cast
        ///</summary>
        [TestMethod()]
        public void IavaSkeletonPointExplicitCastTest() {
            // Init the Kinect object
            SkeletonPoint vector = new SkeletonPoint();
            vector.X = 2.8f;
            vector.Y = -99.7f;
            vector.Z = -5;

            // Create the Iava equivalent
            IavaSkeletonPoint iavaVector = new IavaSkeletonPoint();
            iavaVector.X = 2.8f;
            iavaVector.Y = -99.7f;
            iavaVector.Z = -5;

            // Test object as a whole
            Assert.AreEqual(iavaVector, (IavaSkeletonPoint)vector);
        }

        /// <summary>
        ///A test for Equals
        ///</summary>
        [TestMethod()]
        public void IavaSkeletonPointEqualsTest() {
            try {
                IavaSkeletonPoint point1 = new IavaSkeletonPoint() { X = 0.5, Y = 0.0, Z = -2.0 };
                IavaSkeletonPoint point2 = new IavaSkeletonPoint() { X = 0.5, Y = 0.0, Z = -2.0 };
                IavaSkeletonPoint point3 = new IavaSkeletonPoint() { X = 1.5, Y = 2.7, Z = -3.0 };

                // Make sure collection1 does not equal null
                Assert.IsFalse(point1.Equals(null));

                // Make sure collection1 does not equal a completly different object
                Assert.IsFalse(point1.Equals("Not a point."));

                // Make sure collection1 and collection3 are not equal
                Assert.IsFalse(point1.Equals(point3));

                // Make sure collection1 and collection2 are equal
                Assert.IsTrue(point1.Equals(point2));

                // Make sure collection1 equals itself
                Assert.IsTrue(point1.Equals(point1));
            }
            catch (Exception ex) {
                Assert.Fail(ex.Message);
            }
        }

        /// <summary>
        ///A test for Equality Operation
        ///</summary>
        [TestMethod()]
        public void IavaSkeletonPointEqualityTest() {
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
        public void IavaSkeletonPointInequalityTest() {
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
        public void IavaSkeletonPointAddtionTest() {
            IavaSkeletonPoint vector1 = new IavaSkeletonPoint() { X = 0.5, Y = 0.0, Z = -2.0 };
            IavaSkeletonPoint vector2 = new IavaSkeletonPoint() { X = -0.5, Y = 0.0, Z = 2.0 };
            IavaSkeletonPoint vector3 = new IavaSkeletonPoint() { X = -1.6, Y = -8.9, Z = -2.0 };
            IavaSkeletonPoint vector4 = new IavaSkeletonPoint() { X = -1.1, Y = -8.9, Z = -4.0 };

            // Make sure that point1 + point2 is IavaSkeletonPoint.Zero
            Assert.AreEqual(IavaSkeletonPoint.Zero, vector1 + vector2);

            // Make sure point1 + vector3 is vector4
            Assert.AreEqual(vector4, vector1 + vector3);
        }

        /// <summary>
        ///A test for Subtraction Operation
        ///</summary>
        [TestMethod()]
        public void IavaSkeletonPointSubtractionTest() {
            IavaSkeletonPoint vector1 = new IavaSkeletonPoint() { X = 0.5, Y = 0.0, Z = -2.0 };
            IavaSkeletonPoint vector2 = new IavaSkeletonPoint() { X = 2.1, Y = 8.9, Z = 0.0 };
            IavaSkeletonPoint vector3 = new IavaSkeletonPoint() { X = -1.6, Y = -8.9, Z = -2.0 };

            // Make sure that point1 - point2 is IavaSkeletonPoint.Zero
            Assert.AreEqual(IavaSkeletonPoint.Zero, vector1 - vector1);

            // Make sure point1 - point2 is vector3
            Assert.AreEqual(vector3, vector1 - vector2);
        }

        /// <summary>
        ///A test for X
        ///</summary>
        [TestMethod()]
        public void IavaSkeletonPointXTest() {
            IavaSkeletonPoint vector = new IavaSkeletonPoint();

            // Set the X coordinate
            vector.X = 2;

            // Make sure the property set correctly
            Assert.AreEqual(2, vector.X);
        }

        /// <summary>
        ///A test for Y
        ///</summary>
        [TestMethod()]
        public void IavaSkeletonPointYTest() {
            IavaSkeletonPoint vector = new IavaSkeletonPoint();

            // Set the Y coordinate
            vector.Y = -5.987;

            // Make sure the property set correctly
            Assert.AreEqual(-5.987, vector.Y);
        }

        /// <summary>
        ///A test for Z
        ///</summary>
        [TestMethod()]
        public void IavaSkeletonPointZTest() {
            IavaSkeletonPoint vector = new IavaSkeletonPoint();

            // Set the Z coordinate
            vector.Z = 0.489;

            // Make sure the property set correctly
            Assert.AreEqual(0.489, vector.Z);
        }

        /// <summary>
        ///A test for Zero
        ///</summary>
        [TestMethod()]
        public void IavaSkeletonPointZeroTest() {
            IavaSkeletonPoint zero =  IavaSkeletonPoint.Zero;

            // Make sure the coordinates are 0
            Assert.AreEqual(0.0, zero.X);
            Assert.AreEqual(0.0, zero.Y);
            Assert.AreEqual(0.0, zero.Z);
        }
    }
}
