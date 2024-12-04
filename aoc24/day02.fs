module aoc24.day02

let getReports (input: string) =
    input.Split('\n')
    |> Array.toList
    |> List.map (fun line ->
        line.Split(' ') 
        |> Array.toList
        |> List.map int
    )

let isReportSafe (report: int list) : bool =
    let isDescending = report[0] >= report[1]
    
    let arePairsSafe =
        report
        |> List.pairwise
        |> List.map (fun (first, second) ->
            first <> second &&
            ((first < second) = not isDescending) &&    
            let difference = abs (second - first)
            difference >= 1 && difference <= 3
        )
    
    arePairsSafe
    |> List.forall id

let part01 (input: string) =
    getReports input
    |> List.map isReportSafe
    |> List.filter (fun report -> report = true)
    |> List.length
    |> printfn "\nSafe reports: %A"