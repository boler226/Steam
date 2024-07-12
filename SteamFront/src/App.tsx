import { Layout, Col, Row, Image, Space, Input } from 'antd';
import { SearchOutlined } from '@ant-design/icons';
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
                <div className="site-router">
                    <Space style={{ display: 'flex', justifyContent: 'space-between', alignItems: 'center' }}>
                        <Space>
                            <button className="button-router">Нові й варті уваги</button>
                            <button className="button-router">Новини</button>
                            <button className="button-router">Категорії</button>
                        </Space>
                        <Input
                            placeholder="пошук"
                            prefix={<SearchOutlined />}
                            style={{ width: 200, height: '30px', background: '#316282', border: 'none', color: '#C5C3C0' }}
                        />
                    </Space>
                </div>

                    <Routes>
                        <Route path="/news" element={<NewsListPage />} />
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
