using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Managers
{
    public class GameObjectManager : MyMono
    {
        public static GameObject GetGoInChildren(GameObject root, string name)
        {
            var transforms = new Queue<Transform>();
            transforms.Enqueue(root.transform);

            while (transforms.Count > 0)
            {
                var current = transforms.Dequeue();

                if (current.name == name)
                {
                    return current.gameObject;
                }

                for (int i = 0; i < current.childCount; i++)
                {
                    transforms.Enqueue(current.GetChild(i));                    
                }
            }

            return null;
        }

        public static GameObject[] GetChildren(GameObject parent)
        {
            var queue = new Queue<Transform>();
            var children = new List<GameObject>();
            queue.Enqueue(parent.transform);

            while (queue.Count > 0)
            {
                var current = queue.Dequeue();
                children.Add(current.gameObject);

                for (int i = 0; i < current.childCount; i++)
                {
                    queue.Enqueue(current.GetChild(i));
                }
            }

            children.Remove(parent);
            return children.ToArray();
        }
    }
}