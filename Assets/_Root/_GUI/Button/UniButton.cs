using UnityEditor;
using UnityEngine;
using UnityEngine.UI;

namespace Gamee.Hiuk.Button
{
    [RequireComponent(typeof(Image))]
    public class UniButton : UnityEngine.UI.Button
    {
        [Header("Custom")]
        [SerializeField] internal bool isCustom; 
        [SerializeField] internal Sound soundCustom; 

        protected override void Start()
        {
            this.onClick.AddListener(OnClickButton);
        }

        void OnClickButton() 
        {
            if (!isCustom) AudioButton.Play();
            else AudioButton.Play(soundCustom);
        }
    }

#if UNITY_EDITOR
    [CustomEditor(typeof(UniButton))]
    [CanEditMultipleObjects]
    public class UniButtonEditor : UnityEditor.UI.ButtonEditor
    {
        private UniButton uniButton;
        private SerializedProperty sound;

        protected override void OnEnable()
        {
            base.OnEnable();

            uniButton = (UniButton)target;
            sound = serializedObject.FindProperty("soundCustom");
        }

        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            uniButton.isCustom = EditorGUILayout.Toggle("Is Custom", uniButton.isCustom);
            if (uniButton.isCustom)
            {
                EditorGUILayout.PropertyField(sound);
            }
            serializedObject.ApplyModifiedProperties();
        }
    }
#endif

}

