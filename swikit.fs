INCLUDE operator.fs
INCLUDE stack.fs

\ Stacks
15 CONSTANT stack-size
stack-size INIT-STACK VALUE operatorstack
stack-size INIT-STACK VALUE operandstack


: s>number? ( addr u -- d f )
\ Handles bug in original
\ s>number? word
	2DUP S" -" COMPARE
	IF \ not equal
		s>number?
	ELSE
		2DROP 0. FALSE
	THEN
;

: HANDLE-NUMBER ( c-addr count -- ? )
	s>number? NIP DUP ( n ? ? )
	IF
		SWAP operandstack PUSH \ push number (single cell) to operand stack
	ELSE \ no number
		NIP
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
		operandstack operatorstack POP exec
	REPEAT
	R> operatorstack PUSH
;

: HANDLE-WORD ( count c-addr -- )
	LOOKUP-WORD
	IF \ found
		PERFORM-INFIX-CONVERSION
	ELSE \ not found
	\ internal if of throw with message
		s" Unknown infix operation" exception THROW
	THEN
;

: INTERPRET-EXPR  ( count c-addr -- )
	2DUP HANDLE-NUMBER INVERT
	IF
		HANDLE-WORD
	ELSE
		2DROP
	THEN
;


: ` ( -- )
	\ Marks the beginning of an infix
	\ region.

	operatorstack CLEAR
	operandstack CLEAR

	BEGIN
		PARSE-WORD 2DUP \ Split input by whitespace
	s" ´" COMPARE
	WHILE
		INTERPRET-EXPR
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
		operandstack operatorstack POP exec
	REPEAT

	\ pop single result if size <> 0 to data stack
	\ Fixed from loop to only on if
	operandstack SIZE
	IF
		operandstack POP
	THEN
;
