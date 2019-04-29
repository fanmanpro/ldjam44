using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class ColliderComponent<T> : EZS.Component where T : EZS.Component
{
    public List<T> Collisions;

    private void Awake()
    {
        Collisions = new List<T>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        T other = collision.collider.gameObject.GetComponent<T>();
        if(other == null)
        {
            return;
        }
        Collisions.Add(other);
    }

    private void OnCollisionExit2D(Collision2D collision)
    {
        if(Collisions.Count <= 0)
        {
            return;
        }
        T other = collision.collider.gameObject.GetComponent<T>();
        if(other == null)
        {
            return;
        }
        Collisions.Remove(other);
    }
}
