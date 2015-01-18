namespace PhoneCat.Domain
open PhoneCat.Domain.Measures

module Search =
  

  type SearchFilter = 
  | Ram of (float -> float<MB>)
  | Weight of (float -> float<g>)
  | Screen of (float -> float<inch>)

  type ValueFilter = 
  | Value of float 
  | GreaterThan of float 
  | Range of float * float  

  let searchPhones (phones : Catalog.Phone seq) (filters : (SearchFilter * ValueFilter) list) =

    let metValueFilter toUom valueFilter property  =
      match valueFilter with
      | Value x -> property = toUom x
      | GreaterThan x -> property > toUom x
      | Range (x,y) -> property >= toUom x && property <= toUom y

    let metSearchFilter filter (phone : Catalog.Phone) =
      let valueFilter = (snd filter)
      match (fst filter) with
      | Ram toMB -> metValueFilter toMB valueFilter phone.Storage.Ram
      | Weight toG -> metValueFilter toG valueFilter phone.Weight
      | Screen toInch-> metValueFilter toInch valueFilter phone.Display.ScreenSize

    let filterPhones phones' filter =
      phones'
      |> Seq.filter (metSearchFilter filter)

    
    let rec searchPhones' phones' filters' =
      match filters' with
      | [] -> phones'
      | x :: xs -> (searchPhones' (filterPhones phones' x) xs)

    searchPhones' phones filters
       