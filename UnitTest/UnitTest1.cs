using System;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTest
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void TestMethod1()
        {
            var list = new List<int>() { 9, 8, 7 };
            Assert.AreEqual(9, 9);

        }


        public static int Largest(List<int> list)
        {
            int maxNum = Int32.MaxValue;
            foreach (var num in list)
            {
                if (num > maxNum) maxNum = num;
            }
            return maxNum;
        }
    }
}
