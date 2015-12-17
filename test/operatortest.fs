INCLUDE ../operator.fs

CR CR

also infix-words

\ some helper words
: >stack \ ( n1 ... nj -- a-addr )
	DEPTH INIT-STACK >R
	BEGIN
		DEPTH 0 >
	WHILE
		R@ PUSH
	REPEAT
	R>
;

: stack> \ ( a-addr -- n1 ... nj )
	>R
	BEGIN
		R@ ISEMPTY? INVERT
	WHILE
		R@ POP
	REPEAT
		RDROP
;

\ ------------------------------------------------------------------------
TESTING BASIC ASSUMPTIONS

T{ -> }T

\ ------------------------------------------------------------------------
TESTING OPERATOR IMPLEMENTATIONS

T{ 2 1 >stack DUP plus stack> -> 3 }T
T{ 1 2 >stack DUP plus stack> -> 3 }T
T{ 1 2 >stack DUP minus stack> -> 1 }T
T{ 2 1 >stack DUP minus stack> -> -1 }T
T{ 2 3 >stack DUP mul stack> -> 6 }T
T{ 3 2 >stack DUP mul stack> -> 6 }T
T{ 2 6 >stack DUP div stack> -> 3 }T
T{ 6 2 >stack DUP div stack> -> 0 }T \ integer division
T{ 3 2 >stack DUP power stack> -> 8 }T
T{ 0 2 >stack DUP power stack> -> 1 }T
T{ 2 0 >stack DUP power stack> -> 0 }T
T{ 1 2 >stack DUP power stack> -> 2 }T
T{ 1 -2 >stack DUP power stack> -> -2 }T
T{ 3 -2 >stack DUP power stack> -> -8 }T
T{ 0 0 >stack DUP power stack> -> 1 }T


\ ------------------------------------------------------------------------
TESTING BASIC ARITHMETIC OPERATORS


T{ ' ^ #params  -> 2 }T
T{ ' ^ is-left?  -> FALSE }T
T{ ' ^ is-right?  -> TRUE }T
T{ ' ^ prec  -> 4 }T

T{ ' * #params  -> 2 }T
T{ ' * is-left?  -> TRUE }T
T{ ' * is-right?  -> FALSE }T
T{ ' * prec  -> 3 }T

T{ ' / #params  -> 2 }T
T{ ' / is-left?  -> TRUE }T
T{ ' / is-right?  -> FALSE }T
T{ ' / prec  -> 3 }T

T{ ' + #params  -> 2 }T
T{ ' + is-left?  -> TRUE }T
T{ ' + is-right?  -> FALSE }T
T{ ' + prec  -> 2 }T

T{ ' - #params  -> 2 }T
T{ ' - is-left?  -> TRUE }T
T{ ' - is-right?  -> FALSE }T
T{ ' - prec  -> 2 }T

T{ ' ( #params  -> 0 }T
T{ ' ( is-left?  -> TRUE }T
T{ ' ( is-right?  -> FALSE }T
T{ ' ( prec  -> 0 }T

T{ ' ) #params  -> 0 }T
T{ ' ) is-left?  -> TRUE }T
T{ ' ) is-right?  -> FALSE }T
T{ ' ) prec  -> 0 }T

PREVIOUS