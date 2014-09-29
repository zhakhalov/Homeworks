using System;
using UnityEngine;
using UnityUtils;
using System.Collections.Generic;
using System.Linq;

namespace UnityUtils.Inputs
{
    public class KeyboardInput : SingletonGameObject<KeyboardInput>
    {
        #region Inspector

        [SerializeField]
        List<KeyboardInputItem> _keys;
        public List<KeyboardInputItem> Keys { get { return _keys; } set { _keys = value; } }

        #endregion Inspector

        public KeyboardInputItem this[string key]
        {
            get
            {
                return _keys.First(r => r.Name == key);
            }
        }

        void Update()
        {
            _keys.ForEach(r => r.Update());
        }
    }

    
}