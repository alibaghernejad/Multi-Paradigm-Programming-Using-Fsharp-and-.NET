
module FSharpBook.Chapter3 

let x = 10
let y = 5 + x
//---
let myVALUE = 13
let myvalue = 14
let MYVALUE = 15
//---
let 2dayValue = 20
//---
let ``2dayValue`` = 20
let ``static`` = 2.5

//Unicode identifiers.
let مقداراول = 22
let مقداردوم = 14
let مجموع = مقداراول + مقداردوم
//---
let val1, val2, val3, val4 = 'A', 43, "FSharp", 1.55
//---
let val1, val2, val3, val4 =
    'A', 43, "FSharp", 1.55
//---
let number = 2
//---
let number = 22
//---
let firstName : string = "alex"      
let lastName : string = "patta"   
let age : int = 37

// Get type of values.
let intValue = 263
let charValue : char = 's'
let floatValue = 25.6
intValue.GetType()
charValue.GetType() 
floatValue.GetType() 
floatValue.GetType() = typeof<float>
intValue.GetType() = typeof<char>

//==============================================================================
// Define integer numeric values.
let worldPopulation = 7051311886L
let personAge = 24uy
let roomTemperature = -88s
let fileSize = 3145725
// Define floating-point numeric values.
let average = 18.5f
let pi = 3.14159265358979323846M
let yearlySalary = 82.25
//
let int16Value = 32768s

// Access the smallest and largest possible value of types.
open System
let minValue = Int16.MinValue
let maxValue = Int16.MaxValue

// Hexadeciml,Octal and Binary define integer values.
let unsignedValue:uint16 = -2436s
let hexValue = 0xFFFF
let octalValue = 0o4030607062501L
let binaryValue = 0b00110100s

// Hexadeciml,Octal and Binary define floating-point values.
let hexValue = 0x0FA0LF
let octalValue = 0o46715LF
let binaryValue = 0b01001010lf

// bigint data type
open System.Numerics
let positiveValue = 8053063624326025511100I
let negativeValue = -45681001425555302120I

// Based on IEEE-754 standards float and float32 data types dont have exact representation of values.
let num1 = 0.33333333333
let num2 = 3.0
let result= num1 * num2

// Decimal data type.
let num1 = 0.33333333333m
let num2 = 3.0m
let result= num1 * num2

// Scientific representation of floating-point values.
System.Single.Epsilon
System.Single.MinValue
System.Double.MaxValue

// Special states in floating-point values.
let floatValue = 0.0 / 0.0
let float32Value = 0.0f / 0.0f
let positiveInfinity = 80.0 / 0.0
let negativeInfinity = -80.0 / 0.0
let positiveInfinityf = 426.0f / 0.0f
let negativeInfinityf = -426.0f / 0.0f

// bool data type. 
let automaticBackupSupported = true
let unlimitedBandwidth = false
let dedicatedIPAddress = true
let microsoftAjaxSupported = true
//==============================================================================
// char Data type
let firstAlphabeticChar = 'A'
let lastAlphabeticChar = 'Z'
let digitEight = '8'
let dollarSign = '$'
let persianLetterGhain = 'غ'

// Get byte representation of char values.
let aSCIICodeOfA = 'A'B
let aSCIICodeOfZ = 'Z'B

// Specify characters in decimal & hexadecimal notation.
let firstAlphabeticCharA = '\065'
let firstAlphabeticCharB = '\x41'
let lastAlphabeticCharA = '\090'
let lastAlphabeticCharB = '\x5A'

// UTF-16 ad UTF-32 representation char values.
let firstAlphabeticChar = '\u0041'
let lastAlphabeticChar = '\U0000005A'

// Error: bacause Char is a 2-byte data type.
let musicalSymbolGClef = '\U0001D11E'

// Escape sequences
let backslash = '\' 
let backslash , anotherBackslash = '\\' , '\u005C'
//==============================================================================
// String data type.
let firstName = "John"
let lastName = "Smith"
let jobTitle = "Software Engineer"
let emailAddress = "JohnSmith_123@example.com"
let currentLocation = "ایران"
let notes = ""

// Empty Strings.
open System
let emptyString = String.Empty

// Multiline Support.
let multilineStringA = "first line string.
second line string.
    third line string."
let multilineStringB = "first line string. \nsecond line string. \n    third line string."

// Long Strings
let longString = "This is first line string.\
This is first line string too."

// ASCII, UTF-16 and UTF-32 representation of char values.
let fsharpA = "\070\x23"
let fsharpB = "\u0046\u0023"
let fsharpC = "\U00000046\U00000023"

// Get byte representation of string values.
let byteArray = "my string"B

// Escape sequences in string values.
let sampleString  = "escape \" quotation"
let registryKey = "HKEY_CURRENT_USER\\Control Panel"
let escapeSequences = 
        "...\\Backslash\\...\"Quotation\"...\'Apostrophe\'\n...Newline...\tTab\t..."
let utf16EscapeSequences = "...\u005CBackslash\u005C...\u0022Quotation\u0022...\u0027Apostrophe\u0027\u000A...Newline...\u0009Tab\u0009..."

// Normal strings vs Verbatim Strings.
let normalString = "C:\temp\newfolder\test.tmp" (* \t means tab,\n means newline*)
let verbatimString = @"C:\temp\newfolder\test.tmp" (* \t,\n are not special*)

// Double Quotation in Verbatim Strings.
let str = @"a string with ""quotes"" in middle"

// Triple-Quated strings.
let xaml = """
    <Window x:Class="IRCClient.MainWindow"
            xmlns="http://schemas.microsoft.com/winfx/2006/xaml/presentation"
            xmlns:x="http://schemas.microsoft.com/winfx/2006/xaml"
            Title="Alsasoft IRC Client" Height="398.503" Width="650.843">
        <Grid Margin="0,0,0,0">
        </Grid>
    </Window>"""

// Concatenate strings.
let str1 = "Visual "
let str2 = "F#"
let result = str1 + str2
let tripleQuotedString= """Three """ + "\"\"\"" + """ Quote """  
                                        + "\"\"\"" + """ String"""
let myFavoriteColor = "green"
let message = "My favorite color is " + myFavoriteColor
let result = str1 ^ str2
// Disable FS0062 warning.
#nowarn "62"
let result = str1 ^ str2

// Get length of strings.
let str1 = "sample string"
let str2 = "someone_123@example.com"
let str1Length = str1.Length
let str2Length = String.length str2
let str3Length = str3.Length

// Access to specified characters of strings values.
let str = "my string";;
let firstCharacter = str.[0]
let seventhCharacter = str.Chars(6)
let lastCharacter = str.[str.Length - 1]

// Substrings.
let strSample = "Microsoft Visual F#";;
// Accesses characters from 0 to 8.
strSample.[0..8]
// Accesses characters from the beginning of the string to 8.
strSample.[..8]
// Accesses characters from 10 to the end of the string.
strSample.[10..]
// Accesses all characters of the string.
strSample.[*]
// Compare Strings.
compare "ADAM" "adam"
compare "Sarah" "SaRAH"
compare "David" "David"

// Case Insensitive compare of strings.
let strName1 = "ADAM"
let strName2 = "adam"
compare strName1 strName2
// convert strings to uppercase and compare them.
compare (strName1.ToUpper()) (strName2.ToUpper())
// convert strings to lowercase and compare them.
compare (strName1.ToLower()) (strName2.ToLower())

//==============================================================================
// Arithmetic operators.
let w , x , y , z = 20.0 , 5 , 14 , 2.0f;;
x + y
x + y - 24
x * y + (4 - 2)
w / 3.0
y % 3
z ** 4.0f

// Type mismatch error in arithmetic operators.
let num1 , num2 = 4.5 , 6
let sum = num1 + num2

// Preorder use of + and - operators.
let x , y = 14 , -9
let r1 , r2 = -x , +y

// Overflow and Underflow in arithmetic operators.
let x = 32767s + 1s
let y = x - 1s

// Checked version of arithmetic operators.
open Checked
let x = 32767s + 1s

// Boolean operators.
let conditionA = true
let conditionB = false
not conditionA
conditionA || conditionB
conditionA && conditionB

// Short-Circuited evaluation in boolean expressions.
let value1 , value2 = false , true
(value1) && (not value1) (*Do not need to evaluate (not a) in expression.*)
(value2) || (not value1) (*Do not need to evaluate (not a) in expression.*)

// Bitwise operators.
0x1234ABCD &&& 0x0000FFFF
0xABCD1234 ||| 0x0000FFFF
0x12345678 ^^^ 0x0000FFFF
~~~ 0x0000FFFF000FFFF0000L
0b10101010uy <<< 3
0b11110000us >>> 2
0101uy &&& 0011uy
8046us ^^^ 1100us

//  Comparison operators.
let num1, num2, str1, str2 = 64, 18, "Jack", "David"
num1 = num2
num1 + num2 > 80
num2 + 60 < num1 + 10
num1 >= num2
num1 <= (num2 + ((6 * 8) - 2))
str1 <> str2
str1 < str2 && num1 < num2
not (str1.[1] = str2.[1])

// nan and nanf compartion.
let nanResult = 0.0 / 0.0
let nanfResult = 0.0f / 0.0f
nanResult = nan
nanfResult = nanf
open System
Double.IsNaN(nanResult)
Single.IsNaN(nanfResult)

// Functional form of operators.
(+) 6 8
Checked.(-) 3 9
(||) false true
(&&&) 0b0110 0b0101
(>=) 9 12
(~+) 64 (*unary form of +*)
(~-) -13 (*unary form of -*)

// assign functional form of operators to identifiers.
let add = (+)
let subtract = (-)
let inverse = (not)
let shr = (>>>)
let shl = (<<<)
let isEqual = (=)
add 5 6
subtract 100 50
inverse true
shr 0b10101010 2
shl 0b01010101 3
isEqual (25 + 75) (40 + 60)

// Operators precedence. 
let a, b, c, d, e = 8.0, 1.0, 4.0, 3.0, 2.0;;
a - b + c * d / e ** 2.0 >= 10.0

// Overriding operators precedence. 
( (a - b) + ((c * d) / (e ** 2.0)) ) >= 10.0
( (a - (b + c)) * ((d / e) ** 2.0) ) >= 10.0
//==============================================================================
// printf and printfn functions.
printfn "This is first line, "
printf "This is second line."

// Placeholders.
let myName = "Ali"
let myCountry = "Iran"
printfn "My name is %s, I live in %s." myName myCountry

// Print various values.
open System
let sysRoot= Environment.GetFolderPath(Environment.SpecialFolder.System)
let filePath= sysRoot + @"\shell32.dll"
let objFileInfo= IO.FileInfo(filePath)
printfn "Name: %s" objFileInfo.Name
printfn "Location: %s" objFileInfo.FullName
printfn "Size: %d bytes" objFileInfo.Length
printfn "CreationTime: %O" objFileInfo.CreationTime
printfn "IsExist: %b" objFileInfo.Exists

// ToString() Method.
printfn "CreationTime: %s" (objFileInfo.CreationTime.ToString())

// Value must be compatible with placeholder type.
let intvalue = 22
printfn "float value: %f" intvalue (*Error*)

// Using Width field and 0, + flags.
let num1, num2 = 256425680, 123008641
let num3, num4 = -80027, 3045
let num5, num6 = 2045, 11
printfn "%+010d \t\t %0+10d" num1 num2
printfn "%+010d \t\t %0+10d" num3 num4
printfn "%+010d \t\t %0+10d" num5 num6

// Show how to use '-' flag
printfn "%-30s" "Left Align"
printfn "%30s" "Right Align"
printfn "%-30s" "Another Left Align"
printfn "%30s" "Another Right Align"

// Using % character in Strings.
let progressPercent = 50
printfn "Preparing setup...\nInstalling program...\tProgress: %d%%"
            progressPercent
//==============================================================================
// Comments.
// This is a comment.
(*  This is first line comment.
This is second line comment.
This is third line comment.
*)
/// A very simple constant integer.
let number1 = 3

/// A second very simple constant integer.
let number2 = 4

/// <summary>
///  This constant have sum of two numbers.
/// </summary>
let sum = number1 + number2
//==============================================================================
// Mathematical functions.
let n1, n2, n3, n4, n5, n6 = -2.3, 2.0, 64.0, 5.32, -4.6, 45.0
printfn "abs(%.1f) = %.1f" n1 (abs n1)
printfn "pown(%.1f,%d) = %.1f" n2 4 (pown n2 4)
printfn "sqrt(%.1f) = %.1f" n3 (sqrt n3)
printfn "ceil (%.1f) = %.1f" n4 (ceil n4)
printfn "floor(%.1f) = %.1f" n4 (floor n4)
printfn "round(%.1f) = %.1f" n5 (round n5)
printfn "sin(%.1f) = %.1f" n6  (sin n6)
printfn "tan(%.1f) = %.1f" n6 (tan n6)
//==============================================================================
// Type conversions.
// F#
let i : int32 = 22365
let j : int64 = i
printfn "%d" i
// C#
Int32 i = 22365;
Int64 j = i;        // Implicitly convert type int32 to int64.
Console.WriteLine(i.ToString()); // Output= 22365
// F#
let i:int32 = 22365
let j:int64 = int64 i     // Explicitly convert to int64.
printfn "%d" i

// Type conversions functions.
let floatValue = 321.25
let intValue = 56000124
let stringValue = "98725365.235e+6" 
let byteValue = 0x00FFuy
let charValue = 'D'
int floatValue
string intValue
float stringValue
byte charValue
int byteValue
float charValue
floatValue + (float intValue)

// Checked vs Unchecked conversions.
open System
let longValue = Int64.MaxValue
// Convert int64 to int.(Checked)
let result = Checked.int longValue
// Convert int64 to int.(Unchecked)
let result = int longValue
// Data loss in conversions.
let num64 = 832768500000586L   
let num32 = int32 num64      
let num16 = int16 num32

// Parse Strings.
let strNumber = "920090565898"
let result = int strNumber
let intString  = "-123456789"
let floatString  = "6.0123456e-45"
// Valid string to int convert.
int intString
// Valid string to float convert.
float floatString
// Invalid string to int convert.
int "123abc456#@" 
//==============================================================================
// Functions.
let inc x  = x + 1
inc 22

let cylinderVolume radius length =
    let pi = 3.14159
    length * pi * radius * radius
cylinderVolume 2.0 5.0

let add x y = x + y
add 6 8

open System
// rndVal is a value.
let rndVal = Random().Next();;
// rndFunc is a function.
let rndFunc() = Random().Next()
randFunc()
randFunc()

let sqr x = x * x  
let isEven n = (n % 2 = 0) 
let getAsciiCode (chr : char)  = int chr
sqr 4
isEven 24
getAsciiCode 'A'
//==============================================================================
// Type Inference.

add 4.2 5.3
// Specify an explicit type of x parameter.
let add (x : float) y = x + y
add 4.2 5.3
// Specify an explicit type for each parameters and return value. 
let add (x : float) (y : float) : float = x + y
let add x y = x + y
let result = add 4.2 5.3
//==============================================================================
// Generic Functions.
// Compartion operators are Generic functions.
(=)
(<)
(<=)
(>)
(>=)
(>) 7 3
(>) 3.0 7.0
(>) 0b0001 0b0011
(>) true false
(>) "david" "jack"
(>) 'B' 'A'

let doNothing x = x
doNothing 123
doNothing 123.0
doNothing "abc"
doNothing false
// Explicity define generic parameters.
let doNothing (x : 'a) = x

// Explicitly Generic Constructs.
let genericPrint<'a,'b,'c> (x:'a) (y:'b) (z:'c) = 
    printfn "x : %A\ny : %A\nz : %A" (x.GetType()) (y.GetType()) (z.GetType())
genericPrint<bool,string,int> true "string" 123
genericPrint<char,float,int64> 'A' 123.456 123456789L
// Automatic Generalization
let implicitlyGenericPrint x y z = 
    printfn "x : %A\ny : %A\nz : %A" (x.GetType()) (y.GetType()) (z.GetType());;
implicitlyGenericPrint true "string" 123
implicitlyGenericPrint<byte,bool,decimal> 127uy true 123M
implicitlyGenericPrint<byte,bool,decimal>
// Using Wildcard Character.
genericPrint<_,bool,_> 'A' true "string"

// Generic Constraints
(=)
let isGreater x y = x > y
isGreater "value" "VALUE"
open System.IO.Ports
let port1 = new SerialPort()
let port2 = new SerialPort()
isGreater port1 port2

let isGreater<'a> (x:'a) y = x > y
let isGreater<'a when 'a : comparison> (x:'a) y = x > y
//==============================================================================
// Nested Functions.
open System
/// return a floating-point random number within a specified range.
let generateF_PNumber min max  =         
    let integerPart() = Random().Next(min,max)
    let floatPointPart() = Random().NextDouble()
    // Call inner functions.                  
    let part1 = integerPart()
    let part2 = floatPointPart()            
    let number = float part1 + part2
    // return result number. 
    number
generateF_PNumber 0 100
generateF_PNumber 100 200
//==============================================================================
// Scope.
let moduleValue = 24uy
let function1() = 
    let function1Value = 10uy (*Local value*)
    printfn "function1 say: I can access to moduleValue(%d),\n\
                if i calculate moduleValue+function1Value result is %d" 
                moduleValue (moduleValue + function1Value)

// functionValue is not accessible form here.   
function1Value

let function2() = 
    let function2Value = 20uy (*Local value*)
    printfn "function2 say: I can access to moduleValue too!(%d),\n\
                if i calculate moduleValue+function2Value result is %d" 
                moduleValue (moduleValue + function2Value)
    (* function1 is accessible from here.*)

function1()
function2()

module ModuleTest = 
    // Nested Functions scope.
    let moduleValue = 5
    let parentFunction parentParam = 

        let childFunction1 childParam = 
            let grandChildFunction() =  moduleValue + parentParam + childParam 
            grandChildFunction()
        let childFunction2 childParam = (childFunction1(childParam)) + parentParam

        let res1 = childFunction1 2
        let res2 = childFunction2 3
        res1 + res2

module ModuleTest2 = 
    let x = ModuleTest.parentFunction 4
// Shadowing.
let x = 10
let sampleFunction() = 
    let x = 20
    printfn "Function Scope: content of x is: %d" x
sampleFunction()
printfn "Module Scope: content of x is: %d" x
//==============================================================================
// Inline functions.
let inline inc x = x + 1
let inline dec x = x - 1

// Compiler substitute the code for every instance of a function call.
let incResult = inc 4 
let decResult = dec 8

let incResult = 4 + 1 
let decResult = 4 - 1

// Static type parameters.
let add (x: 'a) y = x + y
add 2 3
add 2.4 3.4

let inline add x y = x + y
add 2 3
add 2.4 3.4
add "Visual " "F#"

let inline printAsFloatingPoint (number:^a) =
    printfn "%f" (float number)
printAsFloatingPoint 26
printAsFloatingPoint 'A'
printAsFloatingPoint "123456.001"

// Explicity define static type parameters.
let inline printAsFloatingPoint< ^a 
                                when ^a : (static member op_Explicit : 
                                    ^a -> float)> (number:^a) =
    printfn "%f" (float number)
printAsFloatingPoint<byte> 164uy
//==============================================================================
// Lambda Expressions.
let inc x = x + 1
inc 4

(fun x -> x + 1) 4

(fun (a : int) (b : string) (c:float) -> 
    printfn "a is: %d\n\
                b is: %s\n\
                c is: %f" a b c) 3 "Anonymous Functions" 1.0

(fun op1  op2 -> (op1 && true) > (op2 || true)) 
    false true

// Using named functions as argument.
let str = "this string contain 'a'"
let aChecker chr = chr = 'a'
String.exists aChecker str

// Using anonymous functions as argument.
let charExist str chr = 
    String.exists( fun c -> c = chr ) str
charExist "First string" 'i'
charExist "Second string" 'k'
//==============================================================================
// Conditional Expressions.
let checkEquality value1 value2 = 
    if value1 = value2 then
        printfn "%A and %A are equal." value1 value2
    else
        printfn "%A and %A are not equal." value1 value2
checkEquality 13 16
checkEquality 'A' 'A'

let isEven number = 
    if number % 2 = 0 then
        printfn "Yes, it is even." 
    else
        printfn "No, it is not even. it is odd."
isEven 61
isEven 42

// Conditional Expressions return value.
open System
/// Verify user.
let verify (uName:string) (uPass:string) =
    let result = 
        if (uName.ToLower() = "admin") && (uPass = "12345") then
            (sprintf "hi %s, Your confirmation was successful!" uName)
        else
            "You failed to confirm,\nusername or passcode incorrect!!"
    result
verify "david" "362541"
verify "admin" "12345"

let result = 
    if 2 > 0 then
        "String value"
    else
        123456
// Use boolean operators in Conditional Expressions.
let num1 , num2 , num3 = 20 , 40 , 8
if not (num1 > num2) && not (num1 > num3) then
    printf "if branch"
else
    printf "else branch";;

// Nested Conditional Expressions.
#r "System.Windows.Forms.Dll"
open System.Windows.Forms
let getChargeStatus () =
    let chargePercent= int(SystemInformation.PowerStatus.BatteryLifePercent * 100.f)
    let status =
        if chargePercent < 7 then
            "Critically low level of battery charge."
        else if (chargePercent >= 7) && (chargePercent < 30)  then
            "Low level of battery charge."
            else if (chargePercent >= 30) && (chargePercent < 70)  then
                "Mid level of battery charge."
                else if (chargePercent >= 70) && (chargePercent < 100) then
                    "High level of battery charge."
                        else
                            "Full level of battery charge."
    printfn "%s" status
    sprintf "%d%% Available (%A, %A)" 
        chargePercent
        SystemInformation.PowerStatus.PowerLineStatus
        SystemInformation.PowerStatus.BatteryChargeStatus
getChargeStatus()

// elif keyword.
let getChargeStatus () =
    let chargePercent= int(SystemInformation.PowerStatus.BatteryLifePercent * 100.f)
    let status =
        if chargePercent < 7 then
            "Critically low level of battery charge."
        elif (chargePercent >= 7) && (chargePercent < 30)  then
            "Low level of battery charge."
        elif (chargePercent >= 30) && (chargePercent < 70)  then
            "Mid level of battery charge."
        elif (chargePercent >= 70) && (chargePercent < 100) then
            "High level of battery charge."
        else
            "Full level of battery charge."
    printfn "%s" status
    sprintf "%d%% Available (%A, %A)" 
        chargePercent
        SystemInformation.PowerStatus.PowerLineStatus
        SystemInformation.PowerStatus.BatteryChargeStatus
    
(*Tax Reporter*)
let taxReport salary = 
    // Check employee status and calculate tax.
    let calculateTax () =
        let tax =
            if salary < 10000000 then
                0
            elif (salary >= 10000000) && (salary < 20000000) then
                salary * 2 / 100
            elif (salary >= 20000000) && (salary < 30000000) then
                salary * 3 / 100
            else
                salary * 5 / 100
        tax 
    let tax = calculateTax ()       
    printfn "=====Tax Report====="
    printfn "Salary: %d Rial" salary
    printfn "Tax: %d Rial" tax
    printfn "Absolute salary: %d Rial" (salary - tax)
taxReport 12500000
taxReport 7000000