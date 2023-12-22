using Data;
using UnityEngine;

namespace BZ_FIG_SDK.Scripts
{
    public class BZ_FIG_DATA : Singleton<BZ_FIG_DATA>
    {
        public FbContext FbContext = new();

        public void InitCurrentContext(string value = "")
        {
            Debug.Log($"InitCurrentContext {value}");
            FbContext.SetDataFromJson(value);
        }
    }
}