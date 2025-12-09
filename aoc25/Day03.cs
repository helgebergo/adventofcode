namespace aoc25;

public class Day3
{
	public static void Run()
	{
		const string input = "987654321111111\n811111111111119\n234234234234278\n818181911112111";
		// var input = File.ReadAllText("input03.txt");

		var banks = input.Split("\n");

		// Console.WriteLine(Part1(banks));
		Console.WriteLine(Part2(banks));
	}

	private static long Part2(string[] banks)
	{
		long joltage = 0;
		foreach (var batteries in banks)
		{
			Console.WriteLine(batteries);

			var jolts = batteries.Select(b => b.Parse()).ToList();

			while (jolts.Count > 12)
			{
				var min = jolts.Min();
				// jolts.Remove()
				jolts.Remove(jolts.Min());
			}

			var r = jolts.ToStrings();
			var t = string.Join("", r);
			Console.WriteLine(t);
			joltage += long.Parse(string.Join("", jolts.Select(j => j.ToString())));
			Console.WriteLine();
		}

		return joltage;
	}
	
	private static int Part1(string[] banks)
	{
		var sum = 0;
		foreach (var batteries in banks)
		{
			Console.WriteLine(batteries);

			var jolts = batteries.Select(b => int.Parse(b.ToString())).ToList();
			var first = jolts[..^1].Max();
			var index = jolts.IndexOf(first)+1;
			var jolts2 = jolts[index..];
			var second = jolts2.Max();
			var jolt = int.Parse($"{first.ToString()}{second.ToString()}");
			sum += jolt;

			Console.WriteLine($"{first} {second}");
		}

		return sum;
	}
}
