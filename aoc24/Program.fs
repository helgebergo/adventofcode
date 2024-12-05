open System.IO

let data = File.ReadAllText "../../day05.txt"

data |> aoc24.day05.part1 |> printfn "Result part 1: %A"
// data |> aoc24.day05.part2 |> printfn "Result part 2: %A"