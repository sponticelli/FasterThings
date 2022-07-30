using LiteNinja.SOVariable;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace LiteNinja.Actions
{
    [AddComponentMenu("LiteNinja/Actions/AutoLoadScene")]
    public class AutoLoadScene : MonoBehaviour
    {
        [SerializeField] private StringVar _sceneName;
        [SerializeField] private bool _isAdditive;

        private void Awake()
        {
            if (string.IsNullOrEmpty(_sceneName.Value))
            {
                Debug.LogError("Scene name is empty");
                return;
            }
            
            //Check if the scene is already loaded
            for (var i = 0; i < SceneManager.sceneCount; i++)
            {
                if (SceneManager.GetSceneAt(i).name == _sceneName.Value)
                {
                    return;
                }
            }
            
            SceneManager.LoadScene(_sceneName.Value, _isAdditive ? LoadSceneMode.Additive : LoadSceneMode.Single);
        }
        
    }
}