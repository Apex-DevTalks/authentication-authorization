import { ReactFragment, ReactNode, ReactPortal } from 'react'

interface IStyledCardProps {
  children: boolean | ReactNode | ReactFragment | ReactPortal | null | undefined
  width: string
  height: string
  title?: string
}

export default function Card (props: IStyledCardProps): JSX.Element {
  return (
    <div className="rounded-2xl mr-4 shadow-lg" style={{ width: props.width, height: props.height }}>
      {props.title !== undefined && props.title.length > 0 &&
            <h3 className="flex items-center h-12 px-8 mt-1 mb-1 pb-1">{props.title}</h3>}
      <div className="px-8">
        {props.children}
      </div>
    </div>
  )
}
