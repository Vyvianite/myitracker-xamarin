namespace MyITracker

open System
open Newtonsoft.Json

type ShortTicket =
  { [<JsonProperty("tid")>]
    Index : string

    [<JsonProperty("qbsql_id")>]
    CustomerId : string

    Name : string

    [<JsonProperty("pcolor")>]
    PriorityColor : string

    [<JsonProperty("priority")>]
    PriorityId : string

    [<JsonProperty("tkt_subject")>]
    Subject : string }

  interface IModel with
    member x.Index = x.Index

type Ticket =
  { [<JsonProperty("tid")>]
    Index : string

    [<JsonProperty("qbsql_id")>]
    CustomerId : string

    [<JsonProperty("priority")>]
    PriorityId : string

    [<JsonProperty("tkt_subject")>]
    Subject : string

    [<JsonProperty("createdby")>]
    Creator : string

    [<JsonProperty("uid")>]
    TechnicianId : string

    [<JsonProperty("situation")>]
    SituationId : string

    [<JsonProperty("datecreated")>]
    Created : string }

  interface IModel with
    member x.Index = x.Index
