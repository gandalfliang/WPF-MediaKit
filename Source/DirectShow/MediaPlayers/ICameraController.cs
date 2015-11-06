namespace WPFMediaKit.DirectShow.MediaPlayers
{
    public interface ICameraController
    {
        int MaxExposure { get; }
        int MinExposure { get; }
        int DefaultExposure { get; }
        int MaxFocus { get; }
        int MinFocus { get; }
        int DefaultFocus { get; }

        void SetExposure(int value);
        void SetFocus(int value);
        void SetToAuto();
    }
}
