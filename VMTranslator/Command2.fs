module Command2

[<Literal>]
let LABEL_REGEX = @"label (.+)(\s*)$"

[<Literal>]
let LABEL_ASM = @"
    ({0}{1})"
