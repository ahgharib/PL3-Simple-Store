namespace SimpleStoreSimulator

open Avalonia
open Avalonia.Controls
open Avalonia.Markup.Xaml
open System.Collections.ObjectModel

// Define the product type
type Product = { Name: string; Price: decimal; Description: string }

type MainWindow() as this =
    inherit Window()

    // Create observable collections for products and cart
    let products = 
        ObservableCollection(
        [ { Name = "💻 Laptop"; Price = 5000M; Description = "A high-performance laptop with 16GB RAM and 512GB SSD." }
          { Name = "📱 Phone"; Price = 3000M; Description = "A smartphone with a 6.5-inch display and 128GB storage." }
          { Name = "🎧 Headphones"; Price = 1500M; Description = "Wireless noise-cancelling headphones with high-quality sound." }
          { Name = "⌚ Smartwatch"; Price = 2000M; Description = "A smartwatch with fitness tracking and heart rate monitor." }
          { Name = "🖥️ Desktop PC"; Price = 7000M; Description = "A powerful desktop PC with a fast processor and large storage." }
          { Name = "📷 Camera"; Price = 4500M; Description = "A DSLR camera with 24MP resolution and 18-55mm lens." } ]
        )
    
    let cart = ObservableCollection<Product>()


    // Load the XAML before accessing controls
    do
        // **Important: Load the XAML first**
        AvaloniaXamlLoader.Load(this)

        // **Now get the controls after XAML is loaded**
        let cartListBox : ListBox = this.FindControl("CartListBox")
        let productsListBox : ListBox = this.FindControl("ProductsListBox")
        let addButton : Button = this.FindControl("AddToCartButton")
        let removeButton : Button = this.FindControl("RemoveItem")
        // let checkoutButton : Button = this.FindControl("CheckoutButton")
        let totalPriceLabel : TextBlock = this.FindControl("TotalPriceLabel")
        let cartEmptyMessage : TextBlock = this.FindControl("CartEmptyMessage")

        // Bind the products to the ListBox using ItemsSource
        productsListBox.ItemsSource <- products
        cartListBox.ItemsSource <- cart