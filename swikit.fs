INCLUDE operator.fs
INCLUDE stack.fs

\ Stacks
15 CONSTANT stack-size
stack-size INIT-STACK VALUE operatorstack
stack-size INIT-STACK VALUE operandstack



: HANDLE-NUMBER ( c-addr count -- )
	s>number?
	IF \ rest length == 0 -> a fully converted number
		DROP operandstack PUSH \ push number (single cell) to operand stack
	ELSE \ no number
		2DROP THROW
	THEN
;

: INFIX-WID ( -- wid )
	\ push infix wordlist on stack
	\ do not alter current wordlists
	ALSO infix-words
	context @
	PREVIOUS
;

: LOOKUP-WORD ( count c-addr -- 0 | xt -1 )
	\ search word in infox word list
	\ and return -1 for default compilation
	\ semantics
	INFIX-WID SEARCH-WORDLIST
;


\ implementation of the Shard-Yard algorithm
: PERFORM-INFIX-CONVERSION ( xt -- )
	>R
	BEGIN
		operatorstack ISEMPTY? INVERT ( ? )
		IF
			operatorstack PEEK DUP ( op-xt op-xt )
			prec R@ prec >= R@ is-left? AND ( op-xt ? )
			SWAP prec R@ prec > R@ is-right? AND ( ? ? )
			OR ( ? )
		ELSE \ operatorstack is empty
			FALSE ( ? )
		THEN
	WHILE
		operatorstack POP exec
	REPEAT
	R> operatorstack PUSH
;

: HANDLE-WORD ( count c-addr -- )
	LOOKUP-WORD
	IF \ found
		PERFORM-INFIX-CONVERSION
	ELSE \ not found
		THROW
	THEN
;

: TO-INFIX ( count c-addr -- )
	TRY
		HANDLE-NUMBER
	ENDTRY-IFERROR
		HANDLE-WORD
	THEN
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
	\ pop entire stack and exec
	BEGIN
		operatorstack ISEMPTY? INVERT
	WHILE
		operatorstack POP exec
	REPEAT
;

' TO-INFIX IS interpret-expr
