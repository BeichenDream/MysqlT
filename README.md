# MysqlT
伪造Myslq服务端,并利用Mysql逻辑漏洞来获取客户端的任意文件反击攻击者

当你需要将本地文件上传到服务器时,没有合适的上传器时,也可以使用本软件,小巧玲珑

该程序利用了Mysql客户端LoadData的逻辑漏洞

与其它软件不同,本软件支持大文件无损传输

支持用户验证

支持自定义Mysql版本

随机的Salt加密,加上用户验证,让攻击者毫无察觉

Net文件夹是.NET程序只能运行在Windows

NetCore文件夹是.NET CORE程序可以运行在 Windows,Linux,MAC

使用教程:https://www.bilibili.com/video/av53068648/

首次使用请在程序中输入Mysql Help,来查看帮助命令


你可以在程序根目录中的User.data文件,批量添加用户,但用户名不可以重复

你可以在程序根目录中的File.data文件,批量添加读取路径,但路径不可以重复

Logincheck:与MysqlN不同的是MysqlT设置为false,会向客户端发生登录成功

退出程序时,建议使用 App Exit命令,这样会保存当前的配置

本程序命令行的命令不区分大小写,你可以Mysql Help,也可以mysql help 或MYSQL HELP

### 安装教程
.NET CORE windows以及linux与mac  SDK下载地址:https://dotnet.microsoft.com/download

 Linux 64位一键安装SDK:  curl -sSL -o dotnet.tar.gz https://download.visualstudio.microsoft.com/download/pr/ece856bb-de15-4df3-9677-67cc817ffc1b/521da52132d23deae5400b8e19e23691/dotnet-sdk-2.2.204-linux-x64.tar.gz&&sudo mkdir -p /opt/dotnet && sudo tar zxf dotnet.tar.gz -C /opt/dotnet&&sudo ln -s /opt/dotnet/dotnet /usr/local/bin

更改代码以及发布代码,请注明版权

如有BUG或者建议可以将BUG或者建议请发至我的邮箱:rroort@qq.com
