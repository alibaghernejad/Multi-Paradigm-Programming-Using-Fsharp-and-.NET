
module FSharpBook.Chapter10

// Graphic user interface(GUI)

// Windows Forms Graphic user interface(GUI).
#r "System.Windows.Forms.dll"
open System.Windows.Forms
let frmMainForm = new Form(Width= 500, Height = 300, Text = "Windows Form in F#") 
frmMainForm.Show()

// WinForm.fsx
#r "System.Drawing.dll"
#r "System.Windows.Forms.dll"
open System
open System.IO 
open System.Drawing
open System.Windows.Forms
let frmMainForm = new Form(Width= 500, Height = 300, Text = "Windows Form in F#") 
let txtPath = new TextBox()
txtPath.Text <- Environment.GetFolderPath(
                    Environment.SpecialFolder. CommonStartMenu)
txtPath.Dock <- DockStyle.Bottom
let txtFiles= new TextBox()
txtFiles.Multiline <- true
txtFiles.ReadOnly <- true
txtFiles.BackColor <- Color.White
txtFiles.ScrollBars <- ScrollBars.Both
txtFiles.Height <- 200
txtFiles.Dock <- DockStyle.Top

let chkSubDirectories = new CheckBox()
chkSubDirectories.AutoSize <- true
chkSubDirectories.Text <- "Search for SubDirectories?"
chkSubDirectories.Location <- Point(0, txtPath.Location.Y + 213)

let btnSearch = new Button()
btnSearch.AutoSize <- true
btnSearch.Text <- "Begin Search..." 
btnSearch.Location <- Point(chkSubDirectories.Width + 70 , txtPath.Location.Y + 210)
btnSearch.Click.AddHandler( 
    fun _ _ ->
        txtFiles.Text <- String.Empty
        txtFiles.Lines <-
            try    
                match chkSubDirectories.Checked with
                | true -> Directory.GetFiles(txtPath.Text, "*.*",
                          SearchOption.AllDirectories) 
                |_     -> Directory.GetFiles(txtPath.Text)               
            with 
                | :? DirectoryNotFoundException -> [|"Directory Not Found ;/"|] 
                | _                             -> [|"An Error occured ;("|])
frmMainForm.Controls.Add(txtPath)
frmMainForm.Controls.Add(txtFiles)
frmMainForm.Controls.Add(chkSubDirectories)
frmMainForm.Controls.Add

//=============================================================================
// WPF Graphic user interface(GUI).

// WPF.fsx
#r "PresentationCore.dll"
#r "PresentationFramework.dll"
#r "WindowsBase.dll"
#r "System.Xaml.dll"
open System.Windows
open System.Windows.Controls
let winTest = new Window(Width = 500.0, Height = 200.0, Title= "Hello WPF From F#!")
let objGrid = new Grid()
let btnTest = new Button(Width = 150.0, Height = 25.0, Content= "Say Hello!")
btnTest.Click.AddHandler(fun _ _ -> MessageBox.Show("Hello, WPF!") |> ignore)
objGrid.Children.Add(btnTest)
winTest.Content <- objGrid
winTest.Show()

//=============================================================================
// Mix F# and C#.

// Client.fs
namespace Whois
open System.IO
open System.Text
open System.Net.Sockets
type DefaultServers  = 
    | Server1_org_gTLD = 1
    | Server2_ir_ccTLD = 2
    | Server3_com_net_edu_gTLD = 3
type WhoisClient(server, port) as this =
    [<DefaultValue>] val mutable private socket: string * int
    [<DefaultValue>] val mutable private defaultPort: int
    do this.socket <- server, port
       this.defaultPort <- 43
    member val Url = "Unspesified" with get, set
    member this.Lookup (url:string) =
        try                                            
            using(new TcpClient(fst this.socket, snd this.socket)) (fun tcpClient ->                
                // Get network stream.
                use netStream = tcpClient.GetStream()                
                // Initializing the Buffer.
                let bufferedNetStream = new BufferedStream(netStream)

                // Sending the url using Stream.
                let writer = new StreamWriter(bufferedNetStream)
                writer.WriteLine(url)
                // Clear the buffer.
                writer.Flush() 
                // Receive the data from Server.
                let reader = new StreamReader(bufferedNetStream)
                let stringBuilder = new StringBuilder()
                stringBuilder.AppendLine((sprintf "Whois Report for %s by %s:%d =\n" 
                                url (fst this.socket) (snd this.socket)))
                                |> ignore
                while (not reader.EndOfStream) do         
                        let line = reader.ReadLine()  
                        stringBuilder.AppendLine(line) |> ignore  
                stringBuilder.ToString()
             )  
        with 
        | exp -> exp.Message         
    member this.Lookup ()  = this.Lookup(this.Url)
    new (server) as this = WhoisClient("whois.pir.org", 43) then
        match server with
        | DefaultServers.Server2_ir_ccTLD  
            -> this.socket <- ("whois.nic.ir", this.defaultPort)  
        | DefaultServers.Server3_com_net_edu_gTLD
            -> this.socket <- ("whois.verisign-grs.com", this.defaultPort) 
        | _ -> ()

// On Form Load
private void frmMainForm_Load(object sender, EventArgs e)
{
    string[] defaultServers = Enum.GetNames(typeof(WhoisServer.DefaultServers));
    cmbWhoisServers.Items.AddRange(defaultServers);
    cmbWhoisServers.SelectedIndex = 0;
}

// On button click
private void btnLookup_Click(object sender, EventArgs e)
{
    try
    {
        string selectedServer = cmbWhoisServers.Text;
        int portNumber = int.Parse(txtPortNumber.Text);
        string domainName = txtDomainName.Text;
        Whois.DefaultServers defaultServers;
        Whois.WhoisClient server =
                new Whois.WhoisClient(selectedServer, portNumber);

        if (Enum.TryParse(selectedServer, out defaultServers))
	{
            server = new Whois.WhoisClient(defaultServers);
	}
        txtResult.Text = server.Lookup(domainName);  
    }
    catch (Exception exp)
    {
        txtResult.Text = exp.Message;
    }           
}

//=============================================================================
// GTK#

namespace HelloGTKSharp
module Main = 
    open System
    open Gtk    
    [<EntryPoint>]
    let Main(args) = 
        Application.Init()
        let win = new MainWindow.MyWindow()
        win.Title <- "Say Hello"
        let newlabel = new Label()
        newlabel.Text <- "Hello GTK#!"
        win.Add(newlabel)
        win.ShowAll()
        Application.Run()
        0 

// UnitsOfMeasure.fs
namespace AngleUnitsConvertor
open System
module AngleUnits = 
    [<Measure>] type deg
    [<Measure>] type gra
    [<Measure>] type rad
    let DegToGra (angle : float<deg>) = float angle * 1.11111111<gra>
    let DegToRad (angle : float<deg>) = (float angle * Math.PI / 180.0) * 1.0<rad>
    let GraToDeg (angle  : float<gra>) =  float angle * 0.9<deg>
    let GraToRad (angle  : float<gra>) = float angle  * 0.0157075<rad>
    let RadToGra (angle : float<rad>) = float angle * 63.6638548<gra>
    let RadToDeg (angle  : float<rad>) = (float angle  * 180.0 / Math.PI) * 1.0<deg

// Main.fs
namespace AngleUnitsConvertor
module Main = 
    open System
    open Gtk
    open AngleUnitsConvertor.AngleUnits
    [<EntryPoint>]
    let Main(args) = 
        Application.Init()
        // Create new widgets.
        let win = new MainWindow.MyWindow()
        let box = new VBox(false, 1)
        let lblFrom = new Label("From:")
        let cmbFrom = new ComboBox([|"Degree"; "Gradian"; "Radian"|])
        let txtFrom = new Entry()
        let lblTo = new Label("To:")
        let cmbTo = new ComboBox([|"Degree"; "Gradian"; "Radian"|])
        let txtTo = new Entry()
        // Set widgets properties.
        win.Title <- "Angle Units Convertor"
        win.Resize(350,200)
        lblFrom.Xalign <- 0.0f
        lblTo.Xalign <- 0.0f
        cmbFrom.Active <- 0
        cmbTo.Active <- 0
        txtTo.IsEditable <- false
        txtFrom.Text <- "0"
        txtTo.Text <- "0"
        // A function to convert units.
        let convertUnits() = 
            try
                let unitFrom = cmbFrom.ActiveText
                let unitTo = cmbTo.ActiveText
                let angle = txtFrom.Text
                match unitFrom, unitTo with
                | "Degree", "Gradian" -> 
                        txtTo.Text <- (DegToGra (float angle * 1.0<deg>)).ToString()
                | "Degree", "Radian"  -> 
                        txtTo.Text <- (DegToRad (float angle * 1.0<deg>)).ToString()
                | "Gradian", "Degree" -> 
                        txtTo.Text <- (GraToDeg (float angle * 1.0<gra>)).ToString()
                | "Gradian", "Radian" -> 
                        txtTo.Text <- (GraToRad (float angle * 1.0<gra>)).ToString()
                | "Radian", "Degree"  -> 
                        txtTo.Text <- (RadToDeg (float angle * 1.0<rad>)).ToString()
                | "Radian", "Gradian" -> 
                        txtTo.Text <- (RadToGra (float angle * 1.0<rad>)).ToString()
                | _                   -> 
                        txtTo.Text <- txtFrom.Text 
            with 
                | exp -> txtTo.Text <- "Error! ;/"
        // Events handle.
        txtFrom.Changed.Add(fun _-> convertUnits())
        cmbFrom.Changed.Add(fun _-> convertUnits())
        cmbTo.Changed.Add(fun _-> convertUnits())
        // Add widgets to main container.
        box.Add(lblFrom)
        box.Add(cmbFrom)
        box.Add(txtFrom)
        box.Add(lblTo)
        box.Add(cmbTo)
        box.Add(txtTo)
        // Add main container to window.
        win.Add(box)
        // Draw anythings
        win.ShowAll() 
        Application.Run()
        0 
