using System;
using UnityEngine;

// shamelessly copied from our main project (all these are still by me).
public class CollisionEvent : MonoBehaviour {

    public void OnCollisionEnter(Collision collision)
    {
        if (Enter != null) Enter(gameObject, new CollisionEventArgs(collision));
    }
    public void OnTriggerEnter(Collider other)
    {
        if (Enter != null) Enter(gameObject, new CollisionEventArgs(other));
    }

    public void OnCollisionExit(Collision collision)
    {
        if (Exit != null) Exit(gameObject, new CollisionEventArgs(collision));
    }
    public void OnTriggerExit(Collider other)
    {
        if (Exit != null) Exit(gameObject, new CollisionEventArgs(other));
    }

    public class CollisionEventArgs : EventArgs
    {
        //no autoimplemented properties here, so i don't bother...
        public readonly bool IsTriggerEvent;
        public readonly Collision Collision;
        public readonly Collider Trigger;

        public CollisionEventArgs(Collision c)
        {
            IsTriggerEvent = false;
            Collision = c;
        }
        public CollisionEventArgs(Collider c)
        {
            IsTriggerEvent = true;
            Trigger = c;
        }
    }

    public event EventHandler<CollisionEventArgs> Enter;
    public event EventHandler<CollisionEventArgs> Exit;
}