using Mossela.Character;
using UnityEngine;

namespace Mossela.World
{
    // Puts Haru into a pose when tapped. Reuse it for any furniture that maps to a single pose.
    public class PoseInteractable : InteractableObject
    {
        [SerializeField] private HaruPose pose = HaruPose.Sitting;

        protected override void OnInteract(HaruController haru)
        {
            haru.SetPose(pose);
        }
    }
}
