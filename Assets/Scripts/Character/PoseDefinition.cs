using UnityEngine;

namespace Mossela.Character
{
    [CreateAssetMenu(menuName = "Mossela/Haru/Pose", fileName = "Pose_")]
    public class PoseDefinition : ScriptableObject
    {
        [SerializeField] private HaruPose pose;
        // Full-character sprite. Null means "use the expression sprite" (standing idle).
        [SerializeField] private Sprite fullSprite;
        // Modular variants, used when the modular rig is active.
        [SerializeField] private Sprite armsSprite;
        [SerializeField] private Sprite legsSprite;

        public HaruPose Pose => pose;
        public Sprite FullSprite => fullSprite;
        public Sprite ArmsSprite => armsSprite;
        public Sprite LegsSprite => legsSprite;
    }
}
