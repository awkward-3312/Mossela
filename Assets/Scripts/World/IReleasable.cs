using Mossela.Character;

namespace Mossela.World
{
    // Implemented by interactables that hold Haru in a pose (chair, bed, sofa) and know how to let her go.
    public interface IReleasable
    {
        void Release(HaruController haru);
    }
}
