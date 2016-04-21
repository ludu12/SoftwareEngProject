using UnityEngine;
using System.Collections;

public interface ICarAudioController {

    void StartSound();
    void StopSound();

    void AdjustPitch(float pitch);
    void AdjustVolumes(float lowFade, float highFade, float decFade, float accFade);
}
