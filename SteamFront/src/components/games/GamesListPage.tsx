import {Flex, Layout, Card } from "antd";
import './style/style.css';
import {useEffect, useState} from "react";
import { IGameItem } from "./types.ts";
import { ImageSizes } from "../../config/config.ts";
import http_common from "../../api/http_common.ts";

const { Content } = Layout;

const GamesListPage = () => {

    const [data, setData] = useState<IGameItem[]>([]);

    useEffect(() => {
        fetchGames();
    }, []);

    const fetchGames = () => {
        http_common.get<IGameItem[]>("/api/Games/List")
            .then(resp => {
                setData(resp.data);
            });
    }

    const handleGameClick = (gameItem: IGameItem) => {
        console.log("game click", gameItem);
    };

    return(
        <Layout style={{ background: 'linear-gradient(to bottom, rgba(42,71,94,1), rgba(27, 40, 56, 0.1))' }}>
                <div className="card" >
                    <Flex  align='center' style={{ height: '100%' }}>
                        <img className="game-card-image" alt="ff"
                             src="https://encrypted-tbn0.gstatic.com/images?q=tbn:ANd9GcTJANR1OZtB3WTef1I80MZGu8t93BEfj5BfXg&s"/>
                        <Flex justify='space-between' align='center' style={{width:'400px', padding:'10px'}}>
                            <Flex vertical justify='space-between'>
                                <p className="card-title">Rocket League</p>
                                <p className="card-categories">Cars, Road, Simulator...</p>
                            </Flex>
                            <p className="card-price">22$</p>
                        </Flex>
                    </Flex>
                </div>
            <Content className="games-container">
                <Flex vertical justify='space-around' align='center' gap={50}>
                {data && data.length > 0 && data.map((item) => (
                        <div key={item.id}>
                            <div className="games-card-list">
                                <div>
                                    <Flex vertical justify='space-around' align='center' gap={25}>
                                        <div className="game-card"
                                             onClick={() => handleGameClick(item)}>
                                            <Card size='small'>
                                                <Flex justify='space-around' align='center'>
                                                    <img className="game-card-image" alt={item.name}
                                                         src={`http://localhost:5002/images/${ImageSizes.extraLarge}_${item.images[0].name}`}/>
                                                    <Flex vertical justify='space-around'>
                                                        <p>{item.name}</p>
                                                        <p>Categories...</p>
                                                    </Flex>
                                                    <p>{item.price}</p>
                                                </Flex>
                                            </Card>
                                        </div>
                                    </Flex>
                                </div>
                                <div>
                                    {/* Інший контент */}
                                </div>
                            </div>
                        </div>
                    ))}
                </Flex>
            </Content>
        </Layout>
    )
}

export default GamesListPage;