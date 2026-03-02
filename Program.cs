using ComputerConfigurator;

namespace ComputerConfigurator;

class Program
{
    static void Main(string[] args)
    {
        // Настройка консоли для поддержки UTF-8
        Console.OutputEncoding = System.Text.Encoding.UTF8;

        Console.WriteLine("=========================================================");
        Console.WriteLine("ЛАБОРАТОРНО-ПРАКТИЧЕСКАЯ РАБОТА №4");
        Console.WriteLine("ПОРОЖДАЮЩИЕ ПАТТЕРНЫ ПРОЕКТИРОВАНИЯ");
        Console.WriteLine("Синглтон, прототип и строитель");
        Console.WriteLine("=========================================================");
        Console.WriteLine();

        // ЭТАП 1: Демонстрация паттерна Строитель
        DemonstrateBuilder();

        // ЭТАП 2: Демонстрация паттерна Прототип
        DemonstratePrototype();

        // ЭТАП 3: Демонстрация паттерна Singleton (Реестр прототипов)
        DemonstrateSingletonRegistry();

        Console.WriteLine("\nНажмите любую клавишу для завершения...");
        Console.ReadKey();
    }

    /// <summary>
    /// Демонстрация паттерна Строитель (Этап 1)
    /// </summary>
    static void DemonstrateBuilder()
    {
        Console.WriteLine("\n【ЭТАП 1】ПАТТЕРН «СТРОИТЕЛЬ»");
        Console.WriteLine(new string('─', 50));

        // Создание компьютера через строителя напрямую
        Console.WriteLine("▶ Создание компьютера через строителя (произвольная конфигурация):");

        var customComputer = new ComputerBuilder()
            .WithCPU("Intel Core i9-13900K")
            .WithRAM(64)
            .WithGPU("NVIDIA RTX 4090")
            .WithComponent("Жидкостное охлаждение Corsair")
            .WithComponent("Блок питания 1200W Gold")
            .WithComponent("Корпус с RGB-подсветкой")
            .WithComponent("SSD Samsung 990 Pro 2TB")
            .Build();

        customComputer.Display();

        // Использование фабрик для создания предопределенных конфигураций
        Console.WriteLine("\n▶ Использование фабричных методов для создания предопределенных конфигураций:");

        IComputerFactory officeFactory = new OfficeComputerFactory();
        var officePC = officeFactory.CreateComputer();
        Console.WriteLine("\n--- Офисный ПК (OfficeComputerFactory) ---");
        officePC.Display();

        IComputerFactory gamingFactory = new GamingComputerFactory();
        var gamingPC = gamingFactory.CreateComputer();
        Console.WriteLine("\n--- Игровой ПК (GamingComputerFactory) ---");
        gamingPC.Display();

        IComputerFactory homeFactory = new HomeComputerFactory();
        var homePC = homeFactory.CreateComputer();
        Console.WriteLine("\n--- Домашний ПК (HomeComputerFactory) ---");
        homePC.Display();
    }

    /// <summary>
    /// Демонстрация паттерна Прототип (Этап 2)
    /// </summary>
    static void DemonstratePrototype()
    {
        Console.WriteLine("\n\n【ЭТАП 2】ПАТТЕРН «ПРОТОТИП»");
        Console.WriteLine(new string('─', 50));

        // Создаем оригинальный компьютер для тестирования клонирования
        var original = new ComputerBuilder()
            .WithCPU("AMD Ryzen 9 7950X")
            .WithRAM(32)
            .WithGPU("NVIDIA RTX 4080")
            .WithComponent("SSD 1TB")
            .WithComponent("Wi-Fi 6E")
            .WithComponent("Звуковая карта")
            .Build();

        Console.WriteLine("▶ Оригинальный объект (original):");
        Console.WriteLine($"  {original}");

        // Поверхностное копирование
        var shallowCopy = original.ShallowCopy();
        Console.WriteLine("\n▶ Создана ПОВЕРХНОСТНАЯ копия (ShallowCopy)");

        // Глубокое копирование
        var deepCopy = original.DeepCopy();
        Console.WriteLine("▶ Создана ГЛУБОКАЯ копия (DeepCopy)");

        // Модифицируем оригинал
        Console.WriteLine("\n▶ Модифицируем оригинал: добавляем компонент 'NVMe SSD 2TB'");
        original.AdditionalComponents.Add("NVMe SSD 2TB");

        // Модифицируем компонент в оригинале (для демонстрации)
        Console.WriteLine("  Модифицируем существующий компонент: 'SSD 1TB' -> 'SSD 1TB (изменен)'");
        original.AdditionalComponents[0] = "SSD 1TB (изменен)";

        // Демонстрация различий
        Console.WriteLine("\n--- РЕЗУЛЬТАТ ПОСЛЕ МОДИФИКАЦИИ ОРИГИНАЛА ---");

        Console.WriteLine("\n▶ Оригинал (был изменен):");
        Console.WriteLine($"  {original}");

        Console.WriteLine("\n▶ Поверхностная копия (ShallowCopy):");
        Console.WriteLine($"  {shallowCopy}");
        Console.WriteLine("  ⚠ ВИДИТ ИЗМЕНЕНИЯ в списке, т.к. ссылается на тот же объект List!");

        Console.WriteLine("\n▶ Глубокая копия (DeepCopy):");
        Console.WriteLine($"  {deepCopy}");
        Console.WriteLine("  ✓ НЕ ВИДИТ ИЗМЕНЕНИЙ, т.к. имеет собственную копию списка!");

        Console.WriteLine("\n【ВЫВОД】");
        Console.WriteLine("  • При поверхностном копировании ссылочные типы (List<string>)");
        Console.WriteLine("    разделяются между оригиналом и копией.");
        Console.WriteLine("  • При глубоком копировании создается независимая копия");
        Console.WriteLine("    всех ссылочных объектов.");
    }

    /// <summary>
    /// Демонстрация паттерна Singleton (Этап 3)
    /// </summary>
    static void DemonstrateSingletonRegistry()
    {
        Console.WriteLine("\n\n【ЭТАП 3】ПАТТЕРН «ОДИНОЧКА» (SINGLETON)");
        Console.WriteLine(new string('─', 50));

        // Демонстрация работы Singleton
        Console.WriteLine("▶ Проверка работы Singleton:");

        var registry1 = PrototypeRegistry.Instance;
        var registry2 = PrototypeRegistry.Instance;

        Console.WriteLine($"  registry1 и registry2 ссылаются на один объект: {ReferenceEquals(registry1, registry2)}");
        Console.WriteLine($"  Хэш-код registry1: {registry1.GetHashCode()}");
        Console.WriteLine($"  Хэш-код registry2: {registry2.GetHashCode()}");

        Console.WriteLine("\n  Попытка создать экземпляр через new PrototypeRegistry() невозможна");
        Console.WriteLine("  (конструктор приватный)");

        // Показываем доступные прототипы
        Console.WriteLine("\n▶ Прототипы, доступные в реестре:");
        foreach (var key in registry1.GetAllKeys())
        {
            Console.WriteLine($"  • {key}");
        }

        // Демонстрация получения и модификации прототипа
        Console.WriteLine("\n▶ Демонстрация получения прототипа из реестра:");

        var gamingCopy = registry1.GetPrototype("gaming");
        Console.WriteLine("\n  Получена копия прототипа 'gaming':");
        Console.WriteLine($"  {gamingCopy}");

        // Модифицируем копию
        if (gamingCopy != null)
        {
            Console.WriteLine("\n  ▶ Модифицируем полученную копию:");
            gamingCopy.RAM = 128;
            gamingCopy.AdditionalComponents.Add("Модифицированный компонент (ДОБАВЛЕН В КОПИЮ)");
            gamingCopy.AdditionalComponents[0] = "Мышь игровая RGB (ИЗМЕНЕНО В КОПИИ)";

            Console.WriteLine("  Модифицированная копия:");
            Console.WriteLine($"  {gamingCopy}");
        }

        // Получаем прототип снова
        var gamingCopy2 = registry1.GetPrototype("gaming");
        Console.WriteLine("\n  ▶ Повторный запрос прототипа 'gaming':");
        Console.WriteLine($"  {gamingCopy2}");
        Console.WriteLine("  ✓ Прототип НЕ ИЗМЕНИЛСЯ, т.к. реестр возвращает глубокие копии!");

        // Получаем оригинал для проверки
        var gamingOriginal = registry1.GetOriginalPrototype("gaming");
        Console.WriteLine("\n  ▶ Оригинал в реестре (для проверки):");
        Console.WriteLine($"  {gamingOriginal}");

        Console.WriteLine("\n【ВЫВОД】");
        Console.WriteLine("  • Реестр прототипов реализован как потокобезопасный Singleton");
        Console.WriteLine("  • GetPrototype() возвращает ГЛУБОКУЮ копию прототипа");
        Console.WriteLine("  • Модификация полученной копии НЕ ВЛИЯЕТ на оригинал в реестре");
        Console.WriteLine("  • Повторный запрос дает чистую копию оригинала");
    }
}