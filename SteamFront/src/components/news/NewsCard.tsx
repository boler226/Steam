import { INewsItem } from "./types.ts";
import { ImageSizes } from "../../config/imageSizes.ts";
import { Card, Flex, Typography } from 'antd';
import './style/NewsCard.css';

const { Text } = Typography;

const NewsCard = ({ title, description, dateOfRelease, image, game }: INewsItem) => (
    <Card hoverable styles={{ body: { padding: 0, overflow: 'hidden'} }}>
        <Flex className="news-card"  justify="space-between">
            <Flex vertical align="flex-start" justify="space-between" style={{padding:'32px'}}>
                <Typography.Title className="news-titel" level={2}>
                    {title}
                </Typography.Title>
                <Text className="news-description">
                    {description}
                </Text>

                <Text>Дата випуску: {new Date(dateOfRelease).toLocaleDateString()}</Text>
                <Text>Гра: {game.name}</Text>
            </Flex>

            <img className="news-card-image" alt={title} src={`http://localhost:5002/images/${ImageSizes.extraLarge}_${image}`}/>
        </Flex>
    </Card>
);

export default NewsCard;