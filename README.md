# employee-test

Asp.net Core API for performing CRUD on an Employee List in DB

# Introduction

- Employee App on .NET core 3.1
- Perform CRUD for Employee repository
- Provide employee specific information

# Getting Started

The solution has been created using visual studio 2019 (Version > 16.2) Respective Project targets .NET core 3.1

1. Install Visual Studio 2019
2. Download and install .NET core 3.1 SDK
3. Clone or download the project
4. If needed restore all the the Nuget packages
5. The application uses Mongo DB for database operations update your Mongo DB spec details in the appsettings.json file
6. The application uses Auth0 for authentication, update Domain and ApiIdentifier in the appsettings.json file

To get the Domain and API identifier details please follow the steps mentioned here :
https://auth0.com/docs/quickstart/backend/aspnet-core-webapi

# Build and Test

1. Solutions and projects are built inside Visual Studio
2. To try out functionality : set the EmployeeApp as start-up project and run the application
3. Since Swagger UI is used a default browser will pop-up in your browser with API specification
4. Please use the API specifications to test the Employee App APIs
5. Post and Delete operations are authorized using Auth0
6. To access the API key for the Post and Delete Operations please find the steps below

   - send a post request to the Auth0 based on your domain name
   - POST https://my-domain.auth0.com/oauth/token
   - Request Headers:
     Content-Type: "application/x-www-form-urlencoded"
     Accept: "_/_"
     ...
   - Request Body:
     grant_type: "client_credentials"
     scope: "api_audience:https://my-api.example.com"
     client_id: "**\***"
     client_secret: "**\***"

7. Once API key is obtained add it in the header in the swagger UI
8. Once authorization is successfull Add and Remove Employee will also be possible
