module aoc24.day03

open System.Text.RegularExpressions

let regex = @"mul\((\d+),(\d+)\)"

let extractAndMultiplyMuls(input: string) =
    Regex.Matches(input, regex)
    |> Seq.cast<Match>
    |> Seq.map (fun m ->
        let x = int m.Groups.[1].Value
        let y = int m.Groups.[2].Value
        (x * y)
        )
    |> Seq.sum

let part1(input: string) =
    extractAndMultiplyMuls input

let part2(input: string) =
    extractAndMultiplyMuls input