import NavigationBar from './Components/Complex/NavigationBar/NavigationBar'
import Home from './Routes/Home/Home'
import { Routes, Route } from 'react-router-dom'
import NotFound from './Routes/NotFound/NotFound'
import About from './Routes/About/About'
import Dashboard from './Routes/Dashboard/Dashboard'

function App (): JSX.Element {
  return (
    <Routes>
      <Route path="/" element={<NavigationBar />}>
        <Route index element={<Home />} />
        <Route path="about" element={<About />} />
        <Route path="dashboard" element={<Dashboard />} />
        <Route path="*" element={<NotFound />} />
      </Route>
    </Routes>
  )
}

export default App
