
module FSharpBook.Chapter2

// First F# program.
// Program.fs
let message = "HelloWorld!"
printfn "%s" message

2 + 4

#light "on" 
#light "off"

//==============================================================================
// F# Syntax.

// Lightweight Syntax:
[<EntryPoint>]
let Main (args : string[]) =
    //Main Body
    if (args.Length > 0) then 
        printfn "Processing args..." //if body
    else
        failwith "No args detected ..." //else body
    0 //Exit code.

// Verbose Syntax:
[<EntryPoint>]
let Main (args : string[]) =
    begin
    //Main Body
    if (args.Length > 0) then 
        begin
        printfn "Processing args..." //if body
        end
    else
        begin
        failwith "No args detected ..." //else body
        end
    0 //Exit code. 
    end

[<EntryPoint>]
let Main (args : string[]) =
    //Main Body
    if (args.Length > 0) then 
    printfn "Processing args..." //if body

//==============================================================================
// Module.

// Program.fs:
module ProgramModule
printfn "%d" (File1Module.value + File2Module.value)	
// File1.fs:
module File1Module
let value = 123
let x = File2Module.value	
// File2.fs:
module File2Module
let value = 234	

module ProgramModule
[<EntryPoint>]
let main argv = 
    printfn "%d" (File1Module.value + File2Module.value)
    0 // return an integer exit code

// Local modules.

// File2.fs:
module File2Module
let value = 234
module Module1 =
    let Module1Value = value - 10
module Module2 =
    let Module2Value = 126	
// File1.fs:
module File1Module
let value = 123
let x = File2Module.value
module Module1 =
    let Module1Value  = value + File2Module.Module1.Module1Value
module Module2 =
    let Module2Value = Module1.Module1Value
    module NestedModule1=
        let NestedModule1Value = x * 2	

// Program.fs
module ProgramModule
[<EntryPoint>]
let main argv = 
    printfn "%d" (File1Module.Module1.Module1Value + 
                  File2Module.Module1.Module1Value)
    0 // return an integer exit code

module ProgramModule
open File1Module
open File2Module.Module1
[<EntryPoint>]
let main argv = 
    printfn "%d" (Module1.Module1Value + Module1Value)
    0 // return an integer exit code

module Module1 = 
    let x = 456 

//==============================================================================
// Namespace.

namespace namespace1
module Module1 =
    let value = 12 
namespace namespace2
module Module1 = 
    let value = 3 
type Type1 = 
    {Field1: string; Field2: string}

// Nested namespaces.
namespace Outer
    // Full name: Outer.Module1
    module Module1 =
        let value = 12 
namespace Outer.Inner
    // Full name: Outer.Inner.Module1
    module Module1 = 
        open Outer.Module1
        let value = value + 3 // 15
namespace Outer.Inner.Kernel
    // Full name: Outer.Inner.Kernel.Module1
    module Module1 = 
        open Outer.Inner.Module1
        let value = value + 10 // 25
