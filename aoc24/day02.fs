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

let aggregateReports report=
    report
    |> List.filter (fun report -> report = true)
    |> List.length

let part1 (input: string) =
    getReports input
    |> List.map isReportSafe
    |> aggregateReports
    
let reportWithProblemDamperApplied (report: list<int>) =
    report
    |> List.mapi (fun i _ ->
        report |> List.mapi (fun j x -> if i <> j then Some x else None)
        |> List.choose id
    )

let part2 (input: string) =
    getReports input
    |> List.map reportWithProblemDamperApplied
    |> List.map (fun reportList ->
                                reportList
                                   |> List.exists isReportSafe)
    |> aggregateReports
    