namespace MyITracker

open System
open Newtonsoft.Json

type Priority = 
  { [<JsonProperty("prid")>]
    Index : string

    [<JsonProperty("priority")>]
    Label : string }
  interface IModel with
    member x.Index = x.Index