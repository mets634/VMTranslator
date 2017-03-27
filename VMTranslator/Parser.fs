﻿module Parser

open System
open System.Text.RegularExpressions
open Command

let (|ParseRegex|_|) regex str =
   let m = Regex(regex).Match(str)
   if m.Success
   then Some (List.tail [ for x in m.Groups -> x.Value ])
   else None


let Parse line = 
    match line with
    | ParseRegex Command.EMPTY_REGEX _ -> Command.CMD_MAP.[EMPTY_REGEX]
    | ParseRegex Command.COMMENT_REGEX _ -> Command.CMD_MAP.[COMMENT_REGEX]
    | ParseRegex Command.PUSH_CONSTANT_REGEX [number; _] -> String.Format(Command.CMD_MAP.[PUSH_CONSTANT_REGEX], number)
    | ParseRegex Command.ADD_REGEX _ -> Command.CMD_MAP.[ADD_REGEX]
    | ParseRegex Command.EQ_REGEX _ -> Command.CMD_MAP.[ADD_REGEX]
    | ParseRegex Command.LT_REGEX _ -> Command.CMD_MAP.[LT_REGEX]
    | ParseRegex Command.GT_REGEX _ -> Command.CMD_MAP.[GT_REGEX]
    | ParseRegex Command.AND_REGEX _ -> Command.CMD_MAP.[AND_REGEX]
    | ParseRegex Command.OR_REGEX _ -> Command.CMD_MAP.[OR_REGEX]
    | ParseRegex Command.NEG_REGEX _ -> Command.CMD_MAP.[AND_REGEX]
    | ParseRegex Command.SUB_REGEX _ -> Command.CMD_MAP.[SUB_REGEX]
    | _ -> "ERROR"
