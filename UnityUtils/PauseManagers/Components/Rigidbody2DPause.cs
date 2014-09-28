using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using UnityEngine;
using UnityUtils.PauseManagers.Abstract;

namespace UnityUtils.PauseManagers.Components
{
    [RequireComponent(typeof(Rigidbody2D))]
    public class Rigidbody2DPause : MonoBehaviour, IPausableUnit
    {
        bool _isPause;

        bool _isKinematic;
        Vector2 _velocity;
        float _angularVelocity;

        bool IPausableUnit.IsPaused
        {
            get { return _isPause; }
        }

        void IPausableUnit.PauseUnit()
        {
            _isPause = true;

            _velocity = rigidbody2D.velocity;
            _angularVelocity = rigidbody2D.angularVelocity;
            _isKinematic = rigidbody2D.isKinematic;

            rigidbody2D.velocity = Vector2.zero;
            rigidbody2D.angularVelocity = 0;
            rigidbody2D.isKinematic = true;
        }

        void IPausableUnit.ResumeUnit()
        {
            _isPause = false;

            rigidbody2D.velocity = _velocity;
            rigidbody2D.angularVelocity = _angularVelocity;
            rigidbody2D.isKinematic = _isKinematic; 
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
