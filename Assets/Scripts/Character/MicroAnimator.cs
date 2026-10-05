using UnityEngine;

namespace Mossela.Character
{
    // Procedural idle motion: breathing, blinking, ear twitch, tail sway.
    // Every target is optional. Blink, ears and tail only act on the modular rig.
    public class MicroAnimator : MonoBehaviour
    {
        [Header("Breathing")]
        [SerializeField] private Transform breathTarget;
        [SerializeField] private float breathAmplitude = 0.015f;
        [SerializeField] private float breathSpeed = 1.2f;

        [Header("Blink")]
        [SerializeField] private SpriteRenderer eyes;
        [SerializeField] private Sprite eyesOpen;
        [SerializeField] private Sprite eyesHalf;
        [SerializeField] private Sprite eyesClosed;
        [SerializeField] private Vector2 blinkIntervalRange = new Vector2(2f, 5f);
        [SerializeField] private float blinkStepDuration = 0.05f;

        [Header("Ears")]
        [SerializeField] private Transform earLeft;
        [SerializeField] private Transform earRight;
        [SerializeField] private float earTwitchAngle = 6f;
        [SerializeField] private Vector2 earTwitchIntervalRange = new Vector2(4f, 9f);

        [Header("Tail")]
        [SerializeField] private Transform tail;
        [SerializeField] private float tailSwayAngle = 4f;
        [SerializeField] private float tailSwaySpeed = 1.5f;

        private Vector3 breathBaseScale;
        private float nextBlinkTime;
        private float blinkStartTime = -1f;
        private float nextEarTwitchTime;
        private float earTwitchStartTime = -1f;

        private void Awake()
        {
            if (breathTarget == null) breathTarget = transform;
            breathBaseScale = breathTarget.localScale;
        }

        private void OnEnable()
        {
            ScheduleBlink();
            ScheduleEarTwitch();
        }

        private void Update()
        {
            UpdateBreath();
            UpdateBlink();
            UpdateEars();
            UpdateTail();
        }

        private void UpdateBreath()
        {
            float s = Mathf.Sin(Time.time * breathSpeed) * breathAmplitude;
            breathTarget.localScale = new Vector3(breathBaseScale.x, breathBaseScale.y * (1f + s), breathBaseScale.z);
        }

        private void UpdateBlink()
        {
            if (eyes == null || eyesOpen == null) return;

            if (blinkStartTime < 0f)
            {
                if (Time.time >= nextBlinkTime) blinkStartTime = Time.time;
                return;
            }

            int step = Mathf.FloorToInt((Time.time - blinkStartTime) / blinkStepDuration);
            switch (step)
            {
                case 0: eyes.sprite = eyesHalf != null ? eyesHalf : eyesOpen; break;
                case 1: eyes.sprite = eyesClosed != null ? eyesClosed : eyesOpen; break;
                case 2: eyes.sprite = eyesHalf != null ? eyesHalf : eyesOpen; break;
                default:
                    eyes.sprite = eyesOpen;
                    blinkStartTime = -1f;
                    ScheduleBlink();
                    break;
            }
        }

        private void UpdateEars()
        {
            if (earLeft == null && earRight == null) return;

            float angle = 0f;
            if (earTwitchStartTime >= 0f)
            {
                float t = (Time.time - earTwitchStartTime) / 0.25f;
                if (t >= 1f)
                {
                    earTwitchStartTime = -1f;
                    ScheduleEarTwitch();
                }
                else
                {
                    angle = Mathf.Sin(t * Mathf.PI) * earTwitchAngle;
                }
            }
            else if (Time.time >= nextEarTwitchTime)
            {
                earTwitchStartTime = Time.time;
            }

            if (earLeft != null) earLeft.localRotation = Quaternion.Euler(0f, 0f, angle);
            if (earRight != null) earRight.localRotation = Quaternion.Euler(0f, 0f, -angle);
        }

        private void UpdateTail()
        {
            if (tail == null) return;
            float angle = Mathf.Sin(Time.time * tailSwaySpeed) * tailSwayAngle;
            tail.localRotation = Quaternion.Euler(0f, 0f, angle);
        }

        private void ScheduleBlink()
        {
            nextBlinkTime = Time.time + Random.Range(blinkIntervalRange.x, blinkIntervalRange.y);
        }

        private void ScheduleEarTwitch()
        {
            nextEarTwitchTime = Time.time + Random.Range(earTwitchIntervalRange.x, earTwitchIntervalRange.y);
        }
    }
}
