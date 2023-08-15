using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IEnemy
{
    public virtual void UpdateState() { }
    public virtual void UpdateAction() { }

}
