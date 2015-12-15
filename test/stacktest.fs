INCLUDE ../stack.fs

\ ------------------------------------------------------------------------
TESTING BASIC ASSUMPTIONS

T{ -> }T

\ ------------------------------------------------------------------------
TESTING EMPTY STACK

0 INIT-STACK VALUE s0
: catchpop ['] POP CATCH DROP ;
: catchpush ['] PUSH CATCH DROP ;

T{ s0 SIZE -> 0 }T
T{ s0 catchpop -> s0 }T
T{ 9 s0 catchpush -> 9 s0 }T

\ ------------------------------------------------------------------------
TESTING STACK WITH ONE ELEMENT

1 INIT-STACK VALUE s1

T{ 9 s1 PUSH -> }T
T{ s1 SIZE -> 1 }T

T{ s1 POP -> 9 }T
T{ s1 SIZE -> 0 }T

\ ------------------------------------------------------------------------
TESTING LIFO ORDER

3 INIT-STACK VALUE s3

T{ 10 s3 PUSH -> }T
T{ 11 s3 PUSH -> }T
T{ 12 s3 PUSH -> }T

T{ s3 SIZE -> 3 }T
T{ s3 POP -> 12 }T
T{ s3 POP -> 11 }T
T{ s3 POP -> 10 }T

\ ------------------------------------------------------------------------
TESTING STORING ADDRESSES

1 INIT-STACK VALUE s1-addr

HERE CONSTANT addr

T{ addr s1-addr PUSH -> }T
T{ s1-addr SIZE -> 1 }T
T{ s1-addr POP -> addr }T

\ ------------------------------------------------------------------------
TESTING ISEMPTY

1 INIT-STACK VALUE s-empty

1 INIT-STACK VALUE s-nonempty
1 s-nonempty PUSH

T{ s-empty ISEMPTY? -> TRUE }T
T{ s-nonempty ISEMPTY? -> FALSE }T

\ ------------------------------------------------------------------------
TESTING CLEAR

2 INIT-STACK VALUE s2
0 s2 PUSH
1 s2 PUSH

T{ s2 SIZE -> 2 }T
T{ s2 CLEAR s2 SIZE -> 0 }T
T{ s2 catchpop -> s2 }T
