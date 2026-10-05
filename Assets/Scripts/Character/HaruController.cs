using System;
using System.Collections.Generic;
using UnityEngine;

namespace Mossela.Character
{
    // Public API for Haru's state. Gameplay code should only talk to this class.
    public class HaruController : MonoBehaviour
    {
        [SerializeField] private HaruVisual visual;
        [SerializeField] private MicroAnimator microAnimator;
        [SerializeField] private HaruPose startPose = HaruPose.Idle;
        [SerializeField] private HaruExpression startExpression = HaruExpression.Neutral;
        [SerializeField] private PoseDefinition[] poses;
        [SerializeField] private ExpressionDefinition[] expressions;

        private readonly Dictionary<HaruPose, PoseDefinition> poseLookup = new Dictionary<HaruPose, PoseDefinition>();
        private readonly Dictionary<HaruExpression, ExpressionDefinition> expressionLookup = new Dictionary<HaruExpression, ExpressionDefinition>();

        public HaruPose CurrentPose { get; private set; }
        public HaruExpression CurrentExpression { get; private set; }
        public MicroAnimator MicroAnimator => microAnimator;

        public event Action<HaruPose> PoseChanged;
        public event Action<HaruExpression> ExpressionChanged;

        private void Awake()
        {
            foreach (var p in poses) if (p != null) poseLookup[p.Pose] = p;
            foreach (var e in expressions) if (e != null) expressionLookup[e.Expression] = e;
        }

        private void Start()
        {
            SetState(startPose, startExpression);
        }

        public void SetPose(HaruPose pose) => SetState(pose, CurrentExpression);

        public void SetExpression(HaruExpression expression) => SetState(CurrentPose, expression);

        public void SetState(HaruPose pose, HaruExpression expression)
        {
            bool poseChanged = pose != CurrentPose;
            bool expressionChanged = expression != CurrentExpression;
            CurrentPose = pose;
            CurrentExpression = expression;

            poseLookup.TryGetValue(pose, out var poseDef);
            expressionLookup.TryGetValue(expression, out var expressionDef);
            if (visual != null) visual.Apply(poseDef, expressionDef);

            if (poseChanged) PoseChanged?.Invoke(pose);
            if (expressionChanged) ExpressionChanged?.Invoke(expression);
        }
    }
}
