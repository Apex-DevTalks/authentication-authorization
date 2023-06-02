import Card from '../../Components/Complex/Card/Card'

export default function About (): JSX.Element {
  return (
    <div className='w-full flex flex-col items-center'>
      <Card width={'400px'} height={'200px'} title='About' >
        <p>DevTalk: Authentication & Authorization in .NET Core using IdentityServer4 + React with PKCE</p>
      </Card>
    </div>
  )
}
