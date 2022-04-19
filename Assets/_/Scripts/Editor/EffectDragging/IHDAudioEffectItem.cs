namespace HerbiDino.Audio
{
    public interface IHDAudioEffectItem
    {
        int Index { get; set; }
        void RegisterPointerEvents();
    }
}