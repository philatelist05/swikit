S" ../operator.fs" INCLUDED

CR CR

also infix-words


\ ------------------------------------------------------------------------
TESTING BASIC ASSUMPTIONS

T{ -> }T

\ ------------------------------------------------------------------------
TESTING BASIC ARITHMETIC OPERATORS

\ overwrite operator implementations

:NONAME '^' ; IS power
:NONAME '*' ; IS mul
:NONAME '/' ; IS div
:NONAME '+' ; IS plus
:NONAME '-' ; IS minus
:NONAME '(' ; IS p(
:NONAME ')' ; IS p)


T{ ' ^ #params  -> 2 }T
T{ ' ^ is-left?  -> FALSE }T
T{ ' ^ is-right?  -> TRUE }T
T{ ' ^ prec  -> 4 }T
T{ ' ^ exec  ->  '^' }T

T{ ' * #params  -> 2 }T
T{ ' * is-left?  -> TRUE }T
T{ ' * is-right?  -> FALSE }T
T{ ' * prec  -> 3 }T
T{ ' * exec  ->  '*' }T

T{ ' / #params  -> 2 }T
T{ ' / is-left?  -> TRUE }T
T{ ' / is-right?  -> FALSE }T
T{ ' / prec  -> 3 }T
T{ ' / exec  ->  '/' }T

T{ ' + #params  -> 2 }T
T{ ' + is-left?  -> TRUE }T
T{ ' + is-right?  -> FALSE }T
T{ ' + prec  -> 2 }T
T{ ' + exec  ->  '+' }T

T{ ' - #params  -> 2 }T
T{ ' - is-left?  -> TRUE }T
T{ ' - is-right?  -> FALSE }T
T{ ' - prec  -> 2 }T
T{ ' - exec  ->  '-' }T

T{ ' ( #params  -> 0 }T
T{ ' ( is-left?  -> TRUE }T
T{ ' ( is-right?  -> FALSE }T
T{ ' ( prec  -> 0 }T
T{ ' ( exec  ->  '(' }T

T{ ' ) #params  -> 0 }T
T{ ' ) is-left?  -> TRUE }T
T{ ' ) is-right?  -> FALSE }T
T{ ' ) prec  -> 0 }T
T{ ' ) exec  ->  ')' }T

\ ------------------------------------------------------------------------
TESTING RULES

: +stack
	3 INIT-STACK
	DUP
	['] + SWAP PUSH
;


T{ +stack ' ( condition rule1 EXECUTE -> TRUE }T
T{ +stack ' ) condition rule1 EXECUTE -> TRUE }T
T{ +stack ' + condition rule1 EXECUTE -> FALSE }T
T{ +stack ' - condition rule1 EXECUTE -> FALSE }T
T{ +stack ' * condition rule1 EXECUTE -> FALSE }T
T{ +stack ' / condition rule1 EXECUTE -> FALSE }T

T{ +stack ' + condition rule2 EXECUTE -> FALSE }T
T{ +stack ' - condition rule2 EXECUTE -> FALSE }T
T{ +stack ' * condition rule2 EXECUTE -> TRUE }T
T{ +stack ' / condition rule2 EXECUTE -> TRUE }T
T{ +stack ' ^ condition rule2 EXECUTE -> TRUE }T

T{ +stack ' + condition rule3 EXECUTE -> FALSE }T
T{ +stack ' - condition rule3 EXECUTE -> FALSE }T
T{ +stack ' * condition rule3 EXECUTE -> FALSE }T
T{ +stack ' / condition rule3 EXECUTE -> FALSE }T
T{ +stack ' ^ condition rule3 EXECUTE -> FALSE }T

T{ +stack ' + condition rule4 EXECUTE -> TRUE }T
T{ +stack ' - condition rule4 EXECUTE -> TRUE }T
T{ +stack ' * condition rule4 EXECUTE -> TRUE }T
T{ +stack ' / condition rule4 EXECUTE -> TRUE }T
T{ +stack ' ^ condition rule4 EXECUTE -> FALSE }T

T{ +stack ' + condition rule5 EXECUTE -> FALSE }T
T{ +stack ' - condition rule5 EXECUTE -> FALSE }T
T{ +stack ' * condition rule5 EXECUTE -> FALSE }T
T{ +stack ' / condition rule5 EXECUTE -> FALSE }T
T{ +stack ' ^ condition rule5 EXECUTE -> TRUE }T


PREVIOUS