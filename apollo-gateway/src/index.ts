import { ApolloServer } from "@apollo/server"
import { ApolloGateway, RemoteGraphQLDataSource } from '@apollo/gateway'
import { startStandaloneServer } from '@apollo/server/standalone'
import { readFileSync } from "fs";

class AuthenticatedDataSource extends RemoteGraphQLDataSource {
  willSendRequest({ request, context }) {
    request.http.headers.set('Authorization', context.token);
  }
}

const supergraphSdl = readFileSync('./supergraph.graphql').toString()
const gateway = new ApolloGateway({ supergraphSdl, buildService({ name, url}) {
    return new AuthenticatedDataSource({ url });
}})

const server = new ApolloServer({
    gateway
})

const { url } = await startStandaloneServer(server, {
    listen: { port: 5000 },
    context: async ({ req }) => {
      const token = req.headers.authorization || '';
      return { token }
    }
  });
  
console.log(`🚀  Server ready at: ${url}`);