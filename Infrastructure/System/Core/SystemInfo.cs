using GameBalance.Infrastructure.System.Componets;

namespace GameBalance.Infrastructure.System.Core
{
    public class SystemInfo()
    {
        public OSInfo OS { get; set; }
        public CpuInfo CPU { get; set; }
        public GpuInfo GPU { get; set; }
        public MemoryInfo Memory { get; set; }
        public StorageInfo Storage { get; set; }
        public MotherboardInfo Motherboard { get; set; }
        public NetworkInfo Network { get; set; }
    }
}
