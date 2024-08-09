using Test;

string connectionString = "Server=HOME-PC;Database=Shop;Trusted_Connection=True;";
DataManager dataManager = new DataManager(connectionString);
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
            string xmlPath = Console.ReadLine().ToString();
            await dataManager.LoadData(xmlPath);
            break;
        case "2":
            await dataManager.GetSales();
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



