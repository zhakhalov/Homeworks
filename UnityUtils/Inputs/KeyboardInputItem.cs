using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;

namespace UnityUtils.Inputs
{
    [Serializable]
    public class KeyboardInputItem
    {
        #region Inspector

        [SerializeField]
        string _name;
        public string Name { get { return _name; } set { _name = value; } }

        [SerializeField]
        KeyCode _key;
        public KeyCode Key { get { return _key; } set { _key = value; } }

        [SerializeField]
        KeyCode _alt;
        public KeyCode Alt { get { return _alt; } set { _alt = value; } }

        #endregion Inspector

        public bool IsPressed { get; set; }
        public bool IsDown { get; set; }
        public bool IsUp { get; set; }

        public void Update()
        {
            IsPressed = Input.GetKey(_key) || Input.GetKey(_alt);
            IsDown = Input.GetKeyDown(_key) || Input.GetKeyDown(_alt);
            IsUp = Input.GetKeyUp(_key) || Input.GetKeyUp(_alt);
        }

        //TODO W/R XML
    }
}
