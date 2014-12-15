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
            | n when n.Contains("t-mobile") -> TMobile
            | n when n.Contains("sanyo") -> Sanyo
            | n when n.Contains("nexus") -> Samsung
            | _ -> Others

    type Phone = 
        { Id : string
          Name : string
          Description : string
          ImageUrl : string }
        static member ToPhone (phone : PhoneTypeProvider.Root) =
              { Id = phone.Id; Name = phone.Name; Description = phone.Description; ImageUrl = phone.Images.[0] }

    type Manufacturer = 
        { Name : string
          Phones : seq<Phone> }

