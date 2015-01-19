namespace PhoneCat.Domain
open PhoneCat.Domain.Measures
open PhoneCat.Domain.Catalog

module Search = 

  type SearchFilter = 
  | Ram of (float -> float<MB>)
  | Weight of (float -> float<g>)
  | Screen of (float -> float<inch>)

  type ValueFilter = 
  | Value of float 
  | GreaterThan of float 
  | Range of float * float  

  let searchPhones phones filters =

    let hasMetValueFilter toUom valueFilter property  =
      match valueFilter with
      | Value x -> property = toUom x
      | GreaterThan x -> property > toUom x
      | Range (x,y) -> property >= toUom x && property <= toUom y

    let hasMetSearchFilter filter phone =
      let valueFilter = (snd filter)
      match (fst filter) with
      | Ram toMB -> hasMetValueFilter toMB valueFilter phone.Storage.Ram
      | Weight toG -> hasMetValueFilter toG valueFilter phone.Weight
      | Screen toInch-> hasMetValueFilter toInch valueFilter phone.Display.ScreenSize

    let filterPhones phones' filter =
      phones'
      |> Seq.filter (hasMetSearchFilter filter)

    
    let rec searchPhones' phones' filters' =
      match filters' with
      | [] -> phones'
      | x :: xs -> (searchPhones' (filterPhones phones' x) xs)

    searchPhones' phones filters
       