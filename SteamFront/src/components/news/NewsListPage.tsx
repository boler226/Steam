import { Button, Col, Collapse, Form, Input, Row } from "antd";
import { useEffect, useState } from "react";
import http_common from "../../api/http_common.ts";
import NewsCard from "./NewsCard.tsx";
import { INewsItem } from "./types.ts";

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
        <>
            <h1>Список новин</h1>
            <Button type="primary" style={{ marginBottom: 20 }}>
                Додати новину
            </Button>

            <Collapse defaultActiveKey={0}>
                <Collapse.Panel key={1} header={"Панель пошуку"}>
                    <Form layout={"vertical"} style={{ minWidth: '100%', padding: 20 }}>
                        <Form.Item label="Заголовок" name="title">
                            <Input autoComplete="title"/>
                        </Form.Item>

                        <Form.Item label="Опис" name="description">
                            <Input autoComplete="description"/>
                        </Form.Item>

                        <Row justify="center">
                            <Button style={{ margin: 10 }} type="primary" htmlType="submit">
                                Пошук
                            </Button>
                            <Button style={{ margin: 10 }} htmlType="button" onClick={() => {}}>
                                Скасувати
                            </Button>
                        </Row>
                    </Form>
                </Collapse.Panel>
            </Collapse>

            <Row gutter={16}>
                <Col span={24}>
                    <Row>
                        {data.length === 0 ? (
                            <h2>Список пустий</h2>
                        ) : (
                            data.map((item) =>
                                <NewsCard key={item.id} {...item} />
                            )
                        )}
                    </Row>
                </Col>
            </Row>
        </>
    );
}

export default NewsListPage;