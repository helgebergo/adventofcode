namespace aoc25;

public class Main
{
	private static List<Instruction> ParseInput(string input) => input.Split("\n").Select(l => new Instruction(l[0], int.Parse(l[1..]))).ToList();
	
	public static void Run()
	{
		// const string input = "L68\nL30\nR48\nL5\nR60\nL55\nL1\nL99\nR14\nL82";
		var input = File.ReadAllText("input01.txt");

		var instructions = ParseInput(input);

		var dial = new Dial();
		dial.Rotate(instructions.First());
		foreach (var instruction in instructions[1..])
		{
			dial.Rotate(instruction);
		}

		Console.WriteLine($"Total zeros: {dial.Zeros}");
	}
}

public record Instruction(char Direction, int Clicks)
{
	public override string ToString() => $"Instr: {Direction}{Clicks}";
}

public class Dial
{
	public override string ToString() => $"Dial: pos {_position}, zeros: {Zeros}";

	public int Zeros;
	private int _position = 50;

	public void Rotate(Instruction instruction)
	{
		Console.WriteLine($"{_position:D2} \t {instruction}\t zeros: {Zeros}");
		if (instruction.Direction == 'R')
		{
			if (_position + instruction.Clicks >= 100)
				Zeros++;
			_position = (_position + instruction.Clicks) % 100;
		}
		else
		{
			if (_position - instruction.Clicks <= 0)
				Zeros++;
			_position = (_position - instruction.Clicks + 100) % 100;
		}
	}
}
