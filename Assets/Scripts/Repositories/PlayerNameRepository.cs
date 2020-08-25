using UnityEngine;

namespace RoboBlast.Repositories
{
    public static class PlayerNameRepository
    {
        public const string PlayerNameKey = "PlayerName";

        public static string PlayerName
        {
            get
            {
                if (PlayerPrefs.HasKey(PlayerNameKey))
                    return PlayerPrefs.GetString(PlayerNameKey);
                
                return "NO NAME SET";
            }

            set
            {
                PlayerPrefs.SetString(PlayerNameKey, value);
            }
        }
    }
}