# csharp_ASPNET_WEBAPI
ASP.NET webapi框架，开发的CRUD API


# c# 编译发布命令

参数 /p:DebugType=None /p:DebugSymbols=false 设置不产生debug文件

+ dotnet publish /p:DebugType=None /p:DebugSymbols=false --self-contained true --configuration Release --runtime win-x64 --output myapp

+ dotnet publish /p:DebugType=None /p:DebugSymbols=false --self-contained  true-c Release -r linux-x64 --output myapp


资料
https://www.cnblogs.com/chillsrc/articles/16818386.html


```
c# sqlsugar
https://www.donet5.com/home/Doc?typeId=1189

<PublishAot>true</PublishAot>


winform的aot编译
https://github.com/kant2002/WinFormsComInterop

c# 防反编译
https://blog.csdn.net/daremeself/article/details/127948674
```


