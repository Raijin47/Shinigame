using UnityEngine;

namespace Plugins.Audio.Utils
{
    public class AudioAutoPause : MonoBehaviour
    {
        private static AudioAutoPause _instance;
        
        private void Awake()
        {
            if (_instance != null)
            {
                Destroy(gameObject);
                return;
            }

            _instance = this;
            DontDestroyOnLoad(gameObject);
        }
        public void OpenADS()
        {
            AudioListener.pause = true;
        }

        public void CloseADS()
        {
            AudioListener.pause = false;
        }
    }
}