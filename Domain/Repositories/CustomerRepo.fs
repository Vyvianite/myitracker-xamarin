namespace MyITracker

open System
open Newtonsoft.Json
open System.Collections.ObjectModel
open System.Threading.Tasks
open HttpApi

module CustomerRepo =
  let uri = Uri (rootAddress, "customers/")
  module commands = 
    let list = "listcustomers"
    let get = "getcustomerdata"
    let insert = "savecustomer"
    let update = "updatecustomer"

  let post = post uri 
  let json<'t> = json<'t> uri

  let list () =
    task <| async {
      return! json<seq<ShortCustomer>> commands.list None
    }

  let get customerId = 
    task <| async {
      let mappings = Map ["cid", customerId]
      return! json<Customer> commands.get (Some mappings)
    }

  let getHistory customerId = 
    let mappings = Map ["cid", customerId]
    TicketRepo.filteredList (Some mappings)

  (*This takes a customer because calling like insert({customer with Phone = newPhone}) is really easy.
    Using this from C# isn't the best however.*)
  let insert (customer : Customer) =
    task <| async {
      let mappings = (*todo Refactor this where it reads JsonProp attributes for the keys?*)
        Map [ "Name",                   customer.Name
              "Phone",                  customer.Phone
              "Email",                  customer.Email
              "Contact",                customer.Contact
              "AltContact",             customer.AltContact
              "contract",               customer.Contract
              "BillAddress_Addr2",      customer.Street
              "BillAddress_City",       customer.City
              "BillAddress_PostalCode", customer.Zipcode
              "BillAddress_State",      customer.State ]
      return! post commands.insert (Some mappings)
    }

  (*These are a alot of params*)
  let update (customer : Customer) =
    task <| async {
      let mappings = 
        Map [ "qbsql_id",               customer.Index
              "Name",                   customer.Name
              "Phone",                  customer.Phone
              "Email",                  customer.Email
              "Contact",                customer.Contact
              "AltContact",             customer.AltContact
              "contract",               customer.Contract
              "BillAddress_Addr2",      customer.Street
              "BillAddress_City",       customer.City
              "BillAddress_PostalCode", customer.Zipcode
              "BillAddress_State",      customer.State ]
      return! post commands.update (Some mappings)
    }

  module Note =
    module commands =
      let list = "getcustomernotes"
      let insert = "savecustomernote"
      let update = "updatecustomernote"
      let delete = "deletecustomernote"

    let list customerId =
      task <| async {
        let mappings = Map["cid", customerId]
        return! json<seq<CustomerNote>> commands.list (Some mappings)
      }

    let insert (note : CustomerNote) =
      task <| async {
        let map = 
          Map [ "cid", note.CustomerId
                "label", note.Label
                "note", note.Body ]
        return! post commands.insert (Some map)
      }

    let update (note : CustomerNote) =
      task <| async {
        let map =
          Map [ "cid", note.CustomerId
                "nid", note.Index
                "label", note.Label
                "note", note.Body
                "version", note.Version ]
        return! json<CustomerNote> commands.update (Some map)
      }

    let delete noteId =
      task <| async {
        let map = Map ["nid", noteId]
        return! post commands.delete (Some map)
      }


  module Computers = 
    module commands =
      let list = "getcomputerdata"