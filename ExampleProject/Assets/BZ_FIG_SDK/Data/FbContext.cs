using System;
using System.Collections.Generic;
using Manager;
using UnityEngine;

namespace Data
{
    public class FbContext
    {
        public string id, type;
        public bool isFanpageTour;
        public List<FbPlayer> players = new();
        public FbTournament tournament = new();

        public void SetDataFromJson(string data)
        {
            var jsonData = JsonUtility.FromJson<FbContext>(data);

            id = jsonData.id;
            type = jsonData.type;
            players = jsonData.players;
            tournament = jsonData.tournament;

            Ryder.Instance.CheckUnityEditor(
                () => { AnalyticsManager.PushTrackingEvent($"user_open_from:{type}"); },
                () => { }
            );
        }

        public bool IsInTournament()
        {
            return !string.IsNullOrEmpty(tournament.id_tour);
        }

        public bool IsSameTournament(string value)
        {
            return Equals(value, tournament.id_tour);
        }

        public void SetIsFanpageTour(bool value)
        {
            isFanpageTour = value;
        }
    }

    [Serializable]
    public class FbPlayer
    {
        public string id, name, photo;
    }

    [Serializable]
    public class FbTournament
    {
        public string tour_name, payload;
        public bool is_current;
        public double start_at, end_at;
        public string id_tour;
    }
}