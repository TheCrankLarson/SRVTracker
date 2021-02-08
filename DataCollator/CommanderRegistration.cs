using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Text.Json;

namespace DataCollator
{
    public class CommanderRegistration
    {
        private Dictionary<Guid, string> _commanderGuids = new Dictionary<Guid, string>();
        private string _saveFile = "commanders.json";

        public CommanderRegistration(string RegistrationFile = "")
        {
            if (!String.IsNullOrEmpty(RegistrationFile))
                _saveFile = RegistrationFile;
            LoadRegisteredCommanders();
        }

        private bool IsValidCommanderNameCharacter(char C)
        {
            if (Char.IsLetterOrDigit(C) || Char.IsWhiteSpace(C))
                return true;
            return false;
        }

        public string RegisterCommander(string CommanderName)
        {
            if (_commanderGuids.Values.ToList<string>().Contains(CommanderName))
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
                _commanderGuids.Add(commanderGuid, CommanderName);
                SaveRegisteredCommanders();
                return commanderGuid.ToString();
            }
            catch (Exception ex)
            {
                return $"ERROR: {ex.Message}";
            }
        }

        public string CommanderName(Guid commanderGuid)
        {
            if (_commanderGuids.ContainsKey(commanderGuid))
                return _commanderGuids[commanderGuid];
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

        public string UpdateCommanderName(Guid commanderGuid, string commanderName)
        {
            if (!_commanderGuids.ContainsKey(commanderGuid))
                return "ERROR: Client Id not found";

            if (_commanderGuids.Values.ToList<string>().Contains(commanderName))
                return "ERROR: Commander name already in use";

            _commanderGuids[commanderGuid] = commanderName;
            SaveRegisteredCommanders();
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

        private void SaveRegisteredCommanders()
        {
            string registeredCommanders = JsonSerializer.Serialize(_commanderGuids);
            try
            {
                File.WriteAllText(_saveFile, registeredCommanders);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Failed to save commander registrations.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Catastrophic Error",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }

        private void LoadRegisteredCommanders()
        {
            if (!File.Exists(_saveFile))
                return;

            try
            {
                string registeredCommanders = File.ReadAllText(_saveFile);
                _commanderGuids = JsonSerializer.Deserialize<Dictionary<Guid, String>>(registeredCommanders);
            }
            catch (Exception ex)
            {
                System.Windows.Forms.MessageBox.Show($"Failed to load commander registrations.{Environment.NewLine}{Environment.NewLine}{ex.Message}", "Error",
                    System.Windows.Forms.MessageBoxButtons.OK, System.Windows.Forms.MessageBoxIcon.Error);
            }
        }
    }
}
