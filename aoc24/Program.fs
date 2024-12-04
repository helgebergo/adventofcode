// For more information see https://aka.ms/fsharp-console-apps

open System.IO
open aoc24

printfn "Hello from F#"


let data = File.ReadAllText "../../day01.txt"

data |> aoc24.day01.part01 |> printfn "%A"
// data |> Day20.part2 |> printfn "%A"