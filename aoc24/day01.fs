module aoc24.day01
open System

let parseInput (s: string) =
    s.Split([|'\n'; '\r'|], StringSplitOptions.RemoveEmptyEntries)
    |> Seq.choose (fun line ->
        match line.Split([| ' '; '\t' |], StringSplitOptions.RemoveEmptyEntries) |> Array.map int with
        | [| x; y |] -> Some (x, y)
        | _ -> None)
    |> Seq.toList

let sortColumns (pairs: (int * int) list) =
    let sortedCol1, sortedCol2 =
        pairs
        |> List.unzip
        |> fun (col1, col2) -> List.sort col1, List.sort col2
    List.zip sortedCol1 sortedCol2 

let computeSumOfDifferences pairs =
    pairs
    |> List.unzip
    |> fun (col1, col2) -> List.map2 (fun x y -> abs (x - y)) col1 col2 
    |> List.sum

let part01 (s: string) =
    parseInput s
    |> sortColumns
    |> computeSumOfDifferences
    
    
let part02 (s: string) =
    let parsedPairs = parseInput s
    let lefts = parsedPairs |> List.map fst |> List.distinct

    lefts
    |> List.map (fun left ->
        let count = parsedPairs |> List.filter (fun (_, right) -> right = left) |> List.length
        left * count)
    |> List.sum