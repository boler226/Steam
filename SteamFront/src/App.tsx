import { Layout, Col, Row, Image } from 'antd';
import SiteRouter from "./components/SiteRouter.tsx";
import NewsListPage from './components/news/list/NewsListPage.tsx';
import GamesListPage from "./components/games/list/GamesListPage.tsx";
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import './App.css';
import NewsCreatePage from "./components/news/create/NewsCreatePage.tsx";

const { Content, Footer , Header} = Layout;

const App = () => {

  return (
    <Router>
        <Layout style={{ backgroundColor: '#353c4e' }}>
            <Header className="header">
                    <Row justify='center' align='middle' gutter={20}>
                        <Col style={{marginRight: 100}}>
                            <Image height={45} preview={false} src='https://store.akamai.steamstatic.com/public/shared/images/header/logo_steam.svg?t=962016'></Image>
                        </Col>
                        <Col>
                            <button className="button-header">Магазин</button>
                        </Col>
                        <Col>
                            <button className="button-header">Спільнота</button>
                        </Col>
                        <Col>
                            <button className="button-header">Player</button>
                        </Col>
                        <Col>
                            <button className="button-header">Чат</button>
                        </Col>
                        <Col>
                            <button className="button-header">Підтримка</button>
                        </Col>
                    </Row>
            </Header>

            <Content className="site-layout-content" >
                <SiteRouter/>
                    <Routes>
                        <Route path="/" element={<GamesListPage />} />
                        <Route path="/news" element={<NewsListPage />} />
                        <Route path="/createNews" element={<NewsCreatePage />} />
                        {/*<Route path="/listNews" element={<NewsListComponent />} />*/}
                    </Routes>
            </Content>
            <Footer className="footer">©2023 Created by boler</Footer>
        </Layout>
    </Router>
);
}

export default App
