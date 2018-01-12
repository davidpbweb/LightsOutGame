using System;
using System.Linq;
using LightsOutGame.Models;

namespace LightsOutGame.helpers
{
    /// <summary>
    /// Contains the operations 
    /// </summary>
    public static class OperationsHelper
    {


        /// <summary>
        /// Set the On position inside the matrix
        /// </summary>
        /// <param name="currentLevel"></param>
        /// <param name="matrix"></param>
        public static void SetOnValues(Level currentLevel, int[,] matrix)
        {
            for (var i = 0; i < currentLevel.Rows; i++)
            {
                for (var k = 0; k < currentLevel.Columns; k++)
                {
                    var position = currentLevel.Rows * i + k;

                    //if the current position is in the array
                    if (currentLevel.On.Contains(position))
                    {
                        matrix[i, k] = 1;
                    }
                    else
                    {
                        matrix[i, k] = 0;
                    }

                }
            }
        }


        /// <summary>
        /// The von Neumann neighbourhood of a cell is the cell itself and the cells at a Manhattan distance of 1.
        /// </summary>
        /// <param name="currentLevel"></param>
        /// <param name="coordinates"></param>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static void SetVonNeumannNeighborhood(Level currentLevel, Tuple<int,int> coordinates, int[,] matrix)
        {
            int x = coordinates.Item1;
            int y = coordinates.Item2;

            //actual coordinates
            matrix[x, y] = SwitchValue(matrix[x, y]);

            //neighbors check for out of bounds
            if ((y - 1) >= 0)
            {
                matrix[x, y - 1] = SwitchValue(matrix[x, y - 1]);//up
            }

            if ((x - 1 )>= 0)
            {
                matrix[x - 1, y] = SwitchValue(matrix[x - 1, y]); //left
            }

            if ((y + 1) < currentLevel.Columns)
            {
                matrix[x, y + 1] = SwitchValue(matrix[x, y + 1]); //down
            }

            if ((x + 1) < currentLevel.Rows)
            {
                matrix[x + 1, y] = SwitchValue(matrix[x + 1, y]); //right
            }           
        }

        /// <summary>
        /// when 0 return 1 and vice versa
        /// </summary>
        /// <param name="value"></param>
        /// <returns></returns>
        public static int SwitchValue(int value)
        {
            return (value == 0) ? 1 : 0;
        }

        /// <summary>
        /// Returns true if all the values in the matrix are 0.
        /// </summary>
        /// <param name="matrix"></param>
        /// <returns></returns>
        public static bool AllLightsOff(int[,] matrix)
        {
            bool result = false;

            result = matrix.Cast<int>().All(x => x == 0);

            return result;
        }


    }
}
