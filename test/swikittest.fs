INCLUDE ../swikit.fs
\ ------------------------------------------------------------------------
\ Some utils for easier testing


: stack> ( a-addr -- n1 ... nj )
	>R
	BEGIN
		R@ ISEMPTY? INVERT
	WHILE
		R@ POP
	REPEAT
		RDROP
;

: >operatorstack ( xt1 ... xtj -- )
	operatorstack CLEAR
	BEGIN
		DEPTH 0 >
	WHILE
		operatorstack PUSH
	REPEAT
;

: >operandstack ( n1 ... nj -- )
	operandstack CLEAR
	BEGIN
		DEPTH 0 >
	WHILE
		operandstack PUSH
	REPEAT
;

: operandstack> ( -- n1 ... nj )
	operandstack stack>
;

: operatorstack> ( -- xt1 ... xtj )
	operatorstack stack>
;

DEFER add
DEFER substract
DEFER multiply
DEFER divide
DEFER topower



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

T{ s" 1" HANDLE-NUMBER operandstack> -> 1 }T
T{ s" 0" HANDLE-NUMBER operandstack> -> 0 }T
T{ s" -1" HANDLE-NUMBER operandstack> -> -1 }T
T{ s" -999" HANDLE-NUMBER operandstack> -> -999 }T
T{ s" 999" HANDLE-NUMBER operandstack> -> 999 }T

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
TESTING PERFORMING INFIX CONVERSION PUSH TO OPERATOR STACK

ALSO infix-words
' + IS add
' - IS substract
' * IS multiply
' / IS divide
' ^ IS topower
PREVIOUS


\ emtpy operator stack -> push to operator stack
T{	>operatorstack 
	' add PERFORM-INFIX-CONVERSION operatorstack> -> ' add }T
T{	>operatorstack 
	' substract PERFORM-INFIX-CONVERSION operatorstack> -> ' substract }T
T{	>operatorstack 
	' multiply PERFORM-INFIX-CONVERSION operatorstack> -> ' multiply }T
T{	>operatorstack 
	' divide PERFORM-INFIX-CONVERSION operatorstack> -> ' divide }T
T{	>operatorstack 
	' topower PERFORM-INFIX-CONVERSION operatorstack> -> ' topower }T



\ emtpy operator stack -> nothing to execute
T{	>operatorstack 
	' add PERFORM-INFIX-CONVERSION -> }T
T{	>operatorstack 
	' substract PERFORM-INFIX-CONVERSION -> }T
T{	>operatorstack 
	' multiply PERFORM-INFIX-CONVERSION -> }T
T{	>operatorstack 
	' divide PERFORM-INFIX-CONVERSION -> }T
T{	>operatorstack 
	' topower PERFORM-INFIX-CONVERSION -> }T


\ both operators are left associative but different precedence
T{	' add >operatorstack
	' multiply PERFORM-INFIX-CONVERSION operatorstack> -> ' multiply ' add }T
T{	' add >operatorstack
	' divide PERFORM-INFIX-CONVERSION operatorstack> -> ' divide ' add }T

\ left and right associative
T{	' add >operatorstack
	' topower PERFORM-INFIX-CONVERSION operatorstack> -> ' topower ' add }T
	
\ both operators are left associative but different precedence
T{	' substract >operatorstack
	' multiply PERFORM-INFIX-CONVERSION operatorstack> -> ' multiply ' substract }T
T{	' substract >operatorstack
	' divide PERFORM-INFIX-CONVERSION operatorstack> -> ' divide ' substract }T

\ left and right associative
T{	' substract >operatorstack
	' topower PERFORM-INFIX-CONVERSION operatorstack> -> ' topower ' substract }T

\ both operators are right associative with same precedence
T{	' topower >operatorstack
	' topower PERFORM-INFIX-CONVERSION operatorstack> -> ' topower ' topower }T

\ ------------------------------------------------------------------------
TESTING PERFORMING INFIX CONVERSION EXECUTE OPERATOR

\ left associative and equal precendence
T{	' add >operatorstack
	2 1 >operandstack
	' add PERFORM-INFIX-CONVERSION operatorstack> -> ' add }T
T{	' add >operatorstack
	2 1 >operandstack
	' add PERFORM-INFIX-CONVERSION operandstack> -> 3 }T

T{	' add >operatorstack
	2 1 >operandstack
	' substract PERFORM-INFIX-CONVERSION operatorstack> -> ' substract }T
T{	' add >operatorstack
	2 1 >operandstack
	' substract PERFORM-INFIX-CONVERSION operandstack> -> 3 }T

\ left associative and less precendence
T{	' multiply >operatorstack
	2 1 >operandstack
	' add PERFORM-INFIX-CONVERSION operatorstack> -> ' add }T
T{	' multiply >operatorstack
	2 1 >operandstack
	' add PERFORM-INFIX-CONVERSION operandstack> -> 2 }T

T{	' multiply >operatorstack
	2 1 >operandstack
	' substract PERFORM-INFIX-CONVERSION operatorstack> -> ' substract }T
T{	' multiply >operatorstack
	2 1 >operandstack
	' substract PERFORM-INFIX-CONVERSION operandstack> -> 2 }T

\ ------------------------------------------------------------------------
TESTING END OF INFIX REGION

\ empty operatorstack and result on data stack
T{	' multiply ' substract >operatorstack
	3 2 1 >operandstack
	´ operatorstack> -> -5 }T


