using System.Collections;
using System.Collections.Generic;
using TBFramework;
using UnityEngine;
using UnityEngine.UI;
public class StepProperty :StandardProperty
{

    public CommonUtils commonUtils;
    public StepCurrentProperty stepCurrentProperty=new StepCurrentProperty();
    public List<StepCurrentProperty> stepCurrentProperties = new List<StepCurrentProperty>();
    /// <summary>
    /// 对传入的 StepCurrentProperty 对象进行深拷贝操作。
    /// 深拷贝会创建一个新的 StepCurrentProperty 实例，并将传入对象的属性值复制到新实例中。
    /// </summary>
    /// <param name="property">需要进行深拷贝的 StepCurrentProperty 对象。</param>
    /// <returns>返回一个新的 StepCurrentProperty 对象，其属性值与传入对象相同。</returns>
    public StepCurrentProperty DeepCopy(StepCurrentProperty property)
    {
        StepCurrentProperty copy = new StepCurrentProperty();
        copy.step1 = property.step1;
        copy.step2 = property.step2;
        return copy;
    }
}

[System.Serializable]
public class CommonUtils
{
    public GameObject movie;
    public bool isOver = false;

   
}

[System.Serializable]
public class StepCurrentProperty
{
    public step1Struct step1;
    public step2Struct step2;
}
[System.Serializable]
public struct step1Struct{
    public float value1;
    public bool OnceMessagePanel;
    public float waitTime;
}
[System.Serializable]
public struct step2Struct{
    public Transform target;
    public Vector3 targetPos;
    public Vector3 targetRot; 
}