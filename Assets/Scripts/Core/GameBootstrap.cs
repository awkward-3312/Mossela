using UnityEngine;

namespace Mossela.Core
{
    // Entry point of the Boot scene: configures the runtime, then loads the first scene.
    public class GameBootstrap : MonoBehaviour
    {
        [SerializeField] private string firstScene = SceneNames.Cottage;
        [SerializeField] private int targetFrameRate = 60;

        private void Start()
        {
            Application.targetFrameRate = targetFrameRate;
            Screen.sleepTimeout = SleepTimeout.NeverSleep;
            SceneLoader.LoadAsync(firstScene);
        }
    }
}
