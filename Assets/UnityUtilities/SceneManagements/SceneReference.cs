using UnityEngine;

namespace UnityUtility.SceneManagements
{
    [CreateAssetMenu(fileName = "SceneReference", menuName = "Scene Management/Scene Reference")]
    public class SceneReference : ScriptableObject
    {
        public string sceneName;
    }
}
