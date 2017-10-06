module Starter.App

open Fable.Core
open Fable.Import
open Fable.Helpers.ReactToolbox
open Fable.Helpers.React.Props
open Elmish.Browser.Navigation
open Elmish.HMR
open Elmish

type Model = 
    { TabIndex: int
      IsChecked: bool
      Info: string }
      
let init () = 
    { TabIndex = 0
      IsChecked = true
      Info = "Something here" }, 
    []
    
type Msg =
    | TabIndexChanged of int
    | InfoChanged of string    
    | IsCheckedChanged of bool
    
let update msg model : Model * Cmd<Msg> = 
    match msg with
    | TabIndexChanged idx -> { model with TabIndex = idx }, [] 
    | InfoChanged info -> { model with Info = info }, []
    | IsCheckedChanged v -> { model with IsChecked = v }, []

module R = Fable.Helpers.React
module RT = Fable.Helpers.ReactToolbox
    
let view model dispatch =
    R.div [] [
        RT.appBar [ AppBarProps.LeftIcon "grade" ] []
        RT.tabs [ Index model.TabIndex; TabsProps.OnChange (fun i -> dispatch (TabIndexChanged i)) ] [
            RT.tab [ Label "Buttons" ] [
                R.section [] [
                    RT.button [ Icon "help"; Label "Help"; ButtonProps.Primary true; Raised true ] []
                    RT.button [ Icon "home"; Label "Home"; Raised true ] []
                    RT.button [ Icon "rowing"; Floating true ] []
                    RT.iconButton [ Icon "power_settings_new"; IconButtonProps.Primary true ] []
                ]
            ]
            RT.tab [ Label "Inputs" ] [
                R.section [] [
                    RT.input [ Type "text"; Label "Information"; InputProps.Value model.Info; InputProps.OnChange (fun v -> dispatch (InfoChanged v)) ] []
                    RT.checkbox [ Label "Check me"; Checked model.IsChecked; CheckboxProps.OnChange (fun v -> dispatch (IsCheckedChanged v)) ] []
                    RT.switch [ Label "Switch me"; Checked model.IsChecked; SwitchProps.OnChange (fun v -> dispatch (IsCheckedChanged v)) ] []
                ]
            ]
            RT.tab [ Label "List" ] [
                RT.list [] [
                    RT.listSubHeader [ Caption "Listing" ] []
                    RT.listDivider [] []
                    RT.listItem [ Caption "Item 1"; Legend "Keeps it simple" ] []
                    RT.listDivider [] []
                    RT.listItem [ Caption "Item 2"; Legend "Turns it up a notch"; RightIcon (U2.Case2 "star") ] []
                ]
            ]
        ]
    ]

open Elmish
open Elmish.React
open Elmish.Debug

// App
Program.mkProgram init update view
#if DEBUG
|> Program.withConsoleTrace
|> Program.withDebugger
|> Program.withHMR
#endif
|> Program.withReact "elmish-app"
|> Program.run