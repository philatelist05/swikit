\ Operator implementations
: power
;

: mul
;

: div
;

: plus
;

: minus
;

: OPERATOR ( n1 n2 n3 n4 --  )
	CREATE , , , ,
	DOES> ( n1 a-addr -- n2 )
	SWAP CELLS + @
;

\ associativity
0 CONSTANT left
1 CONSTANT right
2 CONSTANT none

\ func  #params   assoc  precedence          name
' power  2        right      4       OPERATOR ^
' mul    2        left       3       OPERATOR *
' div    2        left       3       OPERATOR /
' plus   2        left       2       OPERATOR +
' minus  2        left       2       OPERATOR -


\ Syntactic sugar
: prec ( xt -- n )
  0 SWAP EXECUTE
;

: #params ( xt -- n )
  2 SWAP EXECUTE
;

: exec ( xt -- ... )
  3 SWAP EXECUTE
;

: is-left? ( xt -- ? )
  1 SWAP EXECUTE
  left =
;

: is-right? ( xt -- ? )
  1 SWAP EXECUTE
  right =
;
