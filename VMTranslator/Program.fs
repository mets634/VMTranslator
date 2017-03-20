module main

open System.IO
open Parser


[<EntryPoint>]
let main argv = 
    let lines = File.ReadAllLines(argv.[0])
    lines |> Array.filter(fun l -> l.Length > 0) |> Array.iter(fun line -> printfn "%s" (line))
    printfn "---------------"
    lines |> Array.map (fun line -> Parser.Parse line) |> Array.filter(fun l -> l.Length > 0) |> Array.iter (fun line -> printfn "%s" (line))


    0 // return an integer exit code
