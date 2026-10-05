using Mossela.Character;
using UnityEngine;
using UnityEngine.InputSystem;

namespace Mossela.World
{
    // Turns a mouse click or a screen tap into an Interact call on the IInteractable under the pointer.
    public class InteractionInput : MonoBehaviour
    {
        [SerializeField] private Camera worldCamera;
        [SerializeField] private HaruController haru;

        private void Awake()
        {
            if (worldCamera == null) worldCamera = Camera.main;
            if (haru == null) haru = FindFirstObjectByType<HaruController>();
        }

        private void Update()
        {
            var pointer = Pointer.current;
            if (pointer == null || !pointer.press.wasPressedThisFrame) return;
            if (worldCamera == null) return;

            Vector2 screen = pointer.position.ReadValue();
            Vector2 world = worldCamera.ScreenToWorldPoint(screen);

            var hit = Physics2D.OverlapPoint(world);
            if (hit == null) return;

            var interactable = hit.GetComponentInParent<IInteractable>();
            interactable?.Interact(haru);
        }
    }
}
