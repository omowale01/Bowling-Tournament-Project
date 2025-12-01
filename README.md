# Bowling-Tournament-Project
Bowling Tournament Management System
Project Overview
A complete web application for managing bowling tournaments with team registration, player management, and administrative controls. Built with ASP.NET Core MVC and Entity Framework Core.

Team Members
Team Member 1: Wouakam Tientcheu Donald

Team Member 2: Omowale Olamide Obagunwa

Team Member 3: Lekeaka Njinkeng

Main Features
Public Features
View all paid teams

Register new teams with 4 players

View team details

Admin Features (Login Required)
Full CRUD for teams and players

Mark teams as paid with automatic date tracking

Filter teams by division or payment status

View registration summaries and financial reports

Technical Implementation
Key Requirements Met
$200 registration fee per team

Only paid teams appear publicly

Exactly 4 players per team (enforced)

Unique team names

Comprehensive validation with clear error messages

Technology Stack
ASP.NET Core MVC

Entity Framework Core with SQL Server

Bootstrap 5 for UI

Cookie-based authentication with role-based authorization

Database Models
Team (with payment tracking)

Player (individual team members)

Division (tournament categories)

BowlingUser (admin authentication)

Setup Instructions
Run the provided SQL script to create the database

Update connection string in appsettings.json

Build and run the application

Login with admin credentials (provided separately)

Project Status
Complete and ready for production use. All requirements implemented with proper validation, security, and error handling.
