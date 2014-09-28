using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityUtils
{
    public class PoolManager<T> where T : Component
    {
        public int Capacity { get { return List.Capacity; } }
        public Transform Container { get; set; }
        public Type Type { get; private set; }
        public List<T> List { get; private set; }
        public int CountActive { get { return List.Where(r => r.gameObject.activeInHierarchy).ToList().Count; } }

        public PoolManager(int capacity)
        {
            Type = typeof(T);
            List = new List<T>(capacity);
        }

        public T CycleElement()
        {
            T element = List[0];

            List.RemoveAt(0);
            List.Add(element);

            return element;
        }

        public void DeleteInactive()
        {
            List<T> inactive = List.Where(r => !r.gameObject.activeInHierarchy).ToList();

            foreach (var r in inactive)
            {
                List.Remove(r);
                GameObject.Destroy(r);
            }
        }

        public void Clear()
        {
            while (List.Count > 0)
            {
                GameObject go = List[0].gameObject;
                List.RemoveAt(0);
                GameObject.Destroy(go);
            }
        }

        public T GetUsedOrCreate(GameObject prefab)
        {
            foreach (var r in List)
            {
                if (!r.gameObject.activeInHierarchy)
                {
                    r.gameObject.SetActive(true);
                    return r;
                }
            }

            Vector3 position = Vector3.zero;
            Quaternion rotation = Quaternion.identity;
            if (null != Container)
            {
                position = Container.position;
                rotation = Container.rotation;
            }

            GameObject go = (GameObject)(GameObject.Instantiate(prefab, position, rotation));

            T component = go.GetComponent<T>();

            if (null != Container)
            {
                component.transform.parent = Container;
            }

            List.Add(component);

            return component;
        }
    }
}
