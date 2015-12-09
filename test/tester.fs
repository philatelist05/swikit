INCLUDE ttester.fth

\ Some extensions to John Hayes "ttester.fth" 
VARIABLE #ERRORS 0 #ERRORS !

: HANDLE-ERROR ( c-addr u -- )
   ERROR1 \ displays error message
   #ERRORS @ 1 + #ERRORS ! \ update error count
;

' HANDLE-ERROR ERROR-XT !