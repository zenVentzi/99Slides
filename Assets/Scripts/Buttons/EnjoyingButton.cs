using UnityEngine;

namespace Assets.Scripts.Buttons
{
    public class EnjoyingButton : AnimatedButton
    {
        [SerializeField] private bool enjoy;
        [SerializeField] private GameObject rateUs;
        [SerializeField] private GameObject contactUs;

        protected override void OnClick()
        {
            base.OnClick();

            if (enjoy)
            {
                rateUs.SetActive(true);
            }
            else
            {
                contactUs.SetActive(true);
            }

            Tr.parent.gameObject.SetActive(false);
        }
    }
}
