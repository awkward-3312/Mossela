using UnityEngine;

namespace Mossela.Character
{
    [CreateAssetMenu(menuName = "Mossela/Haru/Expression", fileName = "Expression_")]
    public class ExpressionDefinition : ScriptableObject
    {
        [SerializeField] private HaruExpression expression;
        [SerializeField] private Sprite fullSprite;
        // Face-only sprite for the modular rig. Not authored yet.
        [SerializeField] private Sprite faceSprite;

        public HaruExpression Expression => expression;
        public Sprite FullSprite => fullSprite;
        public Sprite FaceSprite => faceSprite;
    }
}
