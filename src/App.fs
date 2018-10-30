module Starter.App

open Fable.Core
open Fable.Import
open Fable.Helpers.ReactToolbox
open Fable.Helpers.React.Props

// [<Literal>]
// let ConfigThemeSource = __SOURCE_DIRECTORY__ + "/theme/_config.scss"
// let config:obj = Fable.Core.JsInterop.importAll ConfigThemeSource
// [<Literal>]
// let AppBarThemeSource = __SOURCE_DIRECTORY__ + "/theme/AppBarTheme.scss"
// let appBarTheme:obj = Fable.Core.JsInterop.importAll AppBarThemeSource

type Model = {
    tabIndex: int
    isChecked: bool
    info: string
}

type Msg =
    | TabChanged of int
    | TextChanged of string
    | CheckboxChanged of bool
    | SwitchChanged of bool


let init () = {
    tabIndex = 0
    isChecked = true
    info = "Something here"
}

let update msg model =
    match msg with
    | TabChanged i ->
        { model with tabIndex = i }
    | TextChanged s ->
        { model with info = s}    
    | CheckboxChanged v 
    | SwitchChanged v ->
        { model with isChecked = v}    

module R = Fable.Helpers.React
module RT = Fable.Helpers.ReactToolbox

let view model dispatch =    
        R.div [] [
            RT.appBar [ AppBarProps.LeftIcon "grade" ] []
            RT.tabs [ Index model.tabIndex; TabsProps.OnChange (TabChanged >> dispatch) ] [
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
                        RT.input [ Type "text"; Label "Information"; InputProps.Value model.info; InputProps.OnChange (TextChanged >> dispatch) ] []
                        RT.checkbox [ Label "Check me"; Checked model.isChecked; CheckboxProps.OnChange (CheckboxChanged >> dispatch) ] []
                        RT.switch [ Label "Switch me"; Checked model.isChecked; SwitchProps.OnChange (SwitchChanged >> dispatch) ] []
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

Program.mkSimple init update view
|> Program.withReactUnoptimized "app"
|> Program.run