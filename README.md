# csharp_ASPNET_WEBAPI
ASP.NET webapi框架，开发的CRUD API


# c# 编译发布命令

参数 /p:DebugType=None /p:DebugSymbols=false 设置不产生debug文件

dotnet publish /p:DebugType=None /p:DebugSymbols=false --self-contained --configuration Release --runtime win7-x64 --output myapp

