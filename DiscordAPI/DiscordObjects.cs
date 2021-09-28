using System.Collections.Generic;

namespace DiscordAPI
{
    class DiscordContent
    {
        public string Content { get; set; }
        public List<DiscordComponent> Components { get; set; }
        public List<DiscordEmbedObject> Embeds { get; set; }

    }

    class DiscordComponent
    {
        public int Type { get; set; }
        public List<DiscordComponent> Components { get; set; }

        public List<string> Embeds { get; set; }

    }
    class DiscordEmbedObject
    {
        public string Title { get; set; }
        public string Description { get; set; }
        public string Url { get; set; }
    }

    class DiscordButtonComponent : DiscordComponent
    {
        public string Label { get; set; }
        public int Style { get; set; }
        public string CustomId { get; set; }
        public string Url { get; set; }
    }
}
