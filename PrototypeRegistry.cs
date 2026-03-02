using System.Collections.Concurrent;

namespace ComputerConfigurator;

/// <summary>
/// Потокобезопасный Singleton реестр прототипов
/// </summary>
public sealed class PrototypeRegistry
{
    // Вариант 1: Lazy<T> - самый простой потокобезопасный способ в C#
    private static readonly Lazy<PrototypeRegistry> _instance =
        new Lazy<PrototypeRegistry>(() => new PrototypeRegistry());

    // Словарь для хранения прототипов
    private readonly Dictionary<string, Computer> _prototypes;

    // Объект для блокировки при доступе к словарю
    private readonly object _lockObject = new object();

    /// <summary>
    /// Приватный конструктор (заполняем реестр прототипами)
    /// </summary>
    private PrototypeRegistry()
    {
        _prototypes = new Dictionary<string, Computer>();
        InitializePrototypes();
        Console.WriteLine(">>> Реестр прототипов инициализирован <<<");
    }

    /// <summary>
    /// Публичное свойство для доступа к экземпляру Singleton
    /// </summary>
    public static PrototypeRegistry Instance => _instance.Value;

    /// <summary>
    /// Инициализация предопределенных прототипов
    /// </summary>
    private void InitializePrototypes()
    {
        // Используем фабрики для создания прототипов
        _prototypes["office"] = new OfficeComputerFactory().CreateComputer();
        _prototypes["gaming"] = new GamingComputerFactory().CreateComputer();
        _prototypes["home"] = new HomeComputerFactory().CreateComputer();

        // Добавим еще один прототип для рабочей станции
        _prototypes["workstation"] = new ComputerBuilder()
            .WithCPU("AMD Ryzen 9 7950X (рабочая станция)")
            .WithRAM(64)
            .WithGPU("NVIDIA RTX 4090 24GB")
            .WithComponent("ECC память")
            .WithComponent("Монитор 4K")
            .WithComponent("Графический планшет")
            .Build();
    }

    /// <summary>
    /// Получение глубокой копии прототипа по ключу
    /// </summary>
    public Computer? GetPrototype(string key)
    {
        lock (_lockObject)
        {
            if (_prototypes.TryGetValue(key.ToLower(), out var prototype))
            {
                Console.WriteLine($">>> Возвращена глубокая копия прототипа '{key}' <<<");
                return prototype.DeepCopy();
            }

            Console.WriteLine($">>> Прототип с ключом '{key}' не найден! <<<");
            return null;
        }
    }

    /// <summary>
    /// Получение оригинала прототипа (ТОЛЬКО для демонстрации!)
    /// </summary>
    public Computer? GetOriginalPrototype(string key)
    {
        lock (_lockObject)
        {
            if (_prototypes.TryGetValue(key.ToLower(), out var prototype))
            {
                return prototype;
            }
            return null;
        }
    }

    /// <summary>
    /// Добавление нового прототипа в реестр
    /// </summary>
    public void AddPrototype(string key, Computer prototype)
    {
        lock (_lockObject)
        {
            _prototypes[key.ToLower()] = prototype.DeepCopy();
            Console.WriteLine($">>> Прототип '{key}' добавлен в реестр <<<");
        }
    }

    /// <summary>
    /// Получение всех доступных ключей
    /// </summary>
    public IEnumerable<string> GetAllKeys()
    {
        lock (_lockObject)
        {
            return _prototypes.Keys.ToList();
        }
    }
}