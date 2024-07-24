import {Form, Spin, Button, Input, message, Select, Upload, Flex, InputNumber } from "antd";
import {IGameCreate} from "../../../interfaces/games";
import {useEffect, useState} from "react";
import {Status} from "../../../enums";
import {ICategoryName} from "../../../interfaces/categories";
import {useNavigate} from "react-router-dom";
import http_common from "../../../api/http_common.ts";
import TextArea from "antd/es/input/TextArea";
import {imageConverterToFileArray} from "../../../config/converter.ts";
import {DownloadOutlined} from "@ant-design/icons";
import type { UploadChangeParam } from 'antd/es/upload';
import "./style/style.css";


const GameCreatePage = () => {
    const [form] = Form.useForm<IGameCreate>();
    const [status] = useState<Status>(Status.IDLE);
    //const [imageUrl, setImageUrl] = useState<string | undefined>();
    const [category, setCategory] = useState<ICategoryName[]>();
    const [formValues, setFormValues] = useState<Partial<IGameCreate>>({});
    const [imageFile, setImageFile] = useState<File[] | null>(null);

    const navigate = useNavigate();
    const [messageApi, contextHolder] = message.useMessage();

    useEffect(() => {
        const fetchCategories = async () => {
            try {
                const resp = await http_common.get<ICategoryName[]>("/api/Category");
                setCategory(resp.data);
            } catch (error) {
                console.error("Failed to fetch categories:", error);
            }
        };
        fetchCategories();
    }, []);

    const onFinish = async (values: IGameCreate) => {
      try
      {
          console.log("Submit form", values);

          const formData = new FormData();
          formData.append("name", values.name);
          formData.append("price", values.price.toString());
          formData.append("description", values.description);
          formData.append("systemRequirements", values.systemRequirements);
          formData.append("categories", values.categories.toString());

          if (imageFile)
          {
              imageFile.forEach(file => formData.append("images", file));
          }

          await http_common.post("/api/Games/Create", formData,{
              headers: {
                  "Content-Type": "multipart/form-data"
              }
          });
          navigate(`/`);
      }
      catch (error)
      {
          console.log("error", error);
          console.log(messageApi.error);
      }
    }

    const handleFormChange = (allValues: any) => {
        setFormValues({...formValues, ...allValues});
    };

    const handleUploadChange = (info: UploadChangeParam) => {
        const files = info.fileList.map(file => file.originFileObj as File).filter((file): file is File => !!file);
        setImageFile(files);
    };

    return(
        <Spin spinning={status === Status.LOADING}>
            <Flex justify="space-around" align="start">
                {contextHolder}
                <Form
                    form={form}
                    onFinish={onFinish}
                    onValuesChange={handleFormChange}
                    layout="vertical"
                    style={{
                        width: '500px',
                        display: 'flex',
                        flexDirection: 'column',
                        justifyContent: 'center',
                        padding: 20,
                    }}
                >
                    <Form.Item
                        label="Назва"
                        name="name"
                        htmlFor="name"
                        rules={[
                            {required: true, message: 'Це поле є обов\'язковим!'},
                            {min: 3, message: 'Назва повинна містити мінімум 3 символи!'}
                        ]}
                        className="custom-label"
                    >
                        <Input autoComplete="name"/>
                    </Form.Item>

                    <Form.Item
                        label="Ціна"
                        name="price"
                        htmlFor="price"
                        className="custom-label"
                    >
                        <InputNumber/>
                    </Form.Item>

                    <Form.Item
                        label="Опис"
                        name="description"
                        htmlFor="description"
                        rules={[
                            {required: true, message: 'Це поле є обов\'язковим!'},
                            {min: 10, message: 'Опис повинен містити мінімум 10 символів!'}
                        ]}
                        className="custom-label"
                    >
                        <TextArea style={{ minHeight: '400px'}} />
                    </Form.Item>

                    <Form.Item
                        label="Системні вимоги"
                        name="systemRequirements"
                        htmlFor="systemRequirements"
                        rules={[
                            {required: true, message: 'Це поле є обов\'язковим!'},
                        ]}
                        className="custom-label"
                    >
                        <TextArea style={{ minHeight: '200px'}} />
                    </Form.Item>

                    <Form.Item
                        label="Категорії"
                        name="categories"
                        rules={[{ required: true, message: "Це поле є обов'язковим!" }]}
                    >
                        <Select
                            placeholder="Оберіть категорії"
                            mode="multiple"
                            options={category?.map(g => ({
                                value: g.id,
                                label: g.name,
                            }))}
                        />
                    </Form.Item>

                    <Form.Item
                        label="Фото"
                        name="images"
                        htmlFor="images"
                        valuePropName="file"
                        getValueFromEvent={imageConverterToFileArray}
                        rules={[
                            {required: true, message: 'Це поле є обов\'язковим!'},
                        ]}
                        className="custom-label"
                    >
                        <Upload
                            listType="picture-card"
                            maxCount={10}
                            multiple
                            showUploadList={{ showPreviewIcon: false }}
                            beforeUpload={() => false}
                            accept="image/*"
                            onChange={handleUploadChange}
                        >
                            <Button type="primary" shape="round" icon={<DownloadOutlined/>}/>
                        </Upload>
                    </Form.Item>

                    <Form.Item>
                        <Button type="primary" htmlType="submit" style={{ width: '140px', height: '40px', marginTop: '20px'}}>
                            Створити
                        </Button>
                    </Form.Item>
                </Form>
            </Flex>
        </Spin>
    )
}

export default GameCreatePage;