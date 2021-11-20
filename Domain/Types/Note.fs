namespace MyITracker

open System
open Newtonsoft.Json

type INote =
  inherit IModel
  abstract member Version : string
  abstract member Body : string
  //abstract member Fullname : string //todo These to can be null. Deserialise to options?
  //abstract member Creator : string

type CustomerNote = 
  { [<JsonProperty("nid")>]
    Index : string 

    [<JsonProperty("version")>]
    Version : string

    [<JsonProperty("cid")>]
    CustomerId : string

    [<JsonProperty("label")>]
    Label : string

    [<JsonProperty("note")>]
    Body : string

    [<JsonProperty("fullname")>]
    Fullname : string

    [<JsonProperty("created_by")>]
    Creator : string }

  interface INote with 
    member x.Index = x.Index
    member x.Version = x.Version
    member x.Body = x.Body

type TicketNote =
  { [<JsonProperty("rid")>]
    Index : string 

    [<JsonProperty("version")>]
    Version : string

    [<JsonProperty("tid")>]
    TicketId : string

    [<JsonProperty("note")>]
    Body : string

    [<JsonProperty("fullname")>]
    Fullname : string

    [<JsonProperty("created_by")>]
    Creator : string //todo test if this is id or name

    [<JsonProperty("date_created")>]
    Created : string

    [<JsonProperty("date_changed")>]
    Updated : string }

  interface INote with 
    member x.Index = x.Index
    member x.Version = x.Version
    member x.Body = x.Body
