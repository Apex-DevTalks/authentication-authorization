/* eslint-disable @typescript-eslint/strict-boolean-expressions */
import { useAuth } from 'react-oidc-context'

export const useFetchData = <TData, TVariables>(
  query: string,
  options?: RequestInit['headers']
): ((variables?: TVariables) => Promise<TData>) => {
  const auth = useAuth()

  return async (variables?: TVariables) => {
    const res = await fetch('http://localhost:5000', {
      method: 'POST',
      headers: {
        'Content-Type': 'application/json',
        Authorization: `Bearer ${auth.user?.access_token ?? ''}`,
        ...options
      },
      body: JSON.stringify({
        query,
        variables
      })
    })

    const json = await res.json()

    if (json.errors) {
      const { message } = json.errors[0] || {}
      throw new Error(message || 'Error…')
    }

    return json.data
  }
}
