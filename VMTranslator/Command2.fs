module Command2

(*A continuation of thee Command1 module*)

[<Literal>]
let LABEL_REGEX = @"^label (.+)(\s*)$"

[<Literal>]
let LABEL_ASM = @"
(LABEL {0}) //added the label sign the differentiate betwwen our labels to the vm labels"

[<Literal>]
let GOTO_REGEX = @"^goto (.+)(\s*)$"

[<Literal>]
let GOTO_ASM = @"
 //goto {0}
    @SP
    D=A
    @LABEL {0}
    0;JMP"

[<Literal>]
let IFGOTO_REGEX = @"^if-goto (.+)(\s*)$"

[<Literal>]
let IFGOTO_ASM = @"
 //if M[SP] == true goto {0}
    @SP
    M=M-1
    A=M
    D=M
    @LABEL {0}
    D;JNE"

[<Literal>]
let FUNCTION_REGEX = @"^function (.+) (\d+)(\s*)$"

//first-filename and func's name,second-num of locals
[<Literal>]
let FUNCTION_ASM = @"
(FUNC {0} START)
    @{1}
    D=A
    @FUNC {0} END BOOT
    0;JEQ
(FUNC {0} BOOT)
    @SP
    A=M
    M=0
    D=D-1
    @SP
    M=M+1
    @FUNC {0} BOOT
    0;JGT
(FUNC {0} END BOOT) //booting the function (insert k 0)"

[<Literal>]
let CALL_REGEX = @"^call (.+) (\d)(\s*)$"

//first-filename and func's name,second-num of variables,third - index to make the return address uniqe
[<Literal>]
let CALL_ASM = @"
 //call {0}
    @FUNC {0} BACK {2}
    D=A
    @SP
    A=M
    M=D
    @SP
    M=M+1
 //push the RA

    @LCL 
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1
 //save the previous LCL
   
    @ARG 
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1
 //save the previous ARG

    @THIS 
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1
 //save the previous THIS

    @THAT 
    D=M
    @SP
    A=M
    M=D
    @SP
    M=M+1
 //save the previous THAT

    @SP
    D=M
    @{1}
    D=D-A
    @5
    D=D-A
    @ARG
    M=D
    @SP
    D=M
    @LCL
    M=D
 //set the new LCL to SP and the new ARG to the pushed args
    
    @FUNC {0} START
    0;JMP
(FUNC {0} BACK {2}) //the RA (added index to differentiate betwwn calls to the same function)
    "

[<Literal>]
let RETURN_REGEX = @"^return$"

[<Literal>]
let RETURN_ASM = @"
 //return 
   @5
   D=A
   @LCL
   A=M
   A=A-D
   D=M
   @ARG
   A=M+1
   M=D
 //set the RA after all the function's args
   
   @SP
   A=M-1
   D=M
   @ARG
   A=M
   M=D
 //take the return value and put it at the new end of the stack (after we delete all the function's args)

   D=A+1
   @SP
   M=D
 //put the SP back aboce the function's args

   @LCL
   A=M-1
   D=M
   @THAT
   M=D
 //restore the THAT pointer

   @2
   D=A
   @LCL
   A=M-D
   D=M
   @THIS
   M=D
 //restore the THIS pointer

   @3
   D=A
   @LCL
   A=M-D
   D=M
   @ARG
   M=D
 //restore the ARG pointer

   @4
   D=A
   @LCL
   A=M-D
   D=M
   @LCL
   M=D
 //restore the LCL pointer

   @SP
   A=M
   A=M
   0;JMP
 //jump back to the RA"