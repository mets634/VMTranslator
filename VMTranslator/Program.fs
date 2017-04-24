module main

open System.IO
open System
open Parser
open System.Text.RegularExpressions



let Translate (file:string) = 
    let singleLineCommentRegex = @"^//.*$"
    let multiLineCommentRegex = @"/\*(.|[\r\n])*?\*/"

    // remove comments
    let noMultiComments = Regex.Replace ((File.ReadAllText file), multiLineCommentRegex, "")
    let noComments = Regex.Replace (noMultiComments, singleLineCommentRegex, "", RegexOptions.Multiline)

    let code = 
        noComments.Split ("\n".ToCharArray())  // split file into lines
        |> Array.map (fun line -> line.Trim())  // remove end whitespaces
        |> Array.map(fun line -> Parser.Parse line)  // parse line
        |> Array.filter(fun l -> l.Length > 0)  // remove empty lines

    // write commands to .asm file
    let asm_name = Path.ChangeExtension(file, ".asm")
    File.WriteAllLines(asm_name, (Array.append [|Command.prefix|] code))

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

    Console.WriteLine("DONE")
    Console.ReadKey() |> ignore

    0 // return an integer exit code
