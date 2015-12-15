INCLUDE ../swikit.fs
\ ------------------------------------------------------------------------
\ Some utils for easier testing

: >datastack ( a-addr -- ... )
	>R
	BEGIN
		R@ ISEMPTY? INVERT
	WHILE
		R@ POP
	REPEAT
		RDROP
;

\ ------------------------------------------------------------------------
TESTING BASIC ASSUMPTIONS

T{ -> }T

\ ------------------------------------------------------------------------
TESTING INTERPRETATION WITH DROP

' 2DROP IS interpret-expr

T{ ` ´ -> }T
T{ 1 ` ´  -> 1 }T
T{ 1 ` ´ 2 -> 1 2 }T

T{ ` r$dl ´ -> }T
T{ 1 ` r$dl ´ -> 1 }T
T{ 1 ` r$dl ´ 2 -> 1 2 }T

T{ ` aa bb cc ´ -> }T
T{ 1 ` aa bb cc ´ -> 1 }T
T{ 1 ` aa bb cc ´ 2 -> 1 2 }T

\ ------------------------------------------------------------------------
TESTING INTERPRETATION WITH EVALUATE

' EVALUATE IS interpret-expr

T{ ` ´ -> }T
T{ 1 ` ´  -> 1 }T
T{ 1 ` ´ 2 -> 1 2 }T

T{ ` 99 ´ -> 99 }T
T{ 1 ` 99 ´ -> 1 99 }T
T{ 1 ` 99 ´ 2 -> 1 99 2 }T

T{ ` 11 22 33 ´ -> 11 22 33 }T
T{ 1 ` 11 22 33 ´ -> 1 11 22 33 }T
T{ 1 ` 11 22 33 ´ 2 -> 1 11 22 33 2 }T

T{ 1 2 ` swap ´ -> 2 1 }T
T{ 1 2 ` swap ´ 3 -> 2 1 3 }T

\ ------------------------------------------------------------------------
TESTING NUMBER CONVERSION

T{ s" 1" HANDLE-NUMBER operandstack >datastack -> 1 }T
T{ s" 0" HANDLE-NUMBER operandstack >datastack -> 0 }T
T{ s" -1" HANDLE-NUMBER operandstack >datastack -> -1 }T
T{ s" -999" HANDLE-NUMBER operandstack >datastack -> -999 }T
T{ s" 999" HANDLE-NUMBER operandstack >datastack -> 999 }T

\ ------------------------------------------------------------------------
TESTING WORD LOOKUP


T{ s" abc" LOOKUP-WORD -> FALSE }T
T{ s"  " LOOKUP-WORD -> FALSE }T
T{ s" dummy" LOOKUP-WORD -> FALSE }T
T{ s" +" LOOKUP-WORD SWAP DROP -> TRUE }T
T{ s" -" LOOKUP-WORD SWAP DROP -> TRUE }T
T{ s" *" LOOKUP-WORD SWAP DROP -> TRUE }T
T{ s" /" LOOKUP-WORD SWAP DROP -> TRUE }T
T{ s" ^" LOOKUP-WORD SWAP DROP -> TRUE }T
T{ s" power" LOOKUP-WORD SWAP DROP -> TRUE }T
T{ s" mul" LOOKUP-WORD SWAP DROP -> TRUE }T
T{ s" div" LOOKUP-WORD SWAP DROP -> TRUE }T
T{ s" plus" LOOKUP-WORD SWAP DROP -> TRUE }T
T{ s" minus" LOOKUP-WORD SWAP DROP -> TRUE }T

\ ------------------------------------------------------------------------
TESTING PERFORMING INFIX CONVERSION

DEFER add
DEFER substract
DEFER multiply
DEFER divide
DEFER topower

ALSO infix-words
' + IS add
' - IS substract
' * IS multiply
' / IS divide
' ^ IS topower
PREVIOUS


\ emtpy operator stack -> push to operator stack
T{	operatorstack CLEAR 
	' add PERFORM-INFIX-CONVERSION operatorstack SIZE -> 1 }T
T{	operatorstack CLEAR 
	' substract PERFORM-INFIX-CONVERSION operatorstack SIZE -> 1 }T
T{	operatorstack CLEAR 
	' multiply PERFORM-INFIX-CONVERSION operatorstack SIZE -> 1 }T
T{	operatorstack CLEAR 
	' divide PERFORM-INFIX-CONVERSION operatorstack SIZE -> 1 }T
T{	operatorstack CLEAR 
	' topower PERFORM-INFIX-CONVERSION operatorstack SIZE -> 1 }T



\ emtpy operator stack -> nothing to execute
T{	operatorstack CLEAR 
	' add PERFORM-INFIX-CONVERSION -> }T
T{	operatorstack CLEAR 
	' substract PERFORM-INFIX-CONVERSION -> }T
T{	operatorstack CLEAR 
	' multiply PERFORM-INFIX-CONVERSION -> }T
T{	operatorstack CLEAR 
	' divide PERFORM-INFIX-CONVERSION -> }T
T{	operatorstack CLEAR 
	' topower PERFORM-INFIX-CONVERSION -> }T

