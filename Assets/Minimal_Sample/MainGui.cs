using UnityEngine;

public class MainGui : MonoBehaviour
{
    private const float FontSizeMult = 0.05f;
    private bool mWaitingForAuth = false;
    private string mStatusText = "Ready.";

    void Start()
    {
        GooglePlayGames.PlayGamesPlatform.Activate();
    }

    void OnGUI()
    {
        GUI.skin.button.fontSize = (int)(FontSizeMult * Screen.height);
        GUI.skin.label.fontSize = (int)(FontSizeMult * Screen.height);

        GUI.Label(new Rect(20, 20, Screen.width, Screen.height * 0.25f),
                  mStatusText);

        Rect buttonRect = new Rect(0.25f * Screen.width, 0.10f * Screen.height,
                          0.5f * Screen.width, 0.25f * Screen.height);
        Rect imageRect = new Rect(buttonRect.x + buttonRect.width / 4f,
                                  buttonRect.y + buttonRect.height * 1.1f,
                                  buttonRect.width / 2f, buttonRect.width / 2f);

        if (mWaitingForAuth)
        {
            return;
        }

        string buttonLabel;


        if (Social.localUser.authenticated)
        {
            buttonLabel = "Sign Out";
            if (Social.localUser.image != null)
            {
                GUI.DrawTexture(imageRect, Social.localUser.image,
                                ScaleMode.ScaleToFit);
            }
            else
            {
                GUI.Label(imageRect, "No image available");
            }
        }
        else
        {
            buttonLabel = "Authenticate";
            mStatusText = "Ready";
        }

        if (GUI.Button(buttonRect, buttonLabel))
        {
            if (!Social.localUser.authenticated)
            {
                // Authenticate
                mWaitingForAuth = true;
                mStatusText = "Authenticating...";
                Social.localUser.Authenticate((bool success) =>
                {
                    mWaitingForAuth = false;
                    if (success)
                    {
                        mStatusText = "Welcome " + Social.localUser.userName;
                    }
                    else
                    {
                        mStatusText = "Authentication failed.";
                    }
                });
            }
            else
            {
                // Sign out!
                mStatusText = "Signing out.";
                ((GooglePlayGames.PlayGamesPlatform)Social.Active).SignOut();
            }
        }
    }
}
