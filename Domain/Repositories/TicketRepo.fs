namespace MyITracker

open System
open HttpApi

module TicketRepo =
  let uri = Uri (rootAddress, "tickets/")

  module commands = 
    let list = "listtickets"
    let get = "getticketdata"
    let insert = "saveticket"
    let update = "updateticket"
    let close = "closeticket"

  let post = post uri
  let json<'t> = json<'t> uri

  (*Base Impl*)
  let filteredList filters = 
    task <| async {
      return! json<seq<ShortTicket>> commands.list filters
    }
  
  let list () =
    filteredList None

  let get ticketId =
    task <| async {
      let mappings = Map ["tid", ticketId]
      return! json<Ticket> commands.get (Some mappings)
    }

  let update (ticket : Ticket) = 
    task <| async {
      let map =
        Map [ "tid", ticket.Index
              "tkt_subject", ticket.Subject
              "user_id", ticket.TechnicianId
              "situation", ticket.SituationId
              "priority", ticket.PriorityId ]
      return! post commands.update (Some map)
    }

  module Note =
    module commands =
      let list = "getticketnotes"
      let delete = "deleteticketnote"
      let insert = "saveticketnote"
      let update = "updateticketnote"

    let list ticketID =
      task <| async {
        let map = Map ["tid", ticketID]
        return! json<seq<TicketNote>> commands.list (Some map)
      }
