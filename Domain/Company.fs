namespace PhoneCat.Domain

[<AutoOpen>]
module Company = 
    type Company = 
        Samsung | Motorola | Dell | LG | TMobile | Sanyo | Others
        static member ToString company =
            match company with
            | Samsung -> "Samsung"
            | Motorola -> "Motorola"
            | Dell -> "Dell"
            | LG -> "LG"
            | TMobile -> "TMobile"
            | Sanyo -> "Sanyo"
            | Others -> "Others"

        static member ToCompany (name : string) =
            match name.ToLower() with
            | n when n.Contains("samsung") -> Samsung
            | n when n.Contains("motorola") -> Motorola
            | n when n.Contains("dell") -> Dell
            | n when n.Contains("lg") -> LG
            | n when n.Contains("tmobile") -> TMobile
            | n when n.Contains("sanyo") -> Sanyo
            | n when n.Contains("nexus") -> Samsung
            | _ -> Others

    type Manufacturer = 
        { Name : string
          Phones : seq<PhoneTypeProvider.Root> }

