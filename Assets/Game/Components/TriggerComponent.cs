using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class TriggerComponent<T> : EZS.Component where T : EZS.Component
{
    public List<T> Triggers;

    private void Awake()
    {
        Triggers = new List<T>();
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        T other = collision.gameObject.GetComponent<T>();
        if(other == null)
        {
            return;
        }
        Triggers.Add(other);
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if(Triggers.Count <= 0)
        {
            return;
        }
        T other = collision.gameObject.GetComponent<T>();
        if(other == null)
        {
            return;
        }
        Triggers.Remove(other);
    }
}
