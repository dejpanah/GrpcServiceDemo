# GrpcServiceDemo
a simple CRUD application using Protocol Buffers (gRPC) and C# .NET Core

This implementation provides:

  - In-memory storage for simplicity
  - Proper error handling with gRPC status codes
  - Full CRUD operations
  - gRPC service configuration

Example client usage

To test:
- Run the server application
- Run the client application (or use a gRPC GUI client)
- Verify operations through the console output or client tool responses


Required Packages for Client if you want to create app for Testing like GrpcService Project:
- Install-Package Grpc.Tools
- Install-Package Grpc.Net.Client
- Install-Package Google.Protobuf

install Proto buffer compiler in windows :
winget install protobuf

