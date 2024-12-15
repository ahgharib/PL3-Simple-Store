namespace SimpleStoreSimulator

open Avalonia.FuncUI
open Avalonia.FuncUI.Components
open Avalonia.FuncUI.DSL
open Avalonia.Controls

type Product = {
    Name: string
    Price: float
    Description: string
}

let productCatalog = [
    { Name = "Laptop"; Price = 999.99; Description = "High performance laptop" }
    { Name = "Headphones"; Price = 49.99; Description = "Noise-cancelling headphones" }
    { Name = "Mouse"; Price = 19.99; Description = "Wireless mouse" }
    { Name = "Keyboard"; Price = 29.99; Description = "Mechanical keyboard" }
]

type State = {
    Cart: Product list
    SelectedProduct: Product option
}

type Msg =
    | AddToCart of Product
    | RemoveFromCart of Product
    | ClearCart

let initState = { Cart = []; SelectedProduct = None }

let update (state: State) (msg: Msg): State =
    match msg with
    | AddToCart product -> { state with Cart = product :: state.Cart }
    | RemoveFromCart product -> { state with Cart = List.filter ((<>) product) state.Cart }
    | ClearCart -> { state with Cart = [] }

let view (state: State) (dispatch: Msg -> unit) =
    DockPanel.create [
        DockPanel.children [
            // Product List
            ListBox.create [
                DockPanel.dock Dock.Top
                ListBox.items productCatalog
                ListBox.dataTemplates [
                    DataTemplate.create (fun product ->
                        StackPanel.create [
                            StackPanel.children [
                                TextBlock.create [
                                    TextBlock.text product.Name
                                    TextBlock.fontSize 16.0
                                    TextBlock.margin 5.0
                                ]
                                TextBlock.create [
                                    TextBlock.text (sprintf "$%.2f - %s" product.Price product.Description)
                                    TextBlock.margin 5.0
                                ]
                                Button.create [
                                    Button.content "Add to Cart"
                                    Button.margin 5.0
                                    Button.onClick (fun _ -> dispatch (AddToCart product))
                                ]
                            ]
                        ]
                    )
                ]
            ]

            // Cart
            StackPanel.create [
                DockPanel.dock Dock.Bottom
                StackPanel.children [
                    TextBlock.create [
                        TextBlock.text "Cart:"
                        TextBlock.fontSize 18.0
                        TextBlock.margin 5.0
                    ]
                    ListBox.create [
                        ListBox.items state.Cart
                        ListBox.dataTemplates [
                            DataTemplate.create (fun product ->
                                StackPanel.create [
                                    StackPanel.children [
                                        TextBlock.create [
                                            TextBlock.text (sprintf "%s - $%.2f" product.Name product.Price)
                                            TextBlock.margin 5.0
                                        ]
                                        Button.create [
                                            Button.content "Remove"
                                            Button.margin 5.0
                                            Button.onClick (fun _ -> dispatch (RemoveFromCart product))
                                        ]
                                    ]
                                ]
                            )
                        ]
                    ]
                    Button.create [
                        Button.content "Clear Cart"
                        Button.margin 5.0
                        Button.onClick (fun _ -> dispatch ClearCart)
                    ]
                    TextBlock.create [
                        TextBlock.text (sprintf "Total: $%.2f" (state.Cart |> List.sumBy (fun p -> p.Price)))
                        TextBlock.fontSize 16.0
                        TextBlock.margin 5.0
                    ]
                ]
            ]
        ]
    ]
