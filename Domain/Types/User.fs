namespace MyITracker

open Newtonsoft.Json

type User = 
  { [<JsonProperty("user_id")>]
    Index : string

    [<JsonProperty("username")>]
    Username : string

    [<JsonProperty("fullname")>]
    Name : string }

  interface IModel with 
    member x.Index = x.Index