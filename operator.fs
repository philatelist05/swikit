\ Define new vocabulary and
\ set in on top of search order
GET-CURRENT
VOCABULARY infix-words ALSO infix-words
DEFINITIONS

INCLUDE stack.fs

\ associativity
0 CONSTANT left
1 CONSTANT right
2 CONSTANT none

\ offsets for decision table
1 CONSTANT condition
0 CONSTANT action



: BINARY ( a-addr -- n1 n2)
	DUP POP SWAP POP SWAP
;


\ operator implementations
: power ( a-addr -- )
	DUP BINARY
	
	1 SWAP ?DUP 
	IF 
		0 
		DO
			OVER * 
		LOOP
	THEN
	NIP
	
	SWAP PUSH
;

: mul ( a-addr -- )
	DUP BINARY *
	SWAP PUSH
;

: div ( a-addr -- )
	DUP BINARY /
	SWAP PUSH
;

: plus ( a-addr -- )
	DUP BINARY +
	SWAP PUSH
;

: minus ( a-addr -- )
	DUP BINARY -
	SWAP PUSH
;



: OPERATOR ( n1 n2 n3 n4 --  )
	CREATE , , , ,
	DOES> ( n1 a-addr -- n2 )
	SWAP CELLS + @
;


\ func  #params   assoc  precedence          name
' power   2        right      4       OPERATOR ^
' mul     2        left       3       OPERATOR *
' div     2        left       3       OPERATOR /
' plus    2        left       2       OPERATOR +
' minus   2        left       2       OPERATOR -


\ Restore original compilation wordlist
SET-CURRENT

: prec \ ( op-xt -- n )
	0 SWAP EXECUTE
;

: #params \ ( op-xt -- n )
	2 SWAP EXECUTE
;

: exec \ ( op-xt -- )
	3 SWAP EXECUTE
	EXECUTE
;

: is-left? \ ( op-xt -- ? )
	1 SWAP EXECUTE
	left =
;

: is-right? \ ( op-xt -- ? )
	1 SWAP EXECUTE
	right =
;

\ Restore original search order
PREVIOUS
