import { useEffect, useState } from "react";
import {Layout, Card, Flex, Input} from 'antd';
import { StarFilled } from '@ant-design/icons';
import http_common from "../../../api/http_common.ts";
import { ImageSizes } from "../../../config/config.ts";
import { INewsItem } from "../../../interfaces/news";
import { INewsFilter } from "../../../interfaces/news";
import { IPage } from "../../../interfaces/pagination";
import './style/style.css';
import Pagination from "../../Pagination.tsx";
import {SearchOutlined} from "@ant-design/icons";

const { Content } = Layout;

const NewsListPage = () => {
    const [data, setData] = useState<INewsItem[]>([]);
    const [currentPage, setCurrentPage] = useState<number>(1);
    const [pageSize, setPageSize] = useState<number>(5);
    const [totalItems, setTotalItems] = useState<number>(0);
    const [filters, setFilters] = useState<INewsFilter>({});

    useEffect(() => {
        fetchNews(currentPage, pageSize, filters);
    }, [currentPage, pageSize, filters]);

    const fetchNews = (page: number, pageSize: number, filters: any) => {
        http_common.get<IPage<INewsItem>>(`/api/News/GetPage?PageIndex=${page - 1}&PageSize=${pageSize}`, {params: filters })
            .then(resp => {
                setData(resp.data.data);
                console.log(resp.data.data);
                setTotalItems(resp.data.itemsAvailable);
            });
    };

    const handleNewsClick = (newsItem: INewsItem) => {
        console.log("Button click", newsItem);
    };

    const handlePageChange = (page: number, pageSize?: number) => {
      setCurrentPage(page);
      if (pageSize)
          setPageSize(pageSize);
    };

    const handleFilterChange = (filterType: string) => {
        if (filterType === "latest") {
            setFilters({ ByRelease: true });
        } else if (filterType === "popular") {
            // Add your logic for popular news
        }
    };

    return (
        <Layout style={{ backgroundColor: '#282B31' }}>
            <Flex justify='center' align='center' gap={60} style={{height: '70px', background: '#383D46'}}>
                <button className="news-filter" onClick={() => handleFilterChange("latest")}>
                    Останні новини
                </button>
                <button className="news-filter" onClick={() => handleFilterChange("popular")}>
                    Популярні новини
                </button>
                <button className="news-filter" onClick={() => handleFilterChange("user")}>
                    Ваші новини
                </button>
                <button className="news-filter" onClick={() => handleFilterChange("official")}>
                    Офіціальні новини
                </button>
                <Input
                    prefix={<SearchOutlined/>}
                    style={{width: 180, height: '45px', background: '#282B31', border: 'none', color: '#C5C3C0'}}
                />
            </Flex>
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
                                        <Flex vertical justify='space-between' align='flex-start' gap='small' style={{ height: '200px'}} >
                                                <p className="news-title">{item.title}</p>
                                                <p className="news-text">{item.description}</p>
                                                <Flex justify='space-between' gap='50%' >
                                                    <p className="news-info">{new Date(item.dateOfRelease).toLocaleDateString()}</p>
                                                    <p className="news-info"
                                                       style={{whiteSpace: 'nowrap'}}
                                                    >
                                                        {item.game.name}
                                                    </p>
                                                </Flex>
                                        </Flex>
                                    </div>
                                    <img className="news-card-image" alt={item.title}
                                         src={`http://localhost:5002/images/${ImageSizes.extraLarge}_${item.media}`}/>
                                </Flex>
                            </Card>
                            <Flex justify='space-between' align='flex-end' gap='small' style={{cursor: 'auto', padding: '5px 20px 5px 20px'}}>
                                <div className="news-icons">
                                    <StarFilled />
                                    {item.rating}
                                </div>
                                <div>

                                </div>
                            </Flex>
                        </div>
                    ))}
                    <Pagination
                        current={currentPage}
                        pageSize={pageSize}
                        total={totalItems}
                        onChange={handlePageChange}
                    />
                </Flex>
            </Content>
        </Layout>
    );
}

export default NewsListPage;