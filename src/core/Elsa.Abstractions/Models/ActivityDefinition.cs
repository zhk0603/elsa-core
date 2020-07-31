using Elsa.Services.Models;
using Newtonsoft.Json.Linq;

namespace Elsa.Models
{
    public class ActivityDefinition
    {
        public static ActivityDefinition FromActivity(IActivity activity)
        {
            return new ActivityDefinition(activity.Id, activity.Type, activity.State, 0, 0);
        }

        public ActivityDefinition()
        {
            State = new JObject();
        }

        public ActivityDefinition(string id, string type, JObject state, int left = 0, int top = 0)
        {
            Id = id;
            Type = type;
            Left = left;
            Top = top;
            State = new JObject(state);
            Name = GetStateValue<string>("name");
            DisplayName = GetStateValue<string>("title");
            Description = GetStateValue<string>("description");
        }

        public string Id { get; set; }
        public string Type { get; set; }
        public string Name { get; set; }
        public string DisplayName { get; set; }
        public string Description { get; set; }
        public int Left { get; set; }
        public int Top { get; set; }
        public JObject State { get; set; }

        private T GetStateValue<T>(string propertyName)
        {
            return State.ContainsKey(propertyName) ? State[propertyName].Value<T>() : default;
        }
    }

    public class ActivityDefinition<T> : ActivityDefinition where T : IActivity
    {
        public ActivityDefinition(string id, JObject state, int left = 0, int top = 0) : base(
            id,
            typeof(T).Name,
            state,
            left,
            top)
        {
        }

        public ActivityDefinition(string id, object state, int left = 0, int top = 0) : base(
            id,
            typeof(T).Name,
            JObject.FromObject(state),
            left,
            top)
        {
        }
    }
}