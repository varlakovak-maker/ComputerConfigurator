namespace ComputerConfigurator;

/// <summary>
/// Интерфейс фабрики для создания компьютеров
/// </summary>
public interface IComputerFactory
{
    Computer CreateComputer();
}