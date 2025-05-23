using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading.Tasks;
using UnityEngine;
using System.Threading;
namespace TBFramework
{
    // 新增枚举
    public enum TestStatus
    {
        MOVEANDROTATE
    }
    public enum OffsetState
    {
        CENTER,
        TOP,
        LEFT,
        Right,
        Bottom
    }

    public interface iTest
    {
        // 新增异步方法
        Task ObjMoveAndRotateMethod(CancellationToken token,params object[] ps);
        IEnumerator StartCoroutineMethod();
    }
}
