
module FSharpBook.Chapter4

// Unit Type
let unitValue = ()
()

let noInput () = 
    2 + 4;;
let noOutput x y = 
    printf "%d + %d = %d" x y (x + y)
let noInputandOutput () =
    ()

// ignore function
let printAdd x y =            
    printfn "Running side effects..." 
    x + y
printAdd 2 4
ignore(printAdd 2 4)

//==============================================================================
// Tuple type.

// Tuple of two integers.
let integerTuple : int * int = 1, 2
// Triple of strings.
let stringTuple = ("one", "two", "three")
// Tuple that has mixed types.
let mixedTuple = (12 / 3,  'A', "str", 128uy - 4uy, -1263s)
// Nested tuples.
let tupleofTuples = (integerTuple, stringTuple, mixedTuple)

// Tuple input parameters.
let divRem (x, y) = 
    let divRes = x / y
    let remRes = x % y
    (divRes, remRes)
printfn "divRem(%d, %d) = %A" 20 3 (divRem(20, 3))

// Access tuple elements.
let tuple = ("one", ("two", "three"))
// First element.
fst tuple
// Second element.
snd tuple
// First element of second element.
fst (snd tuple)

let tuple = (1, 2, 3, 4)
let e1, e2, e3, e4 = tuple
e1, e2

let e1, e2 = tuple (*type mismatch error*)

let e1, e2, _, _ = tuple
let third (_, _, c, _)  = c
third tuple;;

//==============================================================================
// List type.
let contactsList : string list = ["Barack"; "David"; "Sarah"; "Jack"]
let numberList : list<int> = [1; 2; 3; 4; 5]
let listofTuples = [(1,1); (2,2); (3,3); (4,4)]
let emptyList = []
let webSites = [
    "http://www.google.com"        (*Any comment here*)
    "http://www.microsoft.com"     (*Any comment here*)
    "http://www.wolframalpha.com"  (*Any comment here*)
    "http://www.alsasoft.ir"       (*Any comment here*)
    "http://www.susestudio.com" ]  (*Any comment here*)

// Access list elements.
webSites.[1]
webSites.Item 3
List.nth webSites 4
let dataList = [1; 2; 3; 4; 5; 6; 7; 8; 9; 10]
dataList.[6..8]
dataList.[..4]
dataList.[5..]
dataList.[*]

// :: and @ operators.
let consOperatorResult = 'a' :: ['e'; 'h'; 'g'; 'c']
let consFunctionResult = List.Cons('a', ['e'; 'h'; 'g'; 'c'])
List.head consOperatorResult
List.tail consOperatorResult
let appendOperatorResult = [1; 2; 3] @ [4; 5; 6; 7; 8; 9; 10]    
let appendFunctionResult = List.append [1; 2; 3] [4; 5; 6; 7; 8; 9; 10]

['A'; 'B'; 'C'] @ [4; 5; 6; 7; 8; 9; 10] (*error*)

// List.init function.
List.init 10 (fun elem -> elem)
List.init 5 (fun elem -> (elem, float elem ** 3.0))

// Comprehensions Lists.
// Create lists using range expressions.
let list1 = [1 .. 10]
let list2 = [100s .. 100s .. 1000s]
let list3 = ['a' .. 'z']
let list4 = [50L .. -5L .. 0L]
let list5 = [3.0 .. 0.2 .. 4.0]

// Create a list using list comprehension
let squares = [ for x in 1 .. 10 do 
                    yield x * x ]

// Another example of list comprehension 
let generateList n =
    [ let negate y = -y
      for x in 1 .. n do
          if x % 2 = 0 then 
              yield x 
          else
              yield negate x]
printfn "generateList 10 =%A" (generateList 10)

// List comprehension
let resultListA = [ for x in 0..20 do yield x, x * x ]
// Simplified list comprehension
let resultListB = [ for x in 0..20 -> x, x * x ]

// Using list comprehension for calculaing prime numbers.
let primesUnder max =
    let result = 
        [let isPrime x = 
            if [for i in 1 .. x do
                if x % i = 0 then yield i].Length = 2 then
                    true
                else
                    false
                                 
         for n in 1 .. max do if isPrime n then yield n]
    result
primesUnder 30

let anotherList = 
    [ for i in 1..10 do
        if i <> 7 then
            printfn "%d" i
            yield i
      printfn "did thru 10, skipped 7"
      yield! [100; 200; 300 ] ]

 // Sort operation on lists.
let data1 = [8; 14; 0; -2; 22; 11; 34]
let data2 = ["blueberry"; "chimpanzee"; "pear"; "banana"; "apple"; "giraffe"];;
List.sort data1
List.sort data2
List.sortBy (fun elem -> abs elem) data1
List.sortBy (fun (elem : string) -> elem.Remove(0,1) ) data2

// Create a custom made comparer 
let myComparer value1 value2 = 
    if value1 > value2 then -1
    elif value1 < value2 then 1
    else 0
printfn "Sorted using default comparer: %A" (List.sortWith compare data1)
printfn "Sorted using custom comparer: %A" (List.sortWith myComparer data1)

let myComparer2 value1 value2 = 
    let val1Length = value1.ToString().Length
    let val2Length = value2.ToString().Length
    if val1Length > val2Length then 1
    elif val1Length < val2Length then -1
    else 0
List.sortWith myComparer2 data2

// Search operation on lists.
let names = ["adam"; "sarah"; "daniel"; "stephanie"; "david"]
let isDivisibleBy number elem = elem % number = 0
List.find (fun elem -> elem.ToString().[0] = 'd') names
List.find (isDivisibleBy 5) [ 1 .. 100 ]
List.find (fun elem -> elem.ToString().[0] = 'b') names

// Aggregate Operators.
let data1 = ["Cats";"Dogs";"Mice";"Elephants"]
let data2 = ["horses";"hens";"sheeps";"tigers"]

// Iteration on lists
List.iter (fun x -> printfn "item: %s" x) data1
List.iter2 (fun x y -> printfn "items: %-9s\t\t%-s" x y) data1 data2
// Indexed iteration on lists
List.iteri (fun i x -> printfn "item %d: %s" i x) data1
List.iteri2 (fun i x y -> printfn "items %d: %-9s\t\t%-s" i x y) data1 data2

// list.map
let data1 = [1;2;3;4]    
let data2 = [5;6;7;8]
let data3 = ["9";"10";"11";"12"]

let r1 = List.map (fun x -> x + 1) data1
printfn "Adding '1' using map = %A" r1

let r2 = List.map2 (fun x y -> x + y) data1 data2
printfn "Adding together using map = %A" r2

let r3 = List.map3 (fun x y z -> string x + string y + z) data1 data2 data3
printfn "Converting to strings and conjunction together using map = %A" r3

let r4 = List.mapi (fun i x -> (i,x)) data1
printfn "Tupling up using map = %A" r4

let r5 = List.mapi2 (fun i x y -> (i,x,y)) data1 data2
printfn "Tupling up using map = %A" r5

// List.fold
let sumList list = List.fold (fun acc elem -> acc + elem) 0 list
printfn "Sum of the elements of list %A is %d." [ 1 .. 3 ] (sumList [ 1 .. 3 ])

let reverseList list = List.fold (fun acc elem -> elem :: acc) [] list
printfn "%A" (reverseList [1 .. 10])

let printList list = List.fold (fun acc elem -> printfn "%A" elem) () list
printList [0.0; 1.0; 2.5; 5.1 ]

let shoppingCart = [("Ruler",4);
                    ("Notebook",5);
                    ("Eraser",3);
                    ("Pencil",2)]
let priceList = [500; 1500; 250; 750]
let getTotalPrice shCart pList =
    List.fold2 (fun acc (_,x) p -> acc + (x * p)) 0 shCart pList
let printDetails shCart pList =
    printfn "Shopping Cart Details:"
    List.iter2 (fun (nm,x) p ->
                printfn "%-9s*%d\t==>\t %d" nm x (x * p)) shCart pList

printfn "Total price of shopping Cart: %d" (getTotalPrice shoppingCart priceList)
printDetails shoppingCart priceList

// List.reduce
let data = ["Cats"; "Dogs"; "Mice"; "Elephants"]
List.reduce (fun acc elem -> acc + ", " + elem) data

//==============================================================================
// Option type.

let div x y = x / y
div 10 2
div 10 0

let safeDiv x y =
    if y <> 0 then
        Some(x / y)
    else
        None
safeDiv 10 2
safeDiv 10 0

let data = [10; 20; 20; 40; 50]
List.tryFind (fun elem -> elem > 50) data
List.tryFind (fun elem -> (elem < 10) || (elem > 10)) data

let data1 = Some(1,3)
printfn "Option.IsSome(data1) = %b" (Option.isSome data1)
printfn "Option.IsNone(data1) = %b" (Option.isNone data1)
printfn "Option.get(data1) = %O" (Option.get data1)
let data2 : int option = None
printfn "data2.IsSome = %b" data2.IsSome
printfn "data2.IsNone = %b" data2.IsNone

//==============================================================================
// Enum types.

let mutable meetingDay: string = "Sunday" (*Valid*)
meetingDay <- "UnknownDay" (*Invalid*)
meetingDay

// Enumeration Types.
type DayOfWeek =
    | Sunday = 0
    | Monday = 1
    | Tuesday = 2
    | Wednesday = 3
    | Thursday = 4
    | Friday = 5
    | Saturday = 6

open System
let mutable meetingDay: DayOfWeek = DayOfWeek.Sunday
meetingDay <- DayOfWeek.Monday
meetingDay <- DayOfWeek.Tuesday
meetingDay <- DayOfWeek.Wednesday
meetingDay <- DayOfWeek.Thursday
meetingDay <- DayOfWeek.Friday
meetingDay <- DayOfWeek.Saturday
meetingDay <- "UnknownDay" (*error*)

open System
let isFriday (date: DateTime) =
    date.DayOfWeek = DayOfWeek.Friday
let today = DateTime.Now
printfn "%b, The day of the week for %A is %A." 
    (isFriday today) today today.DayOfWeek
let tomorrow = today.AddDays(1.0)
printfn "%b, The day of the week for %A is %A." 
    (isFriday tomorrow) tomorrow tomorrow.DayOfWeek

type TemperatureType = |Celsius = 100us |Fahrenheit = 212us |Kelvin = 373us
let selectedTemperature :TemperatureType = TemperatureType.Celsius
type RGBColor = |R = 0uy |G = 1uy |B = 2uy
let red :RGBColor = RGBColor.R
let rgb = (RGBColor.R, RGBColor.G, RGBColor.B)

let temperatureConvertor(temp:float, from:TemperatureType, into:TemperatureType) =
    let result = 
        if from = TemperatureType.Celsius then
            if into = TemperatureType.Fahrenheit then
                (temp * 9.0 / 5.0) + 32.0
            elif into = TemperatureType.Kelvin then
                temp + 273.15
            else failwith "Conversion not supported."
        elif from = TemperatureType.Fahrenheit then
            if into = TemperatureType.Celsius then
                (temp - 32.0) * 5.0 / 9.0
            elif into = TemperatureType.Kelvin then
                (temp - 32.0) * 5.0 / 9.0 + 273.15
            else failwith "Conversion not supported."
        elif from = TemperatureType.Kelvin then
            if temp < 0.0 then failwith "It can not be lower than 0 degrees kelvin."
            if into = TemperatureType.Celsius then
                temp - 273.15
            elif into = TemperatureType.Fahrenheit then
                (temp - 273.15) * 9.0 / 5.0 + 32.0
            else failwith "Conversion not supported."
        else failwith "Conversion not supported."
    result
printfn "%.2f°C = %.2f°F" -10.0 
  (temperatureConvertor (-10.0, TemperatureType.Celsius, TemperatureType.Fahrenheit))
printfn "%.2f°F = %.2fK" 45.5 
  (temperatureConvertor (45.50, TemperatureType.Fahrenheit, TemperatureType.Kelvin))
printfn "%.2fK = %.2f°C" -1.0 
  (temperatureConvertor (-1.0, TemperatureType.Kelvin, TemperatureType.Celsius))

type SampleEnumType = |FirstValue  = 'A' |SecondValue = '\066'

let mutable meetingDays = (DayOfWeek.Monday, DayOfWeek.Thursday)

type DayOfWeek2 =
    | Sunday = 1
    | Monday = 2
    | Tuesday = 4
    | Wednesday = 8
    | Thursday = 16
    | Friday = 32
    | Saturday = 64

let mutable meetingDays = DayOfWeek2.Monday ||| DayOfWeek2.Thursday

// Enum with FlagsAttribute. 
[<FlagsAttribute>]
type DayOfWeek2 =
    | Sunday = 1
    | Monday = 2
    | Tuesday = 4
    | Wednesday = 8
    | Thursday = 16
    | Friday = 32
    | Saturday = 64
let mutable meetingDays = DayOfWeek2.Monday ||| DayOfWeek2.Thursday
meetingDays <- meetingDays ||| DayOfWeek2.Wednesday
meetingDays
meetingDays <- meetingDays ^^^ DayOfWeek2.Thursday
meetingDays
let testMonday = (meetingDays &&& DayOfWeek2.Monday) = DayOfWeek2.Monday

// Enums are incomplete.
let unknownDay : DayOfWeek2 = enum<DayOfWeek2> 300
open Microsoft.FSharp.Core.LanguagePrimitives
let unknownTemperature: TemperatureType =
    EnumOfValue<uint16, TemperatureType>(1us)

//==============================================================================
// Units of measure.

let downloadedFileSize = 5120
let elapsedTime = 10

[<Measure>] type kb (*Kilobit*)
[<Measure>] type s  (*Second*)
let downloadedFileSize = 5120<kb>
let elapsedTime = 10<s>
let downloadSpeed = downloadedFileSize / elapsedTime

let sizeInDisk = 3.5<kb>
let clipLength = 120s<s>
// Units-of-measure supported only on float, float32, decimal and signed integer types.
let mistakeMeasure = 312u<kb> (*error*)

[<Measure>] type kg (*kilogram*)
[<Measure>] type m  (*Meter*)
let netSpeed = 1024<kb/s>
let runSpeed = 9.5f<m/s>
let forceOnObject = 4<kg*m/s^2>

// Derived Measures.
[<Measure>] type kbps = kb/s
[<Measure>] type mps = m/s
[<Measure>] type N = kg m/s^2
1024<kbps> = 1024<kb/s>
9.5<mps> = 9.5<m/s>
4<N> = 4<kg m/s^2>

// International System of Units.
open Microsoft.FSharp.Data.UnitSystems.SI.UnitNames
let lampPower = 100<watt>
open Microsoft.FSharp.Data.UnitSystems.SI.UnitSymbols
let theTemperature = 100<K>

// Dimensionless values. 
let fstUlessValue = 23
let sndUlessValue = 23<1>

// Units of measure conversion. 
let netSpeeds = [256<kbps>; 512<kbps>; 1024<kbps>; 2048<kbps>; 4096<kbps>]
let nonMeasured_netSpeeds = List.map (fun speed -> speed / 1<kbps>) netSpeeds
let nonMeasured_netSpeeds2 = List.map (fun speed -> int speed) netSpeeds

let anotherNetSpeeds= List.map (fun speed -> speed * 1<kbps>)
                                  nonMeasured_netSpeeds
open Microsoft.FSharp.Core.LanguagePrimitives
let anotherNetSpeeds2 = List.map (fun speed -> 
                        Int32WithMeasure<kbps> speed) nonMeasured_netSpeeds


[<Measure>] type bit
[<Measure>] type byte
// Conversion factor
let bitPerByte = 8<bit/byte>
let sizeInByte = 3<byte>    
let sizeInbit = sizeInByte * bitPerByte

let byteToBit (value: int<byte>) =
    int value * 8<bit>
byteToBit 2<byte>

// print values with Units of measure.
let byteValue = 16<byte> 
let bitValue = byteToBit byteValue
printfn "%d byte = %d bit" (byteValue / 1<byte>) (bitValue / 1<bit>)
// In F# 4.0 or higher: values with measure information are handled seamlessly.
printfn "%d byte = %d bit" byteValue bitValue

// Generic units.
byteToBit 3 (*error*)
byteToBit 3<kbps> (*error*)

let genericCompareUnits (x: int<'u>) (y: int<'u>) = x = y
genericCompareUnits 8<bit> 9<bit> 
genericCompareUnits 8<bit> (byteToBit 1<byte>)
genericCompareUnits 2<m/s> 3<m/s>

// Explicity define type parameters.
let genericCompareUnits<[<Measure>]'u> (x: int<'u>) (y: int<'u>) = x = y

let genericCompareUnits2 (x: int<_>) (y: int<_>) = x = y





























