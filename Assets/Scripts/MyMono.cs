using UnityEngine;

namespace Assets.Scripts
{
    public class MyMono : MonoBehaviour
    {
        private SpriteRenderer spriteRend;
        private Rigidbody2D rigBody;
        private Collider2D colider;
        private AudioSource audioo;
        private Animation anim;
        private Animator animr;

        public Transform Tr
        {
            get { return transform; }
        }

        public GameObject Go
        {
            get { return gameObject; }
        }

        public Rigidbody2D RigBody
        {
            get { return rigBody ?? (rigBody = transform.GetComponent<Rigidbody2D>()); }
        }

        public Collider2D Colider
        {
            get { return colider ?? (colider = GetComponent<Collider2D>()); }
        }

        public Animation Anim
        {
            get { return anim ?? (anim = GetComponent<Animation>()); }
        }

        public Animator Animr
        {
            get { return animr ?? (animr = GetComponent<Animator>()); }
        }

        public AudioSource Audioo
        {
            get { return audioo ?? (audioo = GetComponent<AudioSource>()); }
        }

        public SpriteRenderer SpriteRend
        {
            get
            {
                return spriteRend ?? (spriteRend = GetComponent<SpriteRenderer>());
            }
        }
    }
}