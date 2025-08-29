using CleanArchitecture.Actors.ActorProviders;

namespace CleanArchitecture.Actors;

internal class ActorResolver(IEnumerable<IActorProvider>? providers) : IActorResolver
{
    public Actor? Actor => CurrentActor();

    private Actor? CurrentActor()
    {
        if (providers is null)
        {
            return null;
        }

        foreach (var provider in providers)
        {
            var user = provider.CurrentActor();
            if (user != null)
            {
                return user;
            }
        }

        return null;
    }
}