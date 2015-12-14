
: OVERFLOW? ( a-addr -- )
  DUP CELL+ @ \ retrieve capacity
  SWAP @ = THROW \ capacity == offset
;

: UNDERFLOW? ( a-addr -- )
  @ 0 = THROW
;

: INIT-STACK ( n -- a-addr )
\ initializes the stack with capacity of n
	DUP HERE SWAP 2 + CELLS ALLOT >R
  0 R@ ! \ number of current stack elements
  R@ CELL+ ! \ store the stack capacity
  R>
;

: PUSH ( n a-addr -- )
	\ takes a value from the data stack
	\ and pushes it onto the stack
  DUP OVERFLOW?
  1 OVER +! \ offset++
  DUP @ 1+ CELLS + ! \ store n
;

: POP ( a-addr -- n )
  \ takes a value from the stack
  \ and pushes it onto the data stack
  DUP UNDERFLOW?
  DUP DUP @ 1+ CELLS + @ >R \ retrieve element
  DUP @ 1- SWAP ! \ offset--
  R>
;

: SIZE ( a-addr -- n )
  \ pushes the number of elements
  @
;

: ISEMPTY? ( a-addr -- ? )
  SIZE 0 =
;