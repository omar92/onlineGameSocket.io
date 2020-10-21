using SocketIO;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Server
{
    [RequireComponent(typeof(SocketIOComponent))]
    public class Server_SIO : MonoBehaviour
    {
        public static Server_SIO inistance;
        public SocketIOComponent IO;

        private void Awake()
        {
            Application.runInBackground = true;
            inistance = this;
            IO = gameObject.GetComponent<SocketIOComponent>();
        }

        public void Emit(string ev)
        {
            IO.Emit(ev);
        }
        public void Emit(string ev, JSONObject data)
        {
            IO.Emit(ev, data);
        }
        public void Emit(string ev, JSONObject data, System.Action<JSONObject> action)
        {
            IO.Emit(ev, data, action);
        }

        public void On(string ev, System.Action<SocketIOEvent> callback)
        {
            IO.On(ev, callback);
        }
        public void Off(string ev, System.Action<SocketIOEvent> callback)
        {
            IO.Off(ev, callback);
        }
    }
}