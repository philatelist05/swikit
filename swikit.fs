INCLUDE operator.fs


\ Stacks
15 CONSTANT stack-size
CREATE operatorstack stack-size INIT-STACK ,
CREATE operandstack stack-size INIT-STACK ,



: HANDLE-NUMBER ( count c-addr -- )
	0. 2SWAP >NUMBER
	ROT ROT 2DROP
	0 =
	IF \ rest length == 0 -> a fully converted number
		operandstack SWAP PUSH \ push number (single cell) to operand stack
	ELSE \ no number
		THROW
	THEN
;

: INFIX-WID \ push infix wordlist on stack
	ALSO infix-words
	context @
	PREVIOUS
;

: PERFORM-INFIX-CONVERSION ( xt -- )
	R>
	BEGIN
		operatorstack ISEMPTY? ( ? )
		operatorstack POP SWAP OVER DUP ( op-xt ? op-xt op-xt )
		prec R@ prec > R@ is-left? AND ( op-xt ? op-xt ? )
		SWAP prec R@ >= R@ is-right? AND ( op-xt ? ? ?)
		OR AND ( op-xt ? )
	WHILE
		exec
	REPEAT ( op-xt )
	DROP >R operatorstack PUSH
;

: HANDLE-WORD ( count c-addr -- )
	SWAP INFIX-WID SEARCH-WORDLIST ( 0 | xt +-1  )
	IF \ found
		PERFORM-INFIX-CONVERSION
	ELSE \ not found
		THROW
	THEN
;

: TO-INFIX ( count c-addr -- )
	HANDLE-NUMBER
	HANDLE-WORD
;


DEFER interpret-expr
: ` ( -- )
\ Marks the beginning of an infix
\ region.
	BEGIN
		PARSE-WORD 2DUP \ Split input by whitespace
	s" ´" COMPARE WHILE
	interpret-expr
	REPEAT
	EVALUATE \ e.g. ´
;

: ´ ( -- )
	\ Marks the end of an
	\ infix region
	
;

' TO-INFIX IS interpret-expr