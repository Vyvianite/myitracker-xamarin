namespace MyITracker

open System
open HttpApi

module SituationRepo =
  let uri = Uri (rootAddress, "tickets/")
  module commands = 
    let list = "listsituations"

  let post = post uri
  let json<'t> = json<'t> uri

  let list () =
    task <| async {
      return! json<seq<Situation>> commands.list None
    }