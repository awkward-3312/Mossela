using Mossela.Character;
using UnityEngine;

namespace Mossela.World
{
    // Base for anything Haru can use (chair, bed, sofa, table). Needs a Collider2D to receive taps.
    [RequireComponent(typeof(Collider2D))]
    public abstract class InteractableObject : MonoBehaviour, IInteractable
    {
        public void Interact(HaruController haru)
        {
            if (haru == null) return;
            OnInteract(haru);
        }

        protected abstract void OnInteract(HaruController haru);
    }
}
