namespace MyITracker

open Newtonsoft.Json

type Situation =
  { [<JsonProperty("situation_id")>]
    Index : string

    [<JsonProperty("situation_label")>]
    Label : string }

  interface IModel with 
    member x.Index = x.Index