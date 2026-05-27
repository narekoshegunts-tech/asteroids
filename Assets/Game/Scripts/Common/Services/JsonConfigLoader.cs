using Newtonsoft.Json;
using UnityEngine;

namespace Game.Scripts.Common.Services
{
    public abstract class JsonConfigLoader<TRoot> where TRoot : class
    {
        private readonly string _resourcePath;

        protected JsonConfigLoader(string resourcePath)
        {
            _resourcePath = resourcePath;
        }

        protected TRoot LoadRoot()
        {
            TextAsset textAsset = Resources.Load<TextAsset>(_resourcePath);

            return JsonConvert.DeserializeObject<TRoot>(textAsset.text);
        }
    }
}