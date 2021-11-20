namespace MyITracker

open System
open HttpApi

module PriorityRepo =
  let uri = Uri (rootAddress, "tickets/")
  module commands = 
    let list = "listpriorities"

  let post = post uri
  let json<'t> = json<'t> uri

  let list () =
    task <| async {
      return! json<seq<Priority>> commands.list None
    }