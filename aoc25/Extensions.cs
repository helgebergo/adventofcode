namespace aoc25;

public static class Extensions
{
    public static int Parse(this char s) => int.Parse(s.ToString());
    
    public static IEnumerable<string> ToStrings(this IEnumerable<int> ints) => ints.Select(x => x.ToString());
}