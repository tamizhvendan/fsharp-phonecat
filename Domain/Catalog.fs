namespace PhoneCat.Domain

open PhoneCat.Domain.Measures

module Catalog =    

    type Display = {
        ScreenResolution : string
        ScreenSize : float<inch>
        TouchScreen : bool
    }
    
    type Storage = {
        Flash : float<MB>
        Ram : float<MB>
    }

    type Android = {
        OS : string
        UI : string
    }

    type Camera = {
        Features : seq<string>
        Primary : string
    }

    type Phone = {
        Name : string
        Description : string
        Android : Android
        Camera : Camera
        Display : Display
        Weight : float<g>
        Storage : Storage
    }

