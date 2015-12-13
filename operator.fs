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


\ Operator implementations
DEFER power

DEFER mul

DEFER div

DEFER plus

DEFER minus

DEFER p(

DEFER p)



\ Some helper words
DEFER op-prec

DEFER op-#params

DEFER op-exec

DEFER op-isleft?

DEFER op-isright?


\ Definition of Rules
\ see : http://csis.pace.edu/~wolf/CS122/infix-postfix.htm
: RULE ( xt1 xt2 -- )
	CREATE , ,
	DOES> ( n addr -- )
	SWAP CELLS + @
;


\ Condition ( stack op-xt -- ? )    Action ( stack op-xt -- )
:NONAME SWAP DROP op-#params 0 = ;     :NONAME op-exec DROP ;          RULE rule1 \ no params -> directly evaluate
:NONAME op-prec SWAP POP op-prec > ;   :NONAME SWAP PUSH ;             RULE rule2 \ prec of incoming symbol > prec top of stack -> push
:NONAME op-prec SWAP POP op-prec < ;   :NONAME SWAP POP op-exec PUSH ; RULE rule3 \ prec of incoming symbol < prec top of stack -> pop top and push incoming
:NONAME SWAP DROP op-isleft? ;         :NONAME SWAP POP op-exec PUSH ; RULE rule4 \ equal prec and left-assoc -> pop top and push incoming
:NONAME SWAP DROP op-isright? ;        :NONAME SWAP PUSH ;             RULE rule5 \ equal prec and right-assoc -> push


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
' p(      0        left       0       OPERATOR (
' p)      0        left       0       OPERATOR )


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

' prec IS op-prec
' #params IS op-#params
' exec IS op-exec
' is-left? IS op-isleft?
' is-right? IS op-isright?

\ Restore original search order
PREVIOUS

