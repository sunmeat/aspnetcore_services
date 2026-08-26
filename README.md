# Сервіси в ASP.Net Core

![.NET](https://img.shields.io/badge/.NET-10.0-512BD4?logo=dotnet&logoColor=white)
![ASP.NET Core](https://img.shields.io/badge/ASP.NET%20Core-MVC-blue?logo=dotnet)
![License](https://img.shields.io/badge/license-MIT-green)

Навчальний проєкт на **ASP.NET Core MVC**, що демонструє роботу вбудованого контейнера **Dependency Injection (DI)**: різні способи реєстрації сервісів, три їхні часи життя (*lifetime*) та кілька способів отримати сервіс усередині контролера.

## 📋 Зміст

- [Про проєкт](#-про-проєкт)
- [Структура проєкту](#-структура-проєкту)
- [Часи життя сервісів](#-часи-життя-сервісів)
- [Способи впровадження залежностей](#-способи-впровадження-залежностей)
- [Швидкий старт](#-швидкий-старт)
- [Технології](#-технології)
- [Ліцензія](#-ліцензія)

## 📖 Про проєкт

Проєкт створено як практичний приклад для вивчення механізму DI в ASP.NET Core. У ньому наочно показано:

- реєстрацію сервісу через інтерфейс (`IMyService` → `FirstService`);
- реєстрацію конкретного класу без інтерфейсу (`SecondService`);
- «красивий» варіант реєстрації через **extension-методи** над `IServiceCollection`;
- різницю між `Transient`, `Scoped` і `Singleton` — з коментарями просто в коді;
- три способи отримати сервіс у контролері: через конструктор, через параметр методу дії (`[FromServices]`) та через `HttpContext.RequestServices`.

Результат роботи сервісів виводиться в консоль сервера — достатньо запустити застосунок і відкрити головну сторінку.

## 🗂 Структура проєкту

```
aspnetcore_services/
├── Controllers/
│   └── HomeController.cs              # демонструє 3 способи отримання сервісу
├── Services/
│   ├── FirstService.cs                # реалізація IMyService з унікальним Guid
│   ├── SecondService.cs               # сервіс без інтерфейсу
│   ├── Interfaces/
│   │   └── IMyService.cs              # контракт сервісу
│   └── Extensions/
│       └── ServiceProviderExtensions.cs  # extension-методи для реєстрації сервісів
├── Program.cs                         # реєстрація сервісів + пайплайн застосунку
├── appsettings.json
└── aspnetcore_services.csproj
```

## ⏱ Часи життя сервісів

У `Program.cs` наведено три варіанти реєстрації `IMyService`, кожен закоментований поясненням:

| Lifetime | Поведінка |
|---|---|
| **Transient** | Новий екземпляр `FirstService` створюється щоразу, коли DI-контейнер отримує запит на `IMyService` — навіть у межах одного HTTP-запиту |
| **Scoped** | Один екземпляр на весь HTTP-запит: усі запити на `IMyService` у межах одного запиту повертають той самий об'єкт |
| **Singleton** | Один екземпляр на весь час життя застосунку — перевірити можна кількома оновленнями сторінки: `Id` у консолі не змінюється |

За замовчуванням у проєкті активний варіант `Transient`; `Scoped` і `Singleton` залишені закоментованими для експериментів.

## 🔌 Способи впровадження залежностей

`HomeController` показує одразу три підходи до отримання одного й того ж сервісу:

```csharp
// 1. Через конструктор
public HomeController(IMyService diService) { ... }

// 2. Через параметр методу дії
public IActionResult Index([FromServices] IMyService paramService) { ... }

// 3. Через HttpContext.RequestServices
var reqService = HttpContext.RequestServices.GetRequiredService<IMyService>();
```

## 🚀 Швидкий старт

### Вимоги

- [.NET 10.0 SDK](https://dotnet.microsoft.com/download)

### Запуск

```bash
git clone https://github.com/sunmeat/aspnetcore_services.git
cd aspnetcore_services
dotnet run
```

Після запуску відкрийте застосунок у браузері та перегляньте консоль — там з'являться логи ініціалізації сервісів та їхні `Id`/повідомлення.

## 🛠 Технології

- ASP.NET Core MVC (.NET 10.0)
- Вбудований DI-контейнер `Microsoft.Extensions.DependencyInjection`

## 📄 Ліцензія

Проєкт розповсюджується під ліцензією **MIT** — деталі у файлі [LICENSE.txt](LICENSE.txt).
