using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Framework.Pooling;
using Framework.Core;
using Framework.DI;

public class PoolingTest : MonoBehaviour
{

    [Inject] private IPoolManager _poolMgr;

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.K))
        {
            StartCoroutine(TestCo(_poolMgr.TakeObject("Test")));
        }
    }

    private IEnumerator TestCo(GameObject obj)
    {

        yield return new WaitForSeconds(3);
        _poolMgr.PutObject(obj);

    }

}
