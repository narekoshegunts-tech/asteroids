using Game.Scripts.Common.Services;

namespace Game.Scripts.Features.Enemies.Ufo.Data
{
    public class UfoDataService: JsonConfigLoader<UfoDataRoot>
    {
        private const string ResourcePath = "Configs/ufo";
        
        
        public int PoolSize { get; private set; }
        public float SpawnCooldown { get; private set; }

        public UfoData UfoData { get; private set; }
        
        public UfoDataService(): base(ResourcePath)
        {
            LoadConfig();
        }
        
        private void LoadConfig()
        {
            var root = LoadRoot();

            UfoData = root.UfoData;
            
            PoolSize = root.PoolSize;
            
            SpawnCooldown = root.SpawnCooldown;
        }
        
    }
}