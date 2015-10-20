
module FSharpBook.Chapter6

// Data Types vs Reference Types.
let p1 = "str"
let x = 22
let p2 = [1..4]

// variables in F#
let mutable var1 = 28
let mutable var2 = -6s
open Microsoft.FSharp.Data.UnitSystems.SI.UnitSymbols
[<Measure>] type cm 
let mutable stringVar, charVar, floatVar, tupleVar = 
    "abc", 'a', 123.0, ("john", 78.0<kg>, 183.5<cm>)

var2 <- 18s
var2
tupleVar <- ("david", 85.3<kg>, 177.0<cm>)
tupleVar
stringVar <- "new string"

tupleVar <- ("david", 85.3<kg>, 1.77<m>) (*error*)

#r @"..\FSharp.Data.2.1.1\lib\net40\FSharp.Data.dll"
open FSharp.Data
open System
let apiUrl = "http://api.openweathermap.org/data/2.5/weather?"
type Weather = JsonProvider<"http://api.openweathermap.org/data/2.5/weather?q=Kashmar">
type SearchMethod =  
    | ByCityId of int | ByCityName of string | ByCityCoordinates of decimal * decimal
let searchForWeather location =   
    let mutable query = String.Empty
    // Change state...
    match location with
    | ByCityId(id) -> 
        query <- sprintf "id=%d" id
    | ByCityName(name) -> 
        query <- sprintf "q=%s" <| name
    | ByCityCoordinates(lat, lon) -> 
        query <- sprintf "lat=%M&lon=%M" lat lon
    // Analysis data...
    let w = Weather.Load(apiUrl + query)
    // Print report...
    if w.Cod <> 404 then
        printfn "Weather state for [%s]: %s.\n%s" 
            query (sprintf "%s" w.Weather.[0].Description)
            (sprintf " Temperature: %M°kelvin\n" w.Main.Temp +
             sprintf " Humidity: %d%%\n"         w.Main.Humidity +
             sprintf " Wind: %Mm/s\n"            w.Wind.Speed +
             sprintf " Pressure: %Mhpa\n"        w.Main.Pressure)  
    else
        printfn "Error: Not found city :("
searchForWeather <| ByCityName ("KASHMAR")
searchForWeather <| ByCityId(112931) (* Tehran *)
searchForWeather <| ByCityCoordinates (48.85M, 2.35M) (* Paris *)

open Microsoft.FSharp.Data.UnitSystems.SI.UnitSymbols
let mutable temperature = 298.15<K>
temperature <- 288.15<K>
temperature
temperature <- 268.15<K>

//==============================================================================

// Variables Scope.
let mainFunc ()  =
    let mutable result = 0;
    let inc x = x + 1
    let innerFunc () = result <- inc result
    innerFunc () 
    result
//==============================================================================

// Mutable records.
type Employee = { 
    Name : string;
    StartDate : string;
    mutable Salary : float }
let giveRaise (emp : Employee, increase : float ) =
    emp.Salary <- (1.0 + increase) * emp.Salary
    printfn "%s: Salary= %f" emp.Name emp.Salary
let steve = { Name = "Steve"; StartDate = "01 Jan 2000"; Salary = 32000.0 }
let joe   = { Name = "Joe";   StartDate = "25 Dec 2001"; Salary = 45000.0 }
giveRaise (steve, 0.05)
giveRaise (joe, 0.10)

//==============================================================================
// Reference Cells.
let refVar = ref 1234

// Get the value of refVar.
printfn "Variable Contents: %d" !refVar
// Assign new value to refVar.
refVar := 5678
// Get the value of refVar again.
printfn "Variable Contents: %d" refVar.Value
// Another example.
let data = ref [4; 8; 10; 3; 9; 7]
let neg dataList =
    dataList := !data |> List.map (fun item -> -item)
neg data
data.contents

// ref Type Scope.
let mainFunc ()  =
    let result = ref 0;
    let inc x = x + 
    let innerFunc () = result := inc !result
    innerFunc () 
    result
mainFunc ()

// Reference cells are instances of the Ref generic record type.
type Ref<'a> =
    { mutable contents: 'a }
let anotherRefVar = {contents = 2}

// incr and decr functions.
let x, y = ref 18, ref 26
incr x
decr y
x
y

// Call by Reference.
type Incrementor = 
    {by:int}
    member this.Increment(i : int byref) =
       i <- i + this.by
let incrementor = {by=1}
let x = ref 10
incrementor.Increment(x)
x.Value

// Address-of operator (&) 
let mutable y = 10
incrementor.Increment(&y)

// Aliasing in Reference types.
let var1 = ref 123
let mutable var2 = ref 456
// Aliasing.
var2 <- var1
var1 := 150
var1
var2

//==============================================================================
// for...to expressions.
for i = 1 to 10 do
    printf "%d " i

for i = 10 downto 1 do
    printf "%d " i

for i = 10 downto 1 do
    printf "%d " i

let mutable counter = 0
for i = 1 to 50 do
    counter <- counter + 1
printfn "first for...to expression looped %d time." counter
counter <- 0
for i = -349 to -300 do
    counter <- counter + 1
printfn "second for...to expression looped %d time." counter

let mutable finish = 5
for i = 1 to finish do
    finish <- 10
    printf "%d " i

// nested for loops.
for i = 1 to 5 do
    for j = 0 to 9 do
        printf "%d " j
    printfn ""

// Draw top border
printfn " -----------------------------------------------------"
for i = 1 to 10 do
    printf "|"
    printf "   "
    for j = 1 to 10 do           
        printf "%-5d" (i * j)
    printf "|"
    printfn ""

// Draw bottom border
printfn " -----------------------------------------------------"

// for...in expressions.
let data1 = [24 ; 62 ; 18 ; 10 ; 25]
let data2 = "String"
let printElements data = 
    // Loop inside sequence.
    for elem in data do
        printf "%A " elem
printElements data1
printElements data2

for i in 1..20 do
    printf "%d " i

for i in 1..1..20 do
    printf "%d " i

for i in 'a'..'z' do 
    printf "%c " i

for i in 0..-5..-100 do
    printf "%d " i

open System.IO

// Print information of system drives.
let printDrives () =
    // Loop over an array.
    for drive in DriveInfo.GetDrives() do
        printfn "Drive %s" drive.Name
        // Check that drive is ready for work or not.
        if drive.IsReady then
            printfn "   File Type:                        %20O" 
                        drive.DriveType
            printfn "   Volume Label:                     %20s" 
                        drive.VolumeLabel
            printfn "   File System:                      %20s" 
                        drive.DriveFormat
            printfn "   Total Size:                       %20d bytes" 
                        drive.TotalSize
            printfn "   Total Free Space:                 %20d bytes" 
                        drive.TotalFreeSpace   
            printfn "   Total Free Space for current user:%20d bytes"      
                        drive.AvailableFreeSpace                                      
        else
            printfn "   ---Drive is not ready---"
printDrives ()

let mutable counter = 0
let upperbound = int64 System.Int32.MaxValue + 1L
let lowerbound = int64 System.Int32.MinValue - 1L
for i in lowerbound..upperbound do
    counter <- counter + 1
printf "for..in expression looped %d time." counter

let data = [0..10..100]
seq {yield! data}
seq {for elem in data do yield elem}

// loop over sequences, use tuple patterns. 
let seq1 = seq { for i in 1 .. 10 -> (i, i*i) }
for (a, aSqr) in seq1 do
    printfn "%d squared is %d" a aSqr

// Wildcard pattern. 
for _ in 1..10 do
    printf "."

// while...do expressions.
let mutable searchEngines =  ["google"; "bing"; "yahoo!"; "ask"; "wikipedia"]

// while..do expressions.
while (List.length searchEngines) > 0 do
    printfn "%s" <| List.head searchEngines
    searchEngines <- List.tail searchEngines

while true do
    printfn "loop..."

open System
// Busy-waits until the given time-span has passed.
let waitingFor time =
    let start = DateTime.Now 
    let duration = time 
    let diff (a:DateTime) (b:DateTime) = DateTime.op_Subtraction(DateTime.Now,b) 
    printfn "Waiting..."
    // Here's the loop
    while diff DateTime.Now start < duration do
        printf "."
    // OK, we're done...
    let span = diff DateTime.Now start 
    printfn "\nAttempted to busy-wait %dms, actually waited %dms" 
             span.Milliseconds span.Milliseconds
waitingFor <| TimeSpan.FromMilliseconds (5.0)

open System
let guessGame () =
    let randomValue = new Random()
    let number = randomValue.Next(21)

    let mutable answer = false
    while answer = false do
        printf "guess a number: "
        let guess = int <| Console.ReadLine()
        match guess with
        | g when g = number ->
            answer <- true
            printfn "congratulation, you have guessed number correctly!" 
        | g when g > number ->
                printfn "sorry, number is lower."
        | _ ->  printfn "sorry, number is grater."
guessGame ()

//==============================================================================
// Arrays.

// One-Dimensional Arrays.

// Manually Create and initialize arrays.
let workDays = [| "Mon"; "Tue"; "Wed"; "Thu"; "Fri"; |]
let grades = [| 18.5f; 19.0f; 20.0f; 17.0f; |]

let array1 : int [] = [| 1; 2; 3; 4; 5 |]
let array2 : array<string> = [| "one"; "two"; "three"; "four"; "five" |]
let array3 : char array = [| 'A'; 'B'; 'C'; 'D'; 'E' |]

let names = 
    [|
        "Sarah"
        "Yasmin"
        "Jack"
        "David"
     |]

// Causes an error.
let intArray = [| 2; 6; 5.0f; 8 |]

// Create and initialize arrays using the given values. 
let arrayA : int[] = Array.create 5 10
let arrayB : string[] = Array.create 5 "str"

// Create and initialize arrays using the default values.  
let arrayA : int[] = Array.zeroCreate 5
let arrayB = Array.zeroCreate<string> 5

// Value restriction error. 
let array1 = Array.zeroCreate 10

Array.init 10 (fun index -> index, index+1)

// Access array elements.
let namedGrades = [| 
    ("mike",  19.5f)
    ("jack",  20.0f) 
    ("sarah", 19.0f)
    ("stive", 17.5f)
    ("David", 18.0f) |]
// Access first element.
namedGrades.[0]
// Access fourth element.
namedGrades.[3]
// Access fifth element.
namedGrades.[4]

namedGrades.[5] (*error*)

// Create a single dimensional array.
let names : string[] = Array.create 5 "Unspecified"
// Set array elements values.
names.[0] <- "Jack"
names.[1] <- "Yasmin"
names.[2] <- "Daniel"
names.[3] <- "Sarah"
names.[4] <- "David"
printfn "names= %A" names

let charArray : char[] = Array.zeroCreate 6;;

val charArray : char [] = [|'\000'; '\000'; '\000'; '\000'; '\000'; '\000'|]
    
// Set array elements.
Array.set charArray 0 'F' 
Array.set charArray 1 'S' 
Array.set charArray 2 'h'
Array.set charArray 3 'a' 
Array.set charArray 4 'r' 
Array.set charArray 5 'p'
charArray
// Get array elements .
let char0 = Array.get charArray 0
let char1 = Array.get charArray 1

// Create array using range expressions.
[| 1..15 |]
[| 0s..5s..50s |]
[| 'a'..'h' |]
[| -3s..6s |]
[| 5.1..9.5 |]
[| 8..2|]
[| 8..-1..2|]

// Create an array using array comprehension
let squaresByComp = [| for i in 0..5 -> (i, i*i) |]

// Comprehensions can contain arbitrary code.
let arrayOf data =
    [| yield! data |]
arrayOf "string"
arrayOf ["elem1"; "elem2"]
arrayOf <| seq {1..10}

// Subarrays
let searchEngines = [| "Bing"; "Google"; "Ask"; "Yahoo!"; "Aol"; "Blekko";
                         "Amazon" |]
// Print elements from 0 to 3.
printfn "Top Search engines: %A" searchEngines.[0..3]
// Print elements from the beginning of the array to 1.
printfn "My favorite Search engines: %A" searchEngines.[..1]
// Print elements from 4 to the end of the array.
printfn "Some another of search engines: %A" searchEngines.[4..]
// Print whole array elements.
printfn "All Search engines: %A" searchEngines.[*]

let oddNumbers = [| 1; 3; 5; 7; 9; 11; 13; 15; 17; 19 |]
// Access to subarray of array elements.
let subArray1 = Array.sub oddNumbers 3 6
let subArray2 = Array.sub oddNumbers 0 2
let subArray3 = [|yield subArray1.[3]; yield! subArray2|]

let maleNames = [| "Jack"; "Daniel"; "David"; "Adam"; "michael" |]
let femaleNames = [| "Yasmin"; "Sarah" |]
let names : string[] = Array.create (maleNames.Length+femaleNames.Length) "Unspecified"
// Set array elements values.
names.[0..4] <- maleNames
names.[5..6] <- femaleNames
names

open System
type User = {Username: string; Password: string}
let mutable validUsers = ref Array.empty<User>
let addUser (newUser : obj) =
    let add userToAdd =
        let searchResult = (!validUsers 
            |> Array.tryFind (fun user -> 
            user.Username.ToLower() = userToAdd.Username.ToLower()))
        if searchResult.IsNone then 
            Array.Resize(validUsers,validUsers.Value.Length + 1) 
            validUsers.Value.[validUsers.Value.Length - 1] <- userToAdd 
            printfn "Done. ;)" 
        else
            printfn "Such user exists. ;/"              
    match newUser with 
    | :? User as user ->  add user
    | :? (User list) as userList -> 
        for user in userList do 
            add user
    | :? (string * string) as user ->  
        add {Username= fst user; Password= snd user}
    | _ -> failwith "Invalid parameter type!"

let printUsers () =    
    if validUsers.Value.Length > 0 then
        printfn "All registered users.\nusernames:     passwords:\n\
        -----------------------------------"           
        for user in !validUsers do
            printf "%-15s" user.Username
            printf "%-15s" user.Password
            printfn "" 
    else printfn "No user exists, database is empty."
addUser {Username = "david"; Password = "ert@1234"}
let user1 = {Username = "sarah"; Password = "jklM22493"}
let user2 = {Username = "bill"; Password = "7648300073"}
addUser [user1; user2]
addUser ("john", "smith123")
printUsers ()

// Empty arrays.
let emptyArray1 = Array.empty<int>
let emptyArray2 = Array.empty<(string*string)>
let emptyArray3 : char [] = [||]

let removeUser (existUser: obj) =
    let remove userToRemove =
        let searchResult = (!validUsers 
            |> Array.tryFindIndex (fun user -> 
            user.Username.ToLower() = userToRemove.Username.ToLower()))                
        if searchResult.IsSome then 
            // swap specified element to last index element.
            let tempUser = validUsers.Value.[validUsers.Value.Length - 1]
            validUsers.Value.[validUsers.Value.Length - 1] <- userToRemove
            validUsers.Value.[searchResult.Value] <- tempUser
            Array.Resize(validUsers,validUsers.Value.Length - 1) 
            printfn "Done. ;)" 
        else
            printfn "No such user exists. ;/"              
    match existUser with 
    | :? User as user ->  remove user 
    | :? (User list) as userList -> for user in userList do 
                                        remove user
    | :? (string * string) as user ->  
        remove {Username= fst user; Password= snd user}  
    | _ -> failwith "Invalid parameter type!"
printUsers ()
// password is not important.
removeUser {Username = "SarAh"; Password = ""}
printUsers ()
removeUser ("rayman", "12345678")
removeUser [{Username= "DAVID"; Password= ""}; {Username= "bill"; Password= ""}]
printUsers ()

// Memory usage in arrays
// Create an array with 10 element.
let x : int[] = Array.zeroCreate 10
// Calculate memory usage of array.
let arraySize = x.Length * sizeof<int>

// Combile arrays.
let ids = [| 1001; 1002; 1003 |]
let names = [| "Jack"; "Alex"; "Chris" |]
let lastNames = [| "Stevenson"; "Carter"; "Viktor" |]
let fullNames =  Array.zip names lastNames
let info =  Array.zip3 ids names lastNames

// Splits an array of pairs into two arrays.
let names, lastNames = Array.unzip fullNames
// Splits an array of triples into three arrays.
let ids, names, lastNames = Array.unzip3 info

// Array pattern.
let vectorLength vec =
    match vec with
    | [| |]                  -> 0
    | [| var1 |]             -> 1
    | [| var1; var2 |]       -> 2
    | [| var1; var2; var3 |] -> 3
    | _                      -> Array.length vec
vectorLength [| 1.0 |]
vectorLength [| 1.0; 1.0; 1.0; 1.0 |]
vectorLength [| 1.0; 1.0; 1.0; |]

// Sorting arrays
let array1 = [| 4; 82; 4; 15; 31; 16; 13 |]
// Ascending order sort
let ascendingSortedArray = Array.sort array1
// Descending order sort
let descendingSortedArray = ascendingSortedArray |> Array.rev

// String array
let fruits = [| "Banana"; "Peach"; "Apple"; "Berry"; "Cucumber" |]
let ascendingSortedArray = Array.sort fruits
let descendingSortedArray = ascendingSortedArray |> Array.rev

let array1 = [| 'e'; 'a'; 'c'; 'f'; 'b'; 'd' |]
// Ascending order sort in place 
Array.sortInPlace array1
array1
Array.rev array1

// Searching arrays
let animals = [|"dog"; "giraffe"; "chick"; "elephant"; "eagle"; "sheep"; "tiger"|]
animals |> Array.find (fun elem -> elem.Length = 5)
animals |> Array.findIndex (fun elem -> elem.Length = 5)

open System
let mutable remainedAttempts = 3
let mutable isWaiting = false
let mutable waitingDuration = TimeSpan.FromSeconds (30.0)
let mutable waitingStartTime = DateTime.Now.Subtract(waitingDuration)
let loginWith (user: User) =
    // Check user existence in database.
    let isValidUser  = Option.isSome (!validUsers 
        |> Array.tryFind (fun elem -> 
        elem.Username.ToLower() = user.Username.ToLower()&&
        elem.Password = user.Password))
    remainedAttempts <- remainedAttempts - 1
    // Check user is in waiting mode or not.  
    isWaiting <- DateTime.op_Subtraction
                   (DateTime.Now,waitingStartTime) < waitingDuration    
    if (remainedAttempts > 0) && (not isWaiting) then
        if isValidUser then
            printfn "Welcome %A, you logged in successfully! :)\n" user.Username
            // Prepare a fake menu for loged in user.    
            printfn "Main Menu\nSelect an operation code:\n\
                     -----------------------------"                               
            for i = 1 to 5 do
                printfn "%d: Do Operation%d" i i
            remainedAttempts <- 3      
        else
            printfn "Sorry, username or password is incorrect. :(" 
            printfn "Attempts left: %d" remainedAttempts                  
    else
        // Do security action here.
        printfn "You enter wrong information consecutive for 3 time \
                 and have to wait for 30 second. ;/"
        if isWaiting then
            let elapsedTime =
                DateTime.op_Subtraction(DateTime.Now,waitingStartTime)
            printfn "Elapsed time: %d second, %d milisecond. " 
                     elapsedTime.Seconds elapsedTime.Milliseconds
        else 
            printfn "Waiting..."
            waitingStartTime <- DateTime.Now
        remainedAttempts <- 3
addUser ("david", "ert@1234")
printUsers ()
loginWith {Username = "davidf"; Password = "ERT@1234"}
loginWith {Username = "david"; Password = "ert@123"}
loginWith {Username = "david"; Password = "1234@ert"}
loginWith {Username = "david"; Password = "ert@1234"}
loginWith {Username = "david"; Password = "ert@1234"}

// Aggregate operators.
let digits = [| "zero"; "one"; "two"; "three"; "four";
                "five"; "six"; "seven"; "eight"; "nine" |]
Array.iteri (fun index elem -> printfn "(%d,%s)" index elem) digits
Array.map (fun (elem:string) -> elem.ToUpper()) digits

//==============================================================================
// Multi-Dimensional Arrays.

let my2DArray = array2D  [| [| 1; 2; 3 |]; [| 4; 5; 6 |]; [| 7; 8; 9 |] |]

let my2DArray = array2D  [| [| 1; 2; 3 |]; [| 4; 5; 6 |]; [| 7; 8; 9; 10 |] |] (*error*)

let my2DArray = array2D  [ [ 1; 2; 3 ]; [ 4; 5; 6 ]; [ 7; 8; 9 ] ]

// Create and initialize Multidimensional arrays using the given values.  
let my2DArray = Array2D.create 3 4 ""
let my3DArray = Array3D.create 3 3 2 0s
let my4DArray = Array4D.create 3 3 3 3 false

// Create and initialize Multidimensional arrays using the default values. 
let my2DArray : int[,] = Array2D.zeroCreate 2 3
let my3DArray : string[,,] = Array3D.zeroCreate 5 2 1
let my4DArray = Array4D.zeroCreate<bool> 3 4 2 2

let lengths = [| 2; 3; 4; 5; 6; |]
let arrayType = typeof<int>
let my5DArray = System.Array.CreateInstance(arrayType,lengths)

// Access array elements.
let my2DArray = Array2D.zeroCreate<int> 3 3
let my3DArray = Array3D.zeroCreate<bool> 4 4 2 
// 2D Arrays.
printfn "Value of element[%d,%d] before set is: %d" 0 0 my2DArray.[1,1]
my2DArray.[1,1] <- 10
printfn "Value of element[%d,%d] after set is: %d" 0 0 my2DArray.[1,1]
// 3D Arrays.
printfn "Value of element[%d,%d,%d] before set is: %b" 1 2 1 my3DArray.[1,2,1]
my3DArray.[1,2,1] <- true
printfn "Value of element[%d,%d,%d] after set is: %b" 1 2 1 my3DArray.[1,2,1]

let employeeTable = 
    array2D [|   
                [ "1000"; "Jack";   "Stevenson"; "+981111111111" ]
                [ "1001"; "Alex";   "Carter";    "+982222222222" ]
                [ "1002"; "Sarah";  "Karati";    "+983333333333" ]
                [ "1003"; "Taylor"; "McMahon";   "+984444444444" ]
            |]
> // Access to the name of first employee (element 0,1). 
employeeTable.[0,1]

// Access to the name of second employee (element 1,1). 
employeeTable.[1,1]

// Access to the name of second employee and change it.
employeeTable.[1,1] <- "Alexander"

// Access to all information of first & second employee.
employeeTable.[0..0,*]
employeeTable.[1..1,*]

// Use individual indexes to slice columns and rows.
employeeTable.[0,*]
employeeTable.[1,*]

// Access to all lastnames and phone numbers.
employeeTable.[*,2..3]

// Access to all phone numbers and change them.
employeeTable.[*,3] <- [|"+989320563621";
                         "+989312345678";
                         "+989315236542";
                         "+989360225414"|]

employeeTable.[*,3..3] <- array2D [ ["+989320563621"]
                                    ["+989312345678"]
                                    ["+989315236542"]
                                    ["+989360225414"] ]

// Access all array elements.
employeeTable.[*,*]
 
// Create a 5D array
let lengths = [| 2; 3; 4; 5; 6; |]
let my5DArray = System.Array.CreateInstance(typeof<int>,lengths)
// Try Access to element 0,0,0,0,0
my5DArray.[0,0,0,0,0] (*error*)

printfn "Value of element 0,0,0,0,0 before set is: %O" 
    (my5DArray.GetValue([| 0; 0; 0; 0; 0 |]))
// Set a value to element 0,0,0,0,0
my5DArray.SetValue(1,[| 0; 0; 0; 0; 0 |])
// Get value of element 0,0,0,0,0 again
printfn "Value of element 0,0,0,0,0 after set is: %O" 
    (my5DArray.GetValue([| 0; 0; 0; 0; 0 |]))

// Get number of array ranks.
open System
let array1 : int[,] = Array2D.zeroCreate 2 3
let array2 = Array.CreateInstance(typeof<int>,[|2; 3; 4; 2|])    
array1.Rank
array2.Rank

// Calculate number of elements for each rank.
// Create a 3-dimensional array  
let my4DArray : int[,,,] = Array4D.zeroCreate 3 2 6 4
let arrayRank = my4DArray.Rank;
printfn "Lengths of %d dimension array:
-----------------------------" arrayRank
for i = 0 to arrayRank - 1 do
    printfn "Length of dimension %d is %d " i (my4DArray.GetLength(i))
printfn "Total length of the array is %d " (my4DArray.Length)

// Row processing 2D arrays.
for row = 0 to rowLength - 1 do
    for col = 0 to colLength - 1 do
        my2DArray.[row,col]

// Column processing 2D arrays.
for col = 0 to colLength - 1 do
    for row = 0 to rowLength - 1 do
        my2DArray.[row,col]

type Matrix = 
    {Elements: int[,]}  
        // Calculate matrix1 + matrix2.
        static member (+) (m1 : Matrix, m2 : Matrix) =
            if m1.Elements.GetLength(0) <> m2.Elements.GetLength(0) ||
               m1.Elements.GetLength(1) <> m2.Elements.GetLength(1) then
                failwith "Matrix1 and matrix2 must be same size."
            let resultMatrix : int[,] = 
                Array2D.zeroCreate (m1.Elements.GetLength(0)) 
                                   (m1.Elements.GetLength(1))
            for row = 0 to resultMatrix.GetLength(0) - 1 do
                for col = 0 to resultMatrix.GetLength(1) - 1 do
                        resultMatrix.[row,col] <- 
                        m1.Elements.[row,col] + m2.Elements.[row,col]
            {Elements = resultMatrix}
        // Calculate matrix1 - matrix2.
        static member (-) (m1 : Matrix, m2 : Matrix) =
            if m1.Elements.GetLength(0) <> m2.Elements.GetLength(0) ||
               m1.Elements.GetLength(1) <> m2.Elements.GetLength(1) then
                failwith "Matrix1 and matrix2 must be same size."
            let resultMatrix : int[,] = 
                Array2D.zeroCreate (m1.Elements.GetLength(0)) 
                                   (m1.Elements.GetLength(1))
            for row = 0 to resultMatrix.GetLength(0) - 1 do
                for col = 0 to resultMatrix.GetLength(1) - 1 do
                        resultMatrix.[row,col] <- 
                        m1.Elements.[row,col] - m2.Elements.[row,col]
            {Elements = resultMatrix}
        // Calculate matrix1 * matrix2.
        static member (*) (m1 : Matrix, m2 : Matrix) =
            if m1.Elements.GetLength(1) <> m2.Elements.GetLength(0) then
                failwith "Matrix1's columns must equal matrix2's rows." 
            let resultMatrix : int[,] = 
                Array2D.zeroCreate (m1.Elements.GetLength(0)) 
                                   (m2.Elements.GetLength(1))
            for row = 0 to resultMatrix.GetLength(0) - 1 do
                for col = 0 to resultMatrix.GetLength(1) - 1 do
                    resultMatrix.[row,col] <- 0
                    for k = 0 to m2.Elements.GetLength(0) - 1 do
                        resultMatrix.[row,col] <- resultMatrix.[row,col] + 
                        (m1.Elements.[row,k] * m2.Elements.[k,col])
            {Elements = resultMatrix}
let matrix1 = { Elements= array2D [[| 2; 4; 3 |]
                                   [| 0; 1; 6 |]] }
let matrix2 = { Elements= array2D [[| 8; 1; 0; 1 |]
                                   [| 2; 3; 4; 1 |]
                                   [| 2; 0; 5; 8 |]] }
matrix1 + matrix1
matrix2 - matrix2
matrix1 * matrix2

// Array equality
let array1 = [| 0; 2; 4; 6; 8; 10; |]
let array2 = [| 0..2..10 |]

//==============================================================================
// Jagged Arrays.

// Manually Create and initialize jagged arrays. 
let jaggedArray1 = [| [|1; 2; 3|]; [|4; 5; 6; 7|]; [|8; 9|] |]
let jaggedArray2 = 
    [|
        [|'A';|]
        [|'A'; 'B';|]
        [|'A'; 'B'; 'C'|]
        [|'A'; 'B'; 'C'; 'D'|]
        [|'A'; 'B'; 'C'; 'D'; 'E'|]
     |]
let jaggedArray3 = 
    array2D [|
                [|[|1; 2; 3|]; [|4; 5|]|]
                [|[|6; 7|]; [|8; 9; 10|] |]
             |]

jaggedArray1.Rank

// Create and initialize jagged arrays using the given values.  
let jaggedArray1 = Array.create 3 [| 1..3 |]
let jaggedArray2 = Array.create 3 Array.empty<int array>;;
let jaggedArray3 = Array2D.create 3 2 [| 18.5f; 20.0f; 14.75f; 19.0f |]

// Create and initialize jagged arrays using the default values.  
let jaggedArray1 : string [][]  = Array.zeroCreate 5 
let jaggedArray2 : array<bool> array = Array.zeroCreate 4

// Access array elements
let arrayOfArrays : string[][] = Array.zeroCreate 3
arrayOfArrays.[0]

// Assign new value to the first array.
arrayOfArrays.[0] <- Array.zeroCreate 1
arrayOfArrays

// Assign new value to the first element of first array.
arrayOfArrays.[0].[0] <- "one"
arrayOfArrays
// Assign new value to the second array and its elements.
arrayOfArrays.[1] <- Array.zeroCreate 2
arrayOfArrays.[1].[0] <- "one"
arrayOfArrays.[1].[1] <- "two"
arrayOfArrays
// Assign new value to the tertiary array and its elements.
arrayOfArrays.[2] <- Array.zeroCreate 3
arrayOfArrays.[2].[0] <- "one"
arrayOfArrays.[2].[1] <- "two"
arrayOfArrays.[2].[2] <- "three"
arrayOfArrays
// Access to the second array.
arrayOfArrays.[1]
// Access to the first element of the second array.
arrayOfArrays.[1].[0]

//==============================================================================
// Read/Write in files.
open System
open System.IO
type User = {Username: string; Password: string}
let databasePath = @"D:\Database.txt"

let addUser (newUser : obj) =
    let add userToAdd =
        let mutable anyUserFounded = false
        if File.Exists(databasePath) then
            let databaseReader = new StreamReader(databasePath)
            while (not databaseReader.EndOfStream) && (not anyUserFounded) do
                let userInfo = databaseReader.ReadLine()
                let user = {Username= userInfo.Split([|';'|]).[0]; Password=""}
                if userToAdd.Username.ToLower() = user.Username.ToLower() then
                    anyUserFounded <- true
            databaseReader.Dispose()
        if anyUserFounded then
            printfn "%s already exists in database, Please choose another! ;/" 
                userToAdd.Username
        else
            let databaseWriter = 
               new StreamWriter(databasePath,true,Text.Encoding.UTF8)
            databaseWriter.AutoFlush <- true
            databaseWriter.WriteLine(userToAdd.Username + ";"+ userToAdd.Password)
            databaseWriter.Dispose()
            printfn "Successfully added. ;)"
    match newUser with 
    | :? User as user ->  add user
    | :? (User list) as userList -> 
        for user in userList do 
            add user
    | :? (string * string) as user ->  
    add {Username= fst user; Password= snd user}
    | _ -> failwith "Invalid parameter type! ;("
let printUsers () =   
    if File.Exists(databasePath) then
        printfn "All registered users.\nusernames:     passwords:\n\
        -----------------------------------\n"         
        let databaseReader = new StreamReader(databasePath,Text.Encoding.UTF8)
        while not databaseReader.EndOfStream do
            let userInfo = databaseReader.ReadLine()
            let user = {Username= userInfo.Split([|';'|]).[0]; 
                        Password= userInfo.Split([|';'|]).[1]}
            printf "%-15s" user.Username
            printf "%-15s" user.Password
            printfn "" 
        databaseReader.Dispose()
        printfn "End of database." 
    else
        printfn "Database is not exist. ;("
addUser ("user1", "pass1")
addUser {Username="user2"; Password= "pass2"}
addUser {Username="user2"; Password= "pass2"}
printUsers()

let removeUser (existUser: obj) =
    let remove userToRemove =
        if File.Exists(databasePath) then
            let usersList = File.ReadAllLines(databasePath) |> Array.toList
            let newList = List.collect(fun (elem:string) -> 
                if elem.Split([|';'|]).[0].ToLower() = 
                    userToRemove.Username.ToLower() then []  else [elem]) usersList
            File.WriteAllLines(databasePath,newList)
            if newList.Length < usersList.Length then
                printfn "Successfully removed. ;)"
            else
                printfn "No such user exists. ;/" 
        else
            printfn "Database is not exist. ;(" 
    match existUser with 
    | :? User as user ->  remove user 
    | :? (User list) as userList -> 
         for user in userList do 
             remove user 
    | :? (string * string) as user ->  
         remove {Username= fst user; Password= snd user}
    | _ -> failwith "Invalid parameter type!"
let mutable remainedAttempts = 3
let mutable isWaiting = false
let mutable waitingDuration = TimeSpan.FromSeconds (30.0)
let mutable waitingStartTime = DateTime.Now.Subtract(waitingDuration)
let loginWith (userToLogin: User) =
    // Check user existence in database.
    let mutable isValidUser = false
    if File.Exists(databasePath) then
        let databaseReader = new StreamReader(databasePath)
        while (not databaseReader.EndOfStream) && (not isValidUser) do
            let userInfo = databaseReader.ReadLine()
            let user = {Username= userInfo.Split([|';'|]).[0]; 
                        Password= userInfo.Split([|';'|]).[1]}
            if user.Username.ToLower() = userToLogin.Username.ToLower() &&
               user.Password.ToLower() = userToLogin.Password.ToLower() then
                isValidUser <- true
        databaseReader.Dispose()
    else
        printfn "Database is not exist."  

    remainedAttempts <- remainedAttempts - 1
    // Check user is in waiting mode or not.  
    isWaiting <- DateTime.op_Subtraction
                   (DateTime.Now,waitingStartTime) < waitingDuration
         
    if ((remainedAttempts > 0) || (isValidUser = true)) && (not isWaiting) then
        if isValidUser  then
            printfn "Welcome %A, you logged in successfully! ;)\n"
                     userToLogin.Username
            // Prepare a fake menu for loged in user.    
            printfn "Main Menu\nSelect an operation code:\n\
                     -----------------------------"                               
            for i = 1  to 5 do
                printfn "%d: Do Operation%d" i i
            remainedAttempts <- 3      
        else
            printfn "Sorry, username or password is incorrect. ;(" 
            printfn "Attempts left: %d" remainedAttempts                  
    else
        // Do security action here.
        printfn "You enter wrong information consecutive for 3 time \
                 and have to wait for 30 second. ;/"
        if isWaiting then
            let elapsedTime = 
                DateTime.op_Subtraction(DateTime.Now,waitingStartTime)
            printfn "Elapsed time: %d second, %d milisecond. " 
                     elapsedTime.Seconds elapsedTime.Milliseconds
        else 
            printfn "Waiting..."
            waitingStartTime <- DateTime.Now
        remainedAttempts <- 3
loginWith {Username= "user1"; Password= "pass1"}
removeUser ("user2", "pass2")
loginWith {Username="user2"; Password= "pass2"}

//==============================================================================
// Resources Management.

// use operator.
open System.IO
let writeToFileA (path:string) (data:obj) =
    use writer = new StreamWriter(path) 
    writer.Write(data)
    // writer.Dispose() is called implicitly here.
let writeToFileB (path:string) (data:obj) =
    let writer = new StreamWriter(path) 
    writer.Write(data)
    writer.Dispose()

// using operator.
open System.IO
let writeToFileC (path: string) (data: obj) =
    using (new StreamWriter(path)) (fun writer -> writer.Write(data))
writeToFileC "file.txt" "this is test"











