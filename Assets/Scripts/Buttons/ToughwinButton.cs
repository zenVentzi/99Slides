using UnityEngine;

namespace Assets.Scripts.Buttons
{
    public class ToughwinButton : AnimatedButton
    {
        protected override void OnClick()
        {
            base.OnClick();
            Application.OpenURL("https://www.facebook.com/toughwingames");
        }
    }
}
