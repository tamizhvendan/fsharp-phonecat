namespace PhoneCat.Domain
open PhoneCat.Domain.Search
open PhoneCat.Domain.Measures
open FParsec

module SearchParser =
  
  let parseFilter filterStr = 
  
    let toLowerCase (str : string) = str.ToLowerInvariant() 

    let toSearchFilter str = 
      match toLowerCase str with
      | "ram" -> Ram ((*) 1.<MB>)
      | "weight" -> Weight ((*) 1.<g>)
      | "screen" -> Screen ((*) 1.<inch>)
      | _ -> failwith "Invalid search filter"
   
    let psearchFilter str = pstring str |>> toSearchFilter
    let pseperator = pchar ':'
    let pgreaterThan = pchar '>' >>. pfloat |>> GreaterThan
    let prange = pfloat .>> (pchar '-') .>>. pfloat |>> Range
    let pvalue = pfloat |>> Value
    let pmeasure suffix = pstringCI suffix
    let pvalueFilter = (choice [pgreaterThan; (attempt prange); pvalue])
    let pFilter ptype pmeasure'  = 
      ptype .>> pseperator .>>. pvalueFilter .>> pmeasure'

    let pmb = pmeasure "MB"
    let pg = pmeasure "g"
    let pinch = pmeasure "inch"
    let pram = psearchFilter "ram"
    let pweight = psearchFilter "weight"
    let pscreen = psearchFilter "screen"

    let pramFilter = pFilter pram pmb
    let pweightFilter = pFilter pweight pg
    let pscreenFilter = pFilter pscreen pinch
    let pf = choice [pramFilter;pweightFilter;pscreenFilter]

    let parser : Parser<(SearchFilter * ValueFilter) list, unit> = (sepBy pf (pchar ';'))

    match run parser filterStr with
    | Success(filters, _, _) -> Choice1Of2(filters)
    | Failure(err,_,_) -> Choice2Of2(err)

