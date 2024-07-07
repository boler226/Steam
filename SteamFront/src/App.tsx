import { Layout, Col, Row, Button, Image } from 'antd';
import NewsListPage from './components/news/NewsListPage';
import { BrowserRouter as Router, Route, Routes } from 'react-router-dom';
import './App.css';

const { Content, Footer , Header} = Layout;

const App = () => {

  return (
    <Router>
        <Layout style={{ backgroundColor: '#353c4e' }}>
            <Header className="header">
                <Row justify='center' align='middle' gutter={20}>
                    <Col style={{marginRight: 100}}>
                        <Image height={55} preview={false} src='https://store.akamai.steamstatic.com/public/shared/images/header/logo_steam.svg?t=962016'></Image>
                    </Col>
                    <Col>
                        <Button className="button-header" type='text'>Магазин</Button>
                    </Col>
                    <Col>
                        <Button className="button-header" type='text'>Спільнота</Button>
                    </Col>
                    <Col>
                        <Button className="button-header" type='text'>Player</Button>
                    </Col>
                    <Col>
                        <Button className="button-header" type='text'>Чат</Button>
                    </Col>
                    <Col>
                        <Button className="button-header" type='text'>Підтримка</Button>
                    </Col>
                </Row>
            </Header>

            <Content className="site-layout-content" style={{margin: '0 10px 0 10px'}}>
                    <Routes>
                        <Route path="/" element={<NewsListPage />} />
                        {/*<Route path="/addNews" element={<NewsAddComponent />} />*/}
                        {/*<Route path="/listNews" element={<NewsListComponent />} />*/}
                    </Routes>
            </Content>
            <Footer className="footer">©2023 Created by boler</Footer>
        </Layout>
    </Router>
);
}

export default App
