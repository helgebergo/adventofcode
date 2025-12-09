namespace aoc25;

public class Day5
{
	public static void Run()
	{
		// const string input = "3-5\n10-14\n16-20\n12-18\n\n1\n5\n8\n11\n17\n32";
		// const string input = "3-5\n10-14\n16-20\n12-18\n13-32\n33-34\n35-36\n36-38\n38-39\n\n";
		var input = File.ReadAllText("input05.txt");

		var ingredients = input.Split("\n\n");
		
		var ranges = ingredients[0]
			.Split("\n")
			.Select(f => f.Split("-"))
			.Select(f => new Range(f[0], f[1]))
			.OrderBy(r => r.Start)
			.ToList();
		
		// Part1(ingredients, ranges);
		Part2(ranges);
	}
	
	private static void Part2(List<Range> ranges)
	{
		Console.WriteLine($"{string.Join(" ", ranges)}");
		
		var newRanges = new List<Range>();
		
		for (var i = 0; i < ranges.Count - 1; i++)
		{
			Console.WriteLine($"\n{i}");
			// Console.WriteLine($"{string.Join(" ", ranges[i..])}");
			
			var current = ranges[i];
			var next = ranges[i + 1];
			Console.WriteLine($"{current}, {next}");
			if (current.Contains(next.Start))
			{
				var mergedEnd = Math.Max(current.End, next.End);
				Console.WriteLine($"hit, {current}");
				ranges[i].End = current.Start;
				ranges[i + 1] = new Range (current.Start, mergedEnd);
			}
			newRanges.Add(current);
			Console.WriteLine($"{ranges[i]}, {ranges[i + 1]}");
		}
		newRanges.Add(ranges[^1]);
		
		Console.WriteLine($"{string.Join(" ", ranges)}");
		// Console.WriteLine($"{string.Join(" ", newRanges)}");
		
		// var sum = ranges.Sum(r => r.Value);
		var sum = newRanges.Where(r => r.Start!= r.End).Sum(r => r.End - r.Start + 1);
		Console.WriteLine($"Sum: {sum}");
	}

	private static void Part1(string[] ingredients, List<Range> ranges)
	{
		var available = ingredients[1].Split("\n").Select(long.Parse);

		HashSet<long> counts = [];
		foreach (var ingredient in available)
		{
			foreach (var _ in ranges.Where(range => range.Contains(ingredient)))
				counts.Add(ingredient);
		}
		
		Console.WriteLine($"Count: {counts.Count}");
	}
}

public record Range(long Start, long End)
{
	public override string ToString() => $"[{Start}-{End}, {Value}]";
	public Range(string start, string end) : this(long.Parse(start), long.Parse(end)) { }

	public long Value => End - Start;
	public long Start { get; set; } = Start;
	public long End { get; set; } = End;
	public bool Contains(long value) => value >= Start && value <= End;
}
