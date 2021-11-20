namespace MyITracker

open System
open System.Threading.Tasks

module testPad =
  let none = None //This stops the compiler bugging me about this empty module lol

  //let list<'T when 'T :> IModel> (t : 't) : Task<'t> =
    //match box t with 
    //| :? Customer as x -> Customer.list () //returns seq customer instead on seq t