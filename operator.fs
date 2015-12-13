INCLUDE stack.fs

\ associativity
0 CONSTANT left
1 CONSTANT right
2 CONSTANT none

\ offsets for decision table
1 CONSTANT condition
0 CONSTANT action

\ Stacks
15 CONSTANT stack-size
CREATE operatorstack stack-size INIT-STACK ,
CREATE operandstack stack-size INIT-STACK ,


\ Operator implementations
DEFER power

DEFER mul

DEFER div

DEFER plus

DEFER minus

DEFER p(

DEFER p)


: OPERATOR ( n1 n2 n3 n4 --  )
	CREATE , , , ,
	DOES> ( n1 a-addr -- n2 )
	SWAP CELLS + @
;

\ Some helper words
: prec ( op-xt -- n )
	0 SWAP EXECUTE
;

: #params ( op-xt -- n )
	2 SWAP EXECUTE
;

: exec ( op-xt -- )
	3 SWAP EXECUTE
	EXECUTE
;

: is-left? ( op-xt -- ? )
	1 SWAP EXECUTE
	left =
;

: is-right? ( op-xt -- ? )
	1 SWAP EXECUTE
	right =
;



\ Definition of Rules
\ see : http://csis.pace.edu/~wolf/CS122/infix-postfix.htm
: RULE ( xt1 xt2 -- )
	CREATE , ,
	DOES> ( n addr -- )
	SWAP CELLS + @
;


\ Condition ( stack op-xt -- ? )    Action ( stack op-xt -- )
:NONAME SWAP DROP #params 0 = ;     :NONAME exec DROP ;          RULE rule1 \ no params -> directly evaluate
:NONAME prec SWAP POP prec > ;      :NONAME SWAP PUSH ;          RULE rule2 \ prec of incoming symbol > prec top of stack -> push
:NONAME prec SWAP POP prec < ;      :NONAME SWAP POP exec PUSH ; RULE rule3 \ prec of incoming symbol < prec top of stack -> pop top and push incoming
:NONAME SWAP DROP is-left? ;        :NONAME SWAP POP exec PUSH ; RULE rule4 \ equal prec and left-assoc -> pop top and push incoming
:NONAME SWAP DROP is-right? ;       :NONAME SWAP PUSH ;          RULE rule5 \ equal prec and right-assoc -> push


\ func  #params   assoc  precedence          name
' power   2        right      4       OPERATOR ^
' mul     2        left       3       OPERATOR *
' div     2        left       3       OPERATOR /
' plus    2        left       2       OPERATOR +
' minus   2        left       2       OPERATOR -
' p(      0        left       0       OPERATOR (
' p)      0        left       0       OPERATOR )


