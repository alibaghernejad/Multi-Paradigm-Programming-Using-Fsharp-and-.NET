
module FSharpBook.Chapter7

// A simple class with field and method.
type Person (pName, pFamily) =
    let name, family = pName, pFamily
    let fullName = pName + " " + pFamily
    member this.PrintReport () = 
        printfn "Name: %s \nFamily: %s \nFullName: %s" name family fullName

// Create new instance of Person class using the new keyword.
let person1 = new Person ("john", "smith")

// Create new instance without using the new keyword. 
let person2 =  Person ("jack", "peterson")

person1.PrintReport ()

person1.fullName (*error*)

//==============================================================================
// Constructors

// Implicit Construction.
type JenderType =
    | Male 
    | Female 
    | Unspecified 
type Person (pName, pFamily, pAge, pJender: JenderType) =
    let name, family = pName, pFamily
    let getFullName name family = name + " " + family
    let fullName = getFullName name family
    let jender = pJender
    let age : uint16 = pAge
    member this.PrintReport () = 
        printfn "Name: %s \nFamily: %s \nFullName: %s" pName pFamily fullName
        printfn "Age: %d" pAge
        printfn "Jendar: %A" pJender
let thePerson = new Person ("sarah", "taylor", 28us, JenderType.Female)
thePerson.PrintReport ()

// do bindings.
type MyClass (param1: int, param2: int) as this =
    do printfn "initializing class...\n.\n.\n."
    let testField1 = 2 * param1
    let testField2 = 3 * param2
    do printfn "initializing finished with testField1= %d, testField2= %d, 
                TestMethod()= %d" testField1 testField2 (this.TestMethod ())
    member this.TestMethod () = testField1 + testField2
let theInstance = MyClass (5, 7)

type MyOtherClass (p) as this =
    let field1 = p
    do printfn "field1= %d, Method1()= %d" field1 (this.Method1())
    let func1 () = 2 + field1
    member this.Method1 () = func1 () * 2
let theInstance = MyOtherClass(4) (*error*)

// Constructorless classes.
type ClassWithoutConstructor =
    member this.SampleMethod () = printfn "Sample method is running..."
let instance = ClassWithoutConstructor ()

// Explicit constructors.
type PairOfStrings =
    val firstString : string 
    val secondString : string
    new (str1, str2) = {firstString = str1; secondString = str2}
let strResult = 
      new PairOfStrings ("this is first string.", "this is second string.")

// Accsess explicit fields outside of class.
strResult.firstString
strResult.secondString

type PairOfStrings =
    val firstString : string 
    val secondString : string
    member Me.Concat () = Me.firstString + Me.secondString
    new (str1, str2) = {firstString = str1; secondString = str2}

new (str1, str2) = {firstString = str1} (*error*)

type PairOfStrings =
    val firstString : string 
    val secondString : string
    member Me.Concat () = Me.firstString + Me.secondString
    new (str1, str2) = {firstString = str1; secondString = str2}
    new () = {firstString = ""; secondString = ""}

let cons1Obj = PairOfStrings ("string1", "string2")
let cons2Obj = PairOfStrings ()
printfn "Constructor1 Result:  firstString= %s, secondString= %s" 
    cons1Obj.firstString cons1Obj.secondString
printfn "Constructor2 Result:  firstString= %s, secondString= %s" 
    cons2Obj.firstString cons2Obj.secondString

type Employee (id, name, lastName, annualSalary)=
    let emp_Id = id
    let emp_Name = name
    let emp_LastName = lastName 
    let mutable emp_Salary  = annualSalary
    member this.Id = emp_Id
    member this.Name = emp_Name
    member this.LastName = emp_LastName
    member this.Salary = emp_Salary
    member this.IncreaseSalary (increasePercent) =
        let increase  = (increasePercent * emp_Salary) / 100.0
        emp_Salary <- emp_Salary + increase
    member this.DecreaseSalary  (decreasePercent) = 
        let decrease  = (decreasePercent * emp_Salary) / 100.0
        emp_Salary <- emp_Salary - decrease
    new (id, name, lastName, weeklySalary, numberOfWeeks) = 
        let salary = weeklySalary * numberOfWeeks
        Employee (id, name, lastName, salary)
    new () = Employee ("", "", "", 0.0);;
let employee1 = Employee ()
let employee2 = Employee ("24567853", "david", "lord", 500.0, 30.0)
let employee3 = Employee ("345677", "john", "smith", 65000.0)
employee1.Salary
employee2.Salary
employee3.Salary
employee2.IncreaseSalary 20.0 (*in percent*)
employee2.Salary

type TestType(a, b) as this =
    let fd1 = a + 1
    let fd2 = b + 1
    do printfn "Implicit Constructor is running..."
    do this.PrintSum ()
    member this.PrintSum () = printfn "%d + %d = %d" fd1 fd2 <| fd1 + fd2
    new () as me =  
        TestType (0, 0) 
        printfn "Explicit Constructor is running..."
        me.PrintSum ()

type TestType(a, b) as this =
    let fd1 = a + 1
    let fd2 = b + 1
    do printfn "Implicit Constructor is running..."
    do this.PrintSum ()
    member this.PrintSum () = printfn "%d + %d = %d" fd1 fd2 <| fd1 + fd2
    new () as Me =  
        TestType (0, 0) then
        printfn "Explicit Constructor is running..."
        Me.PrintSum ()
new TestType ()

type PairOfStrings =
    val firstString : string 
    val secondString : string
    member Me.Concat () = Me.firstString + Me.secondString
    member Me.Inverse () = Me.secondString, Me.firstString
    new (str1, str2) as this = 
       // Pre-processing
       match (str1,str2) with   
       | (null, _   )      
       | (_,    null)    -> failwith "Error: null value in strings."
       | _               -> printfn "Pre-processing done successfully!"
       // Initializing fields.   
       {firstString = str1; secondString = str2} 
       // Post-processing       
       then
       printfn " FirstString= %s\n SecondString= %s\n Concat()= %s\n Inverse()= %O" 
        <| this.firstString 
        <| this.secondString  
        <| this.Concat ()  
        <| this.Inverse ()
    new () = {firstString = ""; secondString = ""}
let objPair1 = PairOfStrings (null,"bye")
let objPair2 = PairOfStrings ("hi","bye")

//==============================================================================
// Fields

// implicit fields.
type Point (x : float32 , y : float32) =
    let p_x = x
    let p_y = y

type Point (x : float32 , y : float32) =
    let p_x = x
    let p_y = y
    member this.X = p_x
    member this.Y = p_y

// Access private fields outside of class.
let point1 = new Point (2.0f, 3.0f)
printfn "point1 is originally located at (%.1f, %.1f)" point1.X point1.Y
let zeroPoint = new Point (0.0f, 0.0f)
printfn "zeroPoint is originally located at (%.1f, %.1f)" zeroPoint.X zeroPoint.Y

type Point (x : float32 , y : float32) =
    let mutable p_x = x
    let mutable p_y = y
    member this.X  
        with get () = p_x 
        and set (value) = p_x <- value 
    member this.Y 
        with get () = p_y 
        and set (value) = p_y <- value;;

type Point =
  class
    new : x:float32 * y:float32 -> Point
    member X : float32
    member Y : float32
    member X : float32 with set
    member Y : float32 with set
  end
let myPoint = Point (1.0f, 2.0f)
let myPointTuple= myPoint.X, myPoint.Y
printfn "myPoint is originally located at %A" <| myPointTuple
let newX =  myPointTuple |> fst |> fun x -> x + 1.0f
let newY =  myPointTuple |> snd |> fun x -> x + 1.0f
// Mutate fields.
myPoint.X <- newX
myPoint.Y <- newY
printfn "After change myPoint is located at %A" (myPoint.X, myPoint.Y)

// Functions in primary constructors.
type simpleListGenerator (start, finish)  =
    // fields & functions.
    let generateList x y = [ for i = start to finish do yield i ]
    let list =  generateList start finish 
    let applyToList list operator =
        match operator with
        | "+" -> 
            let rec recprivateSum list = 
                if list = [] then Some (0)
                else  Some (list.Head +  (recprivateSum list.Tail).Value)
            recprivateSum list
        | "*" -> 
            let rec recprivateMul list = 
                if list = [] then Some (1)
                else  Some (list.Head * int (recprivateMul list.Tail).Value)
            recprivateMul list 
        | opr -> None
    // Methods & properties.
    member this.Sum () = 
        list |> applyToList <| "+"
    member this.Mul () = 
        list |> applyToList <| "*"
    member this.Avg () = 
        (this.Sum () |> Option.get)  / list.Length
    member this.Length = list.Length
    member this.GeneratedList = list
let list1 = simpleListGenerator (1,5) 
printfn "Generated List = %A\n Sum()= %A, Mul()= %A, Avg= %d, Length= %d"
    <| list1.GeneratedList 
    <| list1.Sum () 
    <| list1.Mul () 
    <| list1.Avg () 
    <| list1.Length

// Explicit fields.
type SignType =
    | Negative = -1
    | Zero = 0
    | Positive = 1  
type SignedNumber  =
    val mutable number : int
    val mutable sign : SignType
    new (intValue) as this = 
        SignedNumber ()
        then
        let checkSignRes = this.CheckSign intValue
        if not (checkSignRes = SignType.Zero) then 
            this.number <- intValue
            this.sign   <- checkSignRes        
    new () = {number = 0; sign = SignType.Zero }
    member private this.CheckSign number = 
        match number with 
        | negNum when negNum < 0 -> SignType.Negative
        | posNum when posNum > 0 -> SignType.Positive
        | _                      -> SignType.Zero

let num = 3
let theInstanse = SignedNumber (num)
theInstanse.number
theInstanse.sign

// Explicit fields in class with primary constructor.
type PairOfIntegers (x, y) =
    [<DefaultValue>] val mutable x : int
    [<DefaultValue>] val mutable y : int
    new () = PairOfIntegers (0, 0)

//==============================================================================
// Generic classes.

type MyGenericClass<'a> (value: 'a) = 
   do printfn "Value= %A" value
MyGenericClass<int>(123) |> ignore
MyGenericClass<string> ("some string") |> ignore
MyGenericClass<int list> ([1..10]) |> ignore
MyGenericClass<seq<int * int>> (seq { for i in 1 .. 10 -> (i, i*i) }) |> ignore

let chrValue = 'c'
MyGenericClass<_> chrValue

//==============================================================================
// mutually recursive classes.

open System.IO
type Folder(pathIn: string) =
  let path = pathIn
  let filenameArray : string array = Directory.GetFiles(path)
  member this.FileArray = Array.map (fun elem -> new File(elem, this)) filenameArray
and File(filename: string, containingFolder: Folder) = 
   member this.Name = filename
   member this.ContainingFolder = containingFolder
let folder1 = new Folder(@"C:\")
folder1.FileArray |> Array.iter (fun file -> printfn "%s" file.Name)

//==============================================================================
// Methods.

open System
type Mobile  = 
    val mutable m_Manufactory : string  
    val mutable m_Model : string 
    val mutable m_IsConnectedToNet : bool  
    val mutable m_IsPowerOn : bool 
    val mutable m_PhoneBook : PhoneBook  
    // Explicit Constructor.     
    new (manufactory, model) = {
        m_Manufactory = manufactory; 
        m_Model= model; 
        m_IsConnectedToNet= false; 
        m_IsPowerOn= false; 
        m_PhoneBook= new PhoneBook() }
    member this.PowerOn () = 
        this.m_IsPowerOn <- true
        printfn "Hi,Welcome..."
        Console.Beep (345,400);
    member this.PowerOff () = 
        this.m_IsPowerOn <- false
        this.m_IsConnectedToNet <- false
        printfn "Goodbye..."
        Console.Beep (345,400)
        Console.Beep (345,900)
    // Connect mobile phone to internet.
    member this.ConnectToInternet () =         
        this.CheckPowerStatus ()
        printfn "Connecting to internet..."
        this.m_IsConnectedToNet <- true
    // Make a voice call to specified number.
    member this.MakeCall numberToCall = 
        this.CheckPowerStatus ()
        // Find contact name.
        this.m_PhoneBook.Find (numberToCall)
        // Do action depending of result.
        |> function 
            | Some contact -> 
                printfn "Dialing %s... " 
                  (contact.Name + " " + contact.Family + "(" + contact.Number + ")")
            | None   ->  printfn "Dialing %s..." numberToCall
    // Send new message to specified number.
    member this.SendMessage numberToMessage message = 
        this.CheckPowerStatus ()
        this.m_PhoneBook.Find (numberToMessage)
        |> function 
            | Some contact -> 
                printfn "Sending \"%s\" to %s..." 
                  message 
                  (contact.Name + " " + contact.Family + "(" + contact.Number + ")")
            | None   ->  printfn "Sending \"%s\" to %s..." message numberToMessage
    member private this.CheckPowerStatus () = 
        if not this.m_IsPowerOn then
            failwith "First of all turn on mobile please."
and PhoneBook  = 
    val mutable c_List : Contact list
    new () = { c_List= [] }
    // Add new contact.
    member this.Add (name, family, number) = 
        let newContact = {Name= name; Family= family; Number= number}
        this.c_List <- (newContact :: this.c_List)      
    // Remove an exist contact.
    member this.Remove (name, family, number) = 
        let contactToRemove = {Name= name; Family= family; Number= number}
        List.filter(fun (contact: Contact) -> 
            contact.Name.ToLower() <> contactToRemove.Name.ToLower() || 
            contact.Family.ToLower() <> contactToRemove.Family.ToLower() ||
            contact.Number.ToLower() <> contactToRemove.Number.ToLower())
            this.c_List
        |>
        (fun contactList -> this.c_List <- contactList)
    // Search for contact.
    member this.Find (numberTofind) = 
        List.tryFind 
            (fun (contact: Contact) -> 
            String.op_Equality( contact.Number, numberTofind)) 
            this.c_List
and Contact={mutable Name: string; mutable Family: string; mutable Number: string}

let iphone = new Mobile ("Apple", "iPhone AIX")

iphone.PowerOn()

iphone.m_PhoneBook.Add ("Aaron", "Fit", "+1234445033")
iphone.m_PhoneBook.Add ("Grant", "Leonardo", "+4165679801")
iphone.m_PhoneBook.c_List

iphone.MakeCall ("+989151234567")

iphone.ConnectToInternet()

iphone.SendMessage "+1234445033" "Hi aaron, how r u?"

iphone.m_PhoneBook.Remove ("aaron", "fit", "+1234445033")
iphone.m_PhoneBook.c_List

iphone.MakeCall "+1234445033"

iphone.PowerOff()

iphone.ConnectToInternet() (*error*)


let galaxy = new Mobile("Samsung", "Galaxy SX")
galaxy.PowerOn()
galaxy.m_PhoneBook.Add ("john", "smith", "+9832165725")
galaxy.m_PhoneBook.c_List

// Partial application of method arguments. 
let johnMessageSender = galaxy.SendMessage "+9832165725"
johnMessageSender "Sorry johnny, i missed your call."
johnMessageSender "Where is the meeting?"

type DirectoryScanner () as this = 
    [<DefaultValue>] val mutable targetDirectory : string
    [<DefaultValue>] val mutable numberOfFiles : int
    [<DefaultValue>] val mutable numberOfDirectories : int
    [<DefaultValue>] val mutable seqOfFiles : seq<FileInfo>
    do  this.targetDirectory <- Environment.CurrentDirectory
        this.seqOfFiles      <- Seq.empty
    new (pathToScan) as this = 
        DirectoryScanner() 
        then this.targetDirectory <- pathToScan
    member this.ScanFiles () =
          // Reset state.
        this.numberOfFiles <- 0
        this.numberOfDirectories <- 0
        this.seqOfFiles <- Seq.empty
       // Begin scan.
        let directoryInfo = new DirectoryInfo(this.targetDirectory)
        this.Scanner (directoryInfo)
        printfn "Scanned! ;)"
    member private this.Scanner (directory) = 
        // Scan all files in the current path
        let currentDirectoryFiles = directory.GetFiles()
        this.numberOfFiles <- this.numberOfFiles + currentDirectoryFiles.Length 
        let res  = seq {yield! currentDirectoryFiles}
        this.seqOfFiles <- Seq.append this.seqOfFiles res            
        // Scan all subdirectories in the current path.
        let subDirectories  = directory.GetDirectories()
        this.numberOfDirectories <- this.numberOfDirectories + 1
        subDirectories
        |> Array.iter (fun dir -> this.Scanner (dir))      
    member this.PrintReport () = 
        this.seqOfFiles
        |> Seq.iter (fun file -> printfn "%-O\t %-O bytes\t %-O" 
                                    file.CreationTime
                                    file.Length 
                                    file.Name)
let cookiesPath= Environment.GetFolderPath(Environment.SpecialFolder.Cookies)
let cookiesDir = new DirectoryScanner(cookiesPath)
cookiesDir.ScanFiles()
cookiesDir.numberOfFiles
cookiesDir.numberOfDirectories
cookiesDir.PrintReport ()
let currentDir = new DirectoryScanner()
currentDir.ScanFiles()
currentDir.numberOfFiles
currentDir.numberOfDirectories
currentDir.PrintReport ()

//==============================================================================
// Named Parameters.

// Positional Parameters.
open System.IO
File.Create(@"c:\temp\MyTest.txt",1024)

// Named Parameters.
File.Create(path= @"c:\temp\MyTest.txt", bufferSize= 1024)

File.Create(bufferSize= 1024, path= @"c:\temp\MyTest.txt")

File.Create(@"c:\temp\MyTest.txt", bufferSize= 1024)

//==============================================================================
// Optional Parameters.

open System
open System.Diagnostics
type DNSLookup () =     
    member this.Lookup (hostName, ?server)  =
        let procStartInfo = new ProcessStartInfo("nslookup.exe")
        match server with 
        | Some specifiedServer -> procStartInfo.Arguments <- hostName + " " 
                                  + specifiedServer
        | None                 -> procStartInfo.Arguments <- hostName
        procStartInfo.UseShellExecute <- false
        procStartInfo.RedirectStandardInput <- true
        procStartInfo.CreateNoWindow <- true
        let proc = System.Diagnostics.Process.Start(procStartInfo)
        proc.WaitForExit()
        proc.Close()
        printfn "done."
let dnsTool = new DNSLookup()
dnsTool.Lookup(hostName="www.google.com", server="ns1.google.com")
dnsTool.Lookup("www.google.com")

type ExampleClass () = 
    member this.ExampleMethod(requiredInt: int, 
                              ?optionalStr: string, ?optionalInt: int) = 
        let oStr = defaultArg optionalStr ""
        let oInt = defaultArg optionalInt -1        
        printfn "requiredInt=%d" requiredInt
        printfn "optionalStr=%s, IsNone=%b" oStr optionalStr.IsNone
        printfn "optionalInt=%d, IsNone=%b" oInt optionalInt.IsNone
ExampleClass().ExampleMethod(1234,"ABCD",5678)
ExampleClass().ExampleMethod(9876)


//==============================================================================
// Encapsulation.

open System
open System.IO
type DirectoryScanner () as this = 
    [<DefaultValue>] val mutable private targetDirectory : string
    [<DefaultValue>] val mutable private numberOfFiles : int
    [<DefaultValue>] val mutable private numberOfDirectories : int
    [<DefaultValue>] val mutable private seqOfFiles : seq<FileInfo>
    do 
        this.targetDirectory <- Environment.CurrentDirectory
        this.seqOfFiles      <- Seq.empty
    new (pathToScan) as this = 
        DirectoryScanner() 
        then this.targetDirectory <- pathToScan
    member this.ScanFiles () =        
        // Reset state.
        this.numberOfFiles <- 0
        this.numberOfDirectories <- 0
        this.seqOfFiles <- Seq.empty
       // Begin scan.
        let directoryInfo = new DirectoryInfo(this.targetDirectory)
        this.Scanner (directoryInfo)
        printfn "Scanned! ;)"
    member private this.Scanner (directory) = 
        // Scan all files in the current path
        let currentDirectoryFiles = directory.GetFiles()
        this.numberOfFiles <- this.numberOfFiles + currentDirectoryFiles.Length 
        let res  = seq {yield! currentDirectoryFiles}
        this.seqOfFiles <- Seq.append this.seqOfFiles res            
        // Scan all subdirectories in the current path.
        let subDirectories  = directory.GetDirectories()
        this.numberOfDirectories <- this.numberOfDirectories + 1
        subDirectories
        |> Array.iter (fun dir -> this.Scanner (dir))       
    member this.PrintReport () = 
        this.seqOfFiles
        |> Seq.iter (fun file -> printfn "%-O\t %-O bytes\t %-O" 
                                    file.CreationTime
                                    file.Length 
                                    file.Name)
    member this.GetTargetDirectory () = this.targetDirectory
    member this.SetTargetDirectory (directory) =
        this.targetDirectory <- directory
    member this.GetNumberOfFiles () = this.numberOfFiles
    member this.GetNumberOfDirectories () = this.numberOfDirectories
    member this.GetSeqOfFiles () = this.seqOfFiles
let objDirScanner = new DirectoryScanner(@"F:\Gadgets") 
objDirScanner.ScanFiles()
objDirScanner.GetSeqOfFiles ()
objDirScanner.GetNumberOfFiles ()
objDirScanner.GetNumberOfDirectories ()
objDirScanner.SetTargetDirectory (@"F:\Gadgets\Gadget0")
objDirScanner.ScanFiles()
objDirScanner.GetNumberOfFiles ()
objDirScanner.GetNumberOfDirectories ()

val mutable private m_IsPowerOn : bool
member this.GetPowerStatus () = 
    this.m_IsPowerOn

member this.SetTargetDirectory (directory) =
    if  not <| String.IsNullOrWhiteSpace (directory) then
        this.targetDirectory <- directory
    else
        printfn "Error: Invalid path."

//==============================================================================
// Properties.

type ATypeWithProperties (param) =
    let mutable myInternalValue = param
    // A read-only property. 
    member this.MyReadOnlyProperty with get() = myInternalValue
    // A write-only property. 
    member this.MyWriteOnlyProperty with set (value) = myInternalValue <- value
    // A read-write property. 
    member this.MyReadWriteProperty
        with private get () = myInternalValue
        and public set (value) = myInternalValue <- value

let myInstance = ATypeWithProperties(123)
myInstance.MyReadOnlyProperty
myInstance.MyReadOnlyProperty <- 456 (*error*)
myInstance.MyWriteOnlyProperty <- 456
myInstance.MyWriteOnlyProperty (*error*)
myInstance.MyReadWriteProperty <- "new Value"
myInstance.MyReadWriteProperty

member this.MyReadOnlyProperty = myInternalValue

// [ attributes-for-get ]
member this.MyReadWriteProperty with get () = myInternalValue
// [ attributes-for-set ]
member this.MyReadWriteProperty with set (value) = myInternalValue <- value


// Auto-implemented properties.
type MyClass(arg : int) =
    member val AutoProperty1 = arg
    member val AutoProperty2 = "" with get, set;;

type MyClass() =
    let random  = new System.Random()
    member val AutoProperty = random.Next() 
    member this.ExplicitProperty = random.Next()
let class1 = new MyClass()
class1.AutoProperty
class1.AutoProperty
class1.ExplicitProperty
class1.ExplicitProperty

// Mobile.fs
open System 
open System.Threading
open System.Drawing
open System.IO
[<AllowNullLiteral>]
type Mobile () as this = 
    // Private fields (Internal data).
    let mutable m_IsConnectedToNet = false
    let mutable m_IsPowerOn =        false
    let mutable m_Dimensions =       {Height= 0.0<mm>; Width= 0.0<mm>;
                                      Diameter= 0.0<mm>}                                  
    let mutable m_Weight =           0.0<gr>  
    // Auto properties. 
    member val Battery =         "Unspecified" with get,set
    member val CPU =             "Unspecified" with get,set
    member val GPU =             "Unspecified" with get,set
    member val Display =         "Unspecified" with get,set
    member val Memory =          "Unspecified" with get,set
    member val Sensors =         "Unspecified" with get,set
    member val Network =         "Unspecified" with get,set
    member val Model =           "Unspecified" with get,set
    member val Manfuctory =      "Unspecified" with get,set
    member val Extra =           "Unspecified" with get,set
    member val OS =              {OS= MobileOSType.Other; Version= "Unspecified"} 
                                               with get,set 
    member val Color =           Color.Black with get,set
    member val MobileData =      false with get,set
    member val Wifi =            false with get,set
    member val IsNFCSupported =  false with get,set
    member val IsDLNASupported = false with get,set
    member val PhoneBook =       new PhoneBook (List.empty<Contact>,this) 
                                 with get,set
                                 // Maincamera, webcam 
    member val Cameras =         [|new Camera (info="Unspecified", owner=this); 
                                   new Camera (info="Unspecified", owner=this)|] 
                                 with get,set
    member val Bluetooth =       new Bluetooth (name="Unknown", info="Unspecified",
                                                owner=this) with get,set
    member val GPS =             new GPS (info="Unspecified", owner=this) 
                                 with get,set 
    // Explicit Properties.
    member this.Dimensions
        with get() = m_Dimensions
        and  set(newDimension) = 
            match newDimension with
            | {Height= h; Width= w; Diameter= d} when 
                (h>000.0<mm> && w>000.0<mm> && d>000.0<mm>) &&
                (h<500.0<mm> && w<500.0<mm> && d<100.0<mm>)  
                -> m_Dimensions <- newDimension
            |_  -> failwith "Invalid Dimensions!"
    member this.Weight
        with get() = m_Weight
        and  set(newWeight) = 
            match newWeight with
            | weight when weight>000.0<gr> &&
                          weight<500.0<gr> -> m_Weight <- newWeight
            |_  -> failwith "Invalid Weight!"
    member this.IsConnectedToNet = m_IsConnectedToNet
    member this.IsPowerOn =        m_IsPowerOn 
    // Poweron device.
    member this.PowerOn () = 
        m_IsPowerOn <- true
        printfn "Hi,Welcome..."
        Console.Beep (345,400)
    // Poweroff device.
    member this.PowerOff () = 
        m_IsPowerOn <- false
        m_IsConnectedToNet <- false
        this.Wifi <- false
        this.MobileData <- false
        this.Bluetooth.Enable <- false
        this.GPS.Enable <- false
        printfn "Goodbye..."
        Console.Beep (345,400)
        Console.Beep (345,900)
    // Connect mobile phone to internet.
    member this.ConnectToInternet () =         
        this.CheckPowerStatus ()
        match this.Wifi,this.MobileData with
        | true, _ -> 
            printfn "Connecting to internet via wifi..."
            m_IsConnectedToNet <- true
        | _, true -> 
            printfn "Connecting to internet via mobile data..."
            m_IsConnectedToNet <- true  
        | _       -> 
            printfn "Turn wifi or mobile data on to use internet services."
    // Make a voice call to specified number.
    member this.MakeCall numberToCall = 
        this.CheckPowerStatus ()
        this.PhoneBook.Find (numberToCall)
        |> function 
            | Some contact -> 
                printfn "Dialing %s... " 
                    (contact.Name + " " + contact.Family + "(" + contact.Number + ")")
            | None   ->  printfn "Dialing %s..." numberToCall
    // Send new message to specified number.
    member this.SendMessage numberToMessage message = 
        this.CheckPowerStatus ()
        this.PhoneBook.Find (numberToMessage)
        |> function 
            | Some contact -> 
                printfn "Sending \"%s\" to %s..." 
                    message 
                    (contact.Name + " " + contact.Family + "(" + contact.Number + ")")
            | None   ->  printfn "Sending \"%s\" to %s..." message numberToMessage
    member private this.CheckPowerStatus () = 
        if not this.IsPowerOn then
            failwith "First of all turn on mobile please."
    // Explicit Constructors.     
    new (manufactory, model) as this = Mobile() then
         this.Manfuctory <- manufactory
         this.Model <- model    
    new (manufactory, model, phonebook, cameras, bluetooth, gps) 
         as this = Mobile(manufactory, model) then
         this.PhoneBook <- phonebook
         this.Cameras <- cameras
         this.Bluetooth <- bluetooth
         this.GPS <- gps 
and [<Measure>] mm
and [<Measure>] gr
and MobileOSType = 
    | Android        | IOS 
    | WindowsPhone   | BlackBerry 
    | MeeGO          | Symbian 
    | UbuntuMobile   | FirefoxOS 
    | WebOS          | Bada 
    | Java           | Other
and MobileOS  = {OS: MobileOSType; Version: string}
and Dimensions = {Height: float<mm>; Width: float<mm>; Diameter: float<mm>}

// Contact Manager System.  
and PhoneBook () = 
    member val Contacts = List.empty<Contact> with get,set
    member this.NumberOfContacts = this.Contacts.Length
    member val Owner = null with get,set
    new (contactList, owner: Mobile) as this = PhoneBook() then
        this.Contacts <- contactList
        this.Owner <- owner
    // Add new contact.
    member this.Add (name, family, number) = 
        this.CheckPowerStatus ()
        let newContact = {Name= name; Family= family; Number= number}
        List.exists(fun (contact: Contact) -> contact = newContact ) this.Contacts
        |>
        function 
        | true -> printfn "Your target contact is exist among contacts."
        | _    -> this.Contacts <- (newContact :: this.Contacts)  
    // Remove an exist contact.
    member this.Remove (name, family, number) =
        this.CheckPowerStatus () 
        let contactToRemove = {Name= name; Family= family; Number= number}
        List.filter(fun (contact: Contact) -> 
            contact.Name.ToLower() <> contactToRemove.Name.ToLower() || 
            contact.Family.ToLower() <> contactToRemove.Family.ToLower() ||
            contact.Number.ToLower() <> contactToRemove.Number.ToLower())
            this.Contacts
        |>
        (fun contactList -> this.Contacts <- contactList)
    // Search for contact.
    member this.Find (numberTofind) =
        this.CheckPowerStatus () 
        List.tryFind 
            (fun (contact: Contact) -> 
            String.op_Equality( contact.Number, numberTofind)) 
            this.Contacts

    member private this.CheckPowerStatus () = 
        if not this.Owner.IsPowerOn  then
            failwith "First of all turn on mobile please."
and Contact = { mutable Name:string; mutable Family:string; mutable Number:string}

// Camera System.    
and Camera () = 
    member val SmileDetection = false with get,set
    member val AutoFocus = false with get,set
    member val Info = "Unspecified" with get,set
    member val Owner = null with get,set
    new (info, owner: Mobile) as this = Camera() then
        this.Info <- info
        this.Owner <- owner      
    // Capture an image via camera.
    member this.TakePicture (pQuality:PictureQuality, pEffect: PictureEffect) = 
        this.CheckPowerStatus ()
        let checkOptions () = 
           match (this.AutoFocus, this.SmileDetection) with
           | true, true  -> printfn "Auto focus is on.";printfn"Smile detect is on." 
           | true, false -> printfn "Auto focus is on."
           | false, true -> printfn "Smile detect is on."    
           | _           -> ()
        let capture () = 
            checkOptions()
            printfn "Capturing picture..."
            printfn "Quality= %A, Effect= %A TimeTag= %A GeoTag= %A" 
                     pQuality pEffect 
            <| DateTime.Now.ToLongDateString() + " " +
               DateTime.Now.ToLongTimeString()
            <| this.Owner.GPS.CurrentLocation  
        capture ()
    // Record video via camera.
    member this.RecordVideo () = 
            this.CheckPowerStatus ()
            printfn "Recording video..."
    member private this.CheckPowerStatus () = 
        if not this.Owner.IsPowerOn then
            failwith "First of all turn on mobile please."
and PictureEffect = 
    | Retro          | Old 
    | LightHDR       | HeavyHDR 
    | Dreamlike      | MagicColor 
    | Negative       | Colorful 
    | BlackAndWhite  | Normal 
and PictureQuality = | High | Normal | Low  

// Bluetooth System.      
and Bluetooth () = 
    member val Enable = false with get,set
    member val Visible = false with get,set
    member val Name = "Unknown" with get,set
    member val Info = "Unspecified" with get,set
    member val Owner = null with get,set
    member this.NumberOfDevices = List.length <| this.GetDevices ()
    new (name, info, owner: Mobile) as this = Bluetooth() then
        this.Name <- name
        this.Info <- info
        this.Owner <- owner         
    // Share list of files with specified device.
    member this.SendFile (filesList: string list, device) = 
        this.CheckPowerStatus () 
        let beginSend () = 
            List.iter (fun file -> 
                printfn "Sending \"%s\" to \"%s\" ..." file device
                printfn "done.") filesList
        match this.Enable with
        | true -> beginSend ()
        | _    -> printfn "Turn bluetooth on to use bluetooth services." 
    // Recive the list of files from specified device.
    member this.ReceiveFile (device) = 
        this.CheckPowerStatus () 
        let beginReceive() = 
            let count = Random().Next(1,5)
            let filesList = 
                [ for idx= 1 to count do
                    let file =  Path.GetRandomFileName()
                    printfn "Receiving \"%s\" from \"%s\" ..." file device
                    yield file
                    printfn "done." ] 
            filesList
        match this.Enable with
        | true -> beginReceive ()
        | _    -> printfn "Turn bluetooth on to use bluetooth services."; []
    // Retrieve the list of available devices.
    member this.GetDevices () =
        this.CheckPowerStatus ()  
        let beginScan () =
            printfn "Scanning devices..."
            let count = Random().Next(1,10)
            let deviceList = 
                [ for idx= 1 to count do
                    let device =  "Device"+idx.ToString()
                    yield device ] 
            deviceList 
        match this.Enable with
        | true -> beginScan ()
        | _    -> printfn "Turn bluetooth on to use bluetooth services."; []
    member private this.CheckPowerStatus () = 
        if not this.Owner.IsPowerOn then
            failwith "First of all turn on mobile please."

// Global Positioning System.
and GPS () = 
    member val Enable = false with get,set
    member val Info = "Unspecified" with get,set
    member val Owner = null with get,set
    member this.CurrentLocation = this.GetLocation ()    
    new (info, owner: Mobile) as this = GPS() then
        this.Info <- info
        this.Owner <- owner
    // Calculate current location of user.
    member private this.GetLocation() = 
        this.CheckPowerStatus ()
        let getLatitude() =
            let degree, minute, second = 
                Random(int DateTime.Now.Ticks).Next(-90,90),
                Random(int DateTime.Now.Ticks).Next( 00,60), 
                Random(int DateTime.Now.Millisecond).Next( 00,60)
            let directionSymbol = 
                match Math.Sign(degree) with
                | 1  -> "N"
                | 0  -> "Equator"
                | _  -> "S"
            (degree |> sprintf "%03d").ToString() + "° " + 
            (minute |> sprintf "%02d").ToString() + "' " +
            (second |> sprintf "%02d").ToString() + "\" " +
            directionSymbol
        let getLongitude() =
            let degree, minute, second = 
                Random(int DateTime.Now.Ticks).Next(-180,180),
                Random(int DateTime.Now.Ticks).Next( 000,060), 
                Random(int DateTime.Now.Millisecond).Next( 000,060)
            let directionSymbol = 
                match Math.Sign(degree) with
                | 1  -> "E"
                | 0  -> "Prime Meridian"
                | _  -> "W"
            (degree |> sprintf "%03d").ToString() + "° " + 
            (minute |> sprintf "%02d").ToString() + "' " +
            (second |> sprintf "%02d").ToString() + "\" " + 
            directionSymbol
        match this.Enable with
        | true -> let Coordinates = {Latitude= ""; Longitude= ""}
                  Coordinates.Latitude <- getLatitude()
                  Thread.Sleep(15) // wait 15 milisecond.
                  Coordinates.Longitude <- getLongitude() 
                  Coordinates
        | _    -> printfn "Turn GPS on to use GPS services."; 
                  {Latitude="Unknown"; Longitude= "Unknown"}
    // Navigate from current to specified location.
    member this.Navigate (destination: GeoLocation) =
        this.CheckPowerStatus ()
        match this.Enable with
        | true -> printfn "Navigating from %A to %A..." 
                    this.CurrentLocation destination
        | _    -> printfn "Turn GPS on to use GPS services."
    member private this.CheckPowerStatus () = 
        if not this.Owner.IsPowerOn then
            failwith "First of all turn on mobile please."      
and GeoLocation = {mutable Latitude:string; mutable Longitude:string}

let googleNexus = new Mobile("LG", "Nexus X-32GB")
googleNexus.Battery <- "Li_Ion 2300 mAh"
googleNexus.CPU <- "Qualcomm Snapdragon 800 Chipset, Quad-core Krait400 CPU, 2.3GHz"
googleNexus.GPU <- "Adreno 330"
googleNexus.Display <- "True HD IPS Plus LCD, Capacitive, 4.95 inches,
                        FullHD 1920x1080 (445 ppi)"
googleNexus.Sensors <- "Accelerometer, gyro, proximity, Barometer"
googleNexus.Network <- "2G, 3G, 4G"
googleNexus.Memory <- "DDR3, 2GB"
googleNexus.OS <- {OS= MobileOSType.Android; Version= "5.0 Lollipop"}
googleNexus.Color <- Color.Black
googleNexus.IsNFCSupported <- true
googleNexus.IsDLNASupported <- false
googleNexus.Dimensions <- {Height= 138.0<mm>; Width= 69.2<mm>; Diameter= 8.6<mm>}
googleNexus.Weight <- 130.0<gr>

googleNexus.PowerOn()

googleNexus.Wifi <- true
googleNexus.ConnectToInternet ()

googleNexus.PhoneBook.Add ("Stephanie", "Booth", "0012653249")
googleNexus.PhoneBook.Add ("Thomas", "Hawk", "0016253201")
googleNexus.PhoneBook.Contacts
googleNexus.PhoneBook.NumberOfContacts

// use main camera
let mainCamera = googleNexus.Cameras.[0]
mainCamera.Info <- "5MP- 1944×2592 Pixel- Geo&Time Tagging"
mainCamera.SmileDetection <- true
mainCamera.AutoFocus <- true
mainCamera.TakePicture(PictureQuality.High, PictureEffect.MagicColor)

mainCamera.RecordVideo()

// use front camera
let frontCamera = googleNexus.Cameras.[1] 
frontCamera.Info <- "1.2 MP"
frontCamera.SmileDetection <- false
frontCamera.AutoFocus <- false
googleNexus.GPS.Enable <- true
frontCamera.TakePicture(PictureQuality.Normal, PictureEffect.MagicColor)
frontCamera.RecordVideo()

googleNexus.Bluetooth.Enable <- true
googleNexus.Bluetooth.Name <- "My" + googleNexus.Model
googleNexus.Bluetooth.GetDevices()
googleNexus.Bluetooth.NumberOfDevices
let dataList = [@"c:\sampleVideo.mkv"; @"c:\samplePicture.jpg"] 
let destinationDevice = "dc:a6:71:1c:c4:e0"
googleNexus.Bluetooth.SendFile (dataList, destinationDevice)
let sourceDevice = "dc:a1:35:4c:c8:e5" 
googleNexus.Bluetooth.Visible <- true 
googleNexus.Bluetooth.ReceiveFile (sourceDevice)

googleNexus.GPS.Enable <- true
googleNexus.GPS.Info <- "Gps-AGPS"
googleNexus.GPS.CurrentLocation
let coordsOfDestination = 
    {Latitude="""35° 42' 13" N"""; Longitude= """51° 12' 46" E"""}
googleNexus.GPS.Navigate (coordsOfDestination)

let hero1 = new Mobile("Microsoft", "Hero1")
let objPhoneBook = new PhoneBook()
objPhoneBook.Contacts <- googleNexus.PhoneBook.Contacts
objPhoneBook.Owner <- hero1
let objMainCamera = new Camera()
objMainCamera.AutoFocus <- true
objMainCamera.Owner <- hero1
let objFrontCamera = googleNexus.Cameras.[1]
objFrontCamera.Owner <- hero1
let objBluetooth = new Bluetooth()
objBluetooth.Name <- "MyDesiredBthName"
objBluetooth.Visible <- true
objBluetooth.Enable <- true
objBluetooth.Owner <- hero1
let objGPS= new GPS()
objGPS.Enable <- true
objGPS.Owner <- hero1
hero1.PhoneBook <- objPhoneBook
hero1.Cameras <- [|objMainCamera; objFrontCamera|]
hero1.Bluetooth <- objBluetooth
hero1.GPS <- objGPS
hero1.PowerOn()
hero1.PhoneBook.Contacts
hero1.Cameras.[0].TakePicture(PictureQuality.Normal, PictureEffect.Normal)
hero1.Bluetooth.SendFile([@"c:\sampleMusic.mp3"], "dc:a6:75:4c:c5:e2")
hero1.GPS.CurrentLocation

let objPhoneBook = new PhoneBook()
objPhoneBook.Contacts <- googleNexus.PhoneBook.Contacts
let objMainCamera = new Camera()
objMainCamera.AutoFocus <- true
let objFrontCamera = googleNexus.Cameras.[1]
let objBluetooth = new Bluetooth()
objBluetooth.Name <- "MyDesiredBthName"
objBluetooth.Visible <- true
objBluetooth.Enable <- true
let objGPS= new GPS()
objGPS.Enable <- true
let hero2 = new Mobile("Microsoft", "Hero2", 
              objPhoneBook, [|objMainCamera; objFrontCamera|], objBluetooth, objGPS)
hero2.PhoneBook.Owner <- hero2
hero2.Cameras.[0].Owner <- hero2
hero2.Cameras.[1].Owner <- hero2
hero2.Bluetooth.Owner <- hero2
hero2.GPS.Owner <- hero2
hero2.PowerOn()
hero2.GPS.Navigate ({Latitude="""22° 30' 53" N"""; Longitude="""85° 09' 16" N"""})

hero2.PowerOn()
hero2.GPS.Navigate ({Latitude="""22° 30' 53" N"""; 
                       Longitude="""85° 09' 16" N"""})

member private this.CheckPowerStatus () = 
    match this.Owner with
    | null -> failwith "Owner of GPS is not specified."
    | _    -> if this.Owner.IsPowerOn = false then
                failwith "First of all turn on mobile please."

// Setting properties in the constructor.
let colorfulIphone = new Mobile ("Apple", "iPhone XC",
                          CPU= "Apple AX Chipset Dual Core CPU",
                          GPU= "PowerVR SGX 543 MP3",
                          Color= Color.Green,
                          OS= {OS= MobileOSType.IOS; Version= "8.0"},
                          Dimensions= {Height= 124.4<mm>; Width= 59.2<mm>; 
                          Diameter= 8.9<mm>},
                          Weight = 130.0<gr>,
                          Extra= "32GB-LTE Supported")

//==============================================================================
// Static members.

open System
Math.Abs(-29.0645)

// Static members.
open System.Net
open System.Net.Mail
type CookieInspector() =
    static let mutable seqOfCookies = seq []
    static member Count = Seq.length seqOfCookies
    static member BeginInspect(url:string) = 
        let request = WebRequest.Create(url) :?> HttpWebRequest
        request.CookieContainer <- new CookieContainer()
        let response = request.GetResponse() :?> HttpWebResponse
        seqOfCookies <- seq { for cookie in response.Cookies -> cookie } 
        seqOfCookies
CookieInspector.BeginInspect(url= @"http://www.bing.com")
CookieInspector.Count
let newInstance = CookieInspector() (*error*)

// Static fields.
type ChatRoom(rName, rServer) = 
    static let mutable availableRooms = 2
    static let maxUsers = 3
    let mutable usersList = []
    do if availableRooms > 0 then
            printfn "Connecting to %s..." rServer 
            availableRooms <- availableRooms - 1
            printfn "Connected. %s room successfully created.\n%d room(s) left" 
                rName availableRooms
        else failwith "No more room left."
    member room.Users = usersList
    member room.Join(uName) = 
        if room.Users.Length < maxUsers then
            match List.exists (fun joinedUser -> joinedUser = uName) room.Users with
            | false -> printfn "Joining Room..." 
                       usersList <- List.Cons(uName, room.Users)
                       printfn "%s Joined.(%d of %d) Online Users=%A" 
                               uName room.Users.Length maxUsers room.Users 
            | true -> printfn "%s nickname is already in use, 
                      please try with different name." uName
        else  failwith "Room is full."
    member room.DisJoin(uName) =
        if room.Users.Length > 0 then
            match List.exists (fun joinedUser -> joinedUser = uName) room.Users with
            | true -> printfn "Disjoining Room..." 
                      usersList <- List.filter (fun user-> user <> uName) room.Users
                      printfn "%s Disjoined.(%d of %d) Online Users=%A" 
                              uName room.Users.Length maxUsers room.Users
            | false -> printfn "%s is not in this room, 
                       please try with different name." uName
        else  failwith "Room is empty."
let server = "http://OnlineChats.com:8080/ChRoomMgr"
let chRoom1 = ChatRoom("Scientific", server)
chRoom1.Join("edvard")
chRoom1.Join("keith")
let chRoom2 = ChatRoom("International", server)
chRoom2.Join("sarah")
chRoom2.Join("john")
chRoom2.Join("sophia")
chRoom2.Join("jimmy")
chRoom2.DisJoin("sophia")
chRoom2.Join("jimmy")
let chRoom3 = ChatRoom("Family", server)

type ExampleType(value) as this =
    [<DefaultValue>] 
    val mutable NonStaticFiled : int 
    static let StaticFiled = value
    member this.NonStaticProperty = StaticFiled
    static member StaticProperty = NonStaticFiled
    member this.NonStaticMethod() =  ExampleType.StaticMethod ()
    static member StaticMethod() =  this.NonStaticMethod()

static member (+) (room1:ChatRoom, room2: ChatRoom) = 
        (room1.Users.Length) + (room2.Users.Length)
let r1 = new ChatRoom("Public", "http://OnlineChats.com:8080/ChRoomMgr")
let r2 = new ChatRoom("Family", "http://OnlineChats.com:8080/ChRoomMgr")
r1.Join("admin")
r1.Join("bill")
r2.Join("jack")
r1 + r2

type MathClass = 
    [<DefaultValue>] 
    static val mutable private uselessField: int
    static member PI = System.Math.PI
    static member E = System.Math.E
    static member inline Sum x y = x + y
    static member inline Dif x y = x - y
    static member inline Mul x y = x * y
    static member inline Div x y = x / y
MathClass.Div 16 4
MathClass.Mul 2 3 |> MathClass.Sum 6
MathClass.PI
module MathModule = 
    let pI = System.Math.PI
    let e = System.Math.E
    let inline sum x y = x + y
    let inline dif x y = x - y
    let inline mul x y = x * y
    let inline div x y = x / y
MathModule.dif 2 6
4.0 + MathModule.pI |> MathModule.mul 2.0

//==============================================================================
// Static constructors.

type ClassWithStaticConstructor(a:int, b:int) =
    static let mutable instanceCounter = 0
    let x = a * 2
    let y = b * 2
    do  printfn "Initializing object..."
        instanceCounter <- instanceCounter + 1
        printfn "Instance constructor sets x, y to %d, %d." x y
    static do 
        printfn "Initializing Type... "
        printfn "Static constructor sets instance counter to %d." instanceCounter
    member this.Prop1 = x + 2
    member this.Prop2 = y + 2
    static member NumberOfInstances = instanceCounter
let instance1 = ClassWithStaticConstructor(3,5)
let instance2 = ClassWithStaticConstructor(7,9)
ClassWithStaticConstructor.NumberOfInstances
instance1.Prop1, instance1.Prop2

//==============================================================================
// Method Overloading.

open System
String.Compare(strA="123", strB="456")
String.Compare(strA="286", indexA=1, strB="731", indexB=1, length=2);;

member this.Add (newContact) = 
    this.Add(newContact.Name, newContact.Family, newContact.Number)
member this.Remove (oldContact) =
    this.Remove(oldContact.Name, oldContact.Family, oldContact.Number)

let googleNexus = new Mobile()
googleNexus.PowerOn()
let george = {Name="George"; Family="Best"; Number="00465332456"}
googleNexus.PhoneBook.Add("William","Draper", "00314123456")
googleNexus.PhoneBook.Add(george)
googleNexus.PhoneBook.Contacts
googleNexus.PhoneBook.Remove("Pete","Stanko", "007723130033")
googleNexus.PhoneBook.Remove(george)
googleNexus.PhoneBook.Contacts

// Overloaded Constructors.
type ClassWithFourConstructor()  = 
    new (a:int) as this = ClassWithFourConstructor() then 
        this.Value <- a.ToString()
    new (a:float) as this = ClassWithFourConstructor() then 
        this.Value <- a.ToString()
    new (a:char) as this = ClassWithFourConstructor() then 
        this.Value <- a.ToString()
    member val Value = "" with get, set
ClassWithFourConstructor()
ClassWithFourConstructor(34)
ClassWithFourConstructor(45.5)
ClassWithFourConstructor('6')

static member (+) (room:ChatRoom, uName: string) = 
    room.Join(uName)
let room = new ChatRoom ("http://OnlineChats.com:8080/ChRoomMgr","Scientific")
room + "sarah"
room + "bill"

type Customer = { mutable First: string; mutable Last: string; 
    mutable SSN: uint32; mutable AccountNumber: uint32; } with
    member this.SetInfo first last = 
        this.First <-first
        this.Last <-last
    member this.SetInfo (first, last, ssn, number) = 
        this.First <- first
        this.Last <- first
        this.SSN <- ssn
        this.AccountNumber <- number

type T() =    
    member this.M1(arg1, arg2, [<OptionalArgument>] arg3) = arg1 + arg2 + arg3
let obj = new T()
obj.M1(36,14)
obj.M1(36,14,5)

//==============================================================================
// Access levels.

// Module1.fs:
module Module1 
type private PrivateType() = 
    let f = 5 
    member private this.M() = f * 2 
    member this.P = this.M() + 100 
type internal InternalType() = 
    let f = 10 
    member private this.M() = f * 4
    member this.P = this.M() + 200  
let private myPrivateObj = new PrivateType() 
let internal myInternalObj = new InternalType() 
let mResult1 = myPrivateObj.M()  (*Error*)
let mResult2 = myInternalObj.M() (*Error*)
let pResult1 = myPrivateObj.P
let pResult2 = myInternalObj.P
let fResult1 = myPrivateObj.f    (*Error*)
let fResult2 = myInternalObj.f   (*Error*)	
// Module2.fs:
module Module2 
open Module1
let privateObj = new PrivateType()            (*Error*)
let internal internalObj = new InternalType()
let result1 = internalObj.M()                 (*Error*)
let result2 = internalObj.P
let result3 = internalObj.f                   (*Error*)
let result4 = mResult1, mResult2, pResult1, pResult2, fResult1, fResult2	

let myPrivateObj = new PrivateType() 
let myInternalObj = new InternalType()

//==============================================================================
// Indexed properties.

open System
type ArrayCollection<'T> (length) = 
    let data = Array.zeroCreate<'T> length
    member this.Type = typeof<'T>
    member this.Length = data.Length
    member this.Elements
      with get(index) = data.[index]
      and set index value = data.[index] <- value
let printArray (arrayToPrint: ArrayCollection<'T>) =
    printfn "Array Elements="
    for idx= 0 to arrayToPrint.Length - 1 do
        printfn "  %A" (arrayToPrint.Elements(idx))
let sampleArray = ArrayCollection<string>(5)
sampleArray.Elements(0) <- "One"
sampleArray.Elements(1) <- "Two"
sampleArray.Elements(2) <- "Three"
sampleArray.Elements(3) <- "Four"
sampleArray.Elements(4) <- "Five"
printArray sampleArray

// Default and non-default indexed properties.
type ArrayCollection<'T> (length) = 
    let data = Array.zeroCreate<'T> length 
    member this.Length = data.Length
    member this.Type = typeof<'T>
    member this.Elements
      with get(index) = data.[index]
      and set index value = data.[index] <- value
    member this.Item
      with get(index) = data.[index]
      and set index value = data.[index] <- value
let printArray (arrayToPrint: ArrayCollection<'T>) =
    printfn "Array Elements="
    for idx= 0 to arrayToPrint.Length - 1 do
        printfn "  %A" arrayToPrint.[idx]
let anotherSampleArray = ArrayCollection<int>(3)
anotherSampleArray.[0] <- 35
anotherSampleArray.[2] <- 96
printArray anotherSampleArray

member this.Item
    with get(index: string) = data.[System.Int32.Parse(index)]
    and set(index: string) value = data.[System.Int32.Parse(index)] <- value
let byteArray = ArrayCollection<byte>(3)
byteArray.["0"] <- 10uy
byteArray.["1"] <- 20uy
byteArray.["2"] <- 30uy
byteArray.["0"], byteArray.["1"], byteArray.["2"]

member this.GetSlice(lowerBound: int option, upperBound: int option) =
    match lowerBound, upperBound with
    | Some(lB), Some(uB) -> data.[lB..uB]
    | Some(lB), None     -> data.[lB..]
    | None, Some(uB)     -> data.[..uB]
    | None, None         -> data.[*]
member this.SetSlice(lowerBound: int option, upperBound: int option, value) =
    match lowerBound, upperBound with
    | Some(lB), Some(uB) -> data.[lB..uB] <- value
    | Some(lB), None     -> data.[lB..]   <- value
    | None, Some(uB)     -> data.[..uB]   <- value
    | None, None         -> data.[*]      <- value
let intArray = ArrayCollection<int>(10)
(*SetSlice*)
intArray.[*] <- Array.init (intArray.Length) (fun elem -> elem * 2)
(*GetSlice*)
intArray.[*]
intArray.[0..4]
intArray.[8..]
intArray.[..2]

type Matrix<'T> (length1, length2) =
    let mutable table = Array2D.zeroCreate<'T> length1 length2
    member this.Length1 = table.GetLength(0)
    member this.Length2 = table.GetLength(1)
    member this.Item
        with get (idx1, idx2) = table.[idx1, idx2]
        and set (idx1, idx2) value = table.[idx1, idx2] <- value
let sampleMatrix = new Matrix<int>(5,5)
for row = 0 to sampleMatrix.Length1 - 1 do
    for col = 0 to sampleMatrix.Length2 - 1 do
        sampleMatrix.[row, col] <- row * row
for row = 0 to sampleMatrix.Length1 - 1 do
    for col = 0 to sampleMatrix.Length2 - 1 do
        printf "%02d " sampleMatrix.[row, col]
    printfn ""

//==============================================================================
// Type Extensions.

type System.Int32 with
    member this.IsOdd = this % 2 = 0 |> not
    member this.FromString(str: string) = System.Int32.Parse(str)
let intValue = 34
intValue.IsOdd
intValue.FromString("320014")

type ChatRoom with
    member room.Exist(uName) = 
        room.Users
        |> List.tryFind (fun user -> user = uName) 
        |> Option.isSome
// code, code and code...
type ChatRoom with 
    member room.SendMessage (fromUName, toUName, message) = 
        let exist1 = room.Exist fromUName
        let exist2 = room.Exist toUName
        match exist1, exist2 with
        | true, true  -> printfn "Private message %s -> %s: %s" 
                                  fromUName toUName message
        | true, false -> printfn "%s is not in this room." toUName
        | _           -> printfn "First of all, join to room Please."
    member room.SendMessage (fromUName, message) =
        let exist = room.Exist fromUName
        match exist with
        | true  -> printfn "Public message %s -> room: %s" fromUName message
        | false -> printfn "First of all, join to room Please."
room.SendMessage("bill", "hi all!")
room.SendMessage("bill", "sarah", "Hey sarah, where r u?")
room.SendMessage("sarah", "bill", "Hey, Im on the way!!")
room.SendMessage("bill", "sarah", "See u.")
room.DisJoin("sarah")
room.SendMessage("bill", "sarah", "r u there???")

type RType(a,b) = 
    [<DefaultValue>] val mutable PublicMember:float 
    member val internal InternalMember = a + 1.0
    member private this.PrivateMember() = b - 1.0
type RType with
    member this.ExtentionMember() = 
        printfn "PublicMember()= %f InternalMember()= %f PrivateMember= %f" 
            this.PublicMember this.InternalMember (this.PrivateMember())

// Part0.fs:
namespace RealMembers
module Part0 =
    type PartialType() =
        member this.RealMethod() = printfn "Printed from part0..." 
        member this.RealProperty = "Returned from part0..."
// Part1.fs:
namespace ExtentionMembers
module Part1 =
    open RealMembers
    type Part0.PartialType with 
        static member ExtMethodA() = printfn "Printed from part1..." 
        static member ExtPropertyA = "Returned from part1..."
// Part2.fs:
namespace ExtentionMembers
module Part2 =
    open RealMembers
    type Part0.PartialType with 
        member this.ExtMethodB() = printfn "Printed from part2..." 
        member this.ExtPropertyB = "Returned from part2..."
// Program.fs:
module ResultantType 
open System
open RealMembers.Part0
open ExtentionMembers.Part1
open ExtentionMembers.Part2
[<EntryPoint>]
let main argv = 
    let theInstance = PartialType()
    theInstance.RealMethod()
    printfn "%s" theInstance.RealProperty
    PartialType.ExtMethodA()
    printfn "%s" PartialType.ExtPropertyA
    theInstance.ExtMethodB()
    printfn "%s" theInstance.ExtPropertyB
    Console.ReadKey() |> ignore
    0 // return an integer exit code	

//==============================================================================
//Inheritance.

open System
open System.Drawing
type Shape () = (*Base Class.*)
    static let mutable shapeCounter = 0
    let mutable sh_Nam = "Unspesified"
    let mutable sh_Clr = Color.Black
    let mutable sh_Siz = SizeF(0.0f, 0.0f)
    let mutable sh_Pos = PointF(0.0f, 0.0f)       
    let mutable sh_Rot = 0.0f 
    do shapeCounter <- shapeCounter + 1
       sh_Nam <- "Shape" + shapeCounter.ToString()
    (*Properties.*)
    member this.Name 
        with get()= sh_Nam 
        and set(name)= sh_Nam <- name
    member this.Color 
        with get()= sh_Clr
        and set(color)= this.Fill(color)
    member this.Size 
        with get()= sh_Siz 
        and set(size:SizeF)= this.Resize(size.Width, size.Height)
    member this.Position 
        with get()= sh_Pos 
        and set(position:PointF)= this.Move(position.X, position.Y)
    member this.Rotation 
        with get()= sh_Rot 
        and set(angle)= this.Rotate(angle)
    (*Methods*)
    member this.Draw () = 
        printfn "Drawing shape..."
    member this.Delete () = 
        printfn "Deleting shape..."
    member private this.Fill (newColor) = 
        printfn "Filling shape..." 
        sh_Clr <- newColor
        printfn "Well done. Shape color= %A" this.Color
    member private this.Resize (newWidth, newHeight) = 
        printfn "Resizing shape..."
        sh_Siz <- SizeF(newWidth, newHeight)
        printfn "Well done. Shape size= %A" this.Size
    member private this.Move (newX, newY) = 
            printfn "Moving shape..."
            sh_Pos <- PointF(newX, newY)
            printfn "Well done. Shape position= %A" this.Position    
    member private this.Rotate (newAngle) = 
            if newAngle < -360.0f && newAngle > 360.0f then
                failwith "The measurment must be between -360° and 360°."
            printfn "Rotating shape..." 
            printfn "Well done. Shape angle= %.2f" newAngle

// Derived class.
type Rectangle(w, h) as this = 
    inherit Shape()
    do this.Size <- SizeF(w,h)
    new() = Rectangle(0.0f,0.0f)
    member this.GetSide(sideID) = 
        match sideID with
        | w when w = 1 || w= 3 -> Some this.Size.Width
        | h when h = 2 || h= 4 -> Some this.Size.Height
        | _                    -> None
type Ellipse(w, h) as this = 
    inherit Shape()
    do this.Size <- SizeF(w,h)
    new() = Ellipse(0.0f,0.0f)
    member this.MinorAxis
        with get()= Math.Min(this.Size.Width,this.Size.Height)
    member this.MajorAxis
        with get()= Math.Max(this.Size.Width,this.Size.Height)

let objRectangle = new Rectangle(30.0f, 50.0f)
objRectangle.Draw()
objRectangle.Color <- Color.Blue
objRectangle.Position <- PointF (1.0f, 1.0f)
objRectangle.Rotation <- -45.0f
objRectangle.GetSide(2)
objRectangle.Name;
let objEllipse = new Ellipse(100.0f, 80.0f, 
                             Position= PointF (650.0f, 345.0f))
objEllipse.Draw()
objEllipse.MinorAxis
objEllipse.MajorAxis
objEllipse.Name

//==============================================================================
// Abstract and virtual Members.

abstract member Name: string with get,set
default this.Name
    with get()= sh_Nam 
    and set(name)= sh_Nam <- name
abstract member Color: Color with get,set
default this.Color
    with get()= sh_Clr
    and set(color)= this.Fill(color)
abstract member Size: SizeF with get,set
default this.Size
    with get()= sh_Siz 
    and set(size:SizeF)= this.Resize(size.Width, size.Height)
abstract member Position: PointF with get,set
default this.Position
    with get()= sh_Pos 
    and set(position:PointF)= this.Move(position.X, position.Y)
abstract member Rotation: float32 with get,set
default this.Rotation 
    with get()= sh_Rot 
    and set(angle)= this.Rotate(angle)
abstract member Perimeter: float32
default this.Perimeter = 0.0f
abstract member Area: float32
default this.Area = 0.0f
abstract member Draw: unit -> unit
default this.Draw () = printfn "Drawing shape..." 
abstract member Delete: unit -> unit    
default this.Delete() = printfn "Deleting shape..."

// Overriding.
type Rectangle(w, h) as this = 
    inherit Shape()
    do this.Size <- SizeF(w,h)
    new() = Rectangle(0.0f,0.0f)
    member this.GetSide(sideID) = 
        match sideID with
        | w when w = 1 || w= 3 -> Some this.Size.Width
        | h when h = 2 || h= 4 -> Some this.Size.Height
        | _                    -> None
    override this.Perimeter = 2.0f * (this.Size.Width + this.Size.Height)
    override this.Area = this.Size.Width * this.Size.Height
    override this.Draw() = printfn "Drawing a rectangle..."
type Ellipse(w, h) as this = 
    inherit Shape()
    do this.Size <- SizeF(w,h)
    new() = Ellipse(0.0f,0.0f)
    member this.MinorAxis
        with get()= Math.Min(this.Size.Width,this.Size.Height)
    member this.MajorAxis
        with get()= Math.Max(this.Size.Width,this.Size.Height)
    override this.Perimeter = 
        // Approximation 
        pown (this.MinorAxis/2.0f) 2
        |> (+) (pown (this.MajorAxis/2.0f) 2)
        |> (/) <| 2.0f
        |> float
        |> sqrt
        |> (*) <| (2.0 * Math.PI) 
        |> float32     
    override this.Area = 
        (float32 Math.PI) * (this.MinorAxis / 2.0f) *
        (this.MajorAxis/2.0f)
    override this.Draw() = printfn "Drawing an ellipse..."

let r = new Rectangle(15.0f, 70.0f)
r.Draw()
r.Perimeter
r.Area
r.Delete()
let e = new Ellipse(28.0f, 44.0f)
e.Draw()
e.Perimeter
e.Area
e.Delete()

type Shape (w, h) = 
    static let mutable shapeCounter = 0
    let mutable sh_Nam = "Unspesified"
    let mutable sh_Clr = Color.Black
    let mutable sh_Siz = SizeF(0.0f, 0.0f)
    let mutable sh_Pos = PointF(0.0f, 0.0f)       
    let mutable sh_Rot = 0.0f 
    do shapeCounter <- shapeCounter + 1
       sh_Nam <- "Shape" + shapeCounter.ToString()
       sh_Siz <- SizeF(w,h)
    new() = Shape(0.0f, 0.0f)
    abstract member Name: string with get,set
    default this.Name
        with get()= sh_Nam 
        and set(name)= sh_Nam <- name
    abstract member Color: Color with get,set
    default this.Color
        with get()= sh_Clr
        and set(color)= this.Fill(color)
    abstract member Size: SizeF with get,set
    default this.Size
        with get()= sh_Siz 
        and set(size:SizeF)= this.Resize(size.Width, size.Height)
    abstract member Position: PointF with get,set
    default this.Position
        with get()= sh_Pos 
        and set(position:PointF)= this.Move(position.X, position.Y)
    abstract member Rotation: float32 with get,set
    default this.Rotation 
        with get()= sh_Rot 
        and set(angle)= this.Rotate(angle)
    abstract member Perimeter: float32
    default this.Perimeter = 0.0f
    abstract member Area: float32
    default this.Area = 0.0f
    abstract member Draw: unit -> unit
    default this.Draw () = printfn "Drawing shape..." 
    abstract member Delete: unit -> unit    
    default this.Delete() = printfn "Deleting shape..."
    member private this.Fill (newColor) = 
        printfn "Filling shape..." 
        sh_Clr <- newColor
        printfn "Well done. Shape color= %A" this.Color
    member private this.Resize (newWidth, newHeight) = 
        printfn "Resizing shape..."
        sh_Siz <- SizeF(newWidth, newHeight)
        printfn "Well done. Shape size= %A" this.Size
    member private this.Move (newX, newY) = 
            printfn "Moving shape..."
            sh_Pos <- PointF(newX, newY)
            printfn "Well done. Shape position= %A" this.Position    
    member private this.Rotate (newAngle) = 
            if newAngle < -360.0f && newAngle > 360.0f then
                failwith "The measurment must be between -360° and 360°."
            printfn "Rotating shape..." 
            printfn "Well done. Shape angle= %.2f" newAngle

type Rectangle(w, h) = 
    inherit Shape(w, h)
    new() = Rectangle(0.0f,0.0f)
    member this.GetSide(sideID) = 
        match sideID with
        | w when w = 1 || w= 3 -> Some this.Size.Width
        | h when h = 2 || h= 4 -> Some this.Size.Height
        | _                    -> None
    override this.Perimeter = 2.0f * (this.Size.Width + this.Size.Height)
    override this.Area = this.Size.Width * this.Size.Height
    override this.Draw() = printfn "Drawing a rectangle..."
type Ellipse(w, h) = 
    inherit Shape(w, h)
    new() = Ellipse(0.0f,0.0f)
    member this.MinorAxis
        with get()= Math.Min(this.Size.Width,this.Size.Height)
    member this.MajorAxis
        with get()= Math.Max(this.Size.Width,this.Size.Height)
    override this.Perimeter = 
        // Approximation 
        pown (this.MinorAxis/2.0f) 2
        |> (+) (pown (this.MajorAxis/2.0f) 2)
        |> (/) <| 2.0f
        |> float
        |> sqrt
        |> (*) <| (2.0 * Math.PI) 
        |> float32     
    override this.Area = 
        (float32 Math.PI) * (this.MinorAxis / 2.0f) *
        (this.MajorAxis/2.0f)
    override this.Draw() = printfn "Drawing an ellipse..."

type BaseClass =
    val string1 : string
    new (str) = { string1 = str }
    new () = { string1 = "" }
type DerivedClass =
    inherit BaseClass
    val string2 : string
    new (str1, str2) = { inherit BaseClass(str1); string2 = str2 }
    new (str2) = { inherit BaseClass(); string2 = str2 }
let obj1 = DerivedClass("A", "B")
obj1.string1, obj1.string2
let obj2 = DerivedClass("A")
obj2.string1, obj2.string2

type Rectangle(w, h) = 
    inherit Shape(w, h)
    static let mutable shapeCounter = 0
    let mutable sh_Nam = "Unspesified"
    do shapeCounter <- shapeCounter + 1 
       sh_Nam <- "Rectangle" + shapeCounter.ToString()
    new() = Rectangle(0.0f,0.0f)
    member this.Name 
       with get()= sh_Nam 
       and set(name)= sh_Nam <- name
    member this.GetSide(sideID) = 
        match sideID with
        | w when w = 0 || w= 2 -> Some this.Size.Width
        | h when h = 1 || h= 3 -> Some this.Size.Height
        | _                    -> None
    override this.Perimeter = 2.0f * (this.Size.Width + this.Size.Height)
    override this.Area = this.Size.Width * this.Size.Height
    override this.Draw() = printfn "Drawing a rectangle..."
let r1 = Rectangle(45.5f,35.5f)
r1.Name
let r2 = Rectangle()
r2.Name

override this.Name 
    with get()= sh_Nam 
    and set(name)= sh_Nam <- name

// base keyword.
type BaseClass(a,b) = 
    let x = a
    let y = b
    abstract PrintValues: unit -> unit
    default this.PrintValues() = printfn "x= %d y= %d" x y 
type DerivedClass(a,b,c) = 
    inherit BaseClass(a,b)
    let z = c
    override this.PrintValues() = 
        base.PrintValues()
        printfn "z= %d" z
DerivedClass(6,8,11).PrintValues()

type Square(w) =
    inherit Rectangle(w,w)
    static let mutable squareCounter = 0
    let mutable sq_Nam = "Unspesified"
    do squareCounter <- squareCounter + 1 
       sq_Nam <- "Square" + squareCounter.ToString()
    new() = Square(0.0f)
    override this.Name
        with get() = sq_Nam
        and set(name) = sq_Nam <- name 
    override this.Size
        with get() = base.Size
        and set(size) = 
            if size.Width <> size.Height then
                failwith "The width and height measurments must be equal."
            base.Size <- size
    override this.Perimeter = 4.0f * this.Size.Width
    override this.Area = this.Size.Width* this.Size.Height  
    override this.Draw() = printfn "Drawing a square..."
let objSquare = new Square(12.0f)
objSquare.Perimeter
objSquare.Area
objSquare.Size <- SizeF(12.0f, 13.0f) (*error*)
type Circle(r) =
    inherit Ellipse(r*2.0f, r*2.0f)
    static let mutable circleCounter = 0
    let mutable ci_Nam = "Unspesified"
    do circleCounter <- circleCounter + 1 
       ci_Nam <- "Circle" + circleCounter.ToString()
    new() = Circle(0.0f)
    member this.Redius = this.Size.Width / 2.0f
    member this.MinorAxis = this.Redius * 2.0f    
    member this.MajorAxis = this.MinorAxis
    override this.Name 
        with get() = ci_Nam
        and set(name) = ci_Nam <- name 
    override this.Size
        with get() = base.Size
        and set(size) = 
            if size.Width <> size.Height then
                failwith "The width and height measurments must be equal."
            base.Size <- size
    override this.Perimeter = 2.0f * (float32 Math.PI * this.Redius)
    override this.Area = float32  Math.PI * (this.Redius * this.Redius) 
    override this.Draw() = printfn "Drawing a circle..."
let objCircle = new Circle(15.0f)
objCircle.Redius
objCircle.Perimeter
objCircle.Area
objCircle.Size <- SizeF(13.0f, 14.0f) (*error*)

//==============================================================================
// Abstraction.

[<AbstractClass>]
type Shape (w, h) = 
    static let mutable shapeCounter = 0
    let mutable sh_Nam = "Unspesified"
    let mutable sh_Clr = Color.Black
    let mutable sh_Siz = SizeF(0.0f, 0.0f)
    let mutable sh_Pos = PointF(0.0f, 0.0f)       
    let mutable sh_Rot = 0.0f 
    do shapeCounter <- shapeCounter + 1
       sh_Nam <- "Shape" + shapeCounter.ToString()
       sh_Siz <- SizeF(w,h)
    new() = Shape(0.0f, 0.0f)
    abstract member Name: string with get,set
    default this.Name
        with get()= sh_Nam 
        and set(name)= sh_Nam <- name
    abstract member Color: Color with get,set
    default this.Color
        with get()= sh_Clr
        and set(color)= this.Fill(color)
    abstract member Size: SizeF with get,set
    default this.Size
        with get()= sh_Siz 
        and set(size:SizeF)= this.Resize(size.Width, size.Height)
    abstract member Position: PointF with get,set
    default this.Position
        with get()= sh_Pos 
        and set(position:PointF)= this.Move(position.X, position.Y)
    abstract member Rotation: float32 with get,set
    default this.Rotation 
        with get()= sh_Rot 
        and set(angle)= this.Rotate(angle)
    abstract member Perimeter: float32
    abstract member Area: float32
    abstract member Draw: unit -> unit
    default this.Draw () = printfn "Drawing shape..." 
    abstract member Delete: unit -> unit    
    default this.Delete() = printfn "Deleting shape..."
    member private this.Fill (newColor) = 
        printfn "Filling shape..." 
        sh_Clr <- newColor
        printfn "Well done. Shape color= %A" this.Color
    member private this.Resize (newWidth, newHeight) = 
        printfn "Resizing shape..."
        sh_Siz <- SizeF(newWidth, newHeight)
        printfn "Well done. Shape size= %A" this.Size
    member private this.Move (newX, newY) = 
            printfn "Moving shape..."
            sh_Pos <- PointF(newX, newY)
            printfn "Well done. Shape position= %A" this.Position    
    member private this.Rotate (newAngle) = 
            if newAngle < -360.0f && newAngle > 360.0f then
                failwith "The measurment must be between -360° and 360°."
            printfn "Rotating shape..." 
            printfn "Well done. Shape angle= %.2f" newAngle

type Triangle(a, b, c) = 
    inherit Shape(b,
        (* Height = 2(area/base) *)         
        let s = (a + b + c) / 2.0f
        float (s * (s-a) * (s-b) * (s-c))
        |> Math.Sqrt 
        |> float32
        |> (/) <| b
        |> (*) <| 2.0f )
    static let mutable triangleCounter = 0
    let mutable tr_SideA, tr_SideB, tr_SideC = (a, b, c) 
    let mutable tr_Nam = "Unspesified"
    do if not (a+b>c && a+c>b && 
               b+a>c && b+c>a && 
               c+a>b && c+b>a) then
           failwith "Invalid input: make sure a+b>c"
       triangleCounter <- triangleCounter + 1 
       tr_Nam <- "Triangle" + triangleCounter.ToString()
    new() = Triangle (0.0f,0.0f,0.0f)
    member this.SideA = tr_SideA 
    member this.Base  = tr_SideB  
    member this.SideC = tr_SideC  
    override this.Name 
       with get()= tr_Nam 
       and set(name)= tr_Nam <- name 
    override this.Size 
        with get() = base.Size
        and set(value) = 
            base.Size  <- value
            tr_SideB <- value.Width
            printfn "Setting sides..."
    override this.Perimeter = this.SideA + this.Base + this.SideC
    override this.Area = 
        (*Heron's formula*)
        let s = this.Perimeter / 2.0f 
        float (s * (s-this.SideA) * (s-this.Base) * (s-this.SideC))
        |> Math.Sqrt 
        |> float32
    override this.Draw() = printfn "Drawing a triangle..."
let isoscelesTriangle = new Triangle(10.0f, 5.0f, 10.0f)
isoscelesTriangle.Draw()
isoscelesTriangle.Size
isoscelesTriangle.Perimeter
isoscelesTriangle.Area
isoscelesTriangle.Size <- SizeF(20.0f, 20.f)
isoscelesTriangle.SideA, isoscelesTriangle.Base, isoscelesTriangle.SideC

let shapeInstance = new Shape()

[<Sealed>]
type SealedClass() = 
    member this.M1() = ()
type DerivedClass() =
    inherit SealedClass()
    member this.M2() = ()

open System
open System.Text.RegularExpressions
type System.String with
    member this.FromLeft(count) =
        this.Substring(0, count)
    member this.FromRight(count) =
        this.Substring(this.Length-count, count)
    member this.FromMid(start, count) =
        this.Substring(start, count)
    member this.ToInteger() =
        let result = ref 0 
        Int32.TryParse(this, result) |> ignore
        result
    member this.IsInteger = 
        let regularExpression = new Regex("^-[0-9]+$|^[0-9]+$");
        regularExpression.Match(this).Success
let mutable strSample = "Microsoft Visual Studio"
strSample.FromLeft(9)
strSample.FromMid(10,6)
strSample.FromRight(6)
strSample.IsInteger
strSample <- "-123456"
strSample.IsInteger
strSample.ToInteger()

//==============================================================================
// Casting.

// Upcasting.
let objSquare: Square = Square()
let squareAsRectangle = objSquare :> Rectangle
let rectangleAsShape = squareAsRectangle :> Shape

// Downcasting.
let shapeAsRectangle = rectangleAsShape :?> Rectangle
let reactangleAsSquare = shapeAsRectangle :?> Square

let downcastShape (sh:Shape) =    
    match sh with
    | :? Rectangle as myRec -> myRec.Draw()
    | :? Ellipse   as myEli -> myEli.Draw()
    | :? Triangle  as myTri -> myTri.Draw()
    | _                     -> printfn "Could not convert."
downcastShape (rectangleAsShape)

let objTriangle = Triangle(6.0f, 5.0f, 3.0f)
let objShape: Shape = upcast objTriangle
let objTriangle: Triangle = downcast objShape

//==============================================================================
// Polymorphism.

let shapes: Shape[] = Array.zeroCreate 4
shapes.[0] <- new Rectangle() :> Shape
shapes.[1] <- new Ellipse  () :> Shape
shapes.[2] <- new Square   () :> Shape
shapes.[3] <- new Circle   () :> Shape;;

for shape in shapes do
    shape.Draw()

[<Struct>]
type SizeStruct  =
    val private w: float
    val private h: float
    member this.Width = this.w
    member this.Height = this.h
    static member (+) (s1: SizeStruct,s2: SizeStruct) = 
        new SizeStruct(s1.Width+s2.Width, s1.Height+s2.Height)
    new (width, height) = { w = width; h = height}
let sizeA = SizeStruct(10.0, 15.0)
let sizeB = SizeStruct(20.0, 30.0)
sizeA + sizeB

//==============================================================================
// Constructors as first-class functions.

open System
let data = ["http://www.google.com"; "http://www.bing.com"; "http://www.yahoo.com"]
let result  = data |> List.map (fun str -> System.Uri (str))

let data = ["http://www.google.com"; "http://www.bing.com"; "http://www.yahoo.com"]
let result  = data |> List.map Uri 