module aoc24.day05

open System

let parseInput (s : string) =
    s.Split([|"\n\n"|], StringSplitOptions.None)
    |> function
        | [|part1; part2|] -> (part1, part2)
        | _ -> failwith "Expected exactly one double newline."

let splitLines (s : string) =
    s.Split('\n')
    |> Array.toList
    |> List.map int

let parseRules (rules: list<int>) =
    rules
    

let part1(input: string) =
    let r, c = parseInput input
   
    let rules = splitLines r
    let codes = splitLines c
    
    parseRules rules

