namespace MyITracker

open System
open HttpApi

module UserRepo =
  let uri = Uri (rootAddress, "users/")
  module commands = 
    let listTechs = "listtechs"
    let listUsers = "listusers"

  let post = post uri
  let json<'t> = json<'t> uri

  let listTechs () =
    task <| async {
      return! json<seq<User>> commands.listTechs None
    }

  let listUsers () = 
    task <| async {
      return! json<seq<User>> commands.listUsers None
    }
