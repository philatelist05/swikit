S" ../swikit.fs" INCLUDED

\ ------------------------------------------------------------------------
TESTING BASIC ASSUMPTIONS

T{ -> }T

\ ------------------------------------------------------------------------
TESTING INTERPRETATION WITH DROP

' 2DROP interpret-word !

T{ ` ´ -> }T
T{ 1 ` ´  -> 1 }T
T{ 1 ` ´ 2 -> 1 2 }T

T{ ` r$dl ´ -> }T
T{ 1 ` r$dl ´ -> 1 }T
T{ 1 ` r$dl ´ 2 -> 1 2 }T

T{ ` aa bb cc ´ -> }T
T{ 1 ` aa bb cc ´ -> 1 }T
T{ 1 ` aa bb cc ´ 2 -> 1 2 }T
