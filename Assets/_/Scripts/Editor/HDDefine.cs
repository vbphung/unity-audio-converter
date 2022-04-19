namespace HerbiDino.Audio
{
    public struct HDEditor
    {
        public const string TitleText = "title";
    }

    public struct HDStorageView
    {
        public const string InputField = "storagePath";
        public const string CheckButton = "checkStoragePath";
        public const string ValidityText = "validStoragePath";
    }

    public struct HDMixerView
    {
        public const string ListView = "mixerLs";
        public const string View = "mixer";
        public const string NameText = "newMixer";
        public const string CreateButton = "createMixerBtn";
        public const string RemoveButton = "removeMixerBtn";
    }

    public struct HDEffectView
    {
        public const string ListView = "effectLs";
        public const string View = "effect";
        public const string DragDestination = "effectDragDestination";
        public const string DragDestinationHover = "effectDragDestinationHover";
        public const string TypeDropdown = "effectType";
        public const string CreateButton = "createEffectBtn";
        public const string RemoveButton = "removeEffectBtn";
    }
}