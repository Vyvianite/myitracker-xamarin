namespace MyITracker

open System
open HttpApi

module SearchRepo =
  let uri = Uri (rootAddress, "search/")
  module commands = 
    let search = "quickfind"

  let post = post uri
  let json<'t> = json<'t> uri

  let search term =
    task <| async {
      let map = Map ["searchterm", term]
      return! json<Search> commands.search (Some map)
    }