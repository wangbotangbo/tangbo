using System.Collections;
using System.Collections.Generic;
using TBFramework;
using UnityEngine;

public static class StandardStaticMethod
{

    public static void DOFade1(this Transform target, float endValue, float duration)
    {
        StandardProperty.Instance.cameraStruct.mainCamera.transform.position = new Vector3(0, 0, 0);
        // 方法体保持不变
    }

}
