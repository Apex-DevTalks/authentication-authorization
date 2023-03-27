using GraphQL.Types;

namespace ApolloGraphQLFederationExtensions
{
    public class EntityType : UnionGraphType
    {
        public EntityType(IEnumerable<Type> types)
        {
            Name = "_Entity";

            foreach (var type in types)
            {
                Type(type);
            }
        }
    }
}
