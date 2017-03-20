module Command

[<Literal>]
let PUSH_CONSTANT_REGEX = @"^push constant (\d+)(\s*)$"

[<Literal>]
let PUSH_CONSTANT_ASM = @"@{0}
D=A
@sp
A=M
M=D
@sp
M=M+1
"

[<Literal>]
let ADD_REGEX = @"^add(\s*)$"

[<Literal>]
let ADD_ASM = @"@sp
M=M-1
A=M
D=M
A=A-1
M=M+D
"


[<Literal>]
let COMMENT_REGEX = @"^(\s*)//*"

[<Literal>]
let EMPTY_REGEX = @"^\s*$"

let CMD_MAP = Map.empty.
                Add(EMPTY_REGEX, "").
                Add(COMMENT_REGEX, "").
                Add(PUSH_CONSTANT_REGEX, PUSH_CONSTANT_ASM).
                Add(ADD_REGEX, ADD_ASM)