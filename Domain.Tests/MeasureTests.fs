namespace PhoneCat.Domain.Phones.Tests
open NUnit.Framework
open FsUnit
open PhoneCat.Domain.Measures

module MeasureTests = 
    
    [<Test>]
    let ``Can convert inches string to inches type`` () =
        toInch "200inches" |> float |> should equal 200.0<inch>
        
    [<Test>]
    let ``Can convert mb string to mb type`` () =
        toMB "12.5MB" |> float |> should equal 12.5<MB>


    [<Test>]
    let ``Can convert grams string to gram type`` () =
        toGram "156grams" |> should equal 156.0<g>