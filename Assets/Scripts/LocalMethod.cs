using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TBFramework;
using System;
public class LocalMethod : StandardMethod
{
    public StandardClass standardClass;
    protected void Init()
    {        
        standardClass = new StandardClass("test");
        StartCoroutine(standardClass.StartRunAsyncMethod(standardClass));        

        StandardProperty.Instance.cameraStruct.isOpen = false;
        StandardProperty.Instance.cameraStruct.mouseActions = new List<Action<Transform>>();
        StandardProperty.Instance.cameraStruct.mouseActions.Add((T) => {Debug.Log(T.transform.name);});

        StandardProperty.Instance.UICamera.isOpen = false;
        StandardProperty.Instance.UICamera.uiMouseActions = new List<Action<Transform>>();
        StandardProperty.Instance.UICamera.uiMouseActions.Add((T) => {Debug.Log(T.transform.name);});        

        StandardProperty.Instance.minMaxWindows.isOpen = false;
        
        base.UIObjectsAddDrag();
        StandardProperty.Instance.RefreshActions.Add(() => { Refresh(); base.UIPositioning();});
    }

    // Update is called once per frame
    protected void Refresh()
    {
        if (StandardProperty.Instance.cameraStruct.isOpen)
        {
            MainCameraRayControl(StandardProperty.Instance.cameraStruct.mainCamera,StandardProperty.Instance.cameraStruct.distance,StandardProperty.Instance.cameraStruct.layerMask,StandardProperty.Instance.cameraStruct.mouseActions);
        } 
        if (StandardProperty.Instance.UICamera.isOpen)
        {
            UiCameraRayControl(StandardProperty.Instance.UICamera.MiniMap,StandardProperty.Instance.UICamera.uiCamera,StandardProperty.Instance.UICamera.distance,StandardProperty.Instance.UICamera.layerMask,StandardProperty.Instance.UICamera.uiMouseActions);
        }
        if (StandardProperty.Instance.followUI.isOpen)
        {
            StandardProperty.Instance.followUI.followUI.gameObject.SetActive(true);
            UIFollowMouse(StandardProperty.Instance.followUI.followUI,StandardProperty.Instance.followUI.offsetState);
        }
        else
        {
            StandardProperty.Instance.followUI.followUI.gameObject.SetActive(false);
        }
    }  


}
