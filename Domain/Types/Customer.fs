namespace MyITracker

open System
open Newtonsoft.Json

type ShortCustomer =
  { Name : string

    [<JsonProperty("qbsql_id")>]
    Index : string }

  interface IModel with
    member x.Index = x.Index

type Customer =
  { [<JsonProperty("qbsql_id")>]
    Index : string
    Name : string
    Phone : string
    Email : string
    Contact : string
    AltContact : string

    [<JsonProperty("contract")>]
    Contract : string

    [<JsonProperty("BillAddress_Addr2")>]
    Street : string

    [<JsonProperty("BillAddress_City")>]
    City : string

    [<JsonProperty("BillAddress_State")>]
    State : string

    [<JsonProperty("BillAddress_PostalCode")>]
    Zipcode : string }

  interface IModel with
    member x.Index = x.Index