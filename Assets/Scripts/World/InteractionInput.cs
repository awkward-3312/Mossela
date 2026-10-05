using Mossela.Character;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.InputSystem;

namespace Mossela.World
{
    // Turns a mouse click or a screen tap into game actions:
    // a tap on an IInteractable runs it; a tap on empty space releases the furniture Haru is using.
    public class InteractionInput : MonoBehaviour
    {
        [SerializeField] private Camera worldCamera;
        [SerializeField] private HaruController haru;
        [SerializeField] private bool ignoreUi = true;

        private IReleasable current;

        private void Awake()
        {
            if (worldCamera == null) worldCamera = Camera.main;
            if (haru == null) haru = FindFirstObjectByType<HaruController>();
        }

        private void Update()
        {
            var pointer = Pointer.current;
            if (pointer == null || !pointer.press.wasPressedThisFrame) return;

            HandleTap(pointer.position.ReadValue());
        }

        public void HandleTap(Vector2 screenPosition)
        {
            if (worldCamera == null || haru == null) return;
            if (ignoreUi && IsOverUi()) return;

            Vector2 world = worldCamera.ScreenToWorldPoint(screenPosition);
            var hit = Physics2D.OverlapPoint(world);
            var interactable = hit != null ? hit.GetComponentInParent<IInteractable>() : null;

            if (interactable != null)
            {
                interactable.Interact(haru);
                current = interactable as IReleasable;
                return;
            }

            ReleaseCurrent();
        }

        private void ReleaseCurrent()
        {
            if (current != null)
            {
                current.Release(haru);
                current = null;
            }
            else if (haru.CurrentPose != HaruPose.Idle)
            {
                haru.SetPose(HaruPose.Idle);
            }
        }

        private static bool IsOverUi()
        {
            return EventSystem.current != null && EventSystem.current.IsPointerOverGameObject();
        }
    }
}
