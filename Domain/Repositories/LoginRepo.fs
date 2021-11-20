namespace MyITracker

open System
open Newtonsoft.Json
open SQLite
open SQLiteApi
open HttpApi

module LoginRepo = //todo Test me
  dbConnection.CreateTablesAsync(CreateFlags.None, typeof<Login>).ConfigureAwait(false) |> ignore
  let private table = dbConnection.Table<Login>()
  let uri = Uri (rootAddress, "access/login.php")
  let post = post uri
  let json<'t> = json<'t> uri

  let validate (username : string) (password : string) = 
    Async.StartAsTask <| async {
      let mappings = 
        Map [ "username", username
              "password", password ]
      let! res = post "" (Some mappings)
      return 
        match res with
        | Ok x -> 
          match x with 
          | "{\"login\":\"success\"}" -> Ok true 
          | _ -> Ok false
        | Error x -> 
          Error x
    }

  let list() =
    Async.StartAsTask <| async {
      let! logins = await <| table.ToArrayAsync()
      return logins |> Seq.ofArray
  }

  let get () = 
    Async.StartAsTask <| async {
    let! res = await <| table.Where(fun x -> x.Index.Equals(1)).FirstOrDefaultAsync() //todo Test what I get back if empty
    return 
      match res with
      | null -> None
      | _ -> Some res
  }

  let insert username password =
    Async.StartAsTask <| async {
      let login = Login(Username = username, Password=password)
      let! old = await <| get ()
      return 
        match old with
        | Some x -> dbConnection.UpdateAsync(login)
        | None -> dbConnection.InsertAsync(login)
    }

  let delete item =
    dbConnection.DeleteAsync(item)

  let empty () = //todo Find out the exact name of table and use a Query with TRUNCATE
    Async.StartAsTask <| async {
      let! logins = await <| list ()
      return dbConnection.RunInTransactionAsync(fun v -> 
        logins
        |> Seq.map v.Delete 
        |> ignore)
    }

  //let GetItemsNotDoneAsync () =
    //dbConnection.QueryAsync<dbLogin>("SELECT * FROM [LoginModel]")