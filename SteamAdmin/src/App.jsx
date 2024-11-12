import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import logo from './icons/admin_logo.png';
import burger from './icons/burger.png';
import './App.css'
import {useState} from "react";

function App() {
    const [isMenuOpen, setMenuOpen] = useState(true);

    const toggleMenu = () => {
        setMenuOpen(!isMenuOpen);
    };

  return (
      <Router>
          <body>
          <header className="header">
              <button className="burger" onClick={toggleMenu}>
                  <img src={burger} alt="menu" className="burger-icon" />
              </button>
              <img src={logo} alt="logo" className="logo-icon"/>
          </header>

          <nav className={`vertical-nav ${isMenuOpen ? 'nav--open' : ''}`}>
              <ul className="nav-list">
                  <li className="nav-item">
                      <a href="#">Some item</a>
                  </li>
                  <li className="nav-item">
                      <a href="#">Some item</a>
                  </li>
                  <li className="nav-item">
                      <a href="#">Some item</a>
                  </li>
                  <li className="nav-item">
                      <a href="#">Some item</a>
                  </li>
              </ul>
          </nav>
          <main>

          </main>
          <footer>

          </footer>
          </body>
      </Router>
  )
}

export default App
