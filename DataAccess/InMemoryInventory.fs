namespace PhoneCat.DataAccess
open PhoneCat.Domain

module InMemoryInventory =

    let getPhonesSold () = [
        { Id = "motorola-xoom-with-wi-fi"; Quantity = 10}
        { Id = "motorola-atrix-4g"; Quantity = 24}
        { Id = "nexus-s"; Quantity = 4}
        { Id = "samsung-galaxy-tab"; Quantity = 41}
        { Id = "sanyo-zio"; Quantity = 31}
        { Id = "motorola-xoom"; Quantity = 6}
    ]

