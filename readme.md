99bottles kata
==============

First, start with the basic song
https://www.codewars.com/kata/99-bottles-of-beer-1

We used Property Based Testing, trying to find invariants across verses.

Then, try to add the additional feature: "for multiple of 6, say X packs instead of bottles". 

Remark: we struggle a bit to rework existing properties, where we had to remove the "multiple of 6" cases, to handle packs cases instead with a new property. All of our properties were matching 'bottles' substring, so we removed it, but then we were reducing coverage, since we could remove the 's' to bottle without breaking tests...so we added one test with complementary cases to the ones of "pack".
