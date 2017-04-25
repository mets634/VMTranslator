module Command2

[<Literal>]
let LABEL_REGEX = @"^label (.+)(\s*)$"

[<Literal>]
let LABEL_ASM = @"
({0}{1})"


[<Literal>]
let FUNCTION_REGEX = @"^function (.+)(\s*) (\d+)(\s*)$"

[<Literal>]
let FUNCTION_ASM = @"
    @{0}{1}END
    0;jump
({0}{1}START)
    @0
    D=A
({0}{1}BOOT)
    @SP
    A=M
    M=0
    D=D-1
    @SP
    M=M+1
    @{0}{1}BOOT
    0;JEQ"

[<Literal>]
let CALL_REGEX = @"^call (.+)(\s*) (\d)(\s*)$"

[<Literal>]
let CALL_ASM = @"
    @{0}{1}BACK
    D=A
    @SP
    A=M
    M=D
    @SP
    M=M+1
    @LCL 
    D=A
    @SP
    A=M
    M=D
    @SP
    M=M+1
    @ARG 
    D=A
    @SP
    A=M
    M=D
    @SP
    M=M+1
    @THIS 
    D=A
    @SP
    A=M
    M=D
    @SP
    M=M+1
    @THAT 
    D=A
    @SP
    A=M
    M=D
    @SP
    M=M+1

    @SP
    D=M-{2}
    D=D-5
    @ARG
    M=D
    @SP
    D=A
    @LCL
    M=D
    
    @{0}{1}START
    0;jump
    "

[<Literal>]
let RETURN_REGEX = @"^return$"

[<Literal>]
let RETURN_ASM = @"
"