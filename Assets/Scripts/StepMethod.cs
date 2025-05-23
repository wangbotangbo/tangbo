using System.Collections;
using System.Collections.Generic;
using TBFramework;
using UnityEngine;

public class StepMethod:MonoBehaviour
{
     StepProperty stepProperty=new StepProperty();
    public void Init(StepProperty _stepProperty)
    {
        stepProperty = _stepProperty;
    }

    public IEnumerator commonUtilStep()
    {
       stepProperty.commonUtils.movie.gameObject.SetActive(false);
       yield return new WaitForSeconds(1);
    }


    public IEnumerator step1()
    {
       stepProperty.stepCurrentProperty = stepProperty.DeepCopy(stepProperty.stepCurrentProperties[0]);
       while(true)
       {
         if(stepProperty.stepCurrentProperty.step1.waitTime>5)
         {
            print("超时");
            yield break;
         }else
         {
            stepProperty.stepCurrentProperty.step1.waitTime+=Time.deltaTime;
         }

         if(stepProperty.stepCurrentProperty.step1.value1==2)
         {
            print("成功结束");
            yield break;
         }
         else
         {
            if(stepProperty.stepCurrentProperty.step1.OnceMessagePanel)
            {
               print("错误信息");
               stepProperty.stepCurrentProperty.step1.OnceMessagePanel = false;
            }
         }
         yield return null;
       }
    }
    public IEnumerator step2()
    {
       Debug.Log("step2");
       yield return new WaitForSeconds(2);
    }
    public IEnumerator step3()
    {
       Debug.Log("step3");
       yield return new WaitForSeconds(2);
    }
    public void StopAll(Coroutine coro)
    {
       StopCoroutine(coro);
       StopAllCoroutines();
    }
}
