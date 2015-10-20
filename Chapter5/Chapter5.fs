
module FSharpBook.Chapter5

let square x = x * x
/// Sum the squares of a list of numbers with using the imperative style.
let imperativeSum numbers =
    let mutable total = 0
    for i in numbers do
        let x = square i
        total <- total + x
    total
/// Sum the squares of a list of numbers with using the functional style.
let functionalSum numbers =
    numbers
    |> Seq.map square
    |> Seq.sum
imperativeSum [1; 2; 3]
functionalSum [1; 2; 3]

// Higher-order functions
let apply (f: int -> int -> int) x y = f x y
let mul x y = x * y    
let result = apply mul 10 20

let inline getFunc fNumber  =
    if fNumber = 1 then
        let inline add x y = x + y 
        add
    elif fNumber = 2 then
        let inline sub x y = x - y
        sub
    elif fNumber = 3 then
        let inline mul x y = x * y 
        mul
    else 
        let inline div x y = x / y 
        div
// Summation
(getFunc 1) 6.2 4.3
// Multiplication
(getFunc 3) 4 5


// Partial Functions.
let add x y z = x + y + z
add 5 6 8
add 5 3 4
// Create a new function, with a partially applied call to add.
let addFive = add 5
addFive 2 3
addFive 6 9

let add (x, y, z) = x + y + z
let addFive = add (5) (*error*)

let mainFunc (w:int) (x:float) (y:string) (z:char) = 
      printfn "main Function:\n\
               w = %d\nx = %f\ny = %s\nz = %c" w x y z
let partialFunc1 = mainFunc 1
let partialFunc2 = partialFunc1 3.26
let partialFunc3 = partialFunc2 "str"
partialFunc3 'c'

// Non partially applied
List.iter (fun elem -> printfn "%d" elem) [1..3]
// Using partiall application of printf function 
List.iter (printfn "%d") [1..3]

// Non partially applied  
let incEachElements list = List.map (fun elem -> elem + 1) list
incEachElements [1..10]
// Increase list elements using partiall application of add and List.map functions
let incEachElements2 = List.map ((+) 1)
incEachElements2 [1..10]

//==============================================================================
// Recursive functions.
// Using recursive functions to calculate factorial of an integer.
let rec factorial n = 
    if n = 0 then 1
    else n * factorial (n - 1)
factorial 5
factorial 6

// Dont run!
factorial -3 (*StackOverflowException*)

let rec factorial n = 
    if n < 0 then None (*Cannot calculate the factorial of a negative number.*)
    elif n = 0 then Some 1
    else Some (n * Option.get (factorial (n-1)))
factorial 5
factorial -3

// Functional for loop
let rec forLoop body times = 
    if times <= 0 then
        ()
    else
        body()
        forLoop body (times - 1)
// Functional while loop
let rec whileLoop predicate body =
    if predicate() then
        body()
        whileLoop predicate body
    else 
        ()
forLoop (fun () -> printfn "Looping...") 4
whileLoop (fun () -> System.Random().Next(11) > 0)
          (fun () -> printfn "Looping...")

// Mutually recursive functions  
let isEven x =
    if x = 0 then true 
    elif x = 1 then false
    else isOdd (x - 1) (*error*)
let isOdd x =
    if x = 0 then false 
    elif x = 1 then true
    else isEven (x - 1)

// Mutually recursive functions  
let rec isEven x =
    if x = 0 then true 
    elif x = 1 then false
    else isOdd (x - 1)
and isOdd x =
    if x = 0 then false 
    elif x = 1 then true
    else isEven (x - 1)
isEven 5
isOdd 5

//==============================================================================
// Create new operators.
open System.IO 
let (++) x y = x + y * 2    
let (+/) path1 path2 = Path.Combine(path1, path2);;
2 ++ 3
let root = @"C:\"
let filePath = root +/ "Subdirectory" +/ "Test.fsx"
printfn "the file path is %s" filePath

// Create new prefix operators.
let rec (!) n = 
    if n < 0 then None 
    elif n = 0 then Some 1
    else Some (n * Option.get(! (n - 1)))
let (!~) list  = List.map (fun elem -> -elem) list
let (!+*) (x1, y1) (x2, y2) = 
    (x1 + x2 , y1 + y2) , (x1 * x2 , y1 * y2) 
!5
!~ [1..5]
!+* (2,3) (4,5)

let (~%) (x, n) = (x * 100) / n
printfn "%d is %d%% of %d" 100 (% (100, 1000)) 1000
printfn "%d is %d%% of %d" 365 (% (365, 1245)) 1245

// inline operators 
let inline (+@) x y = x + x * y
printfn "%d" (2 +@ 3) (*Call that uses int.*)
printfn "%f" (1.0 +@ 0.5) (*Call that uses float.*)

// Redefine operators.
let inline (~+) x = 
    if sign x < 0 then -x           
    else x
+  123
+ -123.04

//==============================================================================
// Function Composition.
open System.IO
let totalSizeOfDrives () =         
    // (1): Retrives the logical drives on computer.
    let drives = Array.toList (DriveInfo.GetDrives())    
    // (2): Builds a list of drive sizes.
    let driveSizes = 
        List.map (fun (drive : DriveInfo) -> 
                            if drive.IsReady then 
                                drive.TotalSize
                            else 0L)
                            drives          
    // (3): Calculate total size of drives.
    let totalSize = List.sum driveSizes    
    totalSize
printfn "\nTotal size of drives in this computer is: %d bytes." 
        (totalSizeOfDrives())


open System.IO
let badTotalSizeOfDrives () =        
    List.sum 
        (List.map (fun (drive : DriveInfo) -> 
                                if drive.IsReady then 
                                    drive.TotalSize
                                else 0L)
                    (Array.toList (DriveInfo.GetDrives())))
printfn "\nTotal size of drives in this computer is: %d bytes." 
        (totalSizeOfDrives())

// Pipe-Forward Operator.
let result = 90.0f |> sin
let data = [1; 2; 3; 4]
let resultList = data |> List.map (fun elem -> elem * elem * elem)

let result = sin 90.0f
let data = [1; 2; 3; 4]
let resultList = List.map (fun elem -> elem * elem * elem) data

let totalSizeOfDrivesPiped () =         
    let totalSize = 
        Array.toList (DriveInfo.GetDrives())
        |> List.map (fun drive -> if drive.IsReady then 
                                      drive.TotalSize
                                  else 0L)
        |> List.sum                                  
    totalSize

[1; 2; 3]
|> List.map (fun elem -> elem * 100)
|> List.rev
|> List.iter (fun elem -> printfn "%d" elem)

// The ||> and |||> operators 
let append str1 str2 str3 = str1 + "." + str2 + "." + str3
let result = ("abc", "def", "ghi") |||> append
let result2 = ("def", "ghi") ||> append "abc"

// Forward Composition Operator
let addOne x = x + 1
let timesTwo x = x * 2
let makeChar x =  char x
// Composition of two functions.
let addOne_timesTwo = addOne >> timesTwo
printfn "Output: %d" (addOne_timesTwo 100)
// Composition of three functions. 
let addOne_timesTwo_makeChar = addOne_timesTwo >> makeChar
printfn "Output: '%c'" (addOne_timesTwo_makeChar 32)

// Composition of functions with more than one parameter. 
let appendString (str1:string) (str2:string) = str1 + str2
let appendFileExtension extension =
    appendString "." >> appendString extension
printfn "Output: %s" ((appendFileExtension "myfile") "txt")

open System.IO
let totalSizeOfDrivesComposed (*No Parameter*) = 
    (fun () -> Array.toList (DriveInfo.GetDrives())) 
    >> List.map (fun drive -> if drive.IsReady then 
                                    drive.TotalSize
                                else 0L)
    >> List.sum
printfn "\nTotal size of drives in this computer is: %d byte." 
        (totalSizeOfDrivesComposed())

// Pipe-Backward Operator
// Provide argument of a function using default way
let result1 = sin 0.0
// Provide argument of a function using <| operator
let result2 = sin <| 0.0

let appendHttp address  = "http://" + address
printfn "Result Address: %s" (appendHttp "www.google.com")
printfn "Result Address: %s" <| appendHttp "www.google.com"

printfn "Result Address: %s" appendHttp "www.google.com" (*error*)

// Reverse pipelines require parentheses. 
printfn "Result Address: %s" <| (appendHttp <| "www.google.com")

printfn "This example show how to use <|| operator.\n A=%d, B=%d" 
        <|| (10, 20)
printfn "This example show how to use <||| operator.\n A=%d, B=%d, C=%d" 
        <||| (10, 20, 30)

// Backward Composition Operator.
abs, pown, float
// Reverse composition of two functions.
let abs_pown = pown 5 << abs
printfn "Output: %d" <| abs_pown -2
// Reverse composition of three functions.
let abs_pown_float = float << abs_pown
printfn "Output: %.1f" <| abs_pown_float -2

//==============================================================================
// Pattern matching.
let isEven x = x % 2 = 0
// Using pattern matching to specifie that entered number is even or odd.
let checkNumber x =
    match isEven x with
    | true  -> printfn "Selected number is even."
    | false -> printfn "Selected number is odd."
checkNumber 10
checkNumber 11

let order item =
    match item with 
    | "Coffee"      -> printfn "You order => Coffee."
    | "Sunshine"   -> printfn "You order => Sunshine."
    | "Ice Cream"  -> printfn "You order => Ice Cream."
order "Sunshine"
order "Tea"

// Wildcard pattern 
let order item =
    match item with 
    | "Coffee"      -> printfn "You order => Coffee."
    | "Sunshine"   -> printfn "You order => Sunshine."
    | "Ice Cream"  -> printfn "You order => Ice Cream." 
    | _            -> printfn "Invalid order!, Please choose another item."
order "Sunshine"
order "Tea"

// Tuple patterns.
let detectZeroTuple point = 
    match point with
    | (0,0) -> printfn "Both values zero."
    | (0,_) -> printfn "First value is zero."
    | (_,0) -> printfn "Second value is zero."
    | _     -> printfn "Both nonzero."
detectZeroTuple (0, 0)
detectZeroTuple (1, 0)
detectZeroTuple (0, 10
detectZeroTuple (10, 15)

// Match expressions return value as result.
open System
let getToday () =     
    match DateTime.Now.DayOfWeek with 
    | DayOfWeek.Sunday    -> "Sunday" 
    | DayOfWeek.Monday    -> "Monday" 
    | DayOfWeek.Tuesday   -> "Tuesday" 
    | DayOfWeek.Wednesday -> "Wednesday" 
    | DayOfWeek.Thursday  -> "Thursday" 
    | DayOfWeek.Friday    -> "Friday" 
    | DayOfWeek.Saturday  -> "Saturday"
    | _                   -> "IDN :("
let today = getToday ()

// Variable patterns
let detectZeroVariable point = 
    match point with
    | (0,0) -> printfn "Both values zero."
    | (0,x) -> printfn "First value is 0 in (0, %d)" x 
    | (x,0) -> printfn "Second value is 0 in (%d, 0)" x 
    | _     -> printfn "Both nonzero."
detectZeroVariable (1, 0)
detectZeroVariable (0, 10)

// Cant using an existing value as part of pattern matching
let zero = 0
let detectZeroVariable point = 
    match point with
    | (zero,zero) -> printfn "Both values zero."
    | (zero,x)    -> printfn "First value is 0 in (0, %d)" x 
    | (x,zero)    -> printfn "Second value is 0 in (%d, 0)" x 
    | _           -> printfn "Both nonzero."

// Define a literal value.
[<Literal>]
let Zero = 0    
// Match against literal values
let detectZeroVariable point = 
    match point with
    | (Zero,Zero)  -> printfn "Both values zero."
    | (Zero,x)     -> printfn "First value is 0 in (0, %d)" x 
    | (x,Zero)     -> printfn "Second value is 0 in (%d, 0)" x 
    | _            -> printfn "Both nonzero."

// This example uses patterns that have when guards. 
let detectValue point target =
    match point with
    | (x, y) when x = target && y = target 
                -> printfn "Both values match target %d." target

    | (x, y) when x = target 
                -> printfn "First value matched target in (%d, %d)" target y

    | (x, y) when y = target 
                -> printfn "Second value matched target in (%d, %d)" x target

    | _ 
                -> printfn "Neither value matches target."
detectValue (0, 0) 0
detectValue (1, 0) 0
detectValue (0, 10) 0
detectValue (10, 15) 0

// Grouping patterns.
// OR operator 
let detectZeroOR point =
    match point with
    | (0, 0) | (0, _) | (_, 0) -> printfn "Zero found."
    | _ -> printfn "Both nonzero."
detectZeroOR (0, 10)
detectZeroOR (10, 15)
// AND operator
let detectZeroAND point =
    match point with
    | (0, 0) -> printfn "Both values zero."
    | (x, y) & (0, _) -> printfn "First value is 0 in (%d, %d)" x y
    | (x, y) & (_, 0) -> printfn "Second value is 0 in (%d, %d)" x y
    | _ -> printfn "Both nonzero."
detectZeroAND (1, 0)
detectZeroAND (0, 10)

let badDetectZeroOR point =
    match point with
    | (0, 0) -> printfn "Zero found."
    | (0, _) -> printfn "Zero found."
    | (_, 0) -> printfn "Zero found."
    | _      -> printfn "Both nonzero."

// as operator
let appendHttps urlAddress = 
    match urlAddress with
    | "www.google.com" as google -> "https://" + google
    | "www.bing.com"   as bing   -> "https://" + bing
    | "www.yahoo.com"  as yahoo  -> "https://" + yahoo
    | _                as other  -> "https://" + other
appendHttps "www.google.com"
appendHttps "www.aol.com"

let urlAddress as google = "www.google.com"

// Type Test Pattern.
let printTypeOf (value : obj) =
    match value with
    | :? Core.int    as intValue    -> printfn "%A" <| intValue.GetType()
    | :? Core.float  as floatValue  -> printfn "%A" <| floatValue.GetType()
    | :? Core.bool   as boolValue   -> printfn "%A" <| boolValue.GetType()
    | :? Core.string as stringValue -> printfn "%A" <| stringValue.GetType()
    | _                             -> printfn "Unknown Type"
printTypeOf 1234
printTypeOf "str"
printTypeOf 'A'

// Tuple patterns
let socket (ip, port:int) = 
    match ip, port with
    | ("127.0.0.1", _) -> "127.0.0.1" + ":" + string port
    | ("localhost", _) -> "localhost" + ":" + string port
    | tuple            -> fst tuple + ":" + (snd tuple |> string)
socket ("162.17.1.94", 80)
socket ("localhost", 47873)

// List pattern
let printList list =
    match list with
    | []             -> printfn "[empty]"
    | [ e1 ]         -> printfn "%O" e1
    | [ e1; e2 ]     -> printfn "%O %O" e1 e2
    | [ e1; e2; e3 ] -> printfn "%O %O %O" e1 e2 e3
    | _              -> List.iter (fun elem -> printf "%O " elem) list
printList List.Empty
printList [1; 2; 3]
printList [1..10]

// Cons operator
let tailOfList list =
    match list with
    | []              -> []
    | [ e1 ]          -> []
    | [ e1; e2 ]      -> [e2]
    | [ e1; e2; e3 ]  -> [e2; e3]
    | head :: tail    -> tail   
tailOfList [1; 2; 3]
tailOfList [1..10]

let rec sumOfList list =
    match list with
    | []             -> 0
    | [ e1 ]         -> e1
    | [ e1; e2 ]     -> e1 + e2
    | [ e1; e2; e3 ] -> e1 + e2 + e3
    | head :: tail   -> head + sumOfList tail
sumOfList [1; 2; 3; 4; 5]

let printOption data  =
    match data with
    | Some (0)  -> printfn "Option Value is: %d" 0
    | Some (x)  -> printfn "Option Value is: %d" x
    | None      -> ()
Some (2) |> printOption
printOption None

// Explicity define patterns type.
open System.IO
let typeOfDrive driveName =
    let getDriveInfo dName = new DriveInfo(driveName)
    let getDriveType dInfo = 
        // Patterns that have type annotations              
        match dInfo with
        | (dI : DriveInfo) when dI.DriveType = DriveType.Fixed
                            -> DriveType.Fixed

        | (dI : DriveInfo) when dI.DriveType = DriveType.CDRom
                            -> DriveType.CDRom

        | (dI : DriveInfo) when dI.DriveType = DriveType.Network
                            -> DriveType.Network

        | (dI : DriveInfo) when dI.DriveType = DriveType.NoRootDirectory
                            -> DriveType.NoRootDirectory

        | (dI : DriveInfo) when dI.DriveType = DriveType.Ram
                            -> DriveType.Ram

        | (dI : DriveInfo) when dI.DriveType = DriveType.Removable
                            -> DriveType.Removable

        | _                 -> DriveType.Unknown

    // Return type of drive
    driveName |> getDriveInfo |> getDriveType
typeOfDrive @"c:\"
typeOfDrive @"i:\"

// pattern matching functions.
let rec sumOfList list =
    match list with
    | []             -> 0
    | [e1]           -> e1
    | [ e1; e2 ]     -> e1 + e2
    | [ e1; e2; e3 ] -> e1 + e2 + e3
    | head :: tail   -> head + sumOfList tail

let rec functionSumOfList =
    function
    | []             -> 0
    | [e1]           -> e1
    | [ e1; e2 ]     -> e1 + e2
    | [ e1; e2; e3 ] -> e1 + e2 + e3
    | head :: tail   -> head + functionSumOfList tail
functionSumOfList [1; 2; 3; 4; 5]

// fun keyword vs function keyword
let data = [-3; -2; 0; 2; 3]    
// fun keyword
let funResult = List.map (fun elem ->
                              match elem with
                              | x when x > 0 -> '+'
                              | x when x = 0 -> '0'
                              | _            -> '-') data
// function keyword
let functionResult = List.map (function 
                               | x when x > 0 -> '+'
                               | x when x = 0 -> '0'
                               | _            -> '-') data

//==============================================================================
// Discriminated Unions.

// Discriminated union of string and char
type StringCharType =
    | String of string
    | Char of char

type StringCharType =
    | string of string (*error*)
    | char of char     (*error*)

type StringCharType = String of string | Char of char

let stringValue = String "Str Sample"
let charValue = Char 'A'

let stringValue = StringCharType.String "Str Sample"
let charValue = StringCharType.Char 'A'

type Shape =
    | Rectangle of float * float 
    | Triangle of float * float * float
    | Ellipse of float * float

// Named fields.
type Shape =
    | Rectangle of width: float * height: float
    | Triangle of sideA: float * sideB: float * sideC : float
    | Ellipse of width: float * height: float

let rect = Rectangle(width= 10.0, height= 20.0)
let trig = Triangle(5., 5.0, sideC= 5.0)
let elip = Ellipse(2.0, 6.0)

// The option type is a discriminated union.
type Option<'a> =
    | Some of 'a
    | None
Some "string"
Some 1.23
None

// Discriminated unions with enumeration cases
type taskResult =
    | Success of string
    | Unsuccess
let div x y =  
    if y = 0 then
        Unsuccess 
    else
        Success (string <| x / y)
div 10 2
div 10 0

// Discriminated unions have complete structures.
type TemperatureType = |Celsius |Fahrenheit |Kelvin
let unitOFTemperatureA: TemperatureType = TemperatureType.Celsius
let unitOFTemperatureB: TemperatureType = enum<TemperatureType> 6

open System 
type mixedType =
    | Day of DayOfWeek
    | CharList of char list
    | AccountInfo of (username: string * password: string)
    | Notes of (string * string) list
let day = Day DayOfWeek.Friday
let listOfChar = CharList ['a'..'e']
let info = AccountInfo (username= "Admin", password= "123456")
let notes = Notes [("title1", "Description1")
                   ("title2", "Description2")]

// Generic Unions.
// Option is a generic discriminated union
type Option<'a> =
    | Some of 'a
    | None
Some "string"
Some "A"
Some 123456

// Generic discriminated unions 
type genericList<'a> = ListCollection of 'a list
type genericTuple<'a,'b> = TupleCollection of ('a * 'b)
type ('a,'b) mixedGenericType =
    | Lis of genericList<'a>
    | Tup of genericTuple<'a,'b>
let list1  = ListCollection [1; 2; 3; 4]
let list2  = ListCollection [1.0; 2.0; 3.0; 4.0]
let tuple1  = TupleCollection ("Port",9666)
let tuple2  = TupleCollection (20.0,3)
let mList1 = mixedGenericType<int,int>.Lis list1
let mList2 = mixedGenericType<float,int>.Lis list2
let mTuple1 = mixedGenericType<string,int>.Tup tuple1
let mTuple2 = mixedGenericType<float,int>.Tup tuple2

// Pattern matching over discriminated unions
type Color = 
    | Black | White | Red | Blue | Green
    | CustomColor of red: byte * green: byte * blue: byte;;
let codeOFColor color =
    match color with 
    | Black               -> sprintf "000000" 
    | White               -> sprintf "FFFFFF" 
    | Red                 -> sprintf "FF0000" 
    | Blue                -> sprintf "0000FF" 
    | Green               -> sprintf "00FF00" 
    | CustomColor (r,g,b) -> sprintf "%02X%02X%02X" r g b

codeOFColor Red
codeOFColor Blue
codeOFColor <| CustomColor(159uy,150uy,206uy)
codeOFColor <| CustomColor(251uy,199uy,247uy)

// Use the equals sign (=) to extract the value of a named fields.
| CustomColor (red= r; green= g; blue=b) -> sprintf "%02X%02X%02X" r g b

// Recursive unions.
// Recursive discriminated unions can be used to model expressions in programming languages
type Expression =
    | And    of Expression * Expression
    | Or     of Expression * Expression
    | Xor    of Expression * Expression
    | Not    of Expression 
    | BValue of bool
    | IValue of int     
let rec evaluate expr = 
    match expr with
    | And (opr1, opr2) -> evaluate opr1 && evaluate opr2
    | Or  (opr1, opr2) -> evaluate opr1 || evaluate opr2
    | Xor (opr1, opr2) -> (evaluate opr1, evaluate opr2) 
                          |> function
                             | false, true -> true 
                             | true, false -> true 
                             | _           -> false
    | Not opr          -> evaluate opr |> not
    | BValue x         -> x
    | IValue 1         -> true
    | IValue _         -> false
let expr1 = And (BValue true, BValue false)
evaluate expr1
let expr2 = Or (IValue 1, BValue true)
evaluate expr2
let expr3 = Xor (BValue false, BValue true)
evaluate expr3
let expr4 = Not (BValue true)
evaluate expr4
// Represents the expression: Not ((y Or (z Xor w)) And x)
let w, x, y, z = true, false, 1, 0         
let complexExpr = Not (And (Or (IValue y, (Xor (IValue z, BValue w))), BValue x)) 
let result = evaluate complexExpr

// Mutually recursive discriminated unions.
type XmlAttribute = 
    Attrib of name:string * Value:string 
type XmlElement =
    ElementFrom of name:string * attributes:XmlAttribute list * innerText:string *
                   innerXml:XmlTree
and XmlTree =
    | Element of XmlElement
    | ElementList of XmlTree list
    | Empty
// Represents an xml tree:
//  <book category="Programming">
//    <title>MS Visual F#</title>
//    <author>Ali Baghernejad</author>
//    <year>2015</year>
//    <publisher>Naghoos PUB</publisher>
//  </book>
let xmlExpr = 
    XmlTree.Element(ElementFrom("book",[Attrib("category","Programming")], "", 
                      ElementList[
                          (Element(ElementFrom("title", [Attrib("","")],"MS Visual F#", Empty)));
                          (Element(ElementFrom("author",[Attrib("","")],"Ali Baghernejad", Empty)));
                          (Element(ElementFrom("year",  [Attrib("","")],"2015", Empty)));
                          (Element(ElementFrom("publisher",[Attrib("","")],"Naghoos PUB", Empty)))
                                 ]))

// Using discriminated unions for tree data structures
type BinaryTree<'a> =
    | Empty
    | Node of 'a * BinaryTree<'a> * BinaryTree<'a>
type TraversalType =
    | PreOrder = 0 
    | InOrder = 1
    | PostOrder = 2
let rec traversal tree trvType =
        match tree with
        | Empty -> ()
        | Node(value,left,right) when trvType = TraversalType.PreOrder
                -> 
                printf "%A " value 
                traversal left trvType                                       
                traversal right trvType                                        
        | Node(value,left,right) when trvType = TraversalType.InOrder
                -> 
                traversal left trvType 
                printf "%A " value
                traversal right trvType                                      
        | Node(value,left,right) 
                -> 
                traversal left trvType                                      
                traversal right trvType
                printf "%A " value
let sampleTree = Node("A", 
                    Node("B", 
                        Node("D", Empty, Empty), 
                        Node("E", Empty, Empty)), 
                    Node("C", Empty, Empty))
traversal sampleTree TraversalType.PreOrder
traversal sampleTree TraversalType.InOrder
traversal sampleTree TraversalType.PostOrder

// Add members to Discriminated Unions.
type Shape =
    | Square of float
    | Rectangle of float * float
    | Circle of float
    | Triangle of float * float

    member this.Area =
        match this with
        | Square width              -> width * 2.0
        | Rectangle (height, width) -> height * width
        | Circle radius             -> System.Math.PI * (radius * radius)
        | Triangle (tbase, height)  -> (1.0 / 2.0) * tbase * height

    member this.Draw (x, y) =
        match this with
        | Square width  ->            
            printfn "Drawing square that has width %.1f \ 
                     in (%d,%d) Coordinate..." width x y 
        | Rectangle (height, width) ->
            printfn "Drawing rectangle that has height %.1f and width %.1f \
                     in (%d,%d) Coordinate..." height width x y  
        | Circle radius ->             
            printfn "Drawing circle that has radius %.1f \ 
                     in (%d,%d) Coordinate..." radius x y  
        | Triangle (tbase, height) ->   
            printfn "Drawing triangle that has base %.1f and height %.1f \ 
                     in (%d,%d) Coordinate..." tbase height x y
let myRectangle  = Rectangle (6.0, 3.0)                  
let myCircle = Circle (3.4)
myRectangle.Area
myCircle.Area
myRectangle.Draw (20 , 30)
myCircle.Draw (10, 40)

//==============================================================================
// Records.

open System
// Define new record type
type RGBColor = { 
    R: byte
    G: byte
    B: byte     }

type RGBColor = { R: byte; G: byte; B: byte }

let red = { R = 255uy; G = 0uy; B = 0uy}

let red = { B = 0uy; R = 255uy; G = 0uy }

// Access record fields
let rValue, gValue, bValue = red.R, red.G, red.B
printfn "Code of Red: %02X%02X%02X" rValue gValue bValue

open System
let newContact = ("john", "smith", "+00123456789", "johnsmith@example.com", 
                   DateTime (1989, 1, 17, 22, 30, 0))

type Contact = {    FirstName : string
    LastName : string
    Phone : string
    Email : string
    Birthday : DateTime
}
let newContact = { FirstName = "john"
                   LastName = "smith"
                   Phone = "+00123456789"
                   Email = "johnsmith@example.com"
                   Birthday = DateTime (1989, 1, 17, 22, 30, 0)
                 }

// Shadowing in record fields.
type Point = { x : float; y: float; z: float }
type Point3D = { x: float; y: float; z: float }
// Ambiguity: Point or Point3D? 
let myPoint = { x = 1.0; y = 1.0; z = 0.0; }

let myPoint = { Point.x = 1.0; y = 1.0; z = 0.0; }

// Copy And Update Expressions.
type Worker = {Name: string; Job: string; Wage: int}
let worker1 = { Name = "Bill"; Job = "Cashier"; Wage = 20 }
let worker2 = { worker1 with Name = "John"; Wage = 15 }
printfn "%s is a %s and makes $%d per hour" worker1.Name worker1.Job worker1.Wage
printfn "\n%s is a %s and makes $%d per hour" worker2.Name worker2.Job worker2.Wage

let defaultWorker = {Name = "Unspecified"; Job = "Unspecified"; Wage = 0}
(* Use the with keyword to populate only a few chosen fields and leave the rest with default values.*)
let worker3 = { defaultWorker with Name = "David" }
let worker4 = { defaultWorker with Name = "Nicolas"; Job = "Chef" }

// Generic records.
// A generic record, with the type parameter in angle brackets. 
type GenericRecord1<'a> = 
    { Field1: 'a
      Field2: 'a }
// A generic record, with the type parameter before type name. 
type ('a) GenericRecord2 = 
    { Field1: 'a
      Field2: 'a }

// Record Pattern
let worker1 = { Name = "Bill"; Job = "Cashier"; Wage = 20 }    
let worker2 = { Name = "John"; Job = "Cashier"; Wage = 15 } 
let worker3 = { Name = "stephanie"; Job = "Cashier"; Wage = 26 } 
let workerList = [ worker1; worker2; worker3 ]
let resultList = 
    workerList
    |> List.filter
        (function
            | { Wage = w  } when w > 20 -> true
            | _ -> false )

// Add member to records.
#r "System.Drawing.Dll"
open System.Drawing
type RGBAColor = 
    { R :byte; G :byte; B :byte; A :byte }
    member this.Code =
        sprintf "%02X%02X%02X" this.R this.G this.B                                      
    member this.GetHue () = 
        Color.FromArgb(int this.R, int this.G, int this.B).GetHue()
    member this.GetSaturation () = 
        Color.FromArgb(int this.R,int this.G,int this.B).GetSaturation()
    member this.GetBrightness () = 
        Color.FromArgb(int this.R,int this.G,int this.B).GetBrightness()
    static member (+) (op1:RGBAColor, op2: RGBAColor) = 
        {R= op1.R+op2.R; G= op1.G+op2.G; B= op1.B+op2.B ; A= op1.A+op2.A}
    static member (+) (op1:RGBAColor, op2:Color) = 
        (op1.R+op2.R, op1.G+op2.G, op1.B+op2.B, op1.A+op2.A)
let red = { R= 255uy; G= 0uy; B= 0uy; A= 255uy}
let green = { R= 0uy; G= 255uy; B= 0uy; A= 255uy}
let blue = { R= 0uy; G= 0uy; B= 255uy; A= 255uy}
red + green
blue + green + Color.Red
red.Code
blue.GetHue ()
green.GetSaturation ()
red.GetBrightness ()

// Records Equality.
type TestRecord = {X:int; Y: int}
let record1 = { X = 1; Y = 2 }
let record2 = { X = 1; Y = 2 }
let recordEquality record1 record2 = 
    let equality = record1 = record2
    match equality with
    | true -> printfn "The records are equal." 
    | _    -> printfn "The records are unequal."
recordEquality record1 record2
let record1 = {record1 with X = 0}
recordEquality record1 record2

//==============================================================================
// Lazy Evaluation.
let x = lazy (printfn "Evaluating x..."
              1 + 2)
let y = lazy (printfn "Evaluating y..."
              2 + 3)
x.Value
y.Force ()

x.Value
y.Force ()

let lazyFactorial n = Lazy.Create (fun () ->
    let rec factorial n =
        match n with
        | 0 | 1 -> 1
        | n     -> n * factorial (n - 1)
    factorial n)
let lazyFact = lazyFactorial 10
printfn "%d" (lazyFact.Force())

//==============================================================================
// Sequences.
// Create a simple sequence
let simpleSequence = seq {1..10} 
// Create a simple list
let simpleList = [1..10]
simpleSequence
Seq.nth 2 simpleSequence
Seq.length simpleSequence

// Sequence expressions
let charSeq = seq {'A'..'Z'}
let intSeq = seq {0..10..100}
charSeq
intSeq

// Create a sequence using a sequence expression
let squaresSeq = seq { for i in 1..5 do yield i*i }       
squaresSeq

// Alternate form of the previous sequence expression
let squaresSeq2 = seq { for i in 1..5 -> i*i }  

open System.IO
let getDirs path = 
    seq { for directory in Directory.GetDirectories(path) do
              yield directory }
let winDirs = getDirs @"C:\Windows"

open System
let mySequence = seq { for i in UInt32.MinValue .. UInt32.MaxValue do
                           yield i }
mySequence
let myList = [ for i in UInt32.MinValue .. UInt32.MaxValue do
                   yield i ]

// Many data types, such as lists, arrays are implicitly sequences 
let makeSequence value = seq {yield! value}
makeSequence [1..5]
makeSequence [|1..5|]
makeSequence {1..5}

let SeqFromList = seq [1; 2; 3; 4; 5]
let SeqFromArray = seq [|1; 2; 3; 4; 5|]

let SeqFromList = seq {yield! [1; 2; 3; 5; 5]}
let SeqFromArray = seq {yield! [|1; 2; 3; 4; 5|]}
SeqFromList
SeqFromArray

let seqOFList = [1; 2 ; 3; 4; 5] |> Seq.ofList
let seqOFArray = [|1; 2 ; 3; 4; 5|] |> Seq.ofArray

// Recursive sequence expressions 
type BinaryTree<'a> =
    | Node of 'a * BinaryTree<'a> * BinaryTree<'a>    
    | Empty    
let rec inorderTraversal tree =
    seq {
        match tree with
            | Node(value, left, right) ->
                yield! inorderTraversal left
                yield value
                yield! inorderTraversal right
            | Empty  -> ()
        }
let sampleTree = Node(0, Node(1, Node(3, Empty, Empty), 
                            Node(4, Empty, Empty)), Node(2, Empty, Empty))
let seqResult = inorderTraversal sampleTree
Seq.iter (fun elem -> printf "%A " elem) seqResult

// Sorting sequences.
let data = seq [30.0; 90.0; 270.0; 45.0; 180.0; 60.0]
Seq.sort data
Seq.sortBy (fun elem -> sin elem) data

let seqSortWith comparer source = 
    seq { yield! source
                 |> Seq.toList  
                 |> List.sortWith comparer 
                 |> List.toSeq }
seqSortWith (fun elem1 elem2 -> compare elem1 elem2) [1..5]

// Comparing sequences.
let seq1 = seq {1..5}
let seq2 = seq {-1..-1..-5}

let seqCompare source1 source2 = 
    Seq.compareWith (fun elem1 elem2 -> 
        if elem1 > elem2 then 1
        elif elem1 < elem2 then -1
        else 0) 
        source1 source2
let compareResult = seqCompare seq1 seq2
match compareResult with
|  1  -> printfn "seq1 > seq2"
| -1  -> printfn "seq1 < seq2"
|  0  -> printfn "seq1 = seq2"
|  _  -> failwith "Invalid comparison result"

// Searching sequences.
let data = seq {1..100}
Seq.exists (fun elem -> sign elem = -1) data

let isDivisibleBy number elem = elem % number = 0
Seq.find (isDivisibleBy 5) [1..100];;

open System
let data = seq {-100.0..5.0..100.0}
Seq.pick (fun elem -> if Double.IsNaN(sqrt elem) |> not then Some(elem) 
                        else None) data

// Grouping sequences.
let data = seq { 1 .. 100 }
let printSeq seq1 = Seq.iter (printf "%A ") seq1; printfn ""
let seqResult = Seq.countBy (fun elem ->
    if (elem % 2 = 0) then 0 else 1) data
printSeq seqResult

let seqResult = Seq.groupBy (fun elem ->
    if (elem % 2 = 0) then 0 else 1) data
printSeq seqResult

// Aggregate operators.
let data = seq { for i = 1 to 50 do 
                     if i % 10 = 0 then yield i }
data
Seq.iter (fun elem -> printfn "%A" elem) data;;

Seq.map (fun elem -> - elem) data

Seq.fold (+) 0 data
Seq.reduce (+) data

let resultSeq = Seq.unfold (fun state -> 
        if not (state > 10) then Some(state, state + 1) 
        else None) 0
Seq.iter (fun elem -> printf "%A " elem) resultSeq

let fibSeq = Seq.unfold (fun state ->
    if not (snd state > 1000) then 
         Some(fst state + snd state, (snd state, fst state + snd state))
    else None) (1,1)
Seq.iter (fun elem -> printf "%A " elem) fibSeq

let grades = seq [18.5f; 19.5f; 13.0f; 17.5f; 15.0f; 18.0f]
let f count grade = 
    match 1.0f + grade with
    | g when g >= 18.0f -> if g>20.0f then 20.0f, 1+count else g, 1+count
    | g -> g, count
let newGrades, countTopGrades = Seq.mapFold f 0 grades
printfn "newGrades= %A" newGrades
printfn "countTopGrades= %d" countTopGrades
























