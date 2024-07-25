import { useEffect, useState } from "react";
import { Layout, Card, Flex, Form, Checkbox, Button } from 'antd';
import http_common from "../../../api/http_common.ts";
import { ImageSizes } from "../../../config/config.ts";
import { INewsItem } from "../../../interfaces/news";
import { INewsFilter } from "../../../interfaces/news";
import { IPage } from "../../../interfaces/pagination";
import './style/style.css';
import Pagination from "../../Pagination.tsx";

const { Content } = Layout;

const NewsListPage = () => {
    const [data, setData] = useState<INewsItem[]>([]);
    const [currentPage, setCurrentPage] = useState<number>(1);
    const [pageSize, setPageSize] = useState<number>(5);
    const [totalItems, setTotalItems] = useState<number>(0);
    const [filters, setFilters] = useState<INewsFilter>({});
    const [form] = Form.useForm();

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

    const handleFilterChange = () => {
        setFilters(form.getFieldsValue());
        setFilters(values);
    };

    return (
        <Layout style={{ backgroundColor: '#282B31' }}>
            <Content className="news-container">
                <Flex vertical justify='space-around' align='center' gap={50}>
                    <Form
                        form={form}
                        layout="inline"
                        onValuesChange={handleFilterChange}
                    >
                        <Form.Item name="ByRelease" valuePropName="checked">
                            <Checkbox>Filter by Release</Checkbox>
                        </Form.Item>

                        <Form.Item>
                            <Button type="primary" htmlType="button" onClick={() => fetchNews(currentPage, pageSize, filters)}>
                                Apply Filters
                            </Button>
                        </Form.Item>
                    </Form>

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
                                         src={`http://localhost:5002/images/${ImageSizes.extraLarge}_${item.image}`}/>
                                </Flex>
                            </Card>
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