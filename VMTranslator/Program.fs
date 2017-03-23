module main

open System.IO
open System
open Parser


let Translate (file:string) = 
    let code = 
        File.ReadAllLines(file) 
        |> Array.map(fun line -> Parser.Parse line) 
        |> Array.filter(fun l -> l.Length > 0)

    let asm_name = Path.ChangeExtension(file, ".asm")
    File.WriteAllLines(asm_name, code)

let GetFile (argv:string[]) = 
    match argv.Length with
    // no input, prompt user for path
    | 0 -> printfn "ENTER FILE PATH:\n\n"
           Console.ReadLine()
    // got input
    | _ -> argv.[0]

[<EntryPoint>]
let main argv = 
    try
        GetFile argv |> Translate
    with _ as ex -> Console.WriteLine("ERROR:\n{0}", (ex.Message.ToString()))

    0 // return an integer exit code
