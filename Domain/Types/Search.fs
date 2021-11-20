namespace MyITracker

open System
open Newtonsoft.Json

type Search =
  { Index : string

    [<JsonProperty("tickets")>]
    Tickets : seq<TicketNote>

    [<JsonProperty("customers")>]
    Customers : seq<CustomerNote> }

  interface IModel with 
    member x.Index = x.Index