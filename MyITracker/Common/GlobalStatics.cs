using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;

namespace MyITracker {
  #region Lists
  public static class Lists {
    public static List<ShortCustomer> Customers { get; set; } = new List<ShortCustomer>(); //todo make this thread safe or some shit idk
    public static List<Priority> Priorities { get; set; } = new List<Priority>();
    public static List<User> Techs { get; set; } = new List<User>();
    public static List<Situation> Situations { get; set; } = new List<Situation>();
    public static List<string> TicketStates { get; } = new List<string> { "Active", "Inactive", "Closed" }; //todo add to server?
  }
  #endregion
}