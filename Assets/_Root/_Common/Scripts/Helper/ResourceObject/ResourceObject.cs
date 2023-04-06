using System.IO;
using UnityEngine;
namespace Gamee.Hiuk.Helper 
{
    public abstract class ResourceObject<T> : ScriptableObject where T : ScriptableObject
    {
        private static T instance;
        public static T Instance => instance ??= Resources.Load<T>(typeof(T).Name);
    }
}