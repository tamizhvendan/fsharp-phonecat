namespace PhoneCat.Domain

module Measures = 
    [<Measure>] type inch
    [<Measure>] type g
    [<Measure>] type MB
    [<Measure>] type GB

    let private toUOM (measureString : string) stringToReplace (uom : float<_>) = 
        let value = if measureString = "" then 
                      0. 
                    else 
                      measureString.Replace(stringToReplace, "") |> float
        value |> ((*) uom)  

    let toInch (inchStr : string) = toUOM inchStr "inches" 1.0<inch>
    let toGram (weightStr : string) = toUOM weightStr "grams" 1.0<g>
    let toMB (storageStr : string) = toUOM storageStr "MB"  1.0<MB> 

