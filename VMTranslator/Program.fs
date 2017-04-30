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

        // add assembly prefix
        let asm_name = Path.ChangeExtension(dir_name + @"\Assembly", ".asm")
        File.WriteAllLines(asm_name, [|Command1.prefix|])

        try
            // add vm prefix
            let sys_lines = dir.GetFiles() |> Array.find (fun f -> f.Name = "Sys.vm") |> Translate
            File.AppendAllLines(asm_name, sys_lines)

        with _ as ex -> () // ignore exception

        // add actual code
        dir.GetFiles() 
        |> Array.filter (fun f -> f.Extension = ".vm" && f.Name <> "Sys.vm") // only vm files not including sys.vm
        |> Array.map (fun f -> Translate f) // translate each file
        |> Array.map (fun lines -> File.AppendAllLines(asm_name, lines)) // add to file
        |> ignore
        

    with _ as ex -> Console.WriteLine("ERROR:\n{0}", (ex.Message.ToString()))

    Console.WriteLine("DONE")
    Console.ReadKey() |> ignore

    0 // return an integer exit code
