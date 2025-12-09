namespace aoc25;

public static class Day2
{
	public static void Run()
	{
		const string input = "11-22,95-115,998-1012,1188511880-1188511890,222220-222224,1698522-1698528,446443-446449,38593856-38593862,565653-565659,824824821-824824827,2121212118-2121212124";
		// var input = File.ReadAllText("input02.txt");

		var codes = input
			.Split(",").Select(x => x.Split("-"))
			.Select(range => new Tuple<long, long>(long.Parse(range[0]), long.Parse(range[1])));
		
		// Part1(codes);
		Part2(codes);
	}

	private static void Part2(IEnumerable<Tuple<long, long>> ranges)
	{
		HashSet<long> hash = [];
		foreach (var tuple in ranges)
		{
			for (var value = tuple.Item1; value <= tuple.Item2; value++)
			{
				var code = value.ToString();
				
				for (var i = 1; i <= code.Length; i++)
				{
					var part = code[..i];
					if (part == code[i..])
						hash.Add(value);
				}
			}
		}

		Console.WriteLine($"Part 2: {hash.Sum()}");
	}

	private static void Part1(IEnumerable<Tuple<long, long>> codes)
	{
		long sum = 0;
		foreach (var tuple in codes)
		{
			// Console.WriteLine(tuple);
			for (var code = tuple.Item1; code < tuple.Item2; code++)
			{
				var str = code.ToString();
				if (str.Length  % 2 != 0)
					continue;
				var mid  = str.Length / 2;
				var part1 = str[..mid];
				var part2 = str[mid..];

				if (part1 == part2)
					sum += code;
			}
		}
		Console.WriteLine($"Part 1: {sum}");
	}
}