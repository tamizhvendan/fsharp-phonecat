namespace PhoneCat.Web.Controllers

open System.Web
open System.Web.Mvc
open PhoneCat.Domain.Catalog
open System
open System.Reactive.Subjects

type PhoneViewModel = 
  {
    Name : string
    Description : string
    Os : string
    Ui : string
    Flash : string
    Ram : string
    Weight : string
    ScreenResolution : string
    ScreenSize : string
    TouchScreen : string
    Primary : string
    Features : seq<string>
    Images : seq<string>    
  }

  with static member ToPhoneViewModel(phone : Phone) =
        let roundToDigits (value:float) = String.Format("{0:0.00}", value)
        let concatWithSpace str2 str1 = str1 + " " + str2
        let uomToString (measureValue: float<_>) measureName =
          measureValue |> float |> roundToDigits |> concatWithSpace measureName
        
        { 
          Name = phone.Name
          Description = phone.Description
          Os = phone.Android.OS
          Ui = phone.Android.UI
          Flash = phone.Storage.Flash |> int |> sprintf "%d MB"
          Ram =  phone.Storage.Ram |> int |> sprintf "%d MB"
          Weight = uomToString phone.Weight "grams"
          ScreenResolution = phone.Display.ScreenResolution
          ScreenSize = uomToString phone.Display.ScreenSize "inches"
          TouchScreen = if phone.Display.TouchScreen then "Yes" else "No"
          Primary = phone.Camera.Primary
          Features = phone.Camera.Features
          Images = phone.Images
        }

type PhoneController(phones : seq<Phone>) =
  inherit Controller()
  
  let subject = new Subject<string>()
  
  interface IObservable<string> with
    member this.Subscribe observer = subject.Subscribe observer
  
  member this.Show (id : string) = 
    let phone = phones |> Seq.find (fun p -> p.Id = id) |> PhoneViewModel.ToPhoneViewModel
    subject.OnNext id 
    this.View(phone)

  override this.Dispose disposing =
    if disposing then subject.Dispose()
    base.Dispose disposing
