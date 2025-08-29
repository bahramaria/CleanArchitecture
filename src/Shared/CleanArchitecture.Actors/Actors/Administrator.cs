namespace CleanArchitecture.Actors;

public class Administrator(string username, string displayName) : Actor(Role.Administrator, username, displayName)
{
}
