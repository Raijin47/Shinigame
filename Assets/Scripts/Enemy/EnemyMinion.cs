using System.Collections;
using UnityEngine;

public class EnemyMinion : Enemy
{
    private Coroutine _updateNewDirection;

    //public override void Activate()
    //{
    //    base.Activate();

    //    if(_updateNewDirection != null)
    //    {
    //        StopCoroutine(_updateNewDirection);
    //        _updateNewDirection = null;
    //    }

    //    _updateNewDirection = StartCoroutine(UpdateNewPosition());
    //}

    //private IEnumerator UpdateNewPosition()
    //{
    //    while(_isActive)
    //    {
    //        var Timer = new WaitForSeconds(Random.Range(3f, 6f));

    //        Teleport();

    //        yield return Timer;
    //    }
    //}

    //private void Teleport()
    //{
    //    transform.position = UtilityTools.GenerateRandomPositionSquarePattern(_targetDestination.position);
    //}

    //protected override void Defeated()
    //{
    //    base.Defeated();
    //    _updateNewDirection = null;
    //}
}
