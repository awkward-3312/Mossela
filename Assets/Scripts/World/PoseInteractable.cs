using Mossela.Character;
using UnityEngine;

namespace Mossela.World
{
    // Puts Haru into a pose when tapped. Reuse it for any furniture that maps to a single pose.
    // When repositionHaru is set, Haru is first moved onto targetPoint (no walking), then the pose is applied.
    public class PoseInteractable : InteractableObject
    {
        [SerializeField] private HaruPose pose = HaruPose.Sitting;
        [SerializeField] private Transform targetPoint;
        [SerializeField] private bool repositionHaru = true;

        protected override void OnInteract(HaruController haru)
        {
            if (repositionHaru && targetPoint != null)
            {
                Vector3 p = targetPoint.position;
                haru.transform.position = new Vector3(p.x, p.y, haru.transform.position.z);
            }

            haru.SetPose(pose);
        }

        private void OnDrawGizmosSelected()
        {
            if (targetPoint == null) return;
            Gizmos.color = Color.green;
            Gizmos.DrawWireSphere(targetPoint.position, 0.15f);
        }
    }
}
