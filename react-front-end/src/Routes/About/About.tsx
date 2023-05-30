/* eslint-disable indent */
import { useAbout_Page_DataQuery } from '../../GraphQL/graphql'
import { useAuth } from 'react-oidc-context'

export default function About (): JSX.Element {
  const x = useAbout_Page_DataQuery().data?.authorization_getDataAsVisitor
  const auth = useAuth()

  switch (auth.activeNavigator) {
    case 'signinSilent':
      return <div>Signing you in...</div>
    case 'signoutRedirect':
      return <div>Signing you out...</div>
  }

  if (auth.isLoading) {
    return <p>Loading...</p>
  } else if (auth.isAuthenticated) {
    return (
      <div>
        <p>Id is: {x?.id}</p>
        <p>Date created: {x?.dateCreated}</p>
      </div>
    )
  } else {
    return <></>
  }
}
