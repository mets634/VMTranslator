module Parser

open System
open System.Text.RegularExpressions
open Command

(*A method to check for regular expression 
and divide the parameters of expression into list*)
let (|ParseRegex|_|) regex str =
   let m = Regex(regex).Match(str)
   if m.Success
   then Some (List.tail [ for x in m.Groups -> x.Value ])
   else None

let MapCommand regex = CMD_MAP.[regex]

let Parse line = 
    match line with
    | ParseRegex EMPTY_REGEX _ -> MapCommand EMPTY_REGEX
    | ParseRegex COMMENT_REGEX _ -> MapCommand COMMENT_REGEX
    | ParseRegex PUSH_CONSTANT_REGEX [number; _] -> String.Format(MapCommand PUSH_CONSTANT_REGEX, number)
    | ParseRegex ADD_REGEX _ -> MapCommand ADD_REGEX
    | ParseRegex SUB_REGEX _ -> MapCommand SUB_REGEX
    | ParseRegex NEG_REGEX _ -> MapCommand NEG_REGEX
    | ParseRegex EQ_REGEX _ -> MapCommand EQ_REGEX
    | ParseRegex LT_REGEX _ -> MapCommand LT_REGEX
    | ParseRegex GT_REGEX _ -> MapCommand GT_REGEX
    | ParseRegex AND_REGEX _ -> MapCommand AND_REGEX
    | ParseRegex OR_REGEX _ -> MapCommand OR_REGEX
    | _ -> String.Format("ERROR: {0}", line)
