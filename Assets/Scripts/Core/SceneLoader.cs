using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mossela.Core
{
    public static class SceneLoader
    {
        public static AsyncOperation LoadAsync(string sceneName)
        {
            return SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
        }
    }
}
