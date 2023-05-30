import { useAuth } from 'react-oidc-context'

export default function Dashboard (): JSX.Element {
  const auth = useAuth()
  if (auth.isAuthenticated) {
    return (
      <p>You&apos;ve been fully authenticated</p>
    )
  } else {
    return (
      <p>You need to login first</p>
    )
  }
}
