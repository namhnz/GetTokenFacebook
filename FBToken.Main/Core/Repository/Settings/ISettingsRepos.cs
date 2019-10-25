namespace FBToken.Main.Core.Repository.Settings
{
    public interface ISettingsRepos
    {
        string LastLoggedInEmail { get; set; }
    }
}