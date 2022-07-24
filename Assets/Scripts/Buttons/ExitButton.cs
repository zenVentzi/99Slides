using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts.Buttons
{
    public class ExitButton : AnimatedButton
    {
        protected override void OnClick()
        {
            base.OnClick();
            Application.Quit();
        }

        [UsedImplicitly]
        private void Update()
        {
            if(Input.GetKey(KeyCode.Escape))
                Application.Quit();
        }

        private void OnApplicationQuit()
        {
        }
    }
}
