# GrpcServiceDemo
a simple CRUD application using Protocol Buffers (gRPC) and C# .NET Core

This implementation provides using In-memory storage for simplicity

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

=====================================

The logging will now capture:
- All operations (Create, Read, Update, Delete)
- Input validation failures
- Service errors
- Client errors
-Connection issues
- Unexpected exceptions

  
The logs will be written to:
- Console (for immediate feedback)
- Daily rolling log files:
- Service logs: logs/service-YYYYMMDD.log
- Client logs: logs/client-YYYYMMDD.log
