using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    public class SplashScreen : MonoBehaviour
    {
        [UsedImplicitly]
        private void Awake()
        {
        }

        [UsedImplicitly]
        private void OnAnimFinish()
        {
            Destroy(gameObject);
            //Debug.Break();
        }   
    }
}
