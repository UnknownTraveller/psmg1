using System;
using UnityEngine;

// shamelessly copied from our main project (all these are still by me).

/**
    Convert Collision onto this GameObject into .NET Events
**/
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
        //no autoimplemented properties here, so I don't even bother...
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

    /**
        Called whenever another object collides with the game object this script is attached to
    **/
    public event EventHandler<CollisionEventArgs> Enter;

    /**
        Called whenever another object *stops* colliding with the game object this script is attached to
    **/
    public event EventHandler<CollisionEventArgs> Exit;
}