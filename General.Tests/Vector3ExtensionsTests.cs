using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using com.azi.Image;

namespace General.Tests
{
    [TestClass]
    public class Vector3ExtensionsTests
    {
        [TestMethod]
        public void TestShift()
        {
            var Matrix = new[,]
            {
                {1.87f, -0.81f, -0.06f},
                {-0.16f, 1.55f, -0.39f},
                {0.05f, -0.47f, 1.42f}
            }.Shift(0, 0, +0.05f);

            Assert.AreEqual(1.87f + 0.05f, Matrix[0, 0]);
            Assert.AreEqual(-0.81f - (0.05f / 2), Matrix[0, 1]);
            Assert.AreEqual(-0.06f - (0.05f / 2), Matrix[0, 2]);
        }
    }
}
