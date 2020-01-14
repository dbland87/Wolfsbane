using System.Collections;
using UnityEngine;
using Valve.VR.InteractionSystem;

namespace Utils
{
    public class Utils
    {
        [System.Serializable]
        public class Timer : MonoBehaviour
        {
            private System.Action callback;
            private float triggerTime;
            private bool timerActive = false;
            private bool triggerOnEarlyDestroy = false;

            //-------------------------------------------------
            public void Init( float time, System.Action callback, bool earlydestroy )
            {
                triggerTime = time;
                this.callback = callback;
                triggerOnEarlyDestroy = earlydestroy;
                timerActive = true;
                StartCoroutine( Wait() );
            }


            //-------------------------------------------------
            private IEnumerator Wait()
            {
                yield return new WaitForSeconds( triggerTime );
                timerActive = false;
                callback.Invoke();
                Destroy( this );
            }


            //-------------------------------------------------
            void OnDestroy()
            {
                if ( timerActive )
                {
                    //If the component or its GameObject get destroyed before the timer is complete, clean up
                    StopCoroutine( Wait() );
                    timerActive = false;

                    if ( triggerOnEarlyDestroy )
                    {
                        callback.Invoke();
                    }
                }
            }
        }
        
        public static void AddTimer( GameObject go, float time, System.Action callback, bool triggerIfDestroyedEarly = false )
        {
            var timerComponent = go.AddComponent<Timer>();
            timerComponent.Init( time, callback, triggerIfDestroyedEarly );
        }
    }
}
