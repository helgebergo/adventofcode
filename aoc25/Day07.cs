using System.Collections;
using System.Text;

namespace aoc25;

public class Day7
{
	public static void Run()
	{
		const string txt = ".......S.......\n...............\n.......^.......\n...............\n......^.^......\n...............\n.....^.^.^.....\n...............\n....^.^...^....\n...............\n...^.^...^.^...\n...............\n..^...^.....^..\n...............\n.^.^.^.^.^...^.\n...............";
		// var txt = File.ReadAllText("input07.txt");
		var input = txt.Split("\n");

		var cells = new Cell[input.Length, input.First().Length];
		for (var r = 0; r < cells.GetLength(0); r++) // rows
			for (var c = 0; c < cells.GetLength(1); c++) // cols
				cells[r, c] = new Cell(input[r][c], r, c);

		var grid = new Grid(cells);

		var start = grid.First(c => c.IsStart);
		if (start is null)
			throw new Exception("Start cell is null");
		cells[start.Row + 1, start.Col].Beam();
		
		Console.WriteLine(grid);
		for (var i = 1; i < cells.GetLength(0); i++)
		{
			for (var j = 0; j < cells.GetLength(1); j++)
			{
				if (grid.Check(cells[i, j]))
					grid.BeamOn(cells[i, j]);
			}
		}

		Console.WriteLine(grid);

		var beamed = grid.Count(c => c.SplitterUsed);
		Console.WriteLine(beamed);
	}
}

public class Grid(Cell[,] cells) : IEnumerable<Cell>
{
	private int Rows => cells.GetLength(0);
	private int Cols => cells.GetLength(1);
	private bool LeftEdge(Cell cell) => cell.Col == 0;
	private bool RightEdge(Cell cell) => cell.Col == Cols - 1;
	private bool IsBottom(Cell cell) => cell.Row == Rows - 1;

	public bool Check(Cell cell) => cells[cell.Row - 1, cell.Col].IsBeamed;

	public void BeamOn(Cell cell)
	{
		if (IsBottom(cell))
			return;

		if (!cell.IsSplitter)
		{
			cells[cell.Row, cell.Col].Beam();
			return;
		}
		
		if (!LeftEdge(cell))
			cells[cell.Row, cell.Col - 1].Beam();
			
		if (!RightEdge(cell))
			cells[cell.Row, cell.Col + 1].Beam();
		cell.SplitterUsed = true;
	}
	
	public override string ToString()
	{
		StringBuilder sb = new();
		for (var i = 0; i < cells.GetLength(0); i++)
		{
			for (var j = 0; j < cells.GetLength(1); j++)
				sb.Append(cells[i, j].C);
			sb.AppendLine();
		}
		return sb.ToString();
	}
	
	public IEnumerator<Cell> GetEnumerator()
	{
		for (var i = 0; i < cells.GetLength(0); i++)
			for (var j = 0; j < cells.GetLength(1); j++)
				yield return cells[i, j];
	}

	IEnumerator IEnumerable.GetEnumerator() => GetEnumerator();
}

public class Cell(char c, int row, int col)
{
	public char C {get; private set; } = c;
	public readonly int Row = row;
	public readonly int Col = col;
	public bool IsStart => C == 'S';
	public bool IsSplitter => C == '^';

	public void Beam()
	{
		IsBeamed = true;
		C = '|';
	}
	
	public bool IsBeamed { get; private set; }
	public bool SplitterUsed { get; set; }

	public override string ToString() => $"{C}[{Row},{Col}]";
}
