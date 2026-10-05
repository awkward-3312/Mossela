using Mossela.Character;
using UnityEngine;

namespace Mossela.World
{
    // Puts Haru into a pose when tapped. Reuse it for any furniture that maps to a single pose.
    // Interact: Haru is moved onto targetPoint (no walking), then the pose is applied.
    // Release: Haru is moved onto exitPoint, then returns to Idle.
    public class PoseInteractable : InteractableObject, IReleasable
    {
        [SerializeField] private HaruPose pose = HaruPose.Sitting;
        [SerializeField] private Transform targetPoint;
        [SerializeField] private Transform exitPoint;
        [SerializeField] private bool repositionHaru = true;

        protected override void OnInteract(HaruController haru)
        {
            if (repositionHaru) MoveHaru(haru, targetPoint);
            haru.SetPose(pose);
        }

        public void Release(HaruController haru)
        {
            if (haru == null) return;
            if (repositionHaru) MoveHaru(haru, exitPoint);
            haru.SetPose(HaruPose.Idle);
        }

        private static void MoveHaru(HaruController haru, Transform point)
        {
            if (point == null) return;
            Vector3 p = point.position;
            haru.transform.position = new Vector3(p.x, p.y, haru.transform.position.z);
        }

        private void OnDrawGizmosSelected()
        {
            DrawPoint(targetPoint, Color.green);
            DrawPoint(exitPoint, Color.yellow);
        }

        private static void DrawPoint(Transform point, Color color)
        {
            if (point == null) return;
            Gizmos.color = color;
            Gizmos.DrawWireSphere(point.position, 0.15f);
        }
    }
}
