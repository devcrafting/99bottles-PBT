module Tests

open Xunit
open FsCheck
open FsCheck.Xunit

let genNbBottles min max =
    Gen.choose (min, max)
    |> Arb.fromGen

type IntFrom3To99 =
    static member Int() = genNbBottles 3 99

type IntFrom2To99 =
    static member Int() = genNbBottles 2 99

type IntFrom1To99 =
    static member Int() = genNbBottles 1 99

type Int0Or1 =
    static member Int() = genNbBottles 0 1

type Int1Or2 =
    static member Int() = genNbBottles 1 2

open NinetyNineBottles

[<Property( Arbitrary=[| typeof<IntFrom2To99> |] )>]
let ``Verses always contain ' of beer on the wall' from 99 to 2`` nbBottles =
    (verse nbBottles).Contains(" of beer on the wall, ")
    
[<Property( Arbitrary=[| typeof<IntFrom2To99> |] )>]
let ``Verses always contain ' of beer.\n' from 99 to 2`` nbBottles =
    (verse nbBottles).Contains(" of beer.
")

[<Property( Arbitrary=[| typeof<IntFrom3To99> |] )>]
let ``Verses always contain ' of beer on the wall.' from 99 to 3`` nbBottles =
    (verse nbBottles).Contains(" of beer on the wall.")

[<Property( Arbitrary=[| typeof<IntFrom1To99> |] )>]
let ``Verses always contain 'Take one down and pass it around, ' from 99 to 3`` nbBottles =
    (verse nbBottles).Contains("Take one down and pass it around, ")

[<Property( Arbitrary=[| typeof<Int0Or1> |] )>]
let ``Verses always contain ', no more bottles of beer' for 0 or 1`` nbBottles =
    (verse nbBottles).Contains(", no more bottles of beer")

[<Property( Arbitrary=[| typeof<Int1Or2> |] )>]
let ``Verses always contain ', 1 bottle of beer' for 1 or 2`` nbBottles =
    (verse nbBottles).Contains(", 1 bottle of beer")

[<Property( Arbitrary=[| typeof<IntFrom2To99> |] )>]
let ``Verses always contain nbBottles twice and nbBottles-1 once from 99 to 2 when not multiple of 6`` nbBottles =
    nbBottles % 6 <> 0 ==>
        let verse = (verse nbBottles)
        let nbBottlesLength = (string nbBottles).Length
        verse.StartsWith(string nbBottles)
        && verse.Remove(0, nbBottlesLength).Contains(string nbBottles)
        && verse.Contains(string (nbBottles - 1))

[<Property( Arbitrary=[| typeof<IntFrom2To99> |] )>]
let ``Verses always contain 'bottles' from 99 to 2 when not multiple of 6`` nbBottles =
    nbBottles % 6 <> 0 ==>
        (verse nbBottles).Contains("bottles")

[<Property( Arbitrary=[| typeof<Int1Or2> |] )>]
let ``Verses always singular when 1 bottle for 1 or 2`` nbBottles =
    let verse = verse nbBottles
    let charIndexFollowing1Bottle = verse.IndexOf("1 bottle") + 8
    verse.[charIndexFollowing1Bottle] = ' '

[<Fact>]
let ``Last verse starts 'No more'`` () =
    Assert.True((verse 0).StartsWith("No more"))

[<Fact>]
let ``Last verse ends with 'Go to the store and buy some more, 99 bottles of beer on the wall.'`` () =
    Assert.True((verse 0).EndsWith("Go to the store and buy some more, 99 bottles of beer on the wall."))

[<Property( Arbitrary=[| typeof<IntFrom1To99> |] )>]
let ``Verses always contains pack when nbBottles is a multiple of 6`` nbPacks =
    let verse = verse (6 * nbPacks)
    verse.Contains(" pack")
    && not <| verse.Contains(" bottle")
