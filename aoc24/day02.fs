module Aoc2024.Day02

open System
open System.IO

let inputFile = "AdventOfCode24/day01/input.txt"

let parseInput (filePath: string) =
    File.ReadLines(filePath)
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

// part 1
let part1 =
    let parsedPairs = parseInput inputFile
    let sortedColumns = sortColumns parsedPairs
    let result = computeSumOfDifferences sortedColumns
    printfn $"Sum: %d{result}"


// part 2
let parsedPairs = parseInput inputFile

let lefts = parsedPairs |> List.map fst
let rights = parsedPairs |> List.map snd

let counts =
    lefts
    |> List.map (fun left ->
        let count = rights |> List.filter (fun right -> right = left) |> List.length
        (left, count))
        
printfn $"Counts= %A{counts}"

let sumOfCounts =
    counts
    |> List.unzip
    |> fun (num, count) -> List.map2 (fun x y -> x*y) num count
    |> List.sum
    
printfn $"Sum of counts:%d{sumOfCounts}" 