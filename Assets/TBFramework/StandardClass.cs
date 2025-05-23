using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Threading.Tasks;
using System;
using DG.Tweening;
using System.Threading;
namespace TBFramework
{
    public class StandardClass :  iTest//MonoBehaviour,
    {
        [SerializeField] public TestStatus Status { get; set; }
        public string standerdName { get; set; }

        public Queue<IEnumerator> AsyncQueue = new Queue<IEnumerator>();

        public StandardClass(string name)
        { 
            standerdName = name; 
        }
        // 添加一个方法用于向 AsyncQueue 中添加元素
        public void AddToAsyncQueue(IEnumerator iEnumerator)
        {
            // 使用 Enqueue 方法添加元素
            if (AsyncQueue == null)
            {
                AsyncQueue = new Queue<IEnumerator>();
            }
            AsyncQueue.Enqueue(iEnumerator);
        }
        /// <summary>
        /// 方法使用的方式
        /// </summary>
        /// <param name="T">泛型</param>
        public void ChangeGameObject<T>()
        {
            if (typeof(T) == typeof(GameObject))
            {
                Debug.Log("ChangeGameObject: GameObject");
            }
        }
        /// <summary>
        /// 异步方法
        /// </summary>
        /// <param name="ps">ps为传递参数</param>
        public Transform trans;
        public async Task ObjMoveAndRotateMethod(CancellationToken token,params object[] ps)
        {
            var tempG = ps[0] as Transform;            
            trans = tempG;
            var moveTween =tempG.DOMove((Vector3)ps[1],(float)ps[3]);
            var rotateTween = tempG.DORotate((Vector3)ps[2],(float)ps[3]);
            try
            {
                // 等待指定时间，并检查取消请求
                await Task.Delay((int)((float)ps[3] * 1000), token);
                if (!token.IsCancellationRequested)
                {
                   
                }
            }
            catch (OperationCanceledException)
            {
                    // 取消操作时，停止Tween
                    moveTween.Kill();
                    rotateTween.Kill();
                  //  Debug.Log("异步操作已取消");
            }            
        }

        /// <summary>
        /// 销毁与指定变换关联的所有 DOTween 动画。
        /// 如果 <see cref="trans"/> 不为 null，则调用 <see cref="DOTween.Kill(UnityEngine.Object)"/> 方法来停止并移除与该变换相关的所有动画。
        /// </summary>
        public void DestoryDotween()
        {
            // 检查 trans 是否不为 null
            if(trans != null)
            {
                // 销毁与 trans 关联的所有 DOTween 动画
                DOTween.Kill(trans);
            }
        }

        public void ActionMothed(Action action)
        {
            action?.Invoke();
        }

        // 修改ExecuteAsyncMethod以接受CancellationToken
        /// <summary>
        /// 执行异步方法的协程。根据传入的测试实例、状态和取消令牌，调用相应的异步任务，并等待任务完成。
        /// </summary>
        /// <param name="testInstance">标准类的实例，用于调用异步方法。</param>
        /// <param name="status">测试状态，决定调用哪个异步方法。</param>
        /// <param name="cancellationToken">用于取消异步任务的取消令牌。</param>
        /// <param name="ps">传递给异步方法的可变参数。</param>
        /// <returns>返回一个协程枚举器。</returns>
        public IEnumerator ExecuteAsyncMethod(StandardClass testInstance, TestStatus status, CancellationToken cancellationToken, params object[] ps) 
        {
            // 声明一个任务变量，用于存储异步任务
            Task task = null;
            // 根据不同的状态调用不同的异步方法
            switch (status)
            {
                // 当状态为 Ready 时，调用 ObjMoveAndRotateMethod 方法
                case TestStatus.MOVEANDROTATE:
                    task = testInstance.ObjMoveAndRotateMethod(cancellationToken,ps);
                    break;
                
            }

            // 等待任务完成
            while (!task.IsCompleted)
            {
                // 如果取消令牌请求取消任务，则退出协程
                if (cancellationToken.IsCancellationRequested)
                {
                    yield break;
                }
                // 等待下一帧
                yield return null;
            }

            // 检查任务是否出错
            if (task.IsFaulted)
            {
                // 若任务出错，注释掉的代码表示输出错误日志
                // Debug.LogError($"异步方法执行出错: {task.Exception}");
            }
            else
            {
                // 若任务成功完成，输出成功日志
                Debug.Log("异步方法执行成功");
            }
        }
        /// <summary>
        /// 启动运行异步方法的协程。
        /// </summary>
        /// <param name="testInstance">标准类的实例，用于处理异步队列。</param>
        /// <returns>返回一个协程枚举器。</returns>
        public IEnumerator StartRunAsyncMethod(StandardClass testInstance)
        {
            while (true)
            {
                while (testInstance?.AsyncQueue?.Count > 0) 
                {
                    yield return testInstance.AsyncQueue.Dequeue(); 
                }
                yield return null;
            }
        } 
        public IEnumerator StartCoroutineMethod()
        {
            foreach(IEnumerator item in StandardProperty.Instance.step)
            {
                yield return item;
            }
        }

    }
}
