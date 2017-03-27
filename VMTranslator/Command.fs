module Command

[<Literal>]
let PUSH_CONSTANT_REGEX = @"^push constant (\d+)(\s*)$"

[<Literal>]
let PUSH_CONSTANT_ASM = @"@{0}
D=A
@0
A=M
M=D
@0
M=M+1"

[<Literal>]
let ADD_REGEX = @"^(\s*)add(\s*)$"

[<Literal>]
let ADD_ASM = @"@0
M=M-1
A=M
D=M
A=A-1
M=M+D
"

[<Literal>]
let EQ_REGEX = @"^eq$"

[<Literal>]
let EQ_ASM = @"
    @sp
    A=M
    A=A-1
    D=M
    A=A-1
    D=D-M
    @eq
    D;JEQ
    @0
    D=A
    @final
    0;JMP
(eq)
    @-1
    D=A
(final)
    @sp
    A=M
    M=D
"
[<Literal>]
let LT_REGEX = @"^lt$"

[<Literal>]
let LT_ASM = @"
    @sp
    A=M
    A=A-1
    D=M
    A=A-1
    D=D-M
    @lt
    D;JLT
    @0
    D=A
    @final
    0;JMP
(lt)
    @-1
    D=A
(final)
    @sp
    A=M
    M=D
"
[<Literal>]
let GT_REGEX = @"^gt$"

[<Literal>]
let GT_ASM = @"
    @sp
    A=M
    A=A-1
    D=M
    A=A-1
    D=D-M
    @gt
    D;JGT
    @0
    D=A
    @final
    0;JMP
(gt)
    @-1
    D=A
(final)
    @sp
    A=M
    M=D
"
[<Literal>]
let SUB_REGEX = @"^sub$"

[<Literal>]
let SUB_ASM = @"
    @sp
    A=M
    A=A-1
    D=M
    A=A-1
    D=D-M
    A=A+2
    M=D
"
[<Literal>]
let AND_REGEX = @"^and$"

[<Literal>]
let AND_ASM = @"
"

[<Literal>]
let OR_REGEX = @"^or$"

[<Literal>]
let OR_ASM = @"
"

[<Literal>]
let NEG_REGEX = @"^neg$"

[<Literal>]
let NEG_ASM = @"
"

[<Literal>]
let COMMENT_REGEX = @"^(\s*)//*"

[<Literal>]
let EMPTY_REGEX = @"^\s*$"

let CMD_MAP = Map.empty.
                Add(EMPTY_REGEX, "").
                Add(COMMENT_REGEX, "").
                Add(PUSH_CONSTANT_REGEX, PUSH_CONSTANT_ASM).
                Add(ADD_REGEX, ADD_ASM).
                Add(EQ_REGEX,EQ_ASM).
                Add(LT_REGEX,LT_ASM).
                Add(GT_REGEX,GT_ASM).
                Add(SUB_REGEX,SUB_ASM).
                Add(AND_REGEX,AND_ASM).
                Add(OR_REGEX,OR_ASM).
                Add(NEG_REGEX,NEG_ASM)