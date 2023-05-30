export default function Link (props: { href: string, text: string }): JSX.Element {
  return (
    <a href={props.href} className="px-2">{props.text}</a>
  )
}
