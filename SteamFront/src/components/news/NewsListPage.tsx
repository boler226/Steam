import { useEffect, useState } from "react";
import { Layout, Card, Flex } from 'antd';
import http_common from "../../api/http_common.ts";
import { ImageSizes } from "../../config/imageSizes.ts";
import { INewsItem } from "./types.ts";
import './style/NewsListPage.css';

const { Content } = Layout;

const NewsListPage = () => {
    const [data, setData] = useState<INewsItem[]>([]);

    useEffect(() => {
        fetchNews();
    }, []);

    const fetchNews = () => {
        http_common.get<INewsItem[]>("/api/news/List")
            .then(resp => {
                setData(resp.data);
            });

    };

    const handleNewsClick = (newsItem: INewsItem) => {
        console.log("Button click", newsItem);
    };

    return (
        <Layout style={{ backgroundColor: '#282B31' }}>
            <Content className="news-container">
                <Flex vertical justify='space-around' align='center' gap={50}>
                    {data.map((item) => (
                        <div className="news-card" key={item.id}
                             onClick={() => handleNewsClick(item)}
                             role="button"
                             >
                            <Card hoverable className="news-card-inner">
                                <Flex justify='space-between'>
                                    <div className="news-content">
                                        <Flex vertical justify='space-between' align='flex-start' gap='small' style={{height: '200px'}}>
                                                <p className="news-title">{item.title}</p>
                                                <p className="news-text">{item.description}</p>
                                                <Flex justify='space-between' gap='50%'>
                                                    <p className="news-text">{new Date(item.dateOfRelease).toLocaleDateString()}</p>
                                                    <p className="news-text"
                                                       style={{whiteSpace: 'nowrap'}}
                                                    >
                                                        {item.game.name}
                                                    </p>
                                                </Flex>
                                        </Flex>
                                    </div>
                                    <img className="news-card-image" alt={item.title}
                                         src={`http://localhost:5002/images/${ImageSizes.extraLarge}_${item.image}`}/>
                                </Flex>
                            </Card>
                        </div>
                    ))}
                </Flex>
            </Content>
        </Layout>
    );
}

export default NewsListPage;