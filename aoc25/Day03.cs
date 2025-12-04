namespace aoc25;

public class Main
{
	public static void Run()
	{
		// const string input = "987654321111111\n811111111111119\n234234234234278\n818181911112111";
		var input = File.ReadAllText("input03.txt");

		var banks = input.Split("\n");

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
		Console.WriteLine(sum);
	}
}
