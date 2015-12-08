
\ Define new vocabulary and
\ set in on top of search order
GET-CURRENT
VOCABULARY infix-words ALSO infix-words
DEFINITIONS

INCLUDE operator.fs

\ Restore original compilation wordlist
SET-CURRENT

\ Holds the address of the expression parser
VARIABLE interpret-word

: ` ( -- )
\ Marks the beginning of an infix
\ region.
	BEGIN
		PARSE-WORD 2DUP \ Split input by whitespace
	s" ´" COMPARE WHILE
	interpret-word @ EXECUTE
	REPEAT
	EVALUATE \ e.g. ´
;

: ´ ( -- )
	\ Marks the end of an
	\ infix region
;

\ Restore original search order
PREVIOUS
