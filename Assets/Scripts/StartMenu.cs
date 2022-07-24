using System.Collections;
using Assets.Scripts.Managers;
using JetBrains.Annotations;
using UnityEngine;

namespace Assets.Scripts
{
    public class StartMenu : MyMono
    {
        private GameObject crown,
                           loadTxt;
        private GameObject[] crownBubbles;

        [UsedImplicitly]
        private void Awake()
        {
            Tr.localScale = GameManager.Instance.GetScale();

            loadTxt = GameObjectManager.GetGoInChildren(Go, "LoadingText");
            crown = Tr.GetChild(0).gameObject;
            crownBubbles = new GameObject[crown.transform.childCount];

            for (int i = 0; i < crown.transform.childCount; i++)
            {
                crownBubbles[i] = crown.transform.GetChild(i).gameObject;
            }

            StartCoroutine(DisplayLoading());
        }

        private IEnumerator DisplayLoading()
        {
            var bubbleDisplayTime = 1.25f / crownBubbles.Length;
            var yellow = new Color32(255, 255, 72, 255);

            foreach (var bubble in crownBubbles)
            {
                bubble.GetComponent<SpriteRenderer>().color = yellow;
                yield return new WaitForSeconds(bubbleDisplayTime);
            }

            crown.SetActive(false);
            loadTxt.SetActive(false);
        }

        [UsedImplicitly]
        private void OnAppearAnimFinish()
        {
            Anim.Play(name + "Idle");
        }
    }
}
