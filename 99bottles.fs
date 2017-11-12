module NinetyNineBottles

let lower (s: string) = s.ToLower()

let verse (nbBottles: int) = 
    let bottlesOnTheWall x =
        match nbBottles with
        | nb when nb = x -> "No more bottles"
        | nb when nb = x + 1 -> "1 bottle"
        | nb when nb = x + 6 -> "1 pack"
        | nb when (nb - x) % 6 = 0 -> sprintf "%i packs" (nb/6) 
        | nb -> sprintf "%i bottles" (nb - x)
    
    let firstHemistish = sprintf "%s of beer on the wall, %s of beer." (bottlesOnTheWall 0) (bottlesOnTheWall 0 |> lower)

    let secondHemistish =
        match nbBottles with
        | 0 -> "Go to the store and buy some more, 99 bottles of beer on the wall."
        | _ -> sprintf "Take one down and pass it around, %s of beer on the wall."  (bottlesOnTheWall 1 |> lower)

    sprintf "%s
    %s" firstHemistish secondHemistish

let sing () =
    [0..99]
    |> List.rev
    |> List.map verse
