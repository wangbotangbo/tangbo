using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using TBFramework;
using System;
using System.Threading;
public class ScriptRun : LocalMethod
{  
    
    StepMethod stepMethod;
    StepProperty stepProperty;
    Coroutine coro;
    void Start()
    {
        base.Init();


        stepProperty = gameObject.GetComponent<StepProperty>();
        stepMethod = gameObject.AddComponent<StepMethod>();
        stepMethod.Init(stepProperty);

        StandardProperty.Instance.step.Add(stepMethod.commonUtilStep());
        StandardProperty.Instance.step.Add(stepMethod.step1());
        StandardProperty.Instance.step.Add(stepMethod.step2());
        StandardProperty.Instance.step.Add(stepMethod.step3());

        coro = StartCoroutine(standardClass.StartCoroutineMethod());
       //关闭携程结束执行 stepMethod.StopAll(coro);
       
    }

    private CancellationTokenSource cancellationTokenSource;
    public int i = 0;
    public void OnButton()
    {
        
        // 取消之前的异步操作
        if (cancellationTokenSource != null)
        {
            cancellationTokenSource.Cancel();
            cancellationTokenSource.Dispose();
        }
        standardClass.AsyncQueue = null;
        standardClass.DestoryDotween();
        cancellationTokenSource = new CancellationTokenSource();
        if(i % 2== 0)
        {
            base.ChangeMinMaxWin(true);
        // 传递CancellationToken给异步方法
            standardClass.AddToAsyncQueue(standardClass.ExecuteAsyncMethod(standardClass, TestStatus.MOVEANDROTATE, cancellationTokenSource.Token, StandardProperty.Instance.cameraStruct.mainCamera.transform, new Vector3(0, 30, 0), new Vector3(0, 90, 0), 2f));
            standardClass.AddToAsyncQueue(standardClass.ExecuteAsyncMethod(standardClass, TestStatus.MOVEANDROTATE, cancellationTokenSource.Token, StandardProperty.Instance.cameraStruct.mainCamera.transform, new Vector3(0, 0, 0), new Vector3(0, 0, 0), 2f));
            standardClass.ActionMothed(() => { Debug.Log("sdfsdf"); });
        }else
        {
            base.ChangeMinMaxWin(false);
            standardClass.AddToAsyncQueue(standardClass.ExecuteAsyncMethod(standardClass, TestStatus.MOVEANDROTATE, cancellationTokenSource.Token, StandardProperty.Instance.cameraStruct.mainCamera.transform, new Vector3(0, 30, 0), new Vector3(0, 45, 0), 2f));
            standardClass.AddToAsyncQueue(standardClass.ExecuteAsyncMethod(standardClass, TestStatus.MOVEANDROTATE, cancellationTokenSource.Token, StandardProperty.Instance.cameraStruct.mainCamera.transform, new Vector3(0, 0, 0), new Vector3(0, 0, 0), 2f));
            standardClass.ActionMothed(() => { Debug.Log("sdfsdf"); });
        }
        i++;        
    }

    void Update()
    {
        foreach (var item in StandardProperty.Instance.RefreshActions)
        {
            item?.Invoke();
        }        
    }

    public void OnButton2()
    {
        stepProperty.stepCurrentProperty.step1.value1 = 2;
        stepProperty.stepCurrentProperty.step1.OnceMessagePanel = true;
    }


    

}

