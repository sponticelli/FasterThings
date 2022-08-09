using UnityEngine;

namespace FasterThings
{
    public class TrackManager : MonoBehaviour
    {
        public bool ClosedLoopTrack = true;
        
        public int Laps = 3;
        
        public GameObject[] Checkpoints;
    }
}