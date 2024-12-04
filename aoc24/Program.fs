open System.IO

let data = File.ReadAllText "../../day02.txt"

data |> aoc24.day02.part01 |> printfn "%A"
// data |> aoc24.day02.part02 |> printfn "%A"