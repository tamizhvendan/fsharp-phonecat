namespace PhoneCat.DataAccess

open FSharp.Data
open PhoneCat.Domain
open PhoneCat.Domain.Catalog
open PhoneCat.Domain.Measures

[<AutoOpen>]
module TypeProviders = 
  [<Literal>]
  let private samplePhoneIndexes = """
  [{
      "age": 1,
      "id": "id0",
      "imageUrl": "id0.jpg",
      "name": "Name0",
      "snippet": "Sample Snippet0"
  },{
      "age": 2,
      "id": "id1",
      "imageUrl": "id1.jpg",
      "name": "Name1",
      "snippet": "Sample Snippet2"
  }]
  """
    
  type PhoneIndexTypeProvider = JsonProvider<samplePhoneIndexes>
    
  [<Literal>]
  let private samplePhoneJson = """
      {
      "additionalFeatures": "Trackball", 
      "android": {
          "os": "Android 2.2", 
          "ui": "MOTOBLUR"
      }, 
      "availability": [
          "Sprint"
      ], 
      "battery": {
          "standbyTime": "", 
          "talkTime": "4 hours", 
          "type": "Lithium Ion (Li-Ion) (1130 mAH)"
      }, 
      "camera": {
          "features": [
              "Video"
          ], 
          "primary": "3.2 megapixels"
      }, 
      "connectivity": {
          "bluetooth": "Bluetooth 2.1", 
          "cell": "CDMA2000 1xEV-DO Rev.A", 
          "gps": true, 
          "infrared": false, 
          "wifi": "802.11 b/g"
      }, 
      "description": "Zio uses CDMA2000 1xEV-DO rev", 
      "display": {
          "screenResolution": "WVGA (800 x 480)", 
          "screenSize": "3.5 inches", 
          "touchScreen": true
      }, 
      "hardware": {
          "accelerometer": true, 
          "audioJack": "3.5mm", 
          "cpu": "600MHz Qualcomm MSM7627", 
          "fmRadio": false, 
          "physicalKeyboard": false, 
          "usb": "USB 2.0"
      }, 
      "id": "sanyo-zio", 
      "images": [
          "img/phones/sanyo-zio.0.jpg", 
          "img/phones/sanyo-zio.1.jpg", 
          "img/phones/sanyo-zio.2.jpg"
      ], 
      "name": "SANYO ZIO", 
      "sizeAndWeight": {
          "dimensions": [
              "58.6 mm (w)", 
              "116.0 mm (h)", 
              "12.2 mm (d)"
          ], 
          "weight": "105.0 grams"
      }, 
      "storage": {
          "flash": "130MB", 
          "ram": "256MB"
      }
  }

  """
    
  type PhoneTypeProvider = JsonProvider<samplePhoneJson>
    
  let ToPhone(phone : PhoneTypeProvider.Root) = 
    { Id = phone.Id
      Name = phone.Name
      Description = phone.Description
      ImageUrl = phone.Images.[0] }
    
  let ToPhoneIndex(phoneIndex : PhoneIndexTypeProvider.Root) = 
    { Id = phoneIndex.Id
      Name = phoneIndex.Name
      Age = phoneIndex.Age
      ImageUrl = phoneIndex.ImageUrl }
    
  let ToCatalogPhone(phone : PhoneTypeProvider.Root) = 
    let android = { OS = phone.Android.Os; UI = phone.Android.Ui }
        
    let camera = { Features = phone.Camera.Features; Primary = phone.Camera.Primary }        
        
    let display = 
      { ScreenResolution = phone.Display.ScreenResolution
        ScreenSize = toInch phone.Display.ScreenSize
        TouchScreen = phone.Display.TouchScreen }

    let storage = { Flash = toMB phone.Storage.Flash; Ram = toMB phone.Storage.Ram}

    { Id = phone.Id
      Name = phone.Name
      Description = phone.Description
      Android = android
      Camera = camera
      Display = display
      Weight = toGram phone.SizeAndWeight.Weight
      Storage = storage 
      Images = phone.Images}
