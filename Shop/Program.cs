using Shop;
using Shop.Domain;
using Shop.Domain.Repositories.Abstract;
using Shop.Domain.Repositories.EntityFramework;
using Microsoft.Extensions.DependencyInjection;
using DataManager = Shop.Domain.DataManager;

var serviceProvider = new ServiceCollection()
            .AddTransient<IUsersRepository, EFUsersRepository>()
            .AddTransient<IProductsRepository, EFProductsRepository>()
            .AddTransient<IOrdersRepository, EFOrdersRepository>()
            .AddTransient<IOrdersProductsRepository, EFOrdersProductsRepository>()
            .AddTransient<ISalesRepository, EFSalesRepository>()
            .AddTransient<DataManager>()
            .AddTransient<ShopContext>()
            .BuildServiceProvider();
            



DataManager dataManager = serviceProvider.GetService<DataManager>();
DbAction action = new DbAction(dataManager);

Console.Write("Выберите действие:\n" +
                "1) Загрузить данные из XML\n" +
                "2) Отобразить список продаж\n" +
                "3) Закрыть программу\n\n");

bool state = true;
while (state)
{
    switch (Console.ReadLine())
    {
        case "1":
            Console.WriteLine("Введите путь до файла:");
            string path = Console.ReadLine();
            if (Path.Exists(path))
            {
                XmlLoad xmlLoad = new XmlLoad(path);
                var orders = xmlLoad.Load();
                action.ExecuteInsert(orders);
                action.ShowResultInsert();
            }
            else { Console.WriteLine("Файла не существует!"); }
            break;
        case "2":
            action.ShowSales();
            break;
        case "3":
            state = false;
            Environment.Exit(0);
            break;
        default:
            Console.WriteLine("Некорректный ввод!");
            break;
    }
}
Console.ReadLine();



