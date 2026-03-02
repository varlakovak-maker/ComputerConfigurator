namespace ComputerConfigurator;

/// <summary>
/// Фабрика для создания офисного ПК
/// </summary>
public class OfficeComputerFactory : IComputerFactory
{
    public Computer CreateComputer()
    {
        return new ComputerBuilder()
            .WithCPU("Intel Core i3-12100 (офисный)")
            .WithRAM(8)
            .WithGPU("Встроенная графика Intel UHD 730")
            .WithComponent("Клавиатура офисная")
            .WithComponent("Мышь оптическая")
            .WithComponent("Монитор 24\"")
            .WithComponent("Коврик для мыши")
            .Build();
    }
}