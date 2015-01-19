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
    let psearchValueSeperator = pchar ':'
    let pmultiFiltersSeperator = pchar ';'
    let pgreaterThan = pchar '>' >>. pfloat |>> GreaterThan
    let prange = pfloat .>> (pchar '-') .>>. pfloat |>> Range
    let pvalue = pfloat |>> Value
    let pvalueFilters = choice [pgreaterThan; (attempt prange); pvalue]
    let createFilterParser psearchfilter puom  = 
      psearchfilter .>> psearchValueSeperator .>>. pvalueFilters .>> puom

    let pmb = pstringCI "MB"
    let pg = pstringCI "g"
    let pinch = pstringCI "inch"
    let pram = psearchFilter "ram"
    let pweight = psearchFilter "weight"
    let pscreen = psearchFilter "screen"

    let pramFilter = createFilterParser pram pmb
    let pweightFilter = createFilterParser pweight pg
    let pscreenFilter = createFilterParser pscreen pinch
    let pfilter = choice [pramFilter;pweightFilter;pscreenFilter]

    let parser : Parser<(SearchFilter * ValueFilter) list, unit> = (sepBy pfilter pmultiFiltersSeperator)

    match run parser filterStr with
    | Success(filters, _, _) -> Choice1Of2(filters)
    | Failure(err,_,_) -> Choice2Of2(err)

