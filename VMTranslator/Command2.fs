module Command2

(*A continuation of thee Command1 module*)

[<Literal>]
let LABEL_REGEX = @"^label (.+)(\s*)$"

[<Literal>]
let LABEL_ASM = @"
({0})"

[<Literal>]
let GOTO_REGEX = @"^goto (.+)(\s*)$"

[<Literal>]
let GOTO_ASM = @"
    @0
    D=A
    @{0}
    0;JMP"

[<Literal>]
let IFGOTO_REGEX = @"^if-goto (.+)(\s*)$"

[<Literal>]
let IFGOTO_ASM = @"
    @0
    M=M-1
    A=M
    D=M
    @{0}
    D;JNE"
