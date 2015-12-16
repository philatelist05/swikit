\ Run all tests

\ For testing John Hayes "ttester.fth" is used.
\ It may be necessary to adjust the file paths
\ accordingly.

CR .( Running all tests ) CR CR

  S" tester.fs" INCLUDED
  S" stacktest.fs" INCLUDED
  S" swikittest.fs" INCLUDED
  S" operatortest.fs" INCLUDED
  S" interpretertest.fs" INCLUDED


#ERRORS @ 0> [IF]

CR CR .( There were errors in tests: ) #ERRORS @ . .( errors ) CR CR

\ I found no better way to produce exit code 1
\ Unfortunately Gforth does not offer modifying
\ a variable like 'EXIT-CODE'
THROW

[ELSE]

CR CR .( Tests-OK ) CR CR

[THEN]

BYE
