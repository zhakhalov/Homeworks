using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityUtils
{
    static class ComponentFinder
    {
        public static T GetComponentInParents<T>(Transform t) where T : Component
        {
            if (null == t)
            {
                return null;
            }

            T component = t.GetComponent<T>();

            if(null != component)
            {
                return component;
            }

            return GetComponentInParents<T>(t.parent);
        }

        public static T GetComponentInChildren<T>(Transform t) where T : Component
        {
            T component = t.GetComponent<T>();

            if (null != component)
            {
                return component;
            }

            foreach (Transform r in t)
            {
                component = GetComponentInChildren<T>(r);

                if (null != component)
                {
                    return component;
                }
            }

            return null;
        }

        public static Component[] GetComponentsInChildren(Transform t, Type type, Type exclude)
        {
            List<Component> components = new List<Component>();

			GetComponentsInChildren(t, components, type, exclude);

            return components.ToArray();
        }

        static void GetComponentsInChildren(Transform t, List<Component> list, Type type, Type exclude)
        {
            Component[] components = t.GetComponents(typeof(Component));

            foreach (var r in components)
            {
                if (type.IsAssignableFrom(r.GetType()))
                {
                    list.Add(r);
                }
            }

            foreach (Transform r in t)
            {
				bool ignore = false;
				Component[] c = r.GetComponents(typeof(Component));

				foreach (var b in c)
				{
					if (exclude.IsAssignableFrom(b.GetType()))
					{
						ignore = true;
						break;
					}
				}

				if (!ignore)
				{
                	GetComponentsInChildren(r, list, type, exclude);
				}
            }
        }

        public static Component[] GetComponentsInChildren(Transform t, Type type)
        {
            List<Component> components = new List<Component>();

            GetComponentsInChildren(t, components, type);

            return components.ToArray();
        }

        static void GetComponentsInChildren(Transform t, List<Component> list, Type type)
        {
            Component[] components = t.GetComponents(typeof(Component));

            foreach (var r in components)
            {
                if (type.IsAssignableFrom(r.GetType()))
                {
                    list.Add(r);
                }
            }

            foreach (Transform r in t)
            {
                GetComponentsInChildren(r, list, type);
            }
        }

        public static T[] GetComponentsInChildren<T>(Transform t) where T : Component
        {
            List<T> components = new List<T>();

            GetComponentsInChildren<T>(t, components);

            return components.ToArray();
        }

        static void GetComponentsInChildren<T>(Transform t, List<T> list) where T : Component
        {
            T component = t.GetComponent<T>();

            if (null != component)
            {
                list.Add(component);
            }

            foreach (Transform r in t)
            {
                GetComponentsInChildren<T>(r, list);
            }
        }
    }
}
