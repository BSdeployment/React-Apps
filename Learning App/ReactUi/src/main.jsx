import { StrictMode } from 'react'
import { createRoot } from 'react-dom/client'
import './index.css'
import './bootstrap.min.css'
import App from './App.jsx'
import RouterPage from './Components/Layout/RouterPage.jsx'
createRoot(document.getElementById('root')).render(
  <StrictMode>
<RouterPage/>
  
  </StrictMode>,
)
