![version](https://img.shields.io/badge/version-1.0.0-blue)
![GitHub repo size](https://img.shields.io/github/repo-size/ngdechev/smartbot?color=green)
![Bitbucket open issues](https://img.shields.io/bitbucket/issues/ngdechev/library)

# Multi-User Shopping Cart Application
## Description
Implement a multi-user shopping cart application that includes both server and client-side components.

## Requirements
- The application must have both server and client-side components.
- Users can register and log in with different roles: Administrator and Customer.
- Administrators can manage the product list, including adding new products, changing quantities, editing, and deleting products.
- Customers can browse products, add them to their shopping cart, and specify quantities to buy.
- The product list must be saved and loaded from the server.
- Cart data must be stored on the server to support multi-user functionality.
- Users can perform the following actions using commands on the client-side:
  - addProduct
  - removeProduct
  - editProduct
  - listProducts
  - searchProducts
  - addCartItem
  - removeCartItem
  - updateCartItem
  - listCartItems
  - checkout
  - help
  - exit
  - login (optional; user role selection may be implemented at application startup)
- User roles must be enforced to control access to various functionalities.

## List of Entity Classes Fields
**Product Fields**
- Id
- Article Name
- Description
- Price
- Available Quantity

**Cart Item Fields**
- Id
- Product Id
- Quantity

## Project Development
The following programming languages and tools were used for the development of the project:

**Programming Languages**
1. C#

**Development Tools**
1. Visual Studio

![C#](https://img.shields.io/badge/c%23-%23239120.svg?style=for-the-badge&logo=c-sharp&logoColor=white)
![Visual Studio](https://img.shields.io/badge/Visual%20Studio-5C2D91.svg?style=for-the-badge&logo=visual-studio&logoColor=white)
