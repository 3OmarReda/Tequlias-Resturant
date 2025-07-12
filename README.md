# Tequlias Resturant 🍽️

مشروع ويب باستخدام ASP.NET Core MVC لتصميم وإدارة موقع لمطعم.

## 📌 محتوى المشروع

- 🔒 تسجيل الدخول وتسجيل المستخدمين باستخدام Identity
- 🍔 إدارة المنيو (وجبات، مكونات، تصنيفات)
- 👤 نظام Roles (مدير / مستخدم عادي)
- 🛒 إمكانية إنشاء طلبات Orders
- 📦 ربط بقاعدة بيانات Entity Framework Core

## 🛠️ التقنيات المستخدمة

- ASP.NET Core MVC (.NET 8)
- Entity Framework Core
- SQL Server
- Bootstrap 5
- Identity (Authentication & Authorization)

## 📁 هيكل المشروع

TequliasResturant/
│

├── Controllers/

├── Models/

├── Views/

├── Areas/Identity/

├── wwwroot/

├── Data/

├── appsettings.json

├── Program.cs

└── TequliasResturant.csproj



## 🚀 طريقة التشغيل

1. افتح المشروع باستخدام Visual Studio
2. اضبط اتصال قاعدة البيانات داخل `appsettings.json`
3. نفّذ `Update-Database` من Package Manager Console
4. شغّل التطبيق واستمتع ✨

## 🧠 الهدف من المشروع

- التدريب العملي على MVC و EF Core
- تعلم بناء Web App حقيقي من الصفر
- تجربة دمج Authentication و Authorization

---

> 💡 تم تطوير المشروع لأغراض تعليمية وتحسين المهارات البرمجية

