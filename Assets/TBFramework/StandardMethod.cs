using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;
namespace TBFramework
{
    public class StandardMethod:StandrdUIMethod
    {
        
    }
    public class StandrdUIMethod:StandrdControlMethod
    {  

        /// <summary>
        /// 让指定的 UI 跟随鼠标移动。
        /// 该方法会将传入的 RectTransform 组件的位置设置为当前鼠标在屏幕上的位置。
        /// </summary>
        /// <param name="uiRectTransform">需要跟随鼠标移动的 UI 的 RectTransform 组件。</param>
        public static void UIFollowMouse(RectTransform uiRectTransform,OffsetState offsetState)
        {
            if (uiRectTransform != null)
            {
                // 将鼠标屏幕坐标转换为世界坐标，并设置 UI 的位置
                Vector3 mousePosition = Input.mousePosition;
                switch(offsetState)
                {
                    case OffsetState.LEFT:
                        mousePosition.x += (uiRectTransform.rect.width / 2 + 5)* uiRectTransform.root.lossyScale.x;
                        mousePosition.y -= (uiRectTransform.rect.height / 2 + 5 )* uiRectTransform.root.lossyScale.x;
                        break;
                }
                uiRectTransform.position = mousePosition;
            }
        }
        /// <summary>
        /// 为指定的游戏对象添加 UI 拖拽限制组件，并设置标题高度。
        /// 该方法会在传入的游戏对象上添加 <see cref="UIObjectsAddDrag"/> 组件，
        /// 并将其 <c>titleHight</c> 属性设置为指定的值。
        /// </summary>
        /// <param name="dragUiObj">需要添加 UI 拖拽限制功能的游戏对象。</param>
        /// <param name="titleHight">标题的高度，用于控制拖拽触发区域，默认值为 40。</param>
        public void UIObjectsAddDrag()
        {
            foreach (var item in StandardProperty.Instance.draggableUIObjects)
            {
                item.draggableUI.AddComponent<UIDragClamp>().titleHight = item.titleHight;
                item.draggableUI.GetComponent<RectTransform>().anchoredPosition =new Vector2(item.offset.x,-item.offset.y);
            }            
        }
        /// <summary>
        /// 对 UI 元素进行定位。根据 3D 对象的世界位置计算其在屏幕上的位置，
        /// 并根据 UI 父对象的不同，设置 UI 元素的锚点位置。
        /// </summary>
        public void UIPositioning()
        {
            if (StandardProperty.Instance.minMaxWindows.isOpen)
            {
                StandardProperty.Instance.minMaxWindows.posUI.gameObject.SetActive(true);
                Vector3 worldPosition = StandardProperty.Instance.minMaxWindows.target3Dobj.position;
                Vector3 screenPosition = StandardProperty.Instance.minMaxWindows.uiCamera.WorldToScreenPoint(worldPosition);
                if (StandardProperty.Instance.minMaxWindows.windows.parent.name == "Canvas")
                {   
                    var x = StandardProperty.Instance.minMaxWindows.canvas.sizeDelta.x;
                    var y = StandardProperty.Instance.minMaxWindows.canvas.sizeDelta.y;
                    StandardProperty.Instance.minMaxWindows.posUI.anchoredPosition = new Vector3(screenPosition.x * x / 1920f, screenPosition.y * y / 1080f, 0);
                }
                else
                {               
                    var x = StandardProperty.Instance.minMaxWindows.windows.parent.GetComponent<RectTransform>().sizeDelta.x;
                    var y = StandardProperty.Instance.minMaxWindows.windows.parent.GetComponent<RectTransform>().sizeDelta.y-40;
                    var w = StandardProperty.Instance.minMaxWindows.canvas.sizeDelta.x;
                    var h = StandardProperty.Instance.minMaxWindows.canvas.sizeDelta.y;
                    var p = StandardProperty.Instance.minMaxWindows.windows.parent.GetComponent<RectTransform>().anchoredPosition;
                    var px = p.x;
                    var py = p.y-StandardProperty.Instance.minMaxWindows.windows.parent.GetComponent<RectTransform>().sizeDelta.y;
                    StandardProperty.Instance.minMaxWindows.posUI.anchoredPosition = new Vector2(screenPosition.x * x / 1920f  + px,( screenPosition.y * y / 1080f  +h+ py));//, StandardProperty.Instance.minMaxWindows.posUI.anchoredPosition.z
                }
                StandardProperty.Instance.minMaxWindows.posUI.transform.SetAsLastSibling();
            }
            else
            {
                StandardProperty.Instance.minMaxWindows.miniBorder.gameObject.SetActive(false);
                StandardProperty.Instance.minMaxWindows.posUI.gameObject.SetActive(false);
            }
        }
        /// <summary>
        /// 切换最小化和最大化窗口的状态。
        /// 根据传入的布尔值参数，决定是将窗口切换到最小化状态还是恢复到原始状态。
        /// </summary>
        /// <param name="isChange">如果为 true，则将窗口切换到最小化状态；如果为 false，则将窗口恢复到原始状态。</param>
        public void ChangeMinMaxWin(bool isChange) {

            if (isChange)
            {
                StandardProperty.Instance.minMaxWindows.miniBorder.gameObject.SetActive(true);
                StandardProperty.Instance.minMaxWindows.windows.SetParent(StandardProperty.Instance.minMaxWindows.miniBorder);
                StandardProperty.Instance.minMaxWindows.windows.anchorMin = Vector2.zero; // 左下角锚点
                StandardProperty.Instance.minMaxWindows.windows.anchorMax = Vector2.one;  // 右上角锚点
                StandardProperty.Instance.minMaxWindows.windows.offsetMin = Vector2.zero; // Left=0, Bottom=0
                StandardProperty.Instance.minMaxWindows.windows.offsetMax = new Vector2(0, -40f); // Right=0, Top=0
                StandardProperty.Instance.minMaxWindows.windows.SetSiblingIndex(StandardProperty.Instance.minMaxWindows.windows.parent.childCount-2);
            }
            else {
                StandardProperty.Instance.minMaxWindows.windows.SetParent(StandardProperty.Instance.minMaxWindows.miniBorder.transform.parent);
                StandardProperty.Instance.minMaxWindows.miniBorder.gameObject.SetActive(false);
                StandardProperty.Instance.minMaxWindows.windows.anchorMin = Vector2.zero; // 左下角锚点
                StandardProperty.Instance.minMaxWindows.windows.anchorMax = Vector2.one;  // 右上角锚点
                StandardProperty.Instance.minMaxWindows.windows.offsetMin = Vector2.zero; // Left=0, Bottom=0
                StandardProperty.Instance.minMaxWindows.windows.offsetMax = Vector2.zero; // Right=0, Top=0
                StandardProperty.Instance.minMaxWindows.windows.SetAsFirstSibling();
            }
        }
    }
    public class StandrdControlMethod : MonoBehaviour
    {
        /// <summary>
        /// 处理鼠标点击射线检测，并对检测到的物体执行一系列操作。
        /// 该方法为虚方法，允许派生类进行重写。
        /// </summary>
        /// <param name="camera">用于发射射线的相机。</param>
        /// <param name="distance">射线的检测距离。</param>
        /// <param name="layerMask">射线检测的图层掩码，用于过滤检测的对象。</param>
        /// <param name="actions">检测到物体后要执行的操作列表，每个操作接收一个 Transform 类型的参数。</param>
        public virtual void MainCameraRayControl(Camera camera,float distance,LayerMask layerMask,List<Action<Transform>> actions) 
        {
            if (Input.GetMouseButtonDown(0)) 
            {
                Ray ray = camera.ScreenPointToRay(Input.mousePosition);
                RaycastHit hit;
                if (Physics.Raycast(ray, out hit,distance,layerMask)) 
                {
                    if(actions!=null)
                    {
                        foreach (var action in actions)
                        {
                            action?.Invoke(hit.collider.gameObject.transform);
                        }
                    }
                }
            }
        }
        /// <summary>
        /// 基于 UI 相机和小地图执行射线检测。当鼠标在小地图上点击时，发射一条射线并打印击中物体的名称。
        /// </summary>
        /// <param name="MiniMap">小地图的 RawImage 组件，用于获取鼠标在小地图上的位置。</param>
        /// <param name="uiCamera">用于发射射线的 UI 相机。</param>
        /// <param name="distance">射线的检测距离。</param>
        /// <param name="layerMask">射线检测的图层掩码，用于过滤检测的对象。</param>
        /// <param name="actions">检测到物体后要执行的操作列表，每个操作接收一个 Transform 类型的参数。</param>
        public void UiCameraRayControl(RawImage MiniMap, Camera uiCamera, float distance, LayerMask layerMask, List<Action<Transform>> actions)
        {
            Vector2 localPoint;
            RectTransformUtility.ScreenPointToLocalPointInRectangle(MiniMap.rectTransform,Input.mousePosition,null,out localPoint);
            Vector2 uv = new Vector2((localPoint.x + MiniMap.rectTransform.rect.width / 2) / MiniMap.rectTransform.rect.width,(localPoint.y + MiniMap.rectTransform.rect.height / 2) / MiniMap.rectTransform.rect.height);
            Vector3 viewportPos = new Vector3(uv.x, uv.y, uiCamera.nearClipPlane);
            Ray ray = uiCamera.ViewportPointToRay(viewportPos);
            RaycastHit hit;            
            if (Physics.Raycast (ray, out hit, distance, layerMask)) {
                if (Input.GetMouseButtonDown(0))
                {
                    if(actions!=null)
                    {
                        foreach (var action in actions)
                        {
                            action?.Invoke(hit.collider.gameObject.transform);
                        }
                    }
                }
            }
        }
    }








    public class UIDragClamp : MonoBehaviour, IBeginDragHandler, IDragHandler,IEndDragHandler
    {
        [Header("UI限制拖拽位置")]
        public RectTransform container;

        RectTransform rt;

        // 位置偏移量
        Vector3 offset = Vector3.zero;
        // 最小、最大X、Y坐标
        float minX, maxX, minY, maxY;
        public float titleHight = 40;
        void Start()
        {
            rt = GetComponent<RectTransform>();
        }
        bool isHave = false;
        public void OnBeginDrag(PointerEventData eventData)
        {
            if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.enterEventCamera, out Vector3 globalMousePos))
            {
                isHave = false;
                if (Mathf.Abs(rt.position.y - globalMousePos.y) < titleHight) {
                    // 计算偏移量
                    isHave = true;                    
                }

                if (isHave)
                {
                    offset = rt.position - globalMousePos;
                    // 设置拖拽范围
                    SetDragRange();
                    this.transform.SetAsLastSibling();
                }
            }
        }

        public void OnDrag(PointerEventData eventData)
        {
            // 将屏幕空间上的点转换为位于给定RectTransform平面上的世界空间中的位置
            if (isHave)
            {
                if (RectTransformUtility.ScreenPointToWorldPointInRectangle(rt, eventData.position, eventData.pressEventCamera, out Vector3 globalMousePos))
                {
                    rt.position = DragRangeLimit(globalMousePos + offset);
                }
            }

        }
        public void OnEndDrag(PointerEventData eventData)
        {
            
        }
        //通过计算忽略Scale和pivot对UI的影响
        // 设置最大、最小坐标
        void SetDragRange()
        {
            if (container)
            {
                // 最小x坐标 = 容器当前x坐标 - 容器轴心距离左边界的距离 + UI轴心距离左边界的距离
                minX = container.position.x
                - container.pivot.x * container.rect.width
                + rt.rect.width * rt.localScale.x * rt.pivot.x;

                // 最大x坐标 = 容器当前x坐标 + 容器轴心距离右边界的距离 - UI轴心距离右边界的距离
                maxX = container.position.x
                + (1 - container.pivot.x) * container.rect.width
                - rt.rect.width * rt.localScale.x * (1 - rt.pivot.x);

                // 最小y坐标 = 容器当前y坐标 - 容器轴心距离底边的距离 + UI轴心距离底边的距离
                minY = container.position.y
                - container.pivot.y * container.rect.height
                + rt.rect.height * rt.localScale.y * rt.pivot.y;

                // 最大y坐标 = 容器当前x坐标 + 容器轴心距离顶边的距离 - UI轴心距离顶边的距离
                maxY = container.position.y
                + (1 - container.pivot.y) * container.rect.height
                - rt.rect.height * rt.localScale.y * (1 - rt.pivot.y);
            }
            else
            {
                minX = rt.rect.width * rt.pivot.x;
                maxX = Screen.width - rt.rect.width * (1 - rt.pivot.x);
                minY = rt.rect.height * rt.pivot.y;
                maxY = Screen.height - rt.rect.height * (1 - rt.pivot.y);
            }
        }


        // 限制坐标范围
        Vector3 DragRangeLimit(Vector3 pos)
        {
            pos.x = Mathf.Clamp(pos.x, minX, maxX);
            pos.y = Mathf.Clamp(pos.y, minY, maxY);
            return pos;
        }
    }
}
