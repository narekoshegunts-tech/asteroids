using Firebase;
using Firebase.Analytics;
using Firebase.Extensions;
using UnityEngine;

namespace Game.Scripts.SDK
{
    public class FirebaseManager : MonoBehaviour
    {
        private FirebaseApp app;
        private bool isFirebaseReady = false;
        private bool gameStartedEventSent = false;

        private void Start()
        {
            FirebaseApp.CheckAndFixDependenciesAsync().ContinueWithOnMainThread(task =>
            {
                var dependencyStatus = task.Result;
                if (dependencyStatus == DependencyStatus.Available)
                {
                    app = FirebaseApp.DefaultInstance;
                    isFirebaseReady = true;

                    Debug.Log("Firebase initialized successfully");

                    SendGameStartedEvent();
                }
                else
                {
                    Debug.LogError($"Could not resolve all Firebase dependencies: {dependencyStatus}");
                }
            });
        }

        private void SendGameStartedEvent()
        {
            if (!isFirebaseReady || gameStartedEventSent)
                return;

            FirebaseAnalytics.LogEvent("GameStarted");

            gameStartedEventSent = true;

            Debug.Log("Firebase Event: GameStarted отправлено");
        }
    }
}