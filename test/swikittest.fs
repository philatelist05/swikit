S" ../swikit.fs" INCLUDED

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
TESTING SHARD-YARD ALGORITHM

\ TBD
