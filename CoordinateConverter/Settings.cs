using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Windows.Forms;

namespace CoordinateConverter
{
    /// <summary>
    /// Represents the settings for the application
    /// </summary>
    public class Settings
    {
        /// <summary>
        /// Initializes a new instance of the <see cref="Settings"/> class.
        /// </summary>
        public Settings()
        {
            foreach (ELastFileSource fileSource in Enum.GetValues(typeof(ELastFileSource)))
            {
                RecentFiles.Add(fileSource, new List<string>());
            }
        }


        static private int GetPrimaryMonitor()
        {
            int idx = 0;
            foreach (Screen screen in Screen.AllScreens)
            {
                if (screen.Primary)
                {
                    return idx;
                }
                idx++;
            }
            return 0;
        }


        /// <summary>
        /// Gets the settings file.
        /// </summary>
        /// <value>
        /// The settings file.
        /// </value>
        [JsonIgnore]
        public static FileInfo SettingsFile { get; private set; } = new FileInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData), "CoordinateConverter", "settings.json"));

        /// <summary>
        /// The source for the last files directory
        /// </summary>
        public enum ELastFileSource
        {
            /// <summary>
            /// A file containing default points
            /// </summary>
            Points,
            /// <summary>
            /// The file containing AH64 DTC data
            /// </summary>
            AH64DTC,
            /// <summary>
            /// The file containing a custom lua script
            /// </summary>
            Execute
        }

        private readonly Dictionary<ELastFileSource, string> subPaths = new Dictionary<ELastFileSource, string>()
        {
            { ELastFileSource.Points, "Coordinates/" },
            { ELastFileSource.AH64DTC, "AH64/" },
            { ELastFileSource.Execute, "Lua/" },
        };

        /// <summary>
        /// Gets the directory for the given file source.
        /// </summary>
        /// <param name="source">The file-source.</param>
        /// <returns>The directory for the given file source</returns>
        public DirectoryInfo GetDirectory(ELastFileSource source)
        {
            DirectoryInfo info = new DirectoryInfo(Path.Combine(BaseDirectory.FullName, subPaths[source]));
            return info;
        }

        private DirectoryInfo baseDirectory = new DirectoryInfo(Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal), "CoordinateConverter\\"));
        /// <summary>
        /// The base directory for application files
        /// </summary>
        public DirectoryInfo BaseDirectory
        {
            get
            {
                return baseDirectory;
            }
            set
            {
                baseDirectory = value;
                if (!baseDirectory.Exists)
                {
                    baseDirectory.Create();
                }
                foreach (KeyValuePair<ELastFileSource, string> kvp in subPaths)
                {
                    DirectoryInfo path = GetDirectory(kvp.Key);
                    if (!Directory.Exists(path.FullName))
                    {
                        Directory.CreateDirectory(path.FullName);
                    }
                }
            }
        }

        private int dcsMonitor = GetPrimaryMonitor();
        /// <summary>
        /// Gets or sets the DCS monitor index.
        /// </summary>
        /// <value>
        /// The DCS monitor index.
        /// </value>
        public int DCSMonitor {
            get
            {
                if (dcsMonitor < 0 || dcsMonitor >= Screen.AllScreens.Length)
                {
                    dcsMonitor = GetPrimaryMonitor();
                }
                return dcsMonitor;
            }
            set
            {
                if (value < 0 || value >= Screen.AllScreens.Length)
                {
                    dcsMonitor = GetPrimaryMonitor();
                }
                else
                {
                    dcsMonitor = value;
                }
            }
        }

        /// <summary>
        /// When to show the reticle
        /// </summary>
        public enum EReticleSetting
        {
            /// <summary>
            /// Never show the reticle
            /// </summary>
            Never,
            /// <summary>
            /// Always show the reticle
            /// </summary>
            Always,
            /// <summary>
            /// Show the reticle when F10 map is active
            /// </summary>
            WhenF10
        }

        /// <summary>
        /// Gets or sets the reticle setting.
        /// </summary>
        /// <value>
        /// The reticle setting.
        /// </value>
        public EReticleSetting ReticleSetting { get; set; } = EReticleSetting.WhenF10;

        /// <summary>
        /// Represents the different modes for calculating the camera position.
        /// </summary>
        public enum ECameraPosMode
        {
            /// <summary>
            /// The altitude is calculated based on the height of the camera position itself.
            /// </summary>
            CameraAltitude,
            /// <summary>
            /// The altitude is calculated based on the height of the terrain at the camera location.
            /// </summary>
            TerrainElevation
        }

        /// <summary>
        /// Represents the different modes for calculating the camera position.
        /// </summary>
        public ECameraPosMode CameraPosMode = ECameraPosMode.TerrainElevation;

        /// <summary>
        /// The maximum number of file entries
        /// </summary>
        [JsonIgnore]
        public const int MAX_FILE_ENTRIES = 10;
        /// <summary>
        /// Gets or sets the last files used in the application.
        /// </summary>
        /// <remarks>
        /// This property stores the last files used in the application. It is used to remember the last files the user opened or saved.
        /// The files are stored as a list of strings, where each string is the full path of a file.
        /// </remarks>
        public Dictionary<ELastFileSource, List<string>> RecentFiles { get; private set; } = new Dictionary<ELastFileSource, List<string>>();

        /// <summary>
        /// Adds the file to the <see cref="RecentFiles"/> directory.
        /// </summary>
        /// <param name="source">The source.</param>
        /// <param name="filePath">The file path.</param>
        public void AddFile(ELastFileSource source, string filePath)
        {
            FileInfo fi = new FileInfo(filePath);
            if (!fi.Exists)
            {
                return;
            }

            if (!RecentFiles.ContainsKey(source))
            {
                RecentFiles.Add(source, new List<string>());
            }
            // Remove the file from the list if it is in the list already
            RecentFiles[source].Remove(fi.FullName);
            // Add to the beginning of the list
            RecentFiles[source].Insert(0, fi.FullName);
            // remove anything past MAX_FILE_ENTRIES entries
            if (RecentFiles[source].Count > MAX_FILE_ENTRIES)
            {
                RecentFiles[source].RemoveRange(MAX_FILE_ENTRIES, RecentFiles[source].Count - MAX_FILE_ENTRIES);
            }
            Save();
        }

        /// <summary>
        /// Gets or sets a value indicating whether automatic checking for updates is enabled.
        /// </summary>
        /// <value>
        /// <c>true</c> if automatic checking for updates is enabled; otherwise, <c>false</c>.
        /// The default value is <c>false</c>.
        /// </value>
        public bool AutoCheckForUpdates { get; set; } = false;

        /// <summary>
        /// Loads the settings from the settings file if it exists.
        /// </summary>
        static public Settings Load()
        {
            DirectoryInfo settingsDirectory = SettingsFile.Directory;

            if (!settingsDirectory.Exists)
            {
                settingsDirectory.Create();
            }

            if (!SettingsFile.Exists)
            {
                Settings settings = new Settings();
                settings.Save();
                return settings;
            }

            string json = File.ReadAllText(SettingsFile.FullName);
            Settings loadedSettings = JsonConvert.DeserializeObject<Settings>(json);
            return loadedSettings;
        }

        /// <summary>
        /// Saves the current settings to a JSON file.
        /// </summary>
        /// <remarks>
        /// This function saves the current settings to a JSON file. The settings are serialized using Newtonsoft.Json library and are stored in a file named "settings.json" in the application's base directory.
        /// If the file already exists, it is overwritten. If the file does not exist, it is created.
        /// </remarks>
        public void Save()
        {
            JsonSerializerSettings newtonsoftSettings = new JsonSerializerSettings
            {
                NullValueHandling = NullValueHandling.Ignore,
                Formatting = Formatting.Indented,
                Culture = System.Globalization.CultureInfo.InvariantCulture,
                TypeNameHandling = TypeNameHandling.Objects,
                StringEscapeHandling = StringEscapeHandling.Default,
            };

            string json = JsonConvert.SerializeObject(this, newtonsoftSettings);
            File.WriteAllText(SettingsFile.FullName, json);
        }
    }
}
