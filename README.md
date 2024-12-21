# E-Commerce API

## Overview
The **E-Commerce API** is a powerful and scalable backend solution designed to manage an e-commerce platform. Developed with **ASP.NET Core**, the API provides a range of essential features for e-commerce functionality, ensuring a seamless experience for both administrators and customers. It integrates several modern technologies to deliver performance, security, and maintainability.

---

## Features
- **User Management**: Registration, profile updates, and authentication.
- **Product Management**: CRUD operations for products.
- **Category Management**: CRUD operations for product categories.
- **Shopping Cart**: Add, update, and manage items in the cart.
- **Orders**: Place orders and retrieve order details.
- **Reviews**: Add and manage reviews for products.
- **LINQ Queries**: Leverage **LINQ** for efficient querying of data, ensuring optimized and readable code for retrieving and manipulating data.
- **Role-based Authorization**: Separate functionalities for Admin and regular users.
- **JWT Authentication**: Secure API endpoints.
- **Caching**: Improve performance with data caching for frequently accessed resources.
- **AutoMapper**: Simplified object-to-object mapping for better maintainability and reduced code duplication.

---

## Prerequisites

- [.NET 6 or later](https://dotnet.microsoft.com/download)
- [SQL Server](https://www.microsoft.com/en-us/sql-server)
- A tool for managing HTTP requests (e.g., [Postman](https://www.postman.com/))

---

## Getting Started

### 1. Clone the Repository
```bash
https://github.com/omartaha15/E-Commerce-API
```

### 2. Set Up the Database
1. Configure the connection string in `appsettings.json`:
    ```json
    "ConnectionStrings": {
        "DefaultConnection": "Server=YOUR_SERVER;Database=ECommerceDB;Trusted_Connection=True;"
    }
    ```
2. Run migrations to create the database:
    ```bash
    dotnet ef database update
    ```

### 3. Run the Application
```bash
dotnet run
```
---

## Endpoints

### Admin
| **Method** | **Endpoint**                                | **Description**                                   |
|------------|---------------------------------------------|---------------------------------------------------|
| GET        | `/api/Admin/dashboard/sales-summary`        | Retrieve sales summary data for the dashboard.   |
| GET        | `/api/Admin/users`                          | Retrieve a list of all users.                    |
| GET        | `/api/Admin/products/low-stock`             | Get products that are low in stock.              |
| GET        | `/api/Admin/orders/recent`                  | Fetch recently placed orders.                    |

### Auth
| **Method** | **Endpoint**                                | **Description**                                   |
|------------|---------------------------------------------|---------------------------------------------------|
| POST       | `/api/Auth/register`                        | Register a new user.                             |
| POST       | `/api/Auth/login`                           | Authenticate a user and issue a JWT token.       |
| POST       | `/api/Auth/reset-password`                  | Reset user password.                             |

### Category
| **Method** | **Endpoint**                                | **Description**                                   |
|------------|---------------------------------------------|---------------------------------------------------|
| GET        | `/api/Category/GetAll`                      | Retrieve all categories.                         |
| GET        | `/api/Category/{id}`                        | Get details of a specific category.              |
| PUT        | `/api/Category/{id}`                        | Update a category.                               |
| DELETE     | `/api/Category/{id}`                        | Delete a category.                               |
| POST       | `/api/Category`                             | Create a new category.                           |

### Orders
| **Method** | **Endpoint**                                | **Description**                                   |
|------------|---------------------------------------------|---------------------------------------------------|
| GET        | `/api/Orders/admin`                         | Retrieve all orders (Admin).                     |
| GET        | `/api/Orders`                               | Get orders for the current user.                 |
| POST       | `/api/Orders`                               | Place a new order.                               |
| GET        | `/api/Orders/{id}`                          | Retrieve details of a specific order.            |
| PUT        | `/api/Orders/{id}`                          | Update an order.                                 |
| DELETE     | `/api/Orders/{id}`                          | Delete an order.                                 |
| PUT        | `/api/Orders/{id}/status`                   | Update the status of an order.                   |

### Products
| **Method** | **Endpoint**                                | **Description**                                   |
|------------|---------------------------------------------|---------------------------------------------------|
| GET        | `/api/Products/getAll`                      | Retrieve all products.                           |
| GET        | `/api/Products`                             | Retrieve paginated list of products.             |
| GET        | `/api/Products/{id}`                        | Get details of a specific product.               |
| PUT        | `/api/Products/{id}`                        | Update a product.                                |
| DELETE     | `/api/Products/{id}`                        | Delete a product.                                |
| POST       | `/api/Products/Create`                      | Create a new product.                            |
| GET        | `/api/Products/search`                      | Search for products by criteria.                 |

### Review
| **Method** | **Endpoint**                                | **Description**                                   |
|------------|---------------------------------------------|---------------------------------------------------|
| GET        | `/api/Review`                               | Retrieve reviews.                                |
| POST       | `/api/Review`                               | Add a review for a product.                      |
| PUT        | `/api/Review/{reviewId}`                    | Update a review.                                 |
| DELETE     | `/api/Review/{reviewId}`                    | Delete a review.                                 |

### ShoppingCart
| **Method** | **Endpoint**                                | **Description**                                   |
|------------|---------------------------------------------|---------------------------------------------------|
| GET        | `/api/ShoppingCart`                         | Retrieve shopping cart items.                    |
| POST       | `/api/ShoppingCart/add`                     | Add an item to the shopping cart.                |
| PUT        | `/api/ShoppingCart/update`                  | Update an item in the shopping cart.             |
| DELETE     | `/api/ShoppingCart/remove/{productId}`      | Remove a specific item from the shopping cart.   |
| POST       | `/api/ShoppingCart/clear`                   | Clear the shopping cart.                         |

### UserProfile
| **Method** | **Endpoint**                                | **Description**                                   |
|------------|---------------------------------------------|---------------------------------------------------|
| GET        | `/api/UserProfile/profile`                  | Retrieve user profile information.               |
| PUT        | `/api/UserProfile/profile`                  | Update user profile information.                 |

### Wishlist
| **Method** | **Endpoint**                                | **Description**                                   |
|------------|---------------------------------------------|---------------------------------------------------|
| GET        | `/api/Wishlist`                             | Retrieve wishlist items.                         |
| POST       | `/api/Wishlist/add/{productId}`             | Add a product to the wishlist.                   |
| DELETE     | `/api/Wishlist/remove/{productId}`          | Remove a product from the wishlist.              |

---

## Configuration

### AutoMapper
AutoMapper is used for object mapping between models and DTOs. Mappings are defined in `MappingProfile.cs`.

### Authentication
JWT Authentication is implemented to secure the API. Tokens are issued upon login and must be included in the `Authorization` header for protected endpoints:
```
Authorization: Bearer <your-token>
```

---

## Project Structure

```
E_Commerce_API
├── Controllers        # API controllers
├── Data               # Db Context 
├── DTOs               # Data transfer objects
├── Interfaces         # Service interfaces
├── Model              # Entity models
├── Services           # Business logic
├── Mappings           # AutoMapper profiles
├── Program.cs         # Application entry point
└── appsettings.json   # Configuration file
```

---

## Feel free to contact me

- [linkedin](https://www.linkedin.com/in/omar157/)
- [ X ](https://x.com/omar_taha17)  

---

## License
This project is licensed under the MIT License. See the LICENSE file for more details.

