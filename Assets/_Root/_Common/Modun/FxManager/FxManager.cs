using Gamee.Hiuk.Component;
using Gamee.Hiuk.Pattern;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FxManager : Singleton<FxManager>
{
    [SerializeField] AudioComponent fxAudio;
    [SerializeField, Range(0, 1f)] float timeNextPlayWait = 0.1f;
    [Header("Fx")]

    List<GameObject> listFx = new List<GameObject>();
    GameObject fxCurrent = null;
    bool isLoop = false;
    Coroutine coroutine;
    WaitForSeconds waitTime;

    public void Start()
    {
        waitTime = new WaitForSeconds(timeNextPlayWait);
    }
    public GameObject CreateFx(FxItem fxItem, Vector3 pos, bool release = true, float timeRelease = 2)
    {
        if (fxItem == null) return null;
        fxAudio.PlaySound(fxItem.FxSound);
        return CreateObjectFx(fxItem.Fx, pos, release, timeRelease);
    }
    public GameObject CreateFx(GameObject fx, Vector3 pos, bool release = true, float timeRelease = 2)
    {
        if (fx == null) return null;
        return CreateObjectFx(fx, pos, release, timeRelease);
    }
    GameObject CreateObjectFx(GameObject fx, Vector3 pos, bool release = true, float timeRelease = 2) 
    {
        if (fx == fxCurrent)
        {
            isLoop = true;
        }
        else
        {
            isLoop = false;
            fxCurrent = fx;
        }
        coroutine = StartCoroutine(WaitPlaySoundTime());

        if (isLoop) return null;
        GameObject obj = PoolManager.SpawnObject(fx, pos, fx.transform.rotation);
        listFx.Add(obj);
        if (release) StartCoroutine(IEReleaseFx(obj, timeRelease));
        return obj;
    }
    GameObject CreateObject(GameObject fx, Vector3 pos, bool release = false, float timeRelease = 2)
    {
        if (fx == null) return null;

        GameObject obj = PoolManager.SpawnObject(fx, pos, fx.transform.rotation);
        listFx.Add(obj);
        if (release) StartCoroutine(IEReleaseFx(obj, timeRelease));
        return obj;
    }
    IEnumerator WaitPlaySoundTime()
    {
        yield return waitTime;
        isLoop = false;
        fxCurrent = null;
    }
    private void OnDisable()
    {
        isLoop = false;
        fxCurrent = null;
        if (coroutine != null) StopCoroutine(coroutine);
    }
    IEnumerator IEReleaseFx(GameObject fx, float time = 2)
    {
        yield return new WaitForSeconds(time);
        PoolManager.ReleaseObject(fx);
        listFx.Remove(fx);
    }
    public void ReleaseAllFx()
    {
        for(int i = 0; i< listFx.Count; i++) 
        {
            if (listFx[i].activeInHierarchy)
            {
                PoolManager.ReleaseObject(listFx[i]);
            }
        }
        listFx.Clear();
    }
    public void ReleaseFx(GameObject fx) 
    {
        PoolManager.ReleaseObject(fx);
    }
    #region static api
    public static GameObject Create(GameObject fx, Vector3 pos, bool release = true, float timeRelease = 2)
    {
        return Instance.CreateFx(fx, pos, release, timeRelease);
    }
    public static GameObject CreateObj(GameObject fx, Vector3 pos, bool release = false, float timeRelease = 2)
    {
        return Instance.CreateObject(fx, pos, release, timeRelease);
    }
    public static void ReleaseAll() 
    {
        Instance.ReleaseAllFx(); 
    }
    public static void Release(GameObject fx)
    {
        Instance.ReleaseFx(fx);
    }
    #endregion
    [System.Serializable]
    public class FxItem 
    {
        [SerializeField] GameObject fx;
        [SerializeField] Sound fxSound;

        public GameObject Fx => fx;
        public Sound FxSound => fxSound;
    }
}