using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordSearchSolver
{
    public static class Puzzle
    {
        public static string imagePath { get; set; }
        public static string imageType { get; } = "jpeg";
        public static string text { get; set; }
        public static Cell[,] textGrid { get; private set; }
        public static Cell[,] textGrid2 { get; private set;}
        public static int x { get; private set; }
        public static int y { get; private set; }

        public static char lastChar { get; private set; }
        public static void initializePuzzle() {
            text = Tesseract.ConvertImageToText(imagePath);
            text = text.ToUpper();
            textToGrid();
        }

        public static void textToGrid() {
            List<string> lines = text.Split('\n').ToList();
            x = lines[0].Length;
            List<string> validLines = lines.Where(s => s.Length == x).ToList();
            y = validLines.Count;

            textGrid = new Cell[y, x];
            for (int i = 0; i < y; i++)
            {
                for (int j = 0; j < x; j++)
                {
                    textGrid[i, j] = new Cell(validLines[i][j]);
                }
            }
        }

        public static void findWord(string input) {
            input = input.ToUpper();
            resetGridActivity();
            var correctCells = new List<Cell>();
            if (input.Length == 1) {
                for (int i = 0; i < y; i++)
                    for (int j = 0; j < x; j++)
                        if (input[0] == textGrid[i, j].Value)
                            textGrid[i, j].IsActive = true;
                return;
            }
            for (int i = 0; i < y; i++) {
                for (int j = 0; j < x; j++) {
                    if (input[0] == textGrid[i, j].Value)
                    {
                        List<Tuple<int, int>> directions = getDirections(i, j, input[1]);
                        foreach (Tuple<int, int> direction in directions) {
                            int ii = direction.Item1;
                            int jj = direction.Item2;
                            for (int k = 0; k < input.Length; k++) {
                                if (i + ii * k >= 0 && i + ii * k < y && j + jj * k >= 0 && j + jj * k < x && input[k] == textGrid[i + ii * k, j + jj * k].Value)
                                    correctCells.Add(textGrid[i + ii * k, j + jj * k]);
                                else break;
                            }
                            if (correctCells.Count == input.Length)
                            {
                                foreach (Cell cell in correctCells)
                                    cell.IsActive = true;
                                return;
                            }
                        }
                    }
                }
            }
            return;
        }
        public static void resetGridActivity() {
            for (int i = 0; i < y; i++)
                for (int j = 0; j < x; j++)
                    textGrid[i, j].IsActive = false;
        }

        public static List<Tuple<int, int>> getDirections(int i, int j, char target) {
            var directions = new List<Tuple<int, int>>();
            for (int ii = -1; ii < 2; ii++)
                for (int jj = -1; jj < 2; jj++)
                    if (i + ii >= 0 && i + ii < y && j + jj >= 0 && j + jj < x && textGrid[i + ii, j + jj].Value == target)
                        directions.Add(new Tuple<int, int>(ii, jj));
            return directions;
        }
    }

    public class Cell { 
        public char Value { get; set; }
        public bool IsActive { get; set; }
        public Cell(char v) {
            Value = v;
            IsActive = false;
        }
    }
}
