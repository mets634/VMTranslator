module main

open System.IO
open System
open Parser
open System.Text.RegularExpressions


let Translate (file:FileInfo) = 
    let singleLineCommentRegex = @"//.*"
    let multiLineCommentRegex = @"/\*(.|[\r\n])*?\*/"

    // remove comments
    let noMultiComments = Regex.Replace ((File.ReadAllText file.FullName), multiLineCommentRegex, "")
    let noComments = Regex.Replace (noMultiComments, singleLineCommentRegex, "", RegexOptions.Multiline)

    
    noComments.Split ("\n".ToCharArray())  // split file into lines
    |> Array.map (fun line -> line.Trim())  // remove end whitespaces
    |> Array.mapi(fun index line -> Parser.Parse line file.Name index)  // parse line
    |> Array.filter(fun l -> l.Length > 0)  // remove empty lines
    
let GetDirectoryName (argv:string[]) = 
    match argv.Length with
    // no input, prompt user for path
    | 0 -> printfn "ENTER DIRECTORY PATH:\n\n"
           Console.ReadLine()
    // got input
    | _ -> argv.[0]

[<EntryPoint>]
let main argv = 
    try
        let dir_name = GetDirectoryName argv
        let dir = new DirectoryInfo(dir_name)

        let asm_name = Path.ChangeExtension(dir_name + "Assembly", ".asm")
        File.WriteAllLines(asm_name, [|Command1.prefix|])

        dir.GetFiles() 
        |> Array.filter (fun f -> f.Extension = ".vm") 
        |> Array.map (fun f -> Translate f) 
        |> Array.ForEach(fun arr -> arr.ForEach(fun line -> File.AppendAllText(line,asm_name)))
        

    with _ as ex -> Console.WriteLine("ERROR:\n{0}", (ex.Message.ToString()))

    Console.WriteLine("DONE")
    Console.ReadKey() |> ignore

    0 // return an integer exit code
