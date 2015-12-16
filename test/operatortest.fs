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

\ ------------------------------------------------------------------------
TESTING BASIC ASSUMPTIONS

T{ -> }T

\ ------------------------------------------------------------------------
TESTING OPERATOR IMPLEMENTATIONS

T{ 2 1 >stack plus -> 3 }T
T{ 1 2 >stack plus -> 3 }T
T{ 1 2 >stack minus -> 1 }T
T{ 2 1 >stack minus -> -1 }T
T{ 2 3 >stack mul -> 6 }T
T{ 3 2 >stack mul -> 6 }T
T{ 2 6 >stack div -> 3 }T
T{ 6 2 >stack div -> 0 }T \ integer division


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