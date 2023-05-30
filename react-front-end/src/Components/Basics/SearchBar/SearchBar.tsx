import { FontAwesomeIcon } from '@fortawesome/react-fontawesome'
import { faSearch } from '@fortawesome/free-solid-svg-icons'

export default function SearchBar (props: { width: string, placeholder: string }): JSX.Element {
  return (
    <div className='relative'>
      <FontAwesomeIcon icon={faSearch} className="absolute top-1 left-1"/>
      <input
        className='border-none outline-none rounded-2xl pl-6'
        type={'search'}
        style={{ width: `${props.width}` }}
        placeholder={props.placeholder}
      ></input>
    </div>
  )
}
