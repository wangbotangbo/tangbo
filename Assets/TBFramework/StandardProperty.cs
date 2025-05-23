using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
namespace TBFramework
{
    [System.Serializable]
    public struct CameraStruct
    {
        [Tooltip("主相机对象，用于场景渲染和射线检测等操作。")]
        public Camera mainCamera;
        [Tooltip("相机与目标对象之间的距离，可用于控制相机视角。")]
        public float distance;
        [Tooltip("相机射线检测的图层掩码，用于过滤检测的对象。")]
        public LayerMask layerMask;
        [Tooltip("添加相机执行方法")]
        public List<Action<Transform>> mouseActions;
        [Tooltip("是否打开")]
        public bool isOpen;
    }

    [System.Serializable]
    public struct DraggableUIObjects
    {
        [Tooltip("可拖拽的 UI 游戏对象，通常包含拖拽交互逻辑。")]
        public GameObject draggableUI;
        [Tooltip("可拖拽 UI 对象的标题高度，可用于控制拖拽触发区域。")]
        public float titleHight;
        [Tooltip("可拖拽 UI 对象的偏移量，用于调整 UI 显示位置。")]
        public Vector2 offset;
    }
    [System.Serializable]
    public struct MinMaxWindows
    {//最小化窗口相关属性实例，包含目标 3D 对象、UI 相机、画布、窗口和位置 UI 等信息。
       public Transform target3Dobj;
       public Camera uiCamera;
       public RectTransform canvas;
       public  RectTransform windows;
       [Tooltip("定位跟随的UI对象")]
       public RectTransform posUI;
       public Transform miniBorder;
        [Tooltip("是否打开")]
       public bool isOpen;
    }

    [System.Serializable]
    public struct UICamera
    {//UI 相机相关属性实例，包含最小地图、UI 相机、相机距离和图层掩码等信息。
       [Tooltip("最小地图，用于显示场景的缩略图。")]
       public RawImage MiniMap;
       [Tooltip("UI 相机对象，用于 UI 渲染和射线检测等操作。")]
       public Camera uiCamera;
        [Tooltip("相机与目标对象之间的距离，可用于控制相机视角。")]
       public float distance;
       [Tooltip("相机射线检测的图层掩码，用于过滤检测的对象。")]
       public LayerMask layerMask;
       [Tooltip("添加相机执行方法")]
       public List<Action<Transform>> uiMouseActions;

       [Tooltip("是否打开")]
       public bool isOpen;
    }

    [System.Serializable]
    public struct FollowUI{

       [Tooltip("跟随的UI")]
       public RectTransform followUI;
       [Tooltip("是否打开")]
       public bool isOpen;

       public OffsetState offsetState;
    }

    


    public class StandardProperty : Singleton<StandardProperty>
    {
        [Tooltip("相机相关属性实例，包含主相机、相机距离和图层掩码等信息。")]
        public CameraStruct cameraStruct = new CameraStruct();
        [Tooltip("可拖拽 UI 对象列表，存储多个可拖拽 UI 对象的相关属性。")]
        public List<DraggableUIObjects> draggableUIObjects = new List<DraggableUIObjects>();
        [Tooltip("最小化窗口相关属性实例，包含目标 3D 对象、UI 相机、画布、窗口和位置 UI 等信息。")]
        public MinMaxWindows minMaxWindows = new MinMaxWindows();
        [Tooltip("动态刷新的脚本")]
        public List<Action> RefreshActions = new List<Action>();
        [Tooltip("UI相机控制射线触发等")]
        public UICamera UICamera = new UICamera();
        public List<IEnumerator> step=new List<IEnumerator>();
        [Tooltip("跟随鼠标的UI")]
        public FollowUI followUI = new FollowUI();
    }
    
}

