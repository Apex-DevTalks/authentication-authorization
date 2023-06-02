/* eslint-disable no-void */
import { Link, Outlet } from 'react-router-dom'
import SearchBar from '../../Basics/SearchBar/SearchBar'
import { useAuth } from 'react-oidc-context'

export default function NavigationBar (): JSX.Element {
  const auth = useAuth()
  return (
    <>
      <div className="w-full pt-3 pb-3 sticky shadow-lg top-0 bg-slate-200 flex flex-row justify-evenly border-t border-t-slate-900 border-b border-b-slate-900">
        <Link to={'/'}>E-Learning</Link>
        <SearchBar width={'500px'} placeholder={'Buscar cualquier cosa'} />
        <div className="flex justify-between w-60">
          <Link to={'/about'}>About</Link>
          <Link to={'/dashboard'}>Dashboard</Link>
          { auth.isAuthenticated
            ? <button onClick={() => void auth.removeUser()}>Log out</button>
            : <button onClick={() => void auth.signinRedirect()}>Login</button> }
        </div>
      </div>
      <Outlet />
    </>
  )
}
