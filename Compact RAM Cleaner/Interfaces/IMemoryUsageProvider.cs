namespace Compact_RAM_Cleaner
{
    public interface IMemoryUsageProvider
    {
        int CurrentUsage { get; }
        string CurrentUsageString { get; }
        ulong AvailableMemoryInBytes { get; }
        ulong TotalMemoryInBytes { get; }
    }
}
