module Parser

open System
open System.Text.RegularExpressions
open MapCommand
open Command1
open Command2

(*A method to check for regular expression 
and divide the parameters of expression into list*)
let (|ParseRegex|_|) regex str =
   let m = Regex(regex).Match(str)
   if m.Success
   then Some (List.tail [ for x in m.Groups -> x.Value ])
   else None

let MapCommand regex = CMD_MAP.[regex]

let GetSegIndex seg = 
    match seg with
    | "local" -> "1"
    | "argument" -> "2"
    | "this" -> "3"
    | "that" -> "4"
    | "temp" -> "5"
    | "static" -> "16"
    | "pointer" -> "3"
    | _ -> seg

let Parse line (index:System.Int32) = 
    match line with
    | ParseRegex EMPTY_REGEX _ -> MapCommand EMPTY_REGEX
    | ParseRegex PUSH_CONSTANT_REGEX [number; _] -> String.Format(MapCommand PUSH_CONSTANT_REGEX, number)
    | ParseRegex ADD_REGEX _ -> MapCommand ADD_REGEX
    | ParseRegex SUB_REGEX _ -> MapCommand SUB_REGEX
    | ParseRegex NEG_REGEX _ -> MapCommand NEG_REGEX
    | ParseRegex EQ_REGEX _ -> String.Format(MapCommand EQ_REGEX, index)
    | ParseRegex LT_REGEX _ -> String.Format(MapCommand LT_REGEX, index)
    | ParseRegex GT_REGEX _ -> String.Format(MapCommand GT_REGEX, index)
    | ParseRegex AND_REGEX _ -> MapCommand AND_REGEX
    | ParseRegex OR_REGEX _ -> MapCommand OR_REGEX
    | ParseRegex BASIC_POP_REGEX [seg; number; _] -> String.Format(MapCommand BASIC_POP_REGEX,GetSegIndex seg,number)
    | ParseRegex BASIC_PUSH_REGEX [seg; number; _] -> String.Format(MapCommand BASIC_PUSH_REGEX, GetSegIndex seg, number)
    | ParseRegex PUSH_TEMP_STATIC_REGEX [seg; number; _] -> String.Format(MapCommand PUSH_TEMP_STATIC_REGEX,GetSegIndex seg,number)
    | ParseRegex POP_TEMP_STATIC_REGEX [seg; number; _] -> String.Format(MapCommand POP_TEMP_STATIC_REGEX,GetSegIndex seg,number)
    | ParseRegex LABEL_REGEX [labelname; _] -> String.Format(MapCommand LABEL_REGEX,labelname,index)
    | _ -> String.Format("ERROR: {0}", line)
