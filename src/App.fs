module Starter.App

open Fable.Core
open Fable.Import
open Fable.Helpers.ReactToolbox
open Fable.Helpers.React.Props

[<Pojo>]
type Model = {
    tabIndex: int
    isChecked: bool
    info: string
}

let init = {
    tabIndex = 0
    isChecked = true
    info = "Something here"
}

module R = Fable.Helpers.React
module RT = Fable.Helpers.ReactToolbox
    
type App(props) as this =
    inherit React.Component<obj, Model>(props)
    do this.setInitState init
        
    member this.render() =
        R.div [] [
            RT.appBar [ AppBarProps.LeftIcon "grade" ] []
            RT.tabs [ Index this.state.tabIndex; TabsProps.OnChange (fun i -> this.setState({this.state with tabIndex = i})) ] [
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
                        RT.input [ Type "text"; Label "Information"; InputProps.Value this.state.info; InputProps.OnChange (fun v -> this.setState({this.state with info = v})) ] []
                        RT.checkbox [ Label "Check me"; Checked this.state.isChecked; CheckboxProps.OnChange (fun v -> this.setState({this.state with isChecked = v})) ] []
                        RT.switch [ Label "Switch me"; Checked this.state.isChecked; SwitchProps.OnChange (fun v -> this.setState({this.state with isChecked = v})) ] []
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

ReactDom.render(
        R.com<App,_,_> () [],
        Browser.document.getElementsByClassName("app").[0]
    ) |> ignore