namespace PhoneCat.Domain

[<AutoOpen>]
module Production = 
    
    type ManufacturerName = 
        Samsung | Motorola | Dell | LG | TMobile | Sanyo | Unknown
        
        static member ToString company =
            match company with
            | Samsung -> "Samsung"
            | Motorola -> "Motorola"
            | Dell -> "Dell"
            | LG -> "LG"
            | TMobile -> "TMobile"
            | Sanyo -> "Sanyo"
            | Unknown -> "Unknown"

        static member ToManufacturerName (name : string) =
            match name.ToLower() with
            | n when n.Contains("samsung") -> Samsung
            | n when n.Contains("motorola") -> Motorola
            | n when n.Contains("dell") -> Dell
            | n when n.Contains("lg") -> LG
            | n when n.Contains("t-mobile") -> TMobile
            | n when n.Contains("sanyo") -> Sanyo
            | n when n.Contains("nexus") -> Samsung
            | _ -> Unknown

    type Phone = 
        { Id : string
          Name : string
          Description : string
          ImageUrl : string }
        static member ToPhone (phone : PhoneTypeProvider.Root) =
              { Id = phone.Id; Name = phone.Name; Description = phone.Description; ImageUrl = phone.Images.[0] }

    type Manufacturer = 
        { Name : ManufacturerName
          Phones : seq<Phone> }

