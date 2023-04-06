using TMPro;
using UnityEditor;
using UnityEngine;
using Gamee.Hiuk.Component;

public class SpinItem : MonoBehaviour
{
    [SerializeField] Collisider2DComponent collisider;
    [SerializeField] int value;
    [SerializeField] TextMeshProUGUI txtSellect;
    [SerializeField] TextMeshProUGUI txtUnSellect;

    public System.Action<SpinItem> ActionCollisider;
    public int Value => value;
    private void OnEnable()
    {
        collisider.ActionTriggerEnter2D += OnTriggerEnterEvent;
    }
    private void OnDisable()
    {
        collisider.ActionTriggerEnter2D -= OnTriggerEnterEvent;
    }
    void OnTriggerEnterEvent(Collider2D col) 
    {
        ActionCollisider?.Invoke(this);
    }
    public void UpdateView() 
    {
        if (txtSellect == null || txtUnSellect == null) return;
        txtSellect.text = "X" + value;
        txtUnSellect.text = "X" + value;
    }
    public void Sellect() 
    {
        UpdateTextSellect(true);
    }
    public void UnSellect()
    {
        UpdateTextSellect(false);
    }
    private void UpdateTextSellect(bool isSellect = false)
    {
        if (txtSellect == null || txtUnSellect == null) return;
        txtSellect.gameObject.SetActive(isSellect);
        txtUnSellect.gameObject.SetActive(!isSellect);
    }
}
#if UNITY_EDITOR
[CustomEditor(typeof(SpinItem))]
[CanEditMultipleObjects]
public class SpinItemEditor : Editor
{
    SpinItem spinItem;

    void OnEnable()
    {
        spinItem = target as SpinItem;
    }
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        serializedObject.Update();
        spinItem.UnSellect();
        spinItem.UpdateView();
        serializedObject.ApplyModifiedProperties();
    }
}
#endif
