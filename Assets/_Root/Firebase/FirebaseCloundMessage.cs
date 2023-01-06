namespace Gamee.Hiuk.FirebaseCloundMessage 
{
    using UnityEngine;
    public class FirebaseCloundMessage : MonoBehaviour
    {
        public void Init()
        {
            Debug.Log("[FirebaseMessage] init completed!");
            Firebase.Messaging.FirebaseMessaging.TokenReceived += OnTokenReceived;
            Firebase.Messaging.FirebaseMessaging.MessageReceived += OnMessageReceived;

            Subscribe();
        }

        public void OnTokenReceived(object sender, Firebase.Messaging.TokenReceivedEventArgs token)
        {
            UnityEngine.Debug.Log("Received Registration Token: " + token.Token);
        }

        public void OnMessageReceived(object sender, Firebase.Messaging.MessageReceivedEventArgs e)
        {
            UnityEngine.Debug.Log("Received a new message from: " + e.Message.From);
        }

        void Subscribe()
        {
            Firebase.Messaging.FirebaseMessaging.SubscribeAsync("/topics/new_message");
        }
    }
}

