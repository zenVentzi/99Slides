using Assets.Scripts.Utilities;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    public class SetUpSettings : MyMono
    {
        [UsedImplicitly]
        private void Awake()
        {
            float cameraWidth = (Camera.main.orthographicSize * 2 * Camera.main.aspect),
                  cameraHeight = (Camera.main.orthographicSize * 2),
                  scaleX = cameraWidth / SpriteRend.bounds.size.x,
                  scaleY = cameraHeight / SpriteRend.bounds.size.y;

            GamePlayerPrefs.SetFloat("ScaleX", scaleX);
            GamePlayerPrefs.SetFloat("ScaleY", scaleY);
            //GamePlayerPrefs.SetFloat("CameraWidth", cameraWidth);
            //GamePlayerPrefs.SetFloat("CameraHeight", cameraHeight);

            Tr.localScale = new Vector3(scaleX, scaleY, 1);
        }
    }
}