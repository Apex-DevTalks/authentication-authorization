import { useAuth } from 'react-oidc-context'
import { useCourses_DataQuery } from '../../GraphQL/graphql'
import { ReactElement, JSXElementConstructor } from 'react'
import Card from '../../Components/Complex/Card/Card'
import Button from '../../Components/Basics/Button/Button'

export default function Dashboard (): JSX.Element {
  const x = useCourses_DataQuery().data?.accounts_getAllCourses
  const auth = useAuth()
  const results: string | number | boolean | ReactElement<any, string | JSXElementConstructor<any>> | JSX.Element[] | null | undefined = []
  x?.forEach((x, index) => {
    results.push(
      <div key={index}>
        <Card width={'700px'} height={'auto'} title={x?.courseName}>
          <div className='flex flex-column'>
            <img src={x?.imageURL} className='max-h-60 w-48'></img>
            <div className='pl-3'>
              <h2>Professor: {x?.professorName}</h2>
              <p>{x?.description}</p>
            </div>
          </div>
          <br />
          <Button text={'Take this course'}/>
          <br />
        </Card>
        <br />
      </div>
    )
  })

  if (auth.isLoading) {
    return <p>Loading...</p>
  } else if (auth.isAuthenticated) {
    return (
      <div className='w-full flex flex-col items-center'>
        {results}
      </div>
    )
  } else {
    return (
      <div className='w-full flex flex-col items-center'>
        <Card width={'300px'} height={'200px'} title='Please log in'>
          <p>You need to be authenticated. Please click the Login button at the corner of the page.</p>
        </Card>
      </div>
    )
  }
}
