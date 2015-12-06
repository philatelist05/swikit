10 CONSTANT stack-size


: OVERFLOW? ( a-addr -- )
  @ stack-size = THROW
;

: UNDERFLOW? ( a-addr -- )
  @ 0 = THROW
;

: INIT-STACK ( -- a-addr )
	HERE stack-size 1+ CELLS ALLOT >R
  0 R@ ! \ number of current stack elements
  R>
;

: PUSH ( n a-addr -- )
	\ takes a value from the data stack
	\ and pushes it onto the stack
  DUP OVERFLOW?
  1 OVER +! \ #stack elements ++
  DUP @ CELLS + ! \ store n
;

: POP ( a-addr -- n )
  \ takes a value from the stack
  \ and pushes it onto the data stack
  DUP UNDERFLOW?
  DUP DUP @ CELLS + @ >R \ retrieve element
  DUP @ 1- SWAP ! \ #stack elements --
  R>
;
