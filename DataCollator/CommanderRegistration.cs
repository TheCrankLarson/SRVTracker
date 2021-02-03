using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
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
            try
            {
                return CommanderName(Guid.Parse(commanderGuid));
            }
            catch (Exception ex)
            {
                return $"ERROR: {ex.Message}";
            }
        }
        public string UpdateCommanderName(Guid commanderGuid, string commanderName)
        {
            if (!_commanderGuids.ContainsKey(commanderGuid))
                return "ERROR: Commander not found";

            _commanderGuids[commanderGuid] = commanderName;
            SaveRegisteredCommanders();
            return "SUCCESS";
        }

        private void SaveRegisteredCommanders()
        {

        }

        private void LoadRegisteredCommanders()
        {

        }
    }
}
