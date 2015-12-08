
0 [IF]
THIS CODE IS TEMPORARILY NOT NEEDED ANYMORE

0 VALUE src_size
0 VALUE src_pointer
0 VALUE fd-in


: SWIKIT ( "filename<quote>" -- )
	R/O OPEN-FILE THROW TO fd-in \ open file
	fd-in FILE-SIZE THROW DROP TO src_size \ get file size)
	src_size CHARS ALLOCATE THROW TO src_pointer \ allocate buffer for source
	src_pointer src_size fd-in READ-FILE
	fd-in close-file THROW \ close file
;

: DEBUG_I/O
	cr src_pointer src_size type
;
[THEN]
\ ------------------------------------------------------------------------
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
