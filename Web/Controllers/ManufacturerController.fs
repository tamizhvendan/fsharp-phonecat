namespace PhoneCat.Web.Controllers

open System.Web
open System.Web.Mvc
open PhoneCat.Domain
open System

type ManufacturerViewModel = {
  Name : string
  Phones : seq<Phone>
}
with static member ToManufacturerViewModel (name, phones) =
      {Name = ManufacturerName.ToString name; Phones = phones}

type ManufacturerController 
  (
    getPhones : ManufacturerName -> (ManufacturerName * seq<Phone>)
  ) =
  inherit Controller()
  
  member this.Show (id : string) = 
    
    let viewModel = 
      id 
      |> ManufacturerName.ToManufacturerName
      |> getPhones
      |> ManufacturerViewModel.ToManufacturerViewModel

    this.View(viewModel)

