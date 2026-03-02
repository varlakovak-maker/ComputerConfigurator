namespace ComputerConfigurator;

/// <summary>
/// Фабрика для создания домашнего ПК
/// </summary>
public class HomeComputerFactory : IComputerFactory
{
    public Computer CreateComputer()
    {
        return new ComputerBuilder()
            .WithCPU("Intel Core i5-13400 (домашний)")
            .WithRAM(16)
            .WithGPU("NVIDIA RTX 3060 12GB")
            .WithComponent("Wi-Fi адаптер")
            .WithComponent("Bluetooth адаптер")
            .WithComponent("Картридер USB-C")
            .WithComponent("Веб-камера HD")
            .Build();
    }
}