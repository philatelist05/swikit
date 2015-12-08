\ Run all tests

\ For testing John Hayes "ttester.fth" is used.
\ It may be necessary to adjust the file paths
\ accordingly.

CR .( Running all tests ) CR

  S" ttester.fth" INCLUDED
  S" stacktest.fs" INCLUDED
  S" swikittest.fs" INCLUDED

CR CR .( tests completed ) CR CR
