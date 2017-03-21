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
M=M+D"


[<Literal>]
let COMMENT_REGEX = @"^(\s*)//*"

[<Literal>]
let EMPTY_REGEX = @"^\s*$"

let CMD_MAP = Map.empty.
                Add(EMPTY_REGEX, "").
                Add(COMMENT_REGEX, "").
                Add(PUSH_CONSTANT_REGEX, PUSH_CONSTANT_ASM).
                Add(ADD_REGEX, ADD_ASM)