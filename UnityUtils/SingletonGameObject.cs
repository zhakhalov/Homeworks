using UnityEngine;
using System.Collections;
using System;

namespace UnityUtils
{
    public abstract class SingletonGameObject<T> : MonoBehaviour where T : MonoBehaviour
    {
        static T _intance;
        static public T Instance
        {
            get
            {
                if (null == _intance)
                {
                    GameObject go = GameObject.Find(typeof(T).Name);

                    if ((null != go) && (null != (_intance = go.GetComponent<T>())))
                    {
                        return _intance;
                    }

                    go = new GameObject((typeof(T)).Name);
                    _intance = go.AddComponent<T>();
                    go.name = typeof(T).Name;
                }

                return _intance;
            }
        }

        public void Dispose()
        {
            _intance = null;
        }

        protected virtual void OnValidate()
        {
            name = typeof(T).Name;
            transform.position = Vector3.zero;
            transform.rotation = Quaternion.identity;
            transform.localScale = Vector3.one;
        }
    }
}