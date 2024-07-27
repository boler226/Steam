import {Button, Flex, Form, Input, Layout, Typography, Spin} from "antd";
import {ILogin} from "../../../interfaces/account";
import {useState} from "react";
import {Status} from "../../../enums";
import {useNavigate} from "react-router-dom";
import http_common from "../../../api/http_common.ts";

const { Content } = Layout;
const { Text } = Typography;

const UserLoginPage = () => {
    const [form] = Form.useForm<ILogin>();
    const [status, setStatus] = useState<Status>(Status.IDLE);
    const [formValues, setFormValues] = useState<Partial<ILogin>>({});
    const [error, setError] = useState<string | null>(null);

    const navigate = useNavigate();

    const onFinish = async (values: ILogin) => {
        try {
            setStatus(Status.LOADING);
            console.log("Submit form", values);

            const formData = new FormData();
            formData.append("Email", values.Email);
            formData.append("Password", values.Password);

            const response = await http_common.post("/api/Account/SignIn", formData, {
                headers: {
                    "Content-Type": "multipart/form-data"
                }

            });
            localStorage.setItem('token', response.data.accessToken);
            navigate(`/`);
        }
        catch  {
            setError("Будь ласка, перевірте логін і пароль свого акаунта, а потім спробуйте знову.");
        }
        finally {
            setStatus(Status.IDLE);
        }
    }

    const handleFormChange =(allValues: any) => {
        setFormValues({...formValues, ...allValues});
    }

    return(
        <Spin spinning={status === Status.LOADING}>
            <Content style={{ minHeight: '600px'}}>
                <Flex justify="space-around" align="center">
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
                            label="Адреса електронної пошти"
                            name="Email"
                            rules={[
                                {required: true, message: 'Це поле є обов\'язковим!'},
                                {type: 'email', message: 'Неправильний формат електронної пошти!'}
                            ]}
                            className="custom-label"
                        >
                            <Input autoComplete="Email"/>
                        </Form.Item>

                        <Form.Item
                            label="Пароль"
                            name="Password"
                            rules={[
                                {required: true, message: 'Це поле є обов\'язковим!'},
                                {min: 6, message: 'Пароль повинен містити мінімум 6 символів!'}
                            ]}
                            className="custom-label"
                        >
                            <Input.Password autoComplete="Password"/>
                        </Form.Item>

                        <Text type="danger">{error}</Text>

                        <Form.Item>
                            <Button type="primary" htmlType="submit"
                                    style={{width: '140px', height: '40px', marginTop: '20px'}}>
                                Увійти
                            </Button>
                        </Form.Item>
                    </Form>
                </Flex>
            </Content>
        </Spin>
    )
}

export default UserLoginPage;