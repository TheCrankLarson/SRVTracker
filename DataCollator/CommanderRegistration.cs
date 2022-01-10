using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;
using EDTracking;

namespace DataCollator
{
    public class CommanderRegistration
    {
        //private Dictionary<Guid, string> _commanderGuids = new Dictionary<Guid, string>();
        private Dictionary<Guid, EDRacerProfile> _commanderProfiles = new Dictionary<Guid, EDRacerProfile>();
        private string _saveFile = "commanders.json";
        private string _profilesFile = "commanderProfiles.json";

        public CommanderRegistration(string ProfileSaveFile = "")
        {
            if (!String.IsNullOrEmpty(ProfileSaveFile))
                _profilesFile = ProfileSaveFile;
            LoadRegisteredCommanders();
        }

        private bool IsValidCommanderNameCharacter(char C)
        {
            return char.IsLetterOrDigit(C) || C == ' ';
        }

        public string RegisterCommander(string CommanderName)
        {
            foreach (EDRacerProfile racerProfile in _commanderProfiles.Values)
                if (racerProfile.CommanderName.ToLower() == CommanderName.ToLower())
                    return "ERROR: Commander name already in use";

            if (String.IsNullOrEmpty(CommanderName))
                return "ERROR: Commander name cannot be blank";

            if (CommanderName.Contains(Environment.NewLine))
                return "ERROR: Commander name must be a single line";

            if (!CommanderName.All(IsValidCommanderNameCharacter))
                return "ERROR: Commander name can only contain letters and numbers";

            try
            {
                Guid commanderGuid = Guid.NewGuid();
                EDRacerProfile racerProfile = new EDRacerProfile();
                racerProfile.CommanderName = CommanderName;
                _commanderProfiles.Add(commanderGuid, racerProfile);
                SaveCommanderProfiles();
                return commanderGuid.ToString();
            }
            catch (Exception ex)
            {
                return $"ERROR: {ex.Message}";
            }
        }

        public string CommanderName(Guid commanderGuid)
        {
            if (_commanderProfiles.ContainsKey(commanderGuid))
                return _commanderProfiles[commanderGuid].CommanderName;
            return null;
        }

        public string CommanderName(string commanderGuid)
        {
            if (String.IsNullOrEmpty(commanderGuid))
                return null;

            try
            {
                return CommanderName(Guid.Parse(commanderGuid));
            }
            catch
            {
                return null;
            }
        }

        public string GetCommanderProfile(Guid commanderGuid)
        {
            if (!_commanderProfiles.ContainsKey(commanderGuid))
                return "ERROR: Client Id not found";

            return JsonSerializer.Serialize(_commanderProfiles[commanderGuid]);
        }

        public string UpdateCommanderProfile(Guid commanderGuid, string commanderProfile)
        {
            if (!_commanderProfiles.ContainsKey(commanderGuid))
                return "ERROR: Client Id not found";

            try
            {
                EDRacerProfile profile = EDRacerProfile.FromJSON(commanderProfile);
                profile.CommanderName = _commanderProfiles[commanderGuid].CommanderName; // Ensure commander name doesn't change
                _commanderProfiles[commanderGuid] = profile;
                SaveCommanderProfiles();
                return "SUCCESS";
            }
            catch (Exception ex)
            {
                return $"ERROR: Unable to update profile ({ex.Message})";
            }
            
        }

        public string UpdateCommanderProfile(string commanderGuid, string commanderProfile)
        {
            try
            {
                Guid g = Guid.Parse(commanderGuid);
                return UpdateCommanderProfile(g, commanderProfile);
            }
            catch (Exception ex)
            {
                return $"ERROR: Invalid Guid. {ex.Message}";
            }
        }

        public string UpdateCommanderName(Guid commanderGuid, string commanderName)
        {
            if (!_commanderProfiles.ContainsKey(commanderGuid))
                return "ERROR: Client Id not found";

            foreach (EDRacerProfile racerProfile in _commanderProfiles.Values)
                if (racerProfile.CommanderName.ToLower() == commanderName.ToLower())
                    return "ERROR: Commander name already in use";

            _commanderProfiles[commanderGuid].CommanderName = commanderName;
            SaveCommanderProfiles();
            return "SUCCESS";
        }

        public string UpdateCommanderName(string commanderGuid, string commanderName)
        {
            try
            {
                Guid g = Guid.Parse(commanderGuid);
                return UpdateCommanderName(g, commanderName);
            }
            catch (Exception ex)
            {
                return $"ERROR: Invalid Guid. {ex.Message}";
            }
        }

        private void SaveCommanderProfiles()
        {
            string commanderProfiles = JsonSerializer.Serialize(_commanderProfiles);
            try
            {
                File.WriteAllText(_profilesFile, commanderProfiles);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Failed to save commander profiles.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Catastrophic Error",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        //private void SaveRegisteredCommanders()
        //{
        //    string registeredCommanders = JsonSerializer.Serialize(_commanderGuids);
        //    try
        //    {
        //        File.WriteAllText(_saveFile, registeredCommanders);
        //    }
        //    catch (Exception ex)
        //    {
        //        System.Windows.Forms.MessageBox.Show($"Failed to save commander registrations.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Catastrophic Error",
        //            System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
        //    }
        //}

        private void LoadCommanderProfiles()
        {
            try
            {
                string commanderProfiles = File.ReadAllText(_saveFile);
                _commanderProfiles = JsonSerializer.Deserialize<Dictionary<Guid, EDRacerProfile>>(commanderProfiles);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Failed to load commander profiles.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Error",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }
        }

        private void LoadRegisteredCommanders()
        {
            if (File.Exists(_profilesFile))
            {
                LoadCommanderProfiles();
                return;
            }

            if (!File.Exists(_saveFile))
                return;

            Dictionary<Guid, string> commanderGuids;
            try
            {
                string registeredCommanders = File.ReadAllText(_saveFile);
                commanderGuids = JsonSerializer.Deserialize<Dictionary<Guid, String>>(registeredCommanders);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Failed to load commander registrations.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Error",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
                return;
            }

            // As we only had a commander Guid file, we need to generate blank profiles for all commanders
            _commanderProfiles = new Dictionary<Guid, EDRacerProfile>();
            foreach (Guid commanderGuid in commanderGuids.Keys)
            {
                EDRacerProfile profile = new EDRacerProfile();
                profile.CommanderName = commanderGuids[commanderGuid];
                _commanderProfiles.Add(commanderGuid, profile);
            }
            SaveCommanderProfiles();
        }
    }
}
