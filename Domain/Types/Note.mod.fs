namespace MyITracker

open System 

[<Mod(Flags.ModuleSuffix)>]
module Note = 
  let mergeResolver (senpaiNote : INote) kohaiBody kohaiVer = //todo test note merging
    if (senpaiNote.Version > kohaiVer) then
      let merged = sprintf " Version %s:\r\n%s\r\n\r\nVersion %s Conflicting Commit:\r\n%s" senpaiNote.Version senpaiNote.Body kohaiVer kohaiBody
      Some (struct(merged, senpaiNote.Version)) //This feels a bit dirty, but record init doesn't play well with generics.
    else
      None