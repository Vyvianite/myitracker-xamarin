namespace MyITracker

open System
open SQLite

[<AllowNullLiteral>] //todo I don't like this being mutable but sqlite requires it.
  type Login() = 
    [<PrimaryKey>]
    member val Index = 1 with get, set //todo test if you can remove setter or set as literal.
    member val Username = "" with get, set
    member val Password = "" with get, set

//type Login = //I wish but making a record work was overcomplicated
  //{ Username : string
    //Password : string }