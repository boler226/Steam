import { useEffect, useState } from "react";
import { Layout, Button } from 'antd';
import http_common from "../../api/http_common.ts";
import NewsCard from "./NewsCard.tsx";
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

    return (
        <Layout className="layout">
            <Content className="news-container">
                {data.map((item) => (
                    <Button className="news-card" key={item.id} style={{ display: 'flex', alignItems: `center`}}>
                        <NewsCard {...item}/>
                    </Button>
                ))}
            </Content>
        </Layout>
    );
}

export default NewsListPage;