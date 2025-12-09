namespace aoc25;

public static class Day9
{
	public static void Run()
	{
		// const string txt = "7,1\n11,1\n11,7\n9,7\n9,5\n2,5\n2,3\n7,3";
		var txt = File.ReadAllText("input09.txt");
		var input = txt.Split("\n");
		var coords = input.Select(i => i.Split(","))
			.Select(x => new Coordinate(long.Parse(x[0]), long.Parse(x[1])))
			.ToList();
		
		var max = Enumerable.Range(0, coords.Count)
			.Select(i => coords.Where((_, j) => i != j)
			.Select(c => Area(coords[i], c))
			.Max()).Max();

		Console.WriteLine($"Max area: {max}");
	}

	private static long Area(Coordinate c1, Coordinate c2) => (Math.Abs(c1.X - c2.X) + 1) * (Math.Abs(c1.Y - c2.Y) + 1);
}

public record Coordinate(long X, long Y);
