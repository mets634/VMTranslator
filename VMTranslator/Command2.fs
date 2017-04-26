module Command2

(*A continuation of thee Command1 module*)

[<Literal>]
let LABEL_REGEX = @"^label (.+)(\s*)$"

[<Literal>]
let LABEL_ASM = @"
(LABEL {0})"

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

[<Literal>]
let FUNCTION_REGEX = @"^function (.+)(\s*) (\d+)(\s*)$"

//first-filename and func's name,second-num of locals
[<Literal>]
let FUNCTION_ASM = @"
    @{0} END
    0;jump
(FUNC {0} START)
    @{1}
    D=A
(FUNC {0} BOOT)
    @SP
    A=M
    M=0
    D=D-1
    @SP
    M=M+1
    @FUNC {0} BOOT
    0;JEQ"

[<Literal>]
let CALL_REGEX = @"^call (.+)(\s*) (\d)(\s*)$"

//first-filename and func's name,second-num of variables
[<Literal>]
let CALL_ASM = @"
    @FUNC {0} BACK
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
    D=M-{1}
    @5
    D=D-A
    @ARG
    M=D
    @SP
    D=A
    @LCL
    M=D
    
    @0
    D=A
    @FUNC {0} START
    0;JEQ
(FUNC {0} BACK)
    "

[<Literal>]
let RETURN_REGEX = @"^return$"

[<Literal>]
let RETURN_ASM = @"
   @5
   D=A
   @LCL
   A=M
   A=A-D
   D=M
   @ARG
   A=M
   M=D
   
   @SP
   A=M-1
   D=M
   @ARG
   A=M-1
   M=D

   D=A+1
   @SP
   M=D

   @LCL
   A=M-1
   @THAT
   M=D

   @2
   D=A
   @LCL
   A=M-D
   D=M
   @THIS
   M=D

   @3
   D=A
   @LCL
   A=M-D
   D=M
   @ARG
   M=D

   @4
   D=A
   @LCL
   A=M-D
   D=M
   @LCL
   M=D

   @SP
   D=A
   A=M
   A=M
   0;jEQ
"