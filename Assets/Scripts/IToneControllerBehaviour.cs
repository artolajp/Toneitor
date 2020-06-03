namespace Toneitor
{
    public interface IToneControllerBehaviour
    {
        bool IsPlaying { get; }
        OctaveTone CurrentTone { get; }

        void MuteTone();
        void PlayTone(OctaveTone octaveTone);
    }
}