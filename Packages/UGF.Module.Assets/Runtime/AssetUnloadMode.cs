namespace UGF.Module.Assets.Runtime
{
    public enum AssetUnloadMode
    {
        /// <summary>
        /// Unloads asset with tracking.
        /// </summary>
        Track = 0,
        /// <summary>
        /// Changes asset track count without unloading.
        /// </summary>
        TrackOnly = 1,
        /// <summary>
        /// Unloads asset using loader directly without tracking.
        /// </summary>
        Direct = 2
    }
}
