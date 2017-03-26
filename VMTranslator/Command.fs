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
let EQ_ASM = @"
    @sp
    A=M
    A=A-1
    D=M
    A=A-1
    @eq
    D=M;JEQ
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
    @lt
    D=M;JLT
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
    @gt
    D=M;JGT
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
let COMMENT_REGEX = @"^(\s*)//*"

[<Literal>]
let EMPTY_REGEX = @"^\s*$"

let CMD_MAP = Map.empty.
                Add(EMPTY_REGEX, "").
                Add(COMMENT_REGEX, "").
                Add(PUSH_CONSTANT_REGEX, PUSH_CONSTANT_ASM).
                Add(ADD_REGEX, ADD_ASM).
                Add(EQ_REGEX,EQ_ASM).
                Add(LT_REGEX,LT_ASM)