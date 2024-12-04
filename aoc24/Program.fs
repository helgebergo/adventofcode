open System.IO

let data = File.ReadAllText "../../day01.txt"

data |> aoc24.day01.part01 |> printfn "%A"
data |> aoc24.day01.part02 |> printfn "%A"