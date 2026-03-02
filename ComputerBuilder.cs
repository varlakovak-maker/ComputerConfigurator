namespace ComputerConfigurator;

/// <summary>
/// Строитель для пошагового создания компьютера
/// </summary>
public class ComputerBuilder
{
    private Computer _computer;

    public ComputerBuilder()
    {
        _computer = new Computer();
    }

    /// <summary>
    /// Установка процессора
    /// </summary>
    public ComputerBuilder WithCPU(string cpu)
    {
        _computer.CPU = cpu;
        return this; // Возвращаем себя для цепочки вызовов
    }

    /// <summary>
    /// Установка объема памяти
    /// </summary>
    public ComputerBuilder WithRAM(int ram)
    {
        _computer.RAM = ram;
        return this;
    }

    /// <summary>
    /// Установка видеокарты
    /// </summary>
    public ComputerBuilder WithGPU(string gpu)
    {
        _computer.GPU = gpu;
        return this;
    }

    /// <summary>
    /// Добавление дополнительного комплектующего
    /// </summary>
    public ComputerBuilder WithComponent(string component)
    {
        _computer.AdditionalComponents.Add(component);
        return this;
    }

    /// <summary>
    /// Сборка готового объекта Computer
    /// </summary>
    public Computer Build()
    {
        // Простая валидация
        if (string.IsNullOrEmpty(_computer.CPU))
            throw new InvalidOperationException("Ошибка: процессор не указан!");

        if (_computer.RAM <= 0)
            throw new InvalidOperationException("Ошибка: объем памяти должен быть положительным числом!");

        if (string.IsNullOrEmpty(_computer.GPU))
            throw new InvalidOperationException("Ошибка: видеокарта не указана!");

        return _computer;
    }

    /// <summary>
    /// Сброс строителя для создания нового объекта
    /// </summary>
    public void Reset()
    {
        _computer = new Computer();
    }
}