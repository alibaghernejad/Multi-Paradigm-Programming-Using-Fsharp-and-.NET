
module FSharpBook.Chapter8

// Exceptions.
let stringToFloat (str:string) = float str
stringToFloat "-123"
stringToFloat "6.0123456e-45"
stringToFloat "123abc456#@"

open System.Diagnostics
let runFile filename args = 
    let newProcess = new Process()
    newProcess.StartInfo.FileName <- filename
    newProcess.StartInfo.Arguments <- args
    newProcess.Start()
runFile @"C:\Program Files\Internet Explorer\IExplore.exe" "www.google.com"
runFile "IExplore.exe" "www.bing.com"
runFile @"C:\fakefile.exe" "arg"

// try...with expressions. 
open System
let improvedStringToFloat (str:string) = 
    try
        Some (float str)
    with
        | :? FormatException -> 
            printfn "Input string should contain (0-9 + - , . e) characters."; None
        | _                  -> 
            printfn "Unknown Error ;("; None
improvedStringToFloat "1146"
improvedStringToFloat "*724#"
open System.Diagnostics
type OperationState<'a> = Succeeded of 'a |Failed
let improvedRunFile filename args = 
    try
        let newProcess = new Process()
        newProcess.StartInfo.FileName <- filename
        newProcess.StartInfo.Arguments <- args
        Succeeded (newProcess.Start())
    with
        | :? ObjectDisposedException as objDpsExp      
                -> printfn "%s" objDpsExp.Message; Failed

        | :? ArgumentNullException as nullExp          
                -> printfn "%s" nullExp.Message; Failed

        | :? InvalidOperationException as invalOprExp  
                -> printfn "%s" invalOprExp.Message; Failed

        | :? ComponentModel.Win32Exception as win32Exp 
                -> printfn "%s" win32Exp.Message; Failed
        | exp                                          
                -> printfn "%s" exp.Message; Failed
improvedRunFile "Chrome.exe" "mail.google.com"
improvedRunFile @"C:\anotherfakefile.exe" "arg"

// try...finally expressions. 
open System.IO
let writeTo file (data:string) =
    let mutable stream, writer = null, null
    try
        stream <- File.OpenWrite(file)
        writer <- new StreamWriter(stream)
        writer.WriteLine(data)
        writer.Flush()
    finally
        printfn "Checking for cleanup..."
        if not(stream = null) then 
            printfn "Closing stream..."
            stream.Close()
let result =
    try  Succeeded(writeTo @"wrongpath\filename.txt" "this is data")
    with | exp -> printfn "Exception handled:\n %s" exp.Message; Failed

// nest try...with and try...finally expressions. 
open System.IO
let writeTo file (data:string) =
    let mutable stream, writer = null, null
    try
        try
            stream <- File.OpenWrite(file)
            writer <- new StreamWriter(stream)
            writer.WriteLine(data)
            Succeeded (writer.Flush())
        finally
            printfn "Checking for cleanup..."
            if not(stream = null) then 
                printfn "Closing stream..."
                stream.Close()
    with
     | exp -> printfn "Exception handled:\n %s" exp.Message; Failed
let result = writeTo @"WrongPath\fileName.txt" "this is data"

exception FsharpException1 of string
exception FsharpException2 of int * string
type DotNetException1() = inherit Exception() 
type DotNetException2(msg) = inherit exn(msg)

raise<FsharpException1> <| FsharpException1("error message")
raise<FsharpException2> <| FsharpException2(-1,"error message")
raise<DotNetException2>  <| DotNetException2("error message")

open System 
[<Flags>] 
type DayOfWeek = 
  |Sunday= 1 |Monday= 2 |Tuesday= 4 |Wednesday= 8 
  |Thursday= 16 |Friday= 32 |Saturday= 64
exception AlarmFailureException of DayOfWeek * string
let addAlarm reminderMsg (time:TimeSpan) repeats =    
    let validDays = 
        DayOfWeek.Sunday   |||DayOfWeek.Monday   |||DayOfWeek.Tuesday|||
        DayOfWeek.Wednesday|||DayOfWeek.Thursday |||DayOfWeek.Friday |||
        DayOfWeek.Saturday
    if (validDays.HasFlag(repeats)) then
        printfn "New alarm successfully Added:"
        printfn "  Time: %A \n  \
                   Reminder Message: %s\n  \
                   Repeats On: %A" time reminderMsg repeats
    else
        let exp = AlarmFailureException(repeats, "Invalid day of week!")
        exp.HelpLink <- "http://www.alsasoft.ir "
        raise <| exp
try 
    addAlarm "F# Meeting" (TimeSpan.Parse("18:30")) 
                          (DayOfWeek.Thursday|||DayOfWeek.Friday)
    addAlarm "Shopping" (TimeSpan.Parse("20:30")) (enum(620))
with
 | AlarmFailureException(arg, msg) as alarmExp 
        -> printfn "%s: %A\n  For more info see:%s" msg arg alarmExp.HelpLink
 | :? ArgumentException as argExp  
        -> printfn "%s:" argExp.Message
 | exp                             
        -> printfn "%s:" exp.Message

open System
let fileSize fileName =
    if filename = null then 
        nullArg "fileName" "fileName is a null reference." 
    elif String.IsNullOrWhiteSpace(fileName) then 
        invalidArg "fileName" "The file name is empty, contains only white spaces."
    else 
        try System.IO.FileInfo(fileName).Length
        with | exp -> failwith <| "Another exception ;( -> " + exp.Message
fileSize "  "

let div x y =
    try
        x / y                      
    with
    | :? System.DivideByZeroException as divByZeroExp -> 
          printfn "Handle and reraise..."
          reraise()
div 6 0
