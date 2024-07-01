import Header from './components/Header';
import NewsListPage from './components/news/NewsListPage';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';


const App = () => {

  return (
    <Router>
        <div className="App">
        <Header />
        <Routes>
          <Route path='/' element={<NewsListPage/>} />
          {/*<Route path='/addNews' element={<NewsAddComponent/>} />*/}
          {/*<Route path='/listNews' element={<NewsListComponent/>} />*/}
        </Routes>
        
    </div>
    </Router>
);
}

export default App
