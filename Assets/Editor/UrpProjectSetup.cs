using UnityEditor;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.Universal;

namespace Mossela.Editor
{
    // Creates the URP 2D pipeline assets on first open and assigns them to Graphics and Quality settings.
    // The generated files land in Assets/Settings and should be committed.
    [InitializeOnLoad]
    public static class UrpProjectSetup
    {
        private const string SettingsFolder = "Assets/Settings";

        static UrpProjectSetup()
        {
            EditorApplication.delayCall += () =>
            {
                if (GraphicsSettings.defaultRenderPipeline == null) Setup();
            };
        }

        [MenuItem("Mossela/Setup URP 2D")]
        public static void Setup()
        {
            if (!AssetDatabase.IsValidFolder(SettingsFolder)) AssetDatabase.CreateFolder("Assets", "Settings");

            var rendererPath = SettingsFolder + "/Renderer2D.asset";
            var pipelinePath = SettingsFolder + "/UniversalRP-2D.asset";

            var renderer = AssetDatabase.LoadAssetAtPath<Renderer2DData>(rendererPath);
            if (renderer == null)
            {
                renderer = ScriptableObject.CreateInstance<Renderer2DData>();
                AssetDatabase.CreateAsset(renderer, rendererPath);
            }

            var pipeline = AssetDatabase.LoadAssetAtPath<UniversalRenderPipelineAsset>(pipelinePath);
            if (pipeline == null)
            {
                pipeline = UniversalRenderPipelineAsset.Create(renderer);
                AssetDatabase.CreateAsset(pipeline, pipelinePath);
            }

            GraphicsSettings.defaultRenderPipeline = pipeline;
            int current = QualitySettings.GetQualityLevel();
            for (int i = 0; i < QualitySettings.names.Length; i++)
            {
                QualitySettings.SetQualityLevel(i, false);
                QualitySettings.renderPipeline = pipeline;
            }
            QualitySettings.SetQualityLevel(current, false);

            AssetDatabase.SaveAssets();
            Debug.Log("Mossela: URP 2D pipeline assigned.");
        }
    }
}
