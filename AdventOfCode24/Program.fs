open System
open System.IO
open System.Net.Http
open System.Text.RegularExpressions
open System.Threading.Tasks

let year = 2023
let day = 
    if fsi.CommandLineArgs.Length > 1 then 
        Int32.Parse(fsi.CommandLineArgs.[1]) 
    else 
        failwith "Day number is required as a command-line argument."

let dir = Path.Combine("aoc", $"day%02d{day}")
Directory.CreateDirectory(dir) |> ignore

let sessionCookie = Environment.GetEnvironmentVariable("AOC_SESSION")
if String.IsNullOrWhiteSpace(sessionCookie) then
    failwith "AOC_SESSION environment variable is not set."

let getHttpClient () =
    let httpClient = new HttpClient()
    httpClient.DefaultRequestHeaders.Add("Cookie", $"session={sessionCookie}")
    httpClient

let fetchUrl (httpClient: HttpClient) (url: string) : Async<string> =
    async {
        try
            let! response = httpClient.GetStringAsync(url) |> Async.AwaitTask
            return response
        with
        | ex ->
            printfn $"Failed to fetch %s{url}: %s{ex.Message}"
            raise ex
    }

let extractExample html =
    let pattern = @"<code>([\s\S]*?)</code>"
    let matches = Regex.Matches(html, pattern)
    matches 
    |> Seq.cast<Match>
    |> Seq.map (fun m -> m.Groups.[1].Value)
    |> Seq.tryFind (fun code -> code.Split('\n').Length >= 5)
    |> Option.defaultValue "no idea"

let setupDayAsync () =
    async {
        use httpClient = getHttpClient()

        let baseUrl = $"https://adventofcode.com/%d{year}/day/%d{day}"
        let! pageHtml = fetchUrl httpClient baseUrl
        let example = extractExample pageHtml

        let inputUrl = $"%s{baseUrl}/input"
        let! inputText = fetchUrl httpClient inputUrl
        File.WriteAllText(Path.Combine(dir, "input.txt"), inputText)

        let runScriptContent = $"""
// Advent of Code {year} - Day {day}
// Input file: input.txt

open System
open System.IO

let inputFile = "input.txt"

// Example input from the problem description:
let exampleInput = \"\"\"{example}\"\"\"

// Load input
let inputLines = 
    File.ReadAllLines(inputFile) 
    |> Array.toList

// Use this to switch between example input and actual input
let input = exampleInput.Split([|'\n'|]) |> Array.toList
// let input = inputLines

// Solution logic
let solve () =
    printfn "Input: %A" input
    // Add your solution here

solve()
"""

        let runScriptPath = Path.Combine(dir, "run.fsx")
        File.WriteAllText(runScriptPath, runScriptContent)

        let startTimePath = Path.Combine(dir, ".start")
        File.WriteAllText(startTimePath, DateTime.UtcNow.ToString("o"))

        printfn $"Setup for day %d{day} complete."
    }

setupDayAsync ()
|> Async.RunSynchronously