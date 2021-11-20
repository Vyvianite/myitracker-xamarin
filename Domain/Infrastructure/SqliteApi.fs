namespace MyITracker

open System
open System.IO
open SQLite

module SQLiteApi =
  let dbName = "myitracker.db3"
  let dbFlags = SQLiteOpenFlags.ReadWrite ||| SQLiteOpenFlags.Create ||| SQLiteOpenFlags.SharedCache

  let dbPath =
    let basePath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData)
    Path.Combine(basePath, dbName)

  let conString = SQLiteConnectionString(dbPath, dbFlags, false, "yeetinOnEm")

  let dbConnection = SQLiteAsyncConnection(conString)