using UnityEngine;

namespace Mossela.Character
{
    // Owns everything drawn for Haru. Default mode swaps whole-character sprites.
    // Modular mode shows the layered rig (see Visual/Modular in the prefab).
    public class HaruVisual : MonoBehaviour
    {
        [SerializeField] private SpriteRenderer fullBody;
        [SerializeField] private GameObject modularRoot;
        [SerializeField] private SpriteRenderer modularFace;

        private PoseDefinition currentPose;
        private ExpressionDefinition currentExpression;

        public bool IsModular => modularRoot != null && modularRoot.activeSelf;

        public void SetModular(bool modular)
        {
            if (modularRoot != null) modularRoot.SetActive(modular);
            if (fullBody != null) fullBody.enabled = !modular;
            Refresh();
        }

        public void Apply(PoseDefinition pose, ExpressionDefinition expression)
        {
            currentPose = pose;
            currentExpression = expression;
            Refresh();
        }

        private void Refresh()
        {
            if (IsModular)
            {
                if (modularFace != null && currentExpression != null && currentExpression.FaceSprite != null)
                    modularFace.sprite = currentExpression.FaceSprite;
                return;
            }

            if (fullBody == null) return;

            Sprite sprite = null;
            if (currentPose != null) sprite = currentPose.FullSprite;
            if (sprite == null && currentExpression != null) sprite = currentExpression.FullSprite;
            if (sprite != null) fullBody.sprite = sprite;
        }
    }
}
