module aoc24.day03

open System.Text.RegularExpressions

let regex = @"mul\(\d+,\d+\)"

let extractValidMuls(input: string) =
    Regex.Matches(input, regex)
    |> Seq.cast<Match>
    |> Seq.map _.Value
    |> Seq.toList

let multiply(mul: string) =
    mul.Split(',')
    |> fun Regex.Matches "\d+"


let part1(input: string) =
    extractValidMuls input
    |> List.map multiply
