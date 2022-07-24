namespace Assets.Scripts.Buttons
{
    public abstract class AbstractButton : MyMono
    {
        protected static bool ButtonsActive = true;

        public static void DeactivateButtons()
        {
            ButtonsActive = false;
        }

        public static void ActivateButtons()
        {
            ButtonsActive = true;
        }
    }
}