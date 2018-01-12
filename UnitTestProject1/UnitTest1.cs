using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using LightsOutGame.helpers;
using LightsOutGame.Models;

namespace UnitTestProject1
{
    [TestClass]
    public class UnitTest1
    {
        [TestMethod]
        public void IsCorrectLoadOnValues()
        {

            int[,] matrix = new int[4, 4];

            // Define a test input and output value:  
            var expectedResult = new int[4, 4];
            //fill the matrix
            expectedResult[0, 0] = 1;
            expectedResult[0, 1] = 0;
            expectedResult[0, 2] = 0;
            expectedResult[0, 3] = 0;
            expectedResult[1, 0] = 0;
            expectedResult[1, 1] = 1;
            expectedResult[1, 2] = 0;
            expectedResult[1, 3] = 0;
            expectedResult[2, 0] = 0;
            expectedResult[2, 1] = 0;
            expectedResult[2, 2] = 1;
            expectedResult[2, 3] = 0;
            expectedResult[3, 0] = 0;
            expectedResult[3, 1] = 0;
            expectedResult[3, 2] = 0;
            expectedResult[3, 3] = 1;
            //
            Level testLevel = new Level();
            testLevel.On = new int[]  {  0, 5, 10, 15};
            testLevel.Columns = 4;
            testLevel.Rows = 4;
            // Run the method under test:  
            OperationsHelper.SetOnValues(testLevel, matrix);
            //Verify the result:  
            CollectionAssert.AreEqual(expectedResult, matrix);
        }

        [TestMethod]
        public void IsAllSwitchesOff()
        {
            // Define a test input and output value:  
            var matrix = new int[4, 4];
            //fill the matrix
            matrix[0, 0] = 0;
            matrix[0, 1] = 0;
            matrix[0, 2] = 0;
            matrix[0, 3] = 0;
            matrix[1, 0] = 0;
            matrix[1, 1] = 0;
            matrix[1, 2] = 0;
            matrix[1, 3] = 0;
            matrix[2, 0] = 0;
            matrix[2, 1] = 0;
            matrix[2, 2] = 0;
            matrix[2, 3] = 0;
            matrix[3, 0] = 0;
            matrix[3, 1] = 0;
            matrix[3, 2] = 0;
            matrix[3, 3] = 0;
            // Run the method under test:  
            OperationsHelper.AllLightsOff(matrix);
            //Verify the result:  
            Assert.AreEqual(true, OperationsHelper.AllLightsOff(matrix));
        }

        [TestMethod]
        public void IsNeighboursStatusOk()
        {

            int[,] matrix = new int[4, 4];

            // Define a test input and output value:  
            var expectedResult = new int[4, 4];
            //fill the matrix
            expectedResult[0, 0] = 1;
            expectedResult[0, 1] = 0;
            expectedResult[0, 2] = 0;
            expectedResult[0, 3] = 0;
            expectedResult[1, 0] = 0;
            expectedResult[1, 1] = 1;
            expectedResult[1, 2] = 0;
            expectedResult[1, 3] = 0;
            expectedResult[2, 0] = 0;
            expectedResult[2, 1] = 0;
            expectedResult[2, 2] = 1;
            expectedResult[2, 3] = 0;
            expectedResult[3, 0] = 0;
            expectedResult[3, 1] = 0;
            expectedResult[3, 2] = 0;
            expectedResult[3, 3] = 1;
            //
            Level testLevel = new Level();
            testLevel.On = new int[] { 0, 5, 10, 15 };
            testLevel.Columns = 4;
            testLevel.Rows = 4;
            // Run the method under test:  
            OperationsHelper.SetVonNeumannNeighborhood(testLevel, new Tuple<int, int>(1,1), matrix);
            //Verify the result:  
            Assert.AreEqual(1, matrix[0, 1]); //upd
            Assert.AreEqual(1, matrix[1, 0]); //left
            Assert.AreEqual(1, matrix[2, 1]); //down
            Assert.AreEqual(1, matrix[1, 2]); //right
        }
    }
}
