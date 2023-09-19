using Newtonsoft.Json;

namespace CoordinateConverter.GitHub
{
    /// <summary>
    /// This data structure represents the relevant data present in the github release api<br></br>
    /// at https://api.github.com/repos/ORG/REPO/releases/latest
    /// </summary>
    public class GitHubRelease
    {
        /// <summary>
        /// Gets or sets the name.
        /// </summary>
        /// <value>
        /// The name.
        /// </value>
        [JsonProperty("tag_name")]
        public string Name { set; get; }
        /// <summary>
        /// Gets or sets the URL.
        /// </summary>
        /// <value>
        /// The URL.
        /// </value>
        [JsonProperty("html_url")]
        public string URL { get; set; }
    }
}
