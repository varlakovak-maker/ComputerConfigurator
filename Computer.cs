using System.Text;

namespace ComputerConfigurator;

/// <summary>
/// Класс, представляющий компьютерную конфигурацию
/// </summary>
public class Computer : ICloneable
{
    // Поля согласно заданию
    public string CPU { get; set; }
    public int RAM { get; set; } // в ГБ
    public string GPU { get; set; }
    public List<string> AdditionalComponents { get; set; }

    public Computer()
    {
        CPU = string.Empty;
        GPU = string.Empty;
        AdditionalComponents = new List<string>();
    }

    /// <summary>
    /// Конструктор для глубокого копирования
    /// </summary>
    public Computer(string cpu, int ram, string gpu, List<string> additionalComponents)
    {
        CPU = cpu;
        RAM = ram;
        GPU = gpu;
        AdditionalComponents = additionalComponents;
    }

    /// <summary>
    /// Метод для вывода информации о конфигурации
    /// </summary>
    public void Display()
    {
        Console.WriteLine(new string('=', 50));
        Console.WriteLine("КОНФИГУРАЦИЯ КОМПЬЮТЕРА:");
        Console.WriteLine(new string('-', 50));
        Console.WriteLine($"Процессор (CPU): {CPU}");
        Console.WriteLine($"Оперативная память (RAM): {RAM} ГБ");
        Console.WriteLine($"Видеокарта (GPU): {GPU}");

        Console.WriteLine("Дополнительные комплектующие:");
        if (AdditionalComponents.Count == 0)
        {
            Console.WriteLine("  - Нет дополнительных комплектующих");
        }
        else
        {
            foreach (var component in AdditionalComponents)
            {
                Console.WriteLine($"  • {component}");
            }
        }
        Console.WriteLine(new string('=', 50));
    }

    /// <summary>
    /// Поверхностное копирование (Shallow Copy)
    /// </summary>
    public Computer ShallowCopy()
    {
        return (Computer)this.MemberwiseClone();
    }

    /// <summary>
    /// Глубокое копирование (Deep Copy)
    /// </summary>
    public Computer DeepCopy()
    {
        // Сначала создаем поверхностную копию
        Computer clone = (Computer)this.MemberwiseClone();

        // Затем создаем новые копии всех ссылочных типов
        clone.AdditionalComponents = new List<string>(this.AdditionalComponents);

        return clone;
    }

    /// <summary>
    /// Реализация ICloneable (для демонстрации)
    /// </summary>
    public object Clone()
    {
        return this.DeepCopy();
    }

    public override string ToString()
    {
        var sb = new StringBuilder();
        sb.AppendLine($"CPU: {CPU}, RAM: {RAM}ГБ, GPU: {GPU}");
        sb.AppendLine($"Доп. компоненты: [{string.Join(", ", AdditionalComponents)}]");
        return sb.ToString();
    }
}