import { Card } from "antd";
import { Col } from "antd";
import { INewsItem } from "./types.ts";
import { ImageSizes } from "../../config/imageSizes.ts";

const NewsCard = ({ id, title, description, dateOfRelease, image, videoURL, game }: INewsItem) => (

    <Col span={6} key={id}>
        <Card
            hoverable
            cover={<img alt={title} src={`http://localhost:5002/images/${ImageSizes.extraLarge}_${image}`} />}
        >
            <Card.Meta title={title} description={description} />
            <p>Дата випуску: {new Date(dateOfRelease).toLocaleDateString()}</p>
            <p>Гра: {game.name}</p>
            <a href={videoURL} target="_blank" rel="noopener noreferrer">Дивитися відео</a>
        </Card>
    </Col>
);

export default NewsCard;