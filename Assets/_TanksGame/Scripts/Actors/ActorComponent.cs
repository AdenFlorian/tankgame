using System;
using System.Collections;
using UnityEngine;

public abstract class ActorComponent : MonoBehaviour
{
    protected Actor model;

    protected virtual void Awake()
    {
        FindParentModel();
    }

    protected void FindParentModel()
    {
        model = GetComponent<Actor>();
        if (model == null) {
            model = GetComponentInParent<Actor>();
            if (model == null) {
                throw new ComponentWithoutParentModelException();
            }
        }
    }
}

[System.Serializable]
public class ComponentWithoutParentModelException : System.Exception
{
    public ComponentWithoutParentModelException()
    {
    }

    public ComponentWithoutParentModelException(string message)
        : base(message)
    {
    }

    public ComponentWithoutParentModelException(string message, System.Exception inner)
        : base(message, inner)
    {
    }

    protected ComponentWithoutParentModelException(
      System.Runtime.Serialization.SerializationInfo info,
      System.Runtime.Serialization.StreamingContext context)
        : base(info, context)
    {
    }
}
