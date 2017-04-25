﻿module Command1

(*This module creates a map between the 
regular expression of the vm commands 
to their output as Hack assembly*)

[<Literal>]
let prefix = @"    @256
    D=A
    @0
    M=D
    @300
    D=A
    @1
    M=D
    @400
    D=A
    @2
    M=D
    @3000
    D=A
    @3
    M=D
    @3010
    D=A
    @4
    M=D"

[<Literal>]
let BASIC_PUSH_REGEX = @"^push (local|this|that|argument) (\d+)(\s*)$"

[<Literal>]
let BASIC_PUSH_ASM = @"
    @{0}
    D=M
    @{1}
    A=D+A
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1"

[<Literal>]
let PUSH_TEMP_STATIC_REGEX = @"^push (temp|static|pointer) (\d+)(\s*)$" 

[<Literal>]
let PUSH_TEMP_STATIC_ASM = @"
    @{0}
    D=A
    @{1}
    A=D+A
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1"

[<Literal>]
let  POP_TEMP_STATIC_REGEX = @"^pop (temp|static|pointer) (\d+)(\s*)$"

[<Literal>]
let POP_TEMP_STATIC_ASM = @"
    @SP
    M=M-1
    @{1}
    D=A
    @{0}
    D=A+D
    @SP
    A=M+1
    M=D
    @SP
    A=M
    D=M
    A=A+1
    A=M
    M=D
    "

[<Literal>]
let BASIC_POP_REGEX = @"^pop (local|this|that|argument) (\d+)(\s*)$"

[<Literal>]
let BASIC_POP_ASM = @"
    @SP
    M=M-1
    @{1}
    D=A
    @{0}
    D=M+D
    @SP
    A=M+1
    M=D
    @SP
    A=M
    D=M
    A=A+1
    A=M
    M=D
    "

[<Literal>]
let PUSH_CONSTANT_REGEX = @"^push constant (\d+)(\s*)$"

[<Literal>]
let PUSH_CONSTANT_ASM = @"
    @{0}
    D=A
    @SP
    A=M
    M=D
    @SP
    M=M+1"

[<Literal>]
let ADD_REGEX = @"^add$"

[<Literal>]
let ADD_ASM = @"
    @SP
    M=M-1
    A=M
    D=M
    A=A-1
    M=M+D"

[<Literal>]
let SUB_REGEX = @"^sub$"

[<Literal>]
let SUB_ASM = @"
    @SP
    M=M-1
    A=M
    D=M
    A=A-1
    M=M-D"

[<Literal>]
let NEG_REGEX = @"^neg$"

[<Literal>]
let NEG_ASM = @"
    @SP
    A=M-1
    M=-M"


[<Literal>]
let EQ_REGEX = @"^eq$"

[<Literal>]
let EQ_ASM = @"
    @SP
    M=M-1
    A=M
    D=M
    A=A-1
    D=M-D
    @SP
    M=M-1
    @EQ_EQUAL{0}
    D;JEQ
    @SP
    A=M
    M=0
    @EQ_END{0}
    0;JMP
(EQ_EQUAL{0})
    @SP
    A=M
    M=-1
(EQ_END{0})
    @0
    M=M+1"

[<Literal>]
let LT_REGEX = @"^lt$"

[<Literal>]
let LT_ASM = @"
    @0
    M=M-1
    A=M
    D=M
    A=A-1
    D=M-D
    @SP
    M=M-1
    @LT_LT{0}
    D;JLT
    @SP
    A=M
    M=0
    @LT_END{0}
    0;JMP
(LT_LT{0})
    @SP
    A=M
    M=-1
(LT_END{0})
    @SP
    M=M+1"

[<Literal>]
let GT_REGEX = @"^gt$"

[<Literal>]
let GT_ASM = @"
    @SP
    M=M-1
    A=M
    D=M
    A=A-1
    D=M-D
    @SP
    M=M-1
    @GT_GT{0}
    D;JGT
    @SP
    A=M
    M=0
    @GT_END{0}
    0;JMP
(GT_GT{0})
    @SP
    A=M
    M=-1
(GT_END{0})
    @SP
    M=M+1"

[<Literal>]
let AND_REGEX = @"^and$"

[<Literal>]
let AND_ASM = @"
    @SP
    M=M-1
    A=M
    D=M
    A=A-1
    M=M&D"

[<Literal>]
let OR_REGEX = @"^or$"

[<Literal>]
let OR_ASM = @"
    @0
    M=M-1
    A=M
    D=M
    A=A-1
    M=M|D"


[<Literal>]
let EMPTY_REGEX = @"^\s*$"