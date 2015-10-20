
module FSharpBook.Chapter9

// Database.

// Book table.
CREATE TABLE [dbo].[Book]
(
    [BookId] INT NOT NULL PRIMARY KEY, 
    [BookName] NVARCHAR(50) NOT NULL, 
    [Author] NVARCHAR(50) NOT NULL, 
    [Publisher] NVARCHAR(50) NULL 
)

// Member table.
CREATE TABLE [dbo].[Member]
(
    [MemberId] INT NOT NULL PRIMARY KEY, 
    [MemberName] NVARCHAR(50) NOT NULL, 
    [Age] INT NULL
)

// Trust table.
CREATE TABLE [dbo].[Trust]
(
    [Id] INT NOT NULL PRIMARY KEY, 
    [BookId] INT NOT NULL, 
    [MemberId] INT NOT NULL, 
    [IssueDate] DATETIME NOT NULL, 
    [ReturnDate] DATETIME NOT NULL, 
    [IsReturned] BIT NOT NULL DEFAULT 0, 
    CONSTRAINT [FK_Trust_Book] FOREIGN KEY ([BookId]) 
    REFERENCES [Book]([BookId]), 
    CONSTRAINT [FK_Trust_Member] FOREIGN KEY ([MemberId]) 
    REFERENCES [Member]([MemberId]) 
)

//BookId---BookName--------------------------------------Author----------Publisher---
//1        F# Programming	                             Chris Smith     O'Reilly
//2        Multi-Paradigm Programming with F#            Ali Baghernejad Naghoos
//3        Functional programming for the real world     Tomas Petricek	 Manning
//4        Data Mining Introduction	                     David Peterson	 Ronald D
//5        Microsoft SQL Server	                         Jack Sanron	 Fakepub
//
//MemberId---MemberName-------Age---
//1	        John Smith	     35
//2         Sarah Taylor     26
//3         Jack Archer	     28
//4         David Millan     23
//5         Tomas Stvenson   31
//6         Adams Tom	     20
//7         Hance Jim	     25
//
//Id---------BookId----MemberId----IssueDate-------------------ReturnDate----------------IsReturned---
//1          3         1	       1/1/2015 12:00:00 AM	       1/8/2015 12:00:00 PM      False
//2          2         4	       1/2/2015 13:25:00 PM	       1/9/2015 13:25:00 PM      False
//3          4         1	       1/2/2015 9:30:00 AM	       1/9/2015 9:30:00 PM       False
//4          1         6	       1/3/2015 12:00:00 AM	       1/10/2015 12:00:00 AM     False

<connectionStrings>
  <add name="cfgLibraryMgr" 
   providerName="System.Data.SqlClient"
   connectionString="Data Source=(LocalDB)\v11.0;
                     AttachDbFilename=C:\Users\Ali\Documents\LibraryDB.mdf;
                     Integrated Security=True;Connect Timeout=30" />
</connectionStrings>

// Program.fsx
#r "System.Data.dll"
#r "System.Transactions.dll"
#r "System.Configuration.dll"
open System
open System.Data
open System.Data.SqlClient
open System.Configuration
let connectionString =
    ConfigurationManager.ConnectionStrings.["cfgLibraryMgr"].ConnectionString 
module MemberTools =
    let printAllMembers () =     
        try
            // Create connection.
            use objConnection = new SqlClient.SqlConnection(connectionString)
            // Open connection.
            objConnection.Open()
            // Creates a sql command.
            let objCommand = new SqlCommand()
            objCommand.Connection <- objConnection 
            objCommand.CommandText <- "SELECT * FROM Member"
            // Creates a SqlDataAdapter.
            let objDataAdapter = new SqlDataAdapter()
            objDataAdapter.SelectCommand <- objCommand
            // Creates a DataTable object and fill it with data.
            let objDataTable = new DataTable()
            let affectedRows = objDataAdapter.Fill(objDataTable)
            printfn "%d row(s) affected:" affectedRows
            // Move over data.
            for row in objDataTable.Rows do
                printfn "Id:%A  Name:%A  Age:%A" (row.[0]) (row.[1]) (row.[2])
                printf ""
        with
        | exp -> printfn "%s" exp.Message
MemberTools.printAllMembers()

// Entities.fs
module LibraryManagment.Entities
    type Book()=
        member val Id =        0       with get, set
        member val Name: string =      null with get, set
        member val Author: string =    null with get, set
        member val Publisher: string = null with get, set
    type Member()=
        member val Id =   0       with get, set
        member val Name: string = null with get, set
        member val Age =  0       with get, set

#load "Entities.fs"
open LibraryManagment.Entities 
module BookTools =
  let addNewBook (newBook: Book) =     
     try
        use objConnection = new SqlClient.SqlConnection(connectionString)
        objConnection.Open()
        let objCommand = new SqlCommand()
        objCommand.Connection <- objConnection 
        objCommand.CommandText <- "INSERT INTO Book VALUES 
                                       (@BookId,@BookName,@Author, @Publisher)"
        objCommand.Parameters.AddWithValue("BookId", newBook.Id) |> ignore
        objCommand.Parameters.AddWithValue("BookName", newBook.Name) |> ignore
        objCommand.Parameters.AddWithValue("Author", newBook.Author) |> ignore
        objCommand.Parameters.AddWithValue("Publisher", newBook.Publisher) |> ignore
        let affectedRows = objCommand.ExecuteNonQuery()
        printfn "%d row(s) affected." affectedRows
     with
     | exp -> printfn "%s" exp.Message

let newBook = new Book(Id= 6, 
                       Name= "Microsoft Silverlight", 
                       Author= "Sam Hazorn",
                       Publisher= "Manning")
BookTools.addNewBook(newBook)

//==============================================================================
// Type providers.

#r "FSharp.Data.TypeProviders.dll"
#r "System.Data.Linq.dll"
open System.Data.Linq
open Microsoft.FSharp.Data.TypeProviders
open Microsoft.FSharp.Linq
type dbSchema = SqlDataConnection<ConnectionStringName="cfgLibraryMgr">
let db = dbSchema.GetDataContext()

let tblBook = db.Book

let printAllBooks() = 
    let db = dbSchema.GetDataContext()
    let tblBook = db.Book
    let books = query { for book in tblBook do
                        select book }
    printfn "%s %-45s %-20s %-20s\n\ 
    -------------------------------------------------------------------------------"
        "Id" "Name" "Author" "Publisher"
    books
    |> Seq.iter (fun book-> printfn "%d  %-45s %-20s %-20s" 
                             book.BookId book.BookName book.Author book.Publisher)
BookTools.printAllBooks()

// Insert data.
let addNewMember (newMember: dbSchema.ServiceTypes.Member)  =
    let db = dbSchema.GetDataContext()
    db.Member.InsertOnSubmit(newMember)
    try
        db.DataContext.SubmitChanges()
        printfn "Successfully inserted new member."
    with
    | exp  -> printfn "%s" exp.Message
let addNewMembers (newMembers: dbSchema.ServiceTypes.Member list)  =
    let db = dbSchema.GetDataContext()
    db.Member.InsertAllOnSubmit(newMembers)
    try
        db.DataContext.SubmitChanges()
        printfn "Successfully inserted new members."
    with
    | exp  -> printfn "%s" exp.Message;
let newMember = new dbSchema.ServiceTypes.Member()
newMember.MemberId <- 8
newMember.MemberName <- "Will Smith"
newMember.Age <- Nullable<int>(43)
MemberTools.addNewMember(newMember)
let newMembers = [new dbSchema.ServiceTypes.Member(MemberId= 9, 
                                                   MemberName= "Kate Mcdon", 
                                                   Age= Nullable<int>(32));
                  new dbSchema.ServiceTypes.Member(MemberId= 10, 
                                                   MemberName= "Sandar Winsom", 
                                                   Age= Nullable<int>(19))]
MemberTools.addNewMembers(newMembers)

let newRecord = dbSchema.ServiceTypes.Member(MemberId= 11, 
                                             MemberName= 0, 
                                             Age= new Nullable<int>(25))

// Delete data.
let deleteMember (memberToDelete: dbSchema.ServiceTypes.Member) = 
    let db = dbSchema.GetDataContext()
    let tblMember= db.Member
    let query = query { for mem in tblMember do
                                where (mem.MemberId = memberToDelete.MemberId)
                                select mem}
    match Seq.length query with
    | len when len > 0 ->
        let memberToDelete = query |> Seq.head
        db.Member.DeleteOnSubmit(memberToDelete)
        try
            db.DataContext.SubmitChanges()
            printfn "Successfully deleted exist member."
        with
        | exp  -> printfn "%s" exp.Message
    | _ -> printfn "Member not Found."
let deleteMembers (membersToDelete: dbSchema.ServiceTypes.Member list)  =
    membersToDelete
    |> List.iter (fun memberToDelete -> deleteMember(memberToDelete))
let m1 = new dbSchema.ServiceTypes.Member (MemberId= 10)
let m2 = new dbSchema.ServiceTypes.Member (MemberId= 1)
let membersToDelete = [m1; m2]
MemberTools.deleteMembers(membersToDelete)

// Update data.
let updateMember (memberToUpdate: dbSchema.ServiceTypes.Member, 
                    newData: dbSchema.ServiceTypes.Member) = 
    let db = dbSchema.GetDataContext()
    let tblMember= db.Member
    let query = query { for mem in tblMember do
                            where (mem.MemberId = memberToUpdate.MemberId)
                            select mem}
    match Seq.length query with
    | len when len > 0 ->
        let memberToUpdate = query |> Seq.head
        memberToUpdate.MemberId <- newData.MemberId
        memberToUpdate.MemberName <- newData.MemberName
        memberToUpdate.Age <- newData.Age
        try
            db.DataContext.SubmitChanges()
            printfn "Successfully updated exist member."
        with
        | exp  -> printfn "%s" exp.Message
    | _ -> printfn "Member not Found."
let memberToUpdate = new dbSchema.ServiceTypes.Member(MemberId= 6)
let newData = new dbSchema.ServiceTypes.Member(
                 MemberId= 6, MemberName= "Adam Tomwill", Age = Nullable<int>(24))
MemberTools.updateMember(memberToUpdate, newData)

//==============================================================================
// Query Expressions.

module TrustTools = 
     let printAllTrusts() =
        let db = dbSchema.GetDataContext()
        let tblTrust = db.Trust
        let query = query { for trust in tblTrust do
                            select trust }
        printfn "%-5s %-5s %-5s %-25s %-25s %-25s\n\
        ---------------------------------------------------------------------------"
            "TId" "BId" "MId" "IssueDate" "ReturnDate" "Is Returned"
        query
        |> Seq.iter (fun trust -> 
            printfn "%-5d %-5d %-5d %-25O %-25O %-25O" 
                trust.Id trust.BookId trust.MemberId trust.IssueDate 
                trust.ReturnDate trust.IsReturned)
TrustTools.printAllTrusts()

// where operator.
let trustReportFor(mem: dbSchema.ServiceTypes.Member) =
    let db = dbSchema.GetDataContext()
    let tblTrust = db.Trust
    let query = query { for trust in tblTrust do
                        where (trust.MemberId = mem.MemberId)
                        select trust }
    query
let targetMember = new dbSchema.ServiceTypes.Member()
targetMember.MemberId <- 1
printfn "%-5s %-5s %-5s %-25s %-25s %-25s
---------------------------------------------------------------------------"
    "TId" "BId" "MId" "IssueDate" "ReturnDate" "Is Returned"
TrustTools.trustReportFor(targetMember)
|> Seq.iter (fun trust -> 
        printfn "%-5d %-5d %-5d %-25O %-25O %-25O" 
            trust.Id trust.BookId trust.MemberId trust.IssueDate 
            trust.ReturnDate trust.IsReturned)

// contains operator.
let isPublisherExist(pubName)  = 
    let db = dbSchema.GetDataContext()
    let tblBook = db.Book
    let query = query { for book in tblBook do
                        select book.Publisher
                        contains pubName }
    query
BookTools.isPublisherExist ("Geopub")
BookTools.isPublisherExist ("Naghoos")
BookTools.isPublisherExist ("NaghoosPub")

// count operator.
let numberOfBooks = 
    let db = dbSchema.GetDataContext()
    let tblBook = db.Book
    let query = query { for book in tblBook do
                        count }
    query
BookTools.numberOfBooks

// join operator.
let trustedBooks()  = 
    let db = dbSchema.GetDataContext()
    let tblTrust= db.Trust
    let tblBook = db.Book
    let query = query { for trust in tblTrust do
                        join book in tblBook on
                            (trust.BookId = book.BookId)
                        select (trust, book) } 
    query
printfn "%-3s %-3O %-42O %-17O %-15O  
-------------------------------------------------------------------------------"
    "TID" "BID" "BName" "Author" "Publisher"  
TrustTools.trustedBooks() 
|> Seq.iter (fun (trust, book) -> 
        printfn "%-3d %-3O %-42O %-17O %-15O" 
             trust.Id book.BookId book.BookName book.Author book.Publisher)

// leftOuterJoin operator.
let availableBooks()  = 
    let db = dbSchema.GetDataContext()
    let tblBook = db.Book
    let tblTrust= db.Trust
    let query = query { for book in tblBook do
                        leftOuterJoin trust in tblTrust on
                            (book.BookId = trust.BookId) into result
                        for trust in result do
                        where (trust = null ||  
                                not (query { for trust in result do
                                             select trust.IsReturned
                                             contains false }))
                        select book
                        distinct} 
    query
printfn "%-3O %-42O %-17O %-15O 
-------------------------------------------------------------------------------"
    "BID" "BName" "Author" "Publisher" 
BookTools.availableBooks() 
|> Seq.iter (fun book -> 
        printfn "%-3O %-42O %-17O %-15O" 
            book.BookId book.BookName book.Author book.Publisher)

type dbSchema.ServiceTypes.Book with
    member this.IsInTrust =  query {for trust in this.Trust do
                                    select trust.IsReturned
                                    contains false}
let availableBooks2()  = 
    let db = dbSchema.GetDataContext()
    let tblBook = db.Book
    let query = seq { for book in tblBook do
                        if (not book.IsInTrust) then
                            yield book } 
    query
printfn "%-3O %-42O %-17O %-15O 
-------------------------------------------------------------------------------"
    "BID" "BName" "Author" "Publisher" 
BookTools.availableBooks2() 
|> Seq.iter (fun book -> 
        printfn "%-3O %-42O %-17O %-15O" 
            book.BookId book.BookName book.Author book.Publisher)
