using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WordSearchSolver
{
    public static class Puzzle
    {
        public static Boolean UploadAnsFlag { get; set; }
        public static string imagePath { get; set; }
        public static string AnsImagePath { get; set; }
        public static string imageType { get; } = "jpeg";
        public static string text { get; set; }
        public static string AnsText { get; set; }
        public static List<string> AnsList { get; private set; }
        public static Cell[,] textGrid { get; private set; }
        public static int x { get; private set; }
        public static int y { get; private set; }
        public static List<Word> wordList { get; private set; } 
        public static void initializePuzzle() {
            text = Tesseract.ConvertImageToText(imagePath);
            text = text.ToUpper();
            textToGrid();
            wordList = new List<Word>();
        }
        public static void InitializeAns()
        {
            AnsText = Tesseract.ConvertImageToText(AnsImagePath);
            AnsText = AnsText.ToUpper();
            AnsList = AnsText.Split(new char[0]).ToList();
            foreach(var i in AnsList)
            {
                findWord(i);
            }
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

        public static bool findWord(string input) {
            if (input.Length == 0)
                return false;
            input = input.ToUpper();
            resetGridActivity();
            if (input.Length == 1)
            {
                bool wasFound = false;
                for (int i = 0; i < y; i++)
                    for (int j = 0; j < x; j++)
                        if (input[0] == textGrid[i, j].Value) 
                        {
                            textGrid[i, j].IsActive = true;
                            wasFound = true;
                        }
                return wasFound;
            }
            else 
            {
                for (int i = 0; i < y; i++)
                {
                    for (int j = 0; j < x; j++)
                    {
                        if (input[0] == textGrid[i, j].Value)
                        {
                            List<Tuple<int, int>> directions = getDirections(i, j, input[1]);
                            foreach (Tuple<int, int> direction in directions)
                            {
                                var correctCells = new List<Cell>();
                                int ii = direction.Item1;
                                int jj = direction.Item2;
                                for (int k = 0; k < input.Length; k++)
                                {
                                    if (i + ii * k >= 0 && i + ii * k < y && j + jj * k >= 0 && j + jj * k < x && input[k] == textGrid[i + ii * k, j + jj * k].Value)
                                        correctCells.Add(textGrid[i + ii * k, j + jj * k]);
                                    else break;
                                }
                                if (correctCells.Count == input.Length)
                                {
                                    wordList.Add(new Word(input, correctCells));
                                    wordList.Last().Activate();
                                    return true;
                                }
                            }
                        }
                    }
                }
            }
            return false;
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
                    if (i + ii >= 0 && i + ii < y && j + jj >= 0 && j + jj < x && textGrid[i + ii, j + jj].Value == target && !(jj == 0 && ii == 0))
                        directions.Add(new Tuple<int, int>(ii, jj));
            return directions;
        }

        public static void ActivateByIndex(int index) 
        {
            resetGridActivity();
            wordList.ElementAt(index).Activate();
        }
    }

    public class Cell { 
        public char Value { get; private set; }
        public bool IsActive { get; set; }
        public Cell(char v) {
            Value = v;
            IsActive = false;
        }
    }

    public class Word
    {
        public string word { get; private set; }
        public List<Cell> cellList { get; set;}

        public Word(string w, List<Cell> cl) 
        {
            word = w;
            cellList = cl;
        }

        public void Activate() 
        {
            foreach (Cell cell in cellList)
                cell.IsActive = true;
        }
    }
}
