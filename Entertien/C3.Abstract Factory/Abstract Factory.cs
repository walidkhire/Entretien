//Crée des familles d’objets liés sans spécifier leurs classes concrètes.

namespace DesignPatterns.Abstract_Factory
{
    // Interfaces des produits
    public interface IButton { void Render(); }
    public interface ICheckbox { void Render(); }

    // Produits Windows
    public class WindowsButton : IButton { public void Render() => Console.WriteLine("Button Windows"); }
    public class WindowsCheckbox : ICheckbox { public void Render() => Console.WriteLine("Checkbox Windows"); }

    // Produits Mac
    public class MacButton : IButton { public void Render() => Console.WriteLine("Button Mac"); }
    public class MacCheckbox : ICheckbox { public void Render() => Console.WriteLine("Checkbox Mac"); }

    // Abstract Factory
    public interface IGUIFactory
    {
        IButton CreateButton();
        ICheckbox CreateCheckbox();
    }

    // Factories concrètes
    public class WindowsFactory : IGUIFactory
    {
        public IButton CreateButton() => new WindowsButton();
        public ICheckbox CreateCheckbox() => new WindowsCheckbox();
    }

    public class MacFactory : IGUIFactory
    {
        public IButton CreateButton() => new MacButton();
        public ICheckbox CreateCheckbox() => new MacCheckbox();
    }
}