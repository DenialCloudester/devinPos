# Point of Sale (POS) Application

A comprehensive Point of Sale application built with .NET 8.0 Razor Pages, featuring user authentication, product management, sales processing, and inventory tracking.

## Features

### 🔐 Authentication System
- User registration and login using ASP.NET Core Identity
- Secure authentication protecting all POS features
- User management with email-based accounts

### 📦 Product Management
- Add, edit, and view products
- Product categories and descriptions
- SKU tracking and inventory management
- Stock quantity monitoring
- Active/inactive product status

### 🛒 Point of Sale System
- Interactive product selection interface
- Real-time cart management with quantity controls
- Automatic tax calculation (8% configurable)
- Multiple payment methods (Cash, Credit Card, Debit Card)
- Live subtotal and total calculations

### 📊 Sales Tracking & Reporting
- Complete sales history with detailed analytics
- Sales dashboard with key metrics:
  - Total transactions
  - Total revenue
  - Today's sales
  - Average sale amount
- Detailed transaction receipts
- Cashier tracking for each sale

### 💾 Data Management
- In-memory database for easy setup and testing
- Automatic inventory updates after sales
- Data seeding with sample products
- Entity Framework Core integration

## Technology Stack

- **.NET 8.0** - Core framework
- **ASP.NET Core Razor Pages** - Web framework
- **Entity Framework Core** - Data access
- **ASP.NET Core Identity** - Authentication
- **Bootstrap 5** - UI framework
- **FontAwesome** - Icons
- **In-Memory Database** - Data storage

## Prerequisites

- .NET 8.0 SDK or later
- Any modern web browser

## Setup Instructions

1. **Clone or download the repository**
   ```bash
   git clone <repository-url>
   cd pos-application
   ```

2. **Navigate to the application directory**
   ```bash
   cd POSApp
   ```

3. **Restore NuGet packages**
   ```bash
   dotnet restore
   ```

4. **Build the application**
   ```bash
   dotnet build
   ```

5. **Run the application**
   ```bash
   dotnet run
   ```

6. **Access the application**
   - Open your web browser
   - Navigate to `http://localhost:5263` (or the URL shown in the console)

## First Time Setup

1. **Register a new user account**
   - Click "Register" in the top navigation
   - Enter your email and password
   - Click "Register" to create your account

2. **Access POS features**
   - Once logged in, you'll see the main navigation with:
     - **Point of Sale** - Process sales transactions
     - **Products** - Manage your product inventory
     - **Sales** - View sales history and reports

3. **Sample data**
   - The application comes pre-loaded with sample products:
     - Coffee ($4.99)
     - Sandwich ($8.99)
     - Soda ($2.49)

## Usage Guide

### Processing a Sale
1. Navigate to **Point of Sale**
2. Click on products to add them to the cart
3. Adjust quantities using +/- buttons if needed
4. Select payment method (Cash, Credit Card, or Debit Card)
5. Click **Process Payment** to complete the transaction
6. The cart will clear and inventory will be automatically updated

### Managing Products
1. Navigate to **Products**
2. View all products with their details and stock levels
3. Click **Add New Product** to create new products
4. Click **Edit** next to any product to modify its details
5. Products can be marked as active/inactive

### Viewing Sales Reports
1. Navigate to **Sales**
2. View sales analytics dashboard with key metrics
3. Browse complete transaction history
4. Click **Details** on any sale to view the full receipt

## Configuration

### Tax Rate
The tax rate is currently set to 8% and can be modified in the POS page code-behind file (`Pages/POS.cshtml.cs`).

### Database
The application uses an in-memory database that resets when the application restarts. For production use, you would want to configure a persistent database like SQL Server or PostgreSQL.

### Payment Methods
Currently supports Cash, Credit Card, and Debit Card. Additional payment methods can be added by modifying the payment dropdown in the POS interface.

## Project Structure

```
POSApp/
├── Data/
│   └── ApplicationDbContext.cs     # Database context
├── Models/
│   ├── ApplicationUser.cs          # User model
│   ├── Product.cs                  # Product model
│   ├── Sale.cs                     # Sale model
│   └── SaleItem.cs                 # Sale item model
├── Pages/
│   ├── POS.cshtml                  # Point of Sale interface
│   ├── Products/                   # Product management pages
│   ├── Sales/                      # Sales history pages
│   └── Shared/                     # Shared layouts and partials
├── wwwroot/                        # Static files (CSS, JS, images)
└── Program.cs                      # Application configuration
```

## Security Features

- All POS features require authentication
- User sessions are managed securely
- Input validation on all forms
- CSRF protection enabled
- Secure password requirements

## Development Notes

- The application uses Entity Framework Code First approach
- All monetary calculations use decimal type for precision
- Responsive design works on desktop and tablet devices
- Professional UI with consistent styling throughout

## Support

For issues or questions about this POS application, please refer to the code comments and documentation within the source files.
