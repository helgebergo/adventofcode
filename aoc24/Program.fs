open System.IO

let data = File.ReadAllText "../../day03.txt"

data |> aoc24.day03.part1 |> printfn "Result part 1: %A"
// data |> aoc24.day03.part2 |> printfn "Result part 2: %A"