namespace ComputerConfigurator;

/// <summary>
/// Фабрика для создания игрового ПК
/// </summary>
public class GamingComputerFactory : IComputerFactory
{
    public Computer CreateComputer()
    {
        return new ComputerBuilder()
            .WithCPU("AMD Ryzen 7 7800X3D (игровой)")
            .WithRAM(32)
            .WithGPU("NVIDIA RTX 4080 Super 16GB")
            .WithComponent("Мышь игровая RGB")
            .WithComponent("Клавиатура механическая")
            .WithComponent("Наушники игровые 7.1")
            .WithComponent("Коврик большой")
            .WithComponent("Охлаждение жидкостное")
            .Build();
    }
}