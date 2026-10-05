# 仓库物料管理系统V2.1

### 一、项目简介

目前分支是最新版本<br>

> 管理员账号：002  管理员密码：123  <br>
> 普通操作员账号：001    密码：123

* 开发语言：C# <br>
* 框架：Winform  <br>
* 架构：三层架构（UI/BLL/DAL）
* 数据库：SQL Server 2026<br>
* 数据库工具：SQL Server Management Studio 2022<br>
* 开发软件：Visual Studio <br>

### 二、项目详细功能介绍


<img width="4532" height="556" alt="PlantUML" src="https://github.com/user-attachments/assets/d839ce81-fa0d-48ff-8f29-ee7415adfdaf" />



#### 1、登录注册界面

点击启动即可来到登录界面，正确输入账号密码，点击登录按钮即可登录。


<img width="598" height="445" alt="image" src="https://github.com/user-attachments/assets/092eb988-a769-47a1-9ed5-50bfa8cc1633" />


点击注册按钮即可来到注册页面，如果要注册成为管理员，则需要输入”管理员密钥“，否则将注册成普通操作员的身份。<br>
管理员密钥为：”miyao“;

<img width="404" height="458" alt="image" src="https://github.com/user-attachments/assets/e89bac99-7b13-4deb-b0f3-bd56d6b577de" />


#### 2、主页

登录成功即可来到主页面，主页面包括物料管理（需要管理员身份）、物料查询、出库入库、操作日志（暂未更新）、关于（暂未更新）、退出 这几个按钮以及下方的用户信息。

<img width="1283" height="676" alt="image" src="https://github.com/user-attachments/assets/7bbb4189-339b-4b1d-b041-f8ba2cd8082e" />



### 3、物料管理（管理员操作）

点击添加物料按钮可以来到添加物料的界面：

<img width="794" height="480" alt="image" src="https://github.com/user-attachments/assets/63abcc14-3024-4ae5-9d4d-a774170f0bf2" />


输入物料信息点击添加即可添加成功。

<img width="795" height="483" alt="image" src="https://github.com/user-attachments/assets/e0091562-e35e-400e-8f11-84d6903f884f" />




点击删除物料按钮可以来到删除物料的界面，上方是所有物料的信息表，点击其中一个格子就能选中整行并且将整行的信息放入右下角的信息栏中：

<img width="1034" height="661" alt="image" src="https://github.com/user-attachments/assets/03032b3b-d8d5-45cd-af1c-1dfa3015560e" />


当然也可以输入物料名称来进行模糊查找，查找结果将更新在上方表格中：

<img width="1029" height="662" alt="image" src="https://github.com/user-attachments/assets/fe12aa14-5f20-4c90-9904-e5c0b0f29727" />


表格中点击需要删除的行，行信息显示到右下角，此时点击删除按钮即可完成删除操作：

<img width="1040" height="662" alt="image" src="https://github.com/user-attachments/assets/35b204cf-2f89-4fec-a1c3-cd8d2dce70ab" />


接下来是修改物料，主页点击修改物料按钮即可来到修改物料的界面：

<img width="1025" height="659" alt="image" src="https://github.com/user-attachments/assets/42e5c311-be7c-4068-9918-2904d754859a" />


上方表格中选好行，在下方修改栏中输入修改信息，点击修改即可修改成功：

<img width="1033" height="662" alt="image" src="https://github.com/user-attachments/assets/e24c07ec-a3e0-4b06-a198-d2453d47fe12" />


#### 3、物料查询

该功能用于操作员进行物料查询，主页点击物料查询按钮即可打开查询界面：


<img width="1042" height="664" alt="image" src="https://github.com/user-attachments/assets/24b12d9e-cf7c-4eaa-bbc3-320973d6cd59" />



该界面支持物料名称查找和物料编号查找：

<img width="1040" height="669" alt="image" src="https://github.com/user-attachments/assets/25759774-4b1e-455e-a04e-fd24eb2a2869" />



#### 4、出库入库

主页点击出库/入库按钮即可打开出库入库界面：

<img width="1045" height="659" alt="image" src="https://github.com/user-attachments/assets/ba9b0f05-545c-4f84-a606-4b738b292790" />


在出库入库的输入栏里输入数量信息，点击出库or入库即可完成操作：


<img width="1037" height="658" alt="image" src="https://github.com/user-attachments/assets/51132480-a930-437b-9f1e-cc40fbf0e82c" />

<img width="1033" height="659" alt="image" src="https://github.com/user-attachments/assets/9b113169-dc90-4dc1-bef9-ab305440d899" />


#### 5、操作日志

主界面点击“操作日志”按钮即可到日志界面，日志反映了某某用户在某某时间操作了某某物料，操作类型是出库or入库的信息。

<img width="724" height="666" alt="image" src="https://github.com/user-attachments/assets/4149707b-8953-4ea6-9a9f-29dcc263f432" />



在该界面中支持查询日志，用户可以按照用户名查询或者按照物料名查询，输入信息后点击查询即可。

<img width="728" height="663" alt="image" src="https://github.com/user-attachments/assets/2e001537-7cc4-4b26-8bf8-55474b02bf09" />

<img width="721" height="663" alt="image" src="https://github.com/user-attachments/assets/8781614f-0103-4861-a63f-605085ca5e32" />


如果日志过多占用过大，管理员可以定期对过时的操作日志进行清理，点击右下角的清理即可完成。


<img width="727" height="657" alt="image" src="https://github.com/user-attachments/assets/0c20c09a-b279-452e-ac94-a926b2119b92" />




### 三、数据库导入步骤

1、SQL Server Manage Studio 中导入项目中的数据库脚本文件（.sql文件） <br>



2、打开Visual Studio，在DAL层中找到DBHelper.cs文件并打开，更改数据库的连接字符串即可：<br>


<img width="1915" height="833" alt="image" src="https://github.com/user-attachments/assets/4f1f1345-d903-4406-9c97-5d8261ccf1ed" />


### 四、日志存放路径

D:\Logs
