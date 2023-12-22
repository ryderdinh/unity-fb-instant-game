using System;
using System.Collections.Generic;

namespace Data
{
    [Serializable]
    public class Cloud
    {
        public int code;
        public string message = "";
    }

    [Serializable]
    public class UserCloud
    {
        public int code;
        public UserCloudData data;
        public string message = "";
    }

    [Serializable]
    public class UserCloudData
    {
        public string id, name, avatar;
        public int highscore;
    }

    [Serializable]
    public class TournamentCloud : Cloud
    {
        public TournamentCloudData data;
    }

    [Serializable]
    public class TournamentCloudData
    {
        public string _id;
        public string id_tour;
        public string tour_name;
        public string start_at;
        public string end_at;
        public bool is_current;
        public string createdAt;
        public string updatedAt;
        public string id;
        public List<object> users;
    }
}