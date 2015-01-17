namespace PhoneCat.Domain
open PhoneCat.Domain.Measures

module Search =
  
  type ValueFilter = 
  | Value of float 
  | GreaterThan of float 
  | Range of float * float

  type SearchFilter = Ram | Weight | Screen 

  let searchPhones (phones : Catalog.Phone seq) (filters : (SearchFilter * ValueFilter) list) =

    let metValueFilter toMeasure valueFilter property  =
      match valueFilter with
      | Value x -> property = toMeasure x
      | GreaterThan x -> property > toMeasure x
      | Range (x,y) -> property >= toMeasure x && property <= toMeasure y

    let metSearchFilter filter (phone : Catalog.Phone) =
      let valueFilter = (snd filter)
      match (fst filter) with
      | Ram -> metValueFilter ((*) 1.0<MB>) valueFilter phone.Storage.Ram
      | Weight -> metValueFilter ((*) 1.0<g>) valueFilter phone.Weight
      | Screen -> metValueFilter ((*) 1.0<inch>) valueFilter phone.Display.ScreenSize

    let filterPhones phones' filter =
      phones'
      |> Seq.filter (metSearchFilter filter)

    
    let rec searchPhones' phones' filters' =
      match filters' with
      | [] -> phones'
      | x :: xs -> (searchPhones' (filterPhones phones' x) xs)

    searchPhones' phones filters
       