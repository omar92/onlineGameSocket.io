using SocketIO;
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Server
{
    [RequireComponent(typeof(Server_EntityID))]
    public class Server_SyncTransform : MonoBehaviour
    {
        public bool IsEmitter;

        string EID = "";
        private void Awake()
        {
            EID = gameObject.GetComponent<Server_EntityID>().entityID;
        }

        [System.Serializable]
        struct transformData
        {
            public float x;
            public float y;
            public float z;

            public void FromVector3(Vector3 vector3)
            {
                x = vector3.x;
                y = vector3.y;
                z = vector3.z;
            }

            public Vector3 ToVector3()
            {
                return new Vector3(x, y, z);
            }
        }


        struct Data
        {
            public string EID;
            public transformData TData;

        }

        private void Start()
        {
            if (!IsEmitter)
            {
                Server_SIO.inistance.On("sync", OnSync);
            }
        }

        private void OnSync(SocketIOEvent obj)
        {
            var syncPos = JsonUtility.FromJson<transformData>(obj.data.ToString()).ToVector3();
            transform.position = syncPos;
        }

        private void Update()
        {
            if (IsEmitter)
            {
                var data = new transformData();
                data.FromVector3(transform.position);
                Server_SIO.inistance.Emit("sync", new JSONObject(JsonUtility.ToJson(data)));
            }
        }

    }
}