using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityUtils.MementoManagers.Abstract;

namespace UnityUtils.MementoManagers.Component
{
    [RequireComponent(typeof(Rigidbody2D))]
    class Rigidbody2DRemember : MonoBehaviour, IRememberableUnit
    {
        private float _gravityScale;
        private bool _isKinematic;
        private Vector2 _position;
        private Vector2 _velocity;
        private float _rotation;
        private float _angularVelocity;

        void IRememberableUnit.RememberUnit()
        {
            _isKinematic = rigidbody2D.isKinematic;
            _position = rigidbody2D.position;
            _velocity = rigidbody2D.velocity;
            _rotation = rigidbody2D.rotation;
            _angularVelocity = rigidbody2D.angularVelocity;
            _gravityScale = rigidbody2D.gravityScale;
        }

        void IRememberableUnit.RecallUnit()
        {
            rigidbody2D.isKinematic = _isKinematic;
            rigidbody2D.position = _position;
            rigidbody2D.velocity = _velocity;
            rigidbody2D.rotation = _rotation;
            rigidbody2D.angularVelocity = _angularVelocity;
            rigidbody2D.gravityScale = _gravityScale;
        }

        bool UnityUtils.Abstract.IUnit.IsEnabled
        {
            get { return enabled; }
        }

        bool UnityUtils.Abstract.IUnit.IsDestroyed
        {
            get { return null == gameObject; }
        }
    }
}
