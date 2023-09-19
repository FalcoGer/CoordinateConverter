using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace CoordinateConverter.GitHub
{
    /// <summary>
    /// Represents the version of the software
    /// </summary>
    public class Version : IComparable<Version>
    {
        /// <summary>
        /// The GitHub organization
        /// </summary>
        public const string ORG = "FalcoGer";
        /// <summary>
        /// The GitHub repository
        /// </summary>
        public const string REPO = "CoordinateConverter";

        /// <summary>
        /// Gets the releases URL.
        /// </summary>
        /// <value>
        /// The releases URL.
        /// </value>
        public static string RELEASES_URL { get => string.Format("https://github.com/{0}/{1}/releases/latest", ORG, REPO); }

        /// <summary>
        /// Gets the major version number.
        /// </summary>
        /// <value>
        /// The major version number.
        /// </value>
        public int Major { get; private set; }
        /// <summary>
        /// Gets the minor version number.
        /// </summary>
        /// <value>
        /// The minor version number.
        /// </value>
        public int Minor { get; private set; }
        /// <summary>
        /// Gets the patch number.
        /// </summary>
        /// <value>
        /// The patch number.
        /// </value>
        public int Patch { get; private set; }

        /// <summary>
        /// Initializes a new instance of the <see cref="Version"/> class.
        /// </summary>
        /// <param name="major">The major version.</param>
        /// <param name="minor">The minor version.</param>
        /// <param name="patch">The patch number.</param>
        public Version(int major, int minor, int patch)
        {
            Major = major;
            Minor = minor;
            Patch = patch;
        }

        /// <summary>
        /// Initializes a new instance of the <see cref="Version"/> class.
        /// </summary>
        /// <param name="gitHubRelease">The git hub release.</param>
        /// <exception cref="System.ArgumentException">GitHubTag.Name must contain at least 3 version numbers.</exception>
        public Version(GitHubRelease gitHubRelease)
        {
            List<string> versionStr = gitHubRelease.Name.Split('.').ToList();
            if (versionStr.Count <= 2)
            {
                throw new ArgumentException("GitHubTag.Name must contain at least 3 version numbers, but it was " + gitHubRelease.Name + ".");
            }
            if (versionStr[0][0] == 'v')
            {
                versionStr[0] = versionStr[0].Substring(1);
            }

            Major = int.Parse(versionStr[0]);
            Minor = int.Parse(versionStr[1]);
            Patch = int.Parse(versionStr[2]);
        }

        /// <summary>
        /// Converts to string.
        /// </summary>
        /// <returns>
        /// A <see cref="System.String" /> that represents this instance.
        /// </returns>
        public override string ToString()
        {
            return string.Format("v{0}.{1}.{2}", Major, Minor, Patch);
        }

        /// <summary>
        /// Gets the latest version.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="System.NotImplementedException"></exception>
        public static Version GetLatest()
        {
            const string GITHUB_API_URL = "https://api.github.com/repos/{0}/{1}/releases/latest";
            string endpoint = string.Format(GITHUB_API_URL, ORG, REPO);

            GitHubRelease release = null;

            using (HttpClient client = new HttpClient())
            {
                client.DefaultRequestHeaders.Accept.Clear();
                client.DefaultRequestHeaders.Accept.Add(new System.Net.Http.Headers.MediaTypeWithQualityHeaderValue("application/json"));
                client.DefaultRequestHeaders.Add("User-Agent", "Coordinate Converter");

                string json = client.GetStringAsync(endpoint).Result;
                release = JsonConvert.DeserializeObject<GitHubRelease>(json);
            }

            return new Version(release);
        }

        /// <summary>
        /// Compares the current instance with another object of the same type and returns an integer that indicates whether the current instance precedes, follows, or occurs in the same position in the sort order as the other object.
        /// </summary>
        /// <param name="other">An object to compare with this instance.</param>
        /// <returns>
        /// A value that indicates the relative order of the objects being compared. The return value has these meanings: Value Meaning Less than zero This instance precedes <paramref name="other" /> in the sort order.  Zero This instance occurs in the same position in the sort order as <paramref name="other" />. Greater than zero This instance follows <paramref name="other" /> in the sort order.
        /// </returns>
        public int CompareTo(Version other)
        {
            if (Major > other.Major)
            {
                return 1;
            }
            else if (Major < other.Major)
            {
                return -1;
            }

            if (Minor > other.Minor)
            {
                return 1;
            }
            else if (Minor < other.Minor)
            {
                return -1;
            }

            if (Patch > other.Patch)
            {
                return 1;
            }
            else if (Patch < other.Patch)
            {
                return -1;
            }

            return 0;
        }
    }
}
