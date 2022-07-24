using JetBrains.Annotations;
using Soomla.Store;
using UnityEngine;

namespace Assets.Scripts.Plugins
{
    public class SoomlaBilling : MonoBehaviour
    {
        [UsedImplicitly]
        void Start()
        {
            StoreEvents.OnSoomlaStoreInitialized += () =>
            {
                //Debug.Log("Soomla initialized");
            };

            //StoreEvents.OnUnexpectedErrorInStore += Debug.Log;

            SoomlaStore.Initialize(new NinetyNineSlidesAssetStore());
            
        }
    }
}
