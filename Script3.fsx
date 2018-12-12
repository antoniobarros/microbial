

// 
// GET INFORMATION FROM https://dbaasp.org/
//

#I @"C:\Datasets\RuiVitorino2\ConsoleApplication1\packages\FsLab.1.0.2"

#load "FsLab.fsx"

open FSharp.Data

// https://dbaasp.org/api/v1?query=peptide_card&peptide_id=107&format=json

do for i in 5001 .. 7000 do // 11851
    
    printf "%d" i
    let url_format = sprintf "https://dbaasp.org/api/v1?query=peptide_card&peptide_id=%d&format=json" i
    //System.Threading.Thread.Sleep(100) |> ignore
    
    System.GC.Collect()

    let resp = Http.RequestString url_format
    if resp.Length > 50 then     
        let filename = sprintf "c:/fringe/microbial/report_%d.json" i 
        let file = System.IO.File.CreateText(  filename )
        file.WriteLine(resp)
        file.Close()
        printfn " bytes = %d" resp.Length
    else
        printfn " empty"
        ()

    if ( i %  100 = 0) then System.GC.Collect() else ()
    
    

    
// 
[<Literal>]
let filename0 = @"c:/fringe/microbial/report_1.json"

type DB = JsonProvider< filename0 >

let data = DB.Load @"c:/fringe/microbial/report_37.json"

data.PeptideCard