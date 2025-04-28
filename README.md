# **JWT Authentication System**

A simple and secure **JWT (JSON Web Token)** based authentication system built with **ASP.NET Core**.

## **📋 Project Overview**
This project implements a token-based authentication system using **JWT**.  
Users can **register**, **login**, and **access protected endpoints** securely with their issued tokens.

## **🚀 Features**
- **User Registration** with password hashing
- **User Login** with JWT token generation
- **Token-Based Authentication** for API security
- **Role-Based Authorization** (optional if added)
- **Secure Token Validation**
- **Clean and Extendable Codebase**

## **🛠️ Technologies Used**
- **ASP.NET Core**
- **C#**
- **Entity Framework Core**
- **SQL Server**
- **JWT Bearer Authentication**
- **Swagger** for API testing (if you added it)


## **📚 How It Works**
1. **User Registration**  
   Users submit their details and get stored securely in the database.
2. **User Login**  
   Users log in and receive a **JWT Token**.
3. **Authentication**  
   Every protected API checks for the **Bearer Token** in the `Authorization` header.
4. **Authorization**  
   Only users with valid tokens can access protected resources.
