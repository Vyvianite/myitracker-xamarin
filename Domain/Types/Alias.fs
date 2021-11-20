namespace MyITracker

open System
open System.Collections.ObjectModel
open System.Runtime.InteropServices

(*For splitting modules into seperate files*)
type Mod = CompilationRepresentationAttribute
type Flags = CompilationRepresentationFlags

(*For optional args .net interop*)
type Opt = OptionalArgumentAttribute
type Def = DefaultParameterValueAttribute