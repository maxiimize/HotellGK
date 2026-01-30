# HotellGK - Hotel Management System

A console-based hotel management system built with C# and Entity Framework Core, featuring an interactive menu interface for managing rooms, guests, and bookings.

## 🌟 Features

### Room Management
- Add, update, view, and delete rooms
- Support for Single and Double room types
- Room size options (Normal, Large)
- Extra bed configuration for Double rooms
- Track room availability

### Guest Management
- Add and update guest information
- Store guest contact details (email, phone number)
- View all registered guests

### Booking Management
- Create bookings with check-in/check-out dates
- Link bookings to rooms and guests
- View all bookings with detailed information

### Interactive Console UI
- Navigation using arrow keys
- Clean, menu-driven interface powered by Spectre.Console
- User-friendly data entry forms

## 🛠️ Technology Stack

- **.NET 9.0** - Latest .NET framework
- **Entity Framework Core 9.0** - ORM for database operations
- **SQL Server** - Database backend
- **Spectre.Console** - Enhanced console UI library

## 📊 Database Schema

### Room Table

| Column | Type | Description |
|--------|------|-------------|
| `RoomId` | int (PK) | Primary key |
| `RoomType` | string | Single or Double |
| `RoomSize` | string | Normal or Large |
| `HasExtraBeds` | bool | Indicates if room has extra beds |
| `MaxExtraBeds` | int | Maximum extra beds (0-2) |
| `IsAvailable` | bool | Room availability status |

### Guest Table

| Column | Type | Description |
|--------|------|-------------|
| `GuestId` | int (PK) | Primary key |
| `Name` | string | Guest full name |
| `Email` | string (nullable) | Guest email address |
| `PhoneNumber` | string (nullable) | Guest phone number |

### Booking Table

| Column | Type | Description |
|--------|------|-------------|
| `BookingId` | int (PK) | Primary key |
| `RoomId` | int (FK) | Foreign key to Room |
| `GuestId` | int (FK) | Foreign key to Guest |
| `CheckInDate` | DateTime | Booking check-in date |
| `CheckOutDate` | DateTime | Booking check-out date |

## 📋 Prerequisites

- .NET 9.0 SDK or later
- SQL Server (LocalDB or full instance)
- Visual Studio 2022 or Visual Studio Code (optional)

## 🚀 Installation

### Step 1: Clone the Repository

```bash
git clone https://github.com/maxiimize/HotellGK.git
cd HotellGK
```

### Step 2: Configure Database Connection

Update the connection string in `appsettings.json`:

```json
{
  "ConnectionStrings": {
    "DefaultConnection": "Server=localhost;Database=HotellDb;Trusted_Connection=True;TrustServerCertificate=True;"
  }
}
```

### Step 3: Restore Dependencies

```bash
dotnet restore
```

### Step 4: Apply Migrations

The application will automatically create the database on first run. Alternatively, you can manually apply migrations:

```bash
dotnet ef database update
```

### Step 5: Run the Application

```bash
dotnet run --project HotellGK
```

## 📖 Usage

Upon launching the application, you'll be presented with a main menu:

### Menu Options

| Option | Description |
|--------|-------------|
| Add Room | Register a new room with specific configurations |
| Update Room | Modify existing room details |
| View Rooms | Display all rooms in the system |
| Delete Room | Remove a room from the database |
| Add Customer | Register a new guest |
| Update Customer | Modify guest information |
| View Customers | Display all registered guests |
| Add Booking | Create a new reservation |
| View Bookings | Display all bookings with room and guest details |
| Exit | Close the application |

### Navigation Controls

- **↑/↓ Arrow Keys** - Move through menu options
- **Enter** - Select an option

## 💾 Sample SQL Queries

### View All Rooms

```sql
SELECT * FROM Rooms;
```

### View Available Rooms Only

```sql
SELECT * FROM Rooms 
WHERE IsAvailable = 1;
```

### View Rooms Ordered by ID

```sql
SELECT * FROM Rooms 
ORDER BY RoomId ASC;
```

### View Bookings Ordered by Check-In Date

```sql
SELECT * FROM Bookings 
ORDER BY CheckInDate ASC;
```

### View Bookings with Guest and Room Details

```sql
SELECT b.BookingId, b.CheckInDate, b.CheckOutDate, r.RoomType, g.Name AS GuestName
FROM Bookings b
JOIN Rooms r ON b.RoomId = r.RoomId
JOIN Guests g ON b.GuestId = g.GuestId;
```

### View Guests Ordered by Name

```sql
SELECT * FROM Guests 
ORDER BY [Name] DESC;
```

### View Double Rooms Only

```sql
SELECT * FROM Rooms 
WHERE RoomType = 'Double';
```

## 📁 Project Structure

```
HotellGK/
├── Controllers/          # Handle user interactions
│   ├── BookingController.cs
│   ├── GuestController.cs
│   └── RoomController.cs
├── Services/            # Business logic layer
│   ├── BookingService.cs
│   ├── GuestService.cs
│   ├── RoomService.cs
│   └── IService.cs
├── Models/              # Data models
│   ├── Booking.cs
│   ├── Guest.cs
│   └── Room.cs
├── MenuClasses/         # UI components
│   └── Menu.cs
├── Migrations/          # EF Core migrations
│   ├── 20241230200442_InitialCreate.cs
│   ├── 20250101224602_UpdateRoomSchema.cs
│   ├── 20250102185520_AddMaxExtraBedsToRoom.cs
│   └── 20250102201100_AddRoomSizeToRooms.cs
├── HotellDbContext.cs   # Database context
├── Program.cs           # Application entry point
└── appsettings.json     # Configuration
```

## 🏗️ Architecture

The application follows a layered architecture pattern:

### Layers

- **Controllers** - Handle user input and coordinate between UI and services
- **Services** - Implement business logic and data access operations
- **Models** - Define database entities and domain objects
- **MenuClasses** - Provide reusable UI components for console interaction

### Design Patterns

- **Repository Pattern** - Services act as repositories for data access
- **Dependency Injection** - Controllers receive services through constructor injection
- **MVC Pattern** - Separation of concerns between models, views (console UI), and controllers

## 🔧 Development

### Adding New Migrations

When you modify the model classes, create a new migration:

```bash
dotnet ef migrations add YourMigrationName
dotnet ef database update
```

### Removing the Last Migration

If you need to remove the most recent migration:

```bash
dotnet ef migrations remove
```

### Seeded Data

The application includes sample data that is automatically seeded when the database is created:

#### Rooms
- Room 1: Single, Normal, No extra beds
- Room 2: Double, Normal, Max 1 extra bed
- Room 3: Double, Large, Max 2 extra beds
- Room 4: Single, Normal, No extra beds

#### Guests
- John Doe (john@example.com)
- Jane Smith (jane@example.com)
- Mike Brown (mike@example.com)
- Lisa White (lisa@example.com)

## 📦 NuGet Packages

The project uses the following NuGet packages:

```xml
<PackageReference Include="Microsoft.EntityFrameworkCore.SqlServer" Version="9.0.0" />
<PackageReference Include="Microsoft.EntityFrameworkCore.Tools" Version="9.0.0" />
<PackageReference Include="Microsoft.Extensions.Configuration.Json" Version="9.0.0" />
<PackageReference Include="Spectre.Console" Version="0.49.1" />
<PackageReference Include="Spectre.Console.Cli" Version="0.49.1" />
```

## 📄 License

This project is available for educational and personal use.

## 👤 Author

Created by **maxiimize**

---

**HotellGK** - *Modern Hotel Management Made Simple*
