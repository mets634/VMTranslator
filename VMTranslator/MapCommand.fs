module MapCommand

open Command1
open Command2

let CMD_MAP = Map.empty.
                Add(EMPTY_REGEX, "").
                Add(PUSH_CONSTANT_REGEX, PUSH_CONSTANT_ASM).
                Add(ADD_REGEX, ADD_ASM).
                Add(SUB_REGEX, SUB_ASM).
                Add(NEG_REGEX, NEG_ASM).
                Add(EQ_REGEX,EQ_ASM).
                Add(LT_REGEX,LT_ASM).
                Add(GT_REGEX, GT_ASM).
                Add(AND_REGEX, AND_ASM).
                Add(OR_REGEX, OR_ASM).
                Add(NOT_REGEX,NOT_ASM).
                Add(BASIC_POP_REGEX,BASIC_POP_ASM).
                Add(BASIC_PUSH_REGEX,BASIC_PUSH_ASM).
                Add(POP_TEMP_STATIC_REGEX,POP_TEMP_STATIC_ASM).
                Add(PUSH_TEMP_STATIC_REGEX,PUSH_TEMP_STATIC_ASM).
                Add(LABEL_REGEX,LABEL_ASM).
                Add(GOTO_REGEX, GOTO_ASM).
                Add(IFGOTO_REGEX, IFGOTO_ASM).
                Add(FUNCTION_REGEX,FUNCTION_ASM).
                Add(CALL_REGEX,CALL_ASM).
                Add(RETURN_REGEX,RETURN_ASM)
