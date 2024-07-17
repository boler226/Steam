import {Input, Space} from "antd";
import {SearchOutlined} from "@ant-design/icons";
import { Link } from 'react-router-dom';

const SiteRouter = () => {

    return (
        <div className="site-router">
            <Space style={{display: 'flex', justifyContent: 'space-between', alignItems: 'center'}}>
                <Space>
                    <Link to="/"><button className="button-router">Нові й варті уваги</button></Link>
                    <Link to="/news"><button className="button-router">Новини</button></Link>
                    <button className="button-router">Категорії</button>
                </Space>
                <Input
                    placeholder="пошук"
                    prefix={<SearchOutlined/>}
                    style={{width: 200, height: '30px', background: '#316282', border: 'none', color: '#C5C3C0'}}
                />
            </Space>
        </div>
    )
}

export default SiteRouter;