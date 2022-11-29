using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Sudoku
{
    class Program
    {
        static int[,] sudoku = new int[9, 9];
        static List<string[]> steps = new List<string[]>();
        static string file;
        static int row;
        static int column;
        static void Main(string[] args)
        {
            ElsoFeladat();
            HarmadikFeladat();
            NegyedikFeladat();
            OtodikFeladat();

            Console.ReadKey();
        }

        private static void OtodikFeladat()
        {
            Console.WriteLine("\nExercise 5.");

            foreach (string[] item in steps)
            {
                int number = Convert.ToInt32(item[0]);
                int row = Convert.ToInt32(item[1]);
                int column = Convert.ToInt32(item[2]);
                Console.WriteLine("The selected row: " + row + ", column: " + column + ", number: " + number);
                Console.WriteLine(StepCheck(number, row, column) + "\n");
            }
        }

        private static string StepCheck(int number, int row, int column)
        {
            row--;
            column--;
            if (sudoku[Convert.ToInt32(row), Convert.ToInt32(column)] != 0)
            {
                return "The position is already filled.";
            }
            else if (RowCheck(Convert.ToInt32(row), Convert.ToInt32(number)))
            {
                return "The given row already contains the number.";
            }
            else if (ColumnCheck(Convert.ToInt32(column), Convert.ToInt32(number)))
            {
                return "The given column already contains the number.";
            }
            else if (RegionCheck(Convert.ToInt32(row), Convert.ToInt32(column), Convert.ToInt32(number)))
            {
                return "The given region already contains the number.";
            }
            else
            {
                return "The step is valid";
            }
        }

        private static bool RegionCheck(int row, int column, int number)
        {
            return GetNumbersInRegion(row, column).Contains(number);
        }

        private static List<int> GetNumbersInRegion(int row, int column)
        {
            List<int> numbers = new List<int>();
            int region = GetRegion(row, column);
            for (int sor = 0; sor < 9; sor++)
            {
                for (int oszlop = 0; oszlop < 9; oszlop++)
                {
                    if (GetRegion(sor, oszlop) == region)
                    {
                        numbers.Add(sudoku[sor, oszlop]);
                    }
                }
            }
            return numbers;
        }

        private static bool ColumnCheck(int column, int number)
        {
            for (int sor = 0; sor < 9; sor++)
            {
                if (sudoku[sor, column] == number)
                {
                    return true;
                }
            }

            return false;
        }

        private static bool RowCheck(int row, int number)
        {
            for (int oszlop = 0; oszlop < 9; oszlop++)
            {
                if (sudoku[row, oszlop] == number)
                {
                    return true;
                }
            }

            return false;
        }

        private static void NegyedikFeladat()
        {
            Console.WriteLine("\nExercise 4.");
            Console.WriteLine("The ratio of blank positions is " + Math.Round((Percentage() / sudoku.Length) * 100, 1) + "%");
        }

        private static double Percentage()
        {
            double count = 0;
            for (int sor = 0; sor < 9; sor++)
            {
                for (int oszlop = 0; oszlop < 9; oszlop++)
                {
                    if (sudoku[sor, oszlop] == 0)
                    {
                        count++;
                    }
                }
            }

            return count;
        }

        private static void HarmadikFeladat()
        {
            Console.WriteLine("\nExercise 3.");
            Console.WriteLine(sudoku[row, column] == 0 ? "The given position is not filled yet." : "The number in the given position is " + sudoku[row, column]);
            Console.WriteLine("The position belongs to region " + GetRegion(row, column));
        }

        private static int GetRegion(int row, int column)
        {
            if (row > 5)
            {
                if (column > 5)
                {
                    return 9;
                }
                else if (column > 2)
                {
                    return 8;
                }
                else
                {
                    return 7;
                }
            }
            else if (row > 2)
            {
                if (column > 5)
                {
                    return 6;
                }
                else if (column > 2)
                {
                    return 5;
                }
                else
                {
                    return 4;
                }
            }
            else
            {
                if (column > 5)
                {
                    return 3;
                }
                else if (column > 2)
                {
                    return 2;
                }
                else
                {
                    return 1;
                }
            }
        }

        private static void ElsoFeladat()
        {
            Console.WriteLine("Exercise 1.");
            Console.Write("Enter the name of the input file: ");
            file = Console.ReadLine();
            Console.Write("Enter a row identifier: ");
            row = Convert.ToInt32(Console.ReadLine()) - 1;
            Console.Write("Enter a column identifier: ");
            column = Convert.ToInt32(Console.ReadLine()) - 1;
            FajlBeolvasas(file);
        }

        private static void Kiiras()
        {
            for (int sor = 0; sor < 9; sor++)
            {
                for (int oszlop = 0; oszlop < 9; oszlop++)
                {
                    Console.Write(sudoku[sor, oszlop] + " ");
                }
                Console.WriteLine();
            }
        }

        private static void FajlBeolvasas(string file)
        {
            using (StreamReader sr = new StreamReader(file))
            {
                for (int sor = 0; sor < 9; sor++)
                {
                    string[] srLine = sr.ReadLine().Split(' ');
                    for (int oszlop = 0; oszlop < 9; oszlop++)
                    {
                        sudoku[sor, oszlop] = Convert.ToInt32(srLine[oszlop]);
                    }
                }

                while (!sr.EndOfStream)
                {
                    steps.Add(sr.ReadLine().Split(' '));
                }
            }
        }
    }
}
