import {Flex, Layout} from "antd";
import './style/style.css';
import {useEffect, useState} from "react";
import {IGameItem} from "../../../interfaces/games";
import {ImageSizes} from "../../../config/config.ts";
import http_common from "../../../api/http_common.ts";

const { Content } = Layout;

const GamesListPage = () => {

    const [data, setData] = useState<IGameItem[]>([]);
    const [game, setGame] = useState<IGameItem>();
    const [currentPage, setCurrentPage] = useState<number>(1);
    const [pageSize, setPageSize] = useState<number>(5);
    const [totalItems, setTotalItems] = useState<number>(0);

    useEffect(() => {
        fetchGames(currentPage, pageSize);
    }, [currentPage, pageSize]);

    const fetchGames = (page: number, pageSize: number) => {
        http_common.get<IGameItem[]>("/api/Games/List")
            .then(resp => {
                setData(resp.data);
            });
    }

    const handleGameClick = (gameItem: IGameItem) => {
        console.log("game click", gameItem);
    };

    const handleMouseEnter = (gameItem: IGameItem) => {
        setGame(gameItem);
    }

    return(
            <Layout style={{ background: 'linear-gradient(to bottom, rgba(42,71,94,1), rgba(27, 40, 56, 0.1))' }}>
                <Content className="games-container">
                    <div className="games-card-list">
                        <Flex  justify='center' gap={25}>
                            <Flex vertical justify='space-around' align='center' gap={10} style={{padding: '10px 0'}}>
                                {data && data.length > 0 && data.slice(0, 10).map((item) => (
                                    <div key={item.id}>
                                        <div className="card"
                                             onClick={() => handleGameClick(item)}
                                             onMouseEnter={() => handleMouseEnter(item)}>
                                            <div className="left-card">
                                                <Flex align='center' style={{height: '100%'}}>
                                                    {item.images[0] && (
                                                        <img
                                                            className="game-card-image"
                                                            alt={item.name}
                                                            src={`http://localhost:5002/images/${ImageSizes.extraLarge}_${item.images[0].name}`}
                                                        />
                                                    )}
                                                    <Flex justify='space-between' align='center'
                                                          style={{width: '400px', padding: '10px'}}>
                                                        <Flex vertical justify='space-around' style={{height: '70px'}}>
                                                            <div className="card-title">{item.name}</div>
                                                            <Flex justify='space-around' gap={5} style={{ maxWidth: '260px', overflow: 'hidden'}}>
                                                                {item.categories.map(item => (
                                                                    <div className="card-categories" key={item.id}>{item.name}</div>
                                                                ))}
                                                            </Flex>
                                                        </Flex>
                                                        <p className="card-price">{item.price}$</p>
                                                    </Flex>
                                                </Flex>
                                            </div>
                                        </div>
                                    </div>
                                ))}
                            </Flex>

                            <div className="right-card">
                                <Flex vertical justify='space-around' align='start' style={{padding: '0 20px'}}>
                                    <p className="card-title-right">{game?.name}</p>
                                    <div className="card-rating">
                                        Усі рецензії користувачів:<br/>
                                        дуже схвальна (...)
                                    </div>
                                    <Flex justify='space-around' gap={5} style={{padding: '10px 0', maxWidth: '260px', overflow: 'hidden'}}>
                                        {game?.categories.map((item) =>
                                            <div className="card-categories-right" key={item.id}>
                                                {item.name}
                                            </div>
                                        )}
                                    </Flex>
                                    <Flex vertical justify='space-around'>
                                        {game?.images.slice(1, 5).map((item) => (
                                            <img key={item.id} className="card-image-right" alt={item.name}
                                                 src={`http://localhost:5002/images/${ImageSizes.extraLarge}_${item.name}`}/>
                                        ))}
                                    </Flex>
                                </Flex>
                            </div>
                        </Flex>
                    </div>
                </Content>
            </Layout>
    )
}

export default GamesListPage;