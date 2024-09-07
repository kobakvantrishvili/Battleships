public class Program
{

    public static void Main(String[] args)
    {
        string[][] grid = {
            new string[] { ".", "A", "B", "B", "B" },
            new string[] { ".", "A", ".", ".", "C" },
            new string[] { ".", ".", ".", ".", "." },
            new string[] { "D", "D", ".", ".", "." },
            new string[] { ".", "E", "E", "E", "E" }
        };
        int[][] shots = {
            new int[] { 0, 0 }, new int[] { 0, 1 }, new int[] { 0, 2 }, new int[] { 1, 1 }, new int[] { 0, 1 },
            new int[] { 1, 4 }, new int[] { 2, 2 }, new int[] { 2, 4 }, new int[] { 0, 3 }, new int[] { 0, 0 }, new int[] { 0, 4 }
        };

        string[] results = solution(grid, shots);

        foreach (string result in results)
        {
            Console.WriteLine(result);
        }
    }


    public static string[] solution(string[][] grid, int[][] shots)
    {
        Dictionary<string, HashSet<int[]>> Cells = new Dictionary<string, HashSet<int[]>>();

        for (int i = 0; i < grid.GetLength(0); i++)
        {
            for (int j = 0; j < grid[i].GetLength(0); j++)
            {
                string cell = grid[i][j];
                if (Cells.ContainsKey(cell))
                    Cells[cell].Add(new int[] { i, j });
                else
                    Cells.Add(cell, new HashSet<int[]> { new int[] { i, j } });
            }
        }

        List<string> results = new List<String>();
        HashSet<int[]> attackedCells = new HashSet<int[]>();

        foreach (var shot in shots)
        {
            if (attackedCells.Contains(shot))
            {
                results.Add("Already Attacked");
                continue;
            }

            int row = shot[0];
            int column = shot[1];

            string cell = grid[row][column];

            if (cell == ".")
            {
                results.Add("Missed");
            }
            else
            {
                Cells[cell].RemoveWhere(x => x[0] == shot[0] && x[1] == shot[1]);
                attackedCells.Add(shot);

                if (Cells[cell].Any())
                {
                    results.Add($"Attacked ship {cell}");
                }
                else
                {
                    results.Add($"Ship {cell} sunk");
                }
            }
        }
        return results.ToArray();
    }
}



