
namespace RPGPrototype.Interactive
{
    public enum InteractiveAction
    {
        Use,
        Read,
        Take,
    }

    // For all objects that can be used by pressing E
    public interface IInteractive
    {
        string Name { get; }
        bool IsActive { get; }
        InteractiveAction Action { get; }

        void Use();
    }
}

