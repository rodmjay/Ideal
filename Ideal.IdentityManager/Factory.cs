using Ideal.Security.Clients;
using Ideal.Security.Scopes;
using IdentityServer3.Core.Configuration;
using IdentityServer3.Core.Services;
using IdentityServer3.Core.Services.Default;
using IdentityServer3.Core.Services.InMemory;

namespace Ideal.IdentityManager
{
	class Factory
	{
		public static IdentityServerServiceFactory Configure()
		{
			var factory = new IdentityServerServiceFactory();
			var scopeService = new ScopeService();
			var clientService = new ClientService();

			var scopeStore = new InMemoryScopeStore(scopeService.Get());
			factory.ScopeStore = new Registration<IScopeStore>(resolver => scopeStore);

			var clientStore = new InMemoryClientStore(clientService.Get());
			factory.ClientStore = new Registration<IClientStore>(resolver => clientStore);

			factory.CorsPolicyService = new Registration<ICorsPolicyService>(new DefaultCorsPolicyService { AllowAll = true });

			return factory;
		}
	}
}