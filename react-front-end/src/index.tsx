import React from 'react'
import ReactDOM from 'react-dom/client'
import './index.css'
import App from './App'
import reportWebVitals from './reportWebVitals'
import 'tw-elements'
import { BrowserRouter } from 'react-router-dom'
import {
  ApolloClient,
  ApolloProvider,
  InMemoryCache
} from '@apollo/client'
import { AuthProvider } from 'react-oidc-context'
import {
  QueryClient,
  QueryClientProvider
} from '@tanstack/react-query'

const root = ReactDOM.createRoot(
  document.getElementById('root') as HTMLElement
)

const client = new ApolloClient({
  uri: 'http://localhost:5000',
  cache: new InMemoryCache()
})

const oidcConfig = {
  client_id: 't8agr5xKt4$4',
  authority: 'https://localhost:4001',
  redirect_uri: 'http://localhost:3000',
  scope: 'openid profile role DevTalk.Authorization.ApiAPI AccountsApi'
}

const queryClient = new QueryClient()

root.render(
  <ApolloProvider client={client as ApolloClient<any>}>
    <React.StrictMode>
      <QueryClientProvider client={queryClient}>
        <BrowserRouter>
          <AuthProvider {...oidcConfig}>
            <App />
          </AuthProvider>
        </BrowserRouter>
      </QueryClientProvider>
    </React.StrictMode>
  </ApolloProvider>
)

// If you want to start measuring performance in your app, pass a function
// to log results (for example: reportWebVitals(console.log))
// or send to an analytics endpoint. Learn more: https://bit.ly/CRA-vitals
reportWebVitals()
