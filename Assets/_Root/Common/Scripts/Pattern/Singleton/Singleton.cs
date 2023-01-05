namespace Gamee.Hiuk.Pattern 
{
    using UnityEngine;
    public class Singleton<T> : MonoBehaviour where T : Component
    {
        [SerializeField] protected bool dontDestroy;
        private static T instance;

        public static T Instance
        {
            get
            {
                if (instance != null) return instance;

                instance = (T)FindObjectOfType(typeof(T));
                if (instance == null)
                {
                    GameObject obj = new GameObject();
                    obj.name = typeof(T).Name;
                    instance = obj.AddComponent<T>();
                }
                return instance;
            }
        }

        protected virtual void Awake()
        {
            if (dontDestroy && CheckInstance()) DontDestroyOnLoad(gameObject);
        }

        protected bool CheckInstance()
        {
            if (this == Instance) return true;

            Destroy(this);
            return false;
        }
    }
}
