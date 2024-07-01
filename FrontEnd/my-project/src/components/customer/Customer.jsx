import React, { useState, useEffect } from "react";
import axios from "axios";
import { Row, Col, Table, Modal, Form, Input, Button, Popconfirm, message, DatePicker, InputNumber, Checkbox } from "antd";
import { EditOutlined, DeleteOutlined, PlusCircleOutlined , ShoppingCartOutlined} from '@ant-design/icons';
import Card from "../../components/card";
import { commonAPI } from "../../api/common.api";
import { toast } from "react-toastify";
import moment from 'moment';
import Footer from '../footer/FooterHomePage';
import Navbar from '../navbar/Navbar';
import { useNavigate } from "react-router-dom";
const Customer = () => {
    const navigate = useNavigate();
    const [Customer, setCustomer] = useState([]);
    const [loading, setLoading] = useState(true);
    const [searchId, setSearchId] = useState("");
    const [isModalOpen, setIsModalOpen] = useState(false);
    const [editData, setEditData] = useState(null);
    const [isModalOpenReturn, setIsModalOpenReturn] = useState(false);

    useEffect(() => {
        fetchCustomer();
    }, []);

    const fetchCustomer = async () => {
        try {
            const response = await commonAPI.getAPI("Customer/getall");
            setCustomer(response.data);
            setLoading(false);
        } catch (error) {
            console.error("Error fetching Customer:", error);
            setLoading(false);
        }
    };

    const handleSearch = async () => {
        try {
            const token = localStorage.getItem("token");
            if (!token) {
                throw new Error("No token found. Please log in.");
            }

            if (searchId.trim() === "") {
                fetchCustomer();
            } else {
                const response = await commonAPI.getAPI(`Customer/searchcustomer?lastName=${searchId}`);
                setCustomer([response.data]);
                setLoading(false);
            }
        } catch (error) {
            setCustomer([]);
            console.error("Error searching Customer by ID:", error);
            setLoading(false);
        }
    };


    const handleAddCustomer = async (values) => {
        try {
            const token = localStorage.getItem("token");
            if (!token) {
                throw new Error("No token found. Please log in.");
            }
            await commonAPI.postAPI("Customer/register", values);
            setIsModalOpen(false);
            fetchCustomer();
            message.success("Customer added successfully");
        } catch (error) {
            console.error("Error adding Customer:", error);
            message.error("Failed to add Customer");
        }
    };


    const handleEditCustomer = async (values) => {
        try {

            const token = localStorage.getItem("token");
            if (!token) {
                throw new Error("No token found. Please log in.");
            }
            editData.firstName = values.firstName;
            editData.lastName = values.lastName;
            editData.email = values.email;
            editData.phoneNumber = values.phoneNumber;
            editData.address = values.address;

            // backend đang return no content 
            await commonAPI.putAPI(`Customer/update/${editData.customerId}`, editData);
            setIsModalOpen(false);
            fetchCustomer();
            message.success("Customer updated successfully");
        } catch (error) {
            console.error("Error updating Customer:", error);
            message.error("Failed to update Customer");
        }
    };


    const handleDeleteCustomer = async (displayId) => {
        try {
            const token = localStorage.getItem("token");
            if (!token) {
                throw new Error("No token found. Please log in.");
            }

            await commonAPI.deleteAPI(`Customer/delete/${displayId}`);
            fetchCustomer();
            message.success("Customer deleted successfully");
        } catch (error) {
            console.error("Error deleting Customer:", error);
            message.error("Failed to delete Customer");
        }
    };

    const handleEditModalOpen = (record) => {
        if (!record) return;
        var cloneRecord = JSON.parse(JSON.stringify(record));
        setEditData(cloneRecord);
        setIsModalOpen(true);
    };

    const handleAddProductReturnModel = (record) => {
        if (!record) return;

        var cloneRecord = JSON.parse(JSON.stringify(record));
        setEditData(cloneRecord);
        setIsModalOpenReturn(true);
    };

    const handleAddProductReturn = async (values) => {
        try {
            const token = localStorage.getItem("token");
            if (!token) {
                throw new Error("No token found. Please log in.");
            }
            await commonAPI.postAPI("Product/addReturnProduct", values);
            setIsModalOpenReturn(false);
            fetchCustomer();
            message.success("Customer added successfully");
        } catch (error) {
            console.error("Error adding Customer:", error);
            message.error("Failed to add Customer");
        }
    };

    const handleClickBtnAdd = () => {
        setEditData(null);
        setIsModalOpen(true);
    }

    const viewCart = (record) => {
        navigate(`/cart/${record.customerId}`);
    }

    const columns = [
        {
            title: "CustomerID",
            dataIndex: "customerId",
            key: "customerId",
        },
        {
            title: "FirstName",
            dataIndex: "fistName",
            key: "fistName",
        },
        {
            title: "LastName",
            dataIndex: "lastName",
            key: "lastName",
        },
        {
            title: "Email",
            dataIndex: "email",
            key: "email",
        },
        {
            title: "PhoneNumber",
            dataIndex: "phoneNumber",
            key: "phoneNumber",
        },
        {
            title: "Address",
            dataIndex: "address",
            key: "address",
        },
        {
            title: "Action",
            dataIndex: "action",
            key: "action",
            align: "center",
            render: (text, record) => (
                <span style={{ display: "flex", justifyContent: "center" }}>
                    <Button
                        type="link"
                        icon={<ShoppingCartOutlined />}
                        onClick={() => viewCart(record)}
                        style={{ marginRight: 8 }}
                    >
                        View Cart
                    </Button>
                    <Button
                        type="link"
                        icon={<PlusCircleOutlined />}
                        onClick={() => handleAddProductReturnModel(record)}
                        style={{ marginRight: 8 }}
                    >
                        Add Product Return
                    </Button>
                    <Button
                        type="link"
                        icon={<EditOutlined />}
                        onClick={() => handleEditModalOpen(record)}
                        style={{ marginRight: 8 }}
                    >
                        Edit
                    </Button>
                    <Popconfirm
                        title="Are you sure delete this Customer?"
                        onConfirm={() => handleDeleteCustomer(record.customerId)}
                        okText="Yes"
                        cancelText="No"
                    >
                        <Button type="link" icon={<DeleteOutlined />} danger>
                            Delete
                        </Button>
                    </Popconfirm>
                </span>
            ),
        },
    ];

    return (
        <div className="duration-200 bg-white dark:bg-gray-900 dark:text-white">
            < Navbar />
            <div className="min-h-[550px] flex justify-center items-center py-12 sm:py-0">
                <div className="container">
                    <Card extra="w-full sm:overflow-auto p-4">
                        <header className="relative flex items-center justify-between">
                            <div className="text-xl font-bold text-navy-700 dark:text-white">
                                Customer Display
                            </div>
                            <div className="flex items-center">
                                <input
                                    type="text"
                                    value={searchId}
                                    onChange={(e) => setSearchId(e.target.value)}
                                    placeholder="Search by Last Name"
                                    className="px-3 py-2 border rounded-lg me-2"
                                />
                                <button
                                    type="button"
                                    className="rounded-lg bg-gradient-to-br from-purple-600 to-blue-500 px-3 py-2.5 text-center text-sm font-medium text-white me-2 hover:bg-gradient-to-bl focus:outline-none focus:ring-4 focus:ring-blue-300 dark:focus:ring-blue-800"
                                    onClick={handleSearch}
                                >
                                    Search
                                </button>
                                <button
                                    type="button"
                                    className="rounded-lg bg-gradient-to-br from-purple-600 to-blue-500 px-3 py-2.5 text-center text-sm font-medium text-white me-2 hover:bg-gradient-to-bl focus:outline-none focus:ring-4 focus:ring-blue-300 dark:focus:ring-blue-800"
                                    onClick={handleClickBtnAdd}
                                >
                                    Add Customer
                                </button>
                            </div>
                        </header>
                        <Table
                            dataSource={Customer}
                            columns={columns}
                            loading={loading}
                            rowKey={(record) => record.displayId}
                        />

                        {/* Add/Edit Gold Price Modal */}
                        <Modal
                            title={editData ? "Edit Customer" : "Add Customer"}
                            visible={isModalOpen}
                            onCancel={() => setIsModalOpen(false)}
                            footer={null}
                        >
                            <CustomerForm
                                onFinish={editData ? handleEditCustomer : handleAddCustomer}
                                initialValues={editData}
                            />
                        </Modal>
                        <Modal
                            title={"Add Product Return"}
                            visible={isModalOpenReturn}
                            onCancel={() => setIsModalOpenReturn(false)}
                            footer={null}
                        >
                            <AddProductReturn
                                onFinish={handleAddProductReturn}
                                initialValues={editData}
                            />
                        </Modal>
                    </Card>
                </div>
            </div>
            <Footer />
        </div >

    );
};

const CustomerForm = ({ onFinish, initialValues }) => {
    const [form] = Form.useForm();

    useEffect(() => {
        form.resetFields();
        if (initialValues) {
            form.setFieldsValue(initialValues);
        }
    }, [initialValues, form]);

    const handleSubmit = async () => {
        try {

            const values = await form.validateFields();
            values.password = "abc123abc";
            onFinish(values);
            form.resetFields();
        } catch (error) {
            console.error("Validation failed:", error);
        }
    };

    return (
        <Form form={form} onFinish={handleSubmit}>
            <Form.Item
                name="firstName"
                labelCol={{ style: { width: '100px', display: 'flex' } }}
                label="First Name"
                rules={[{ required: true, message: "Please enter FirstName" }]}
            >
                <Input placeholder="Enter FirstName" />
            </Form.Item>
            <Form.Item
                name="lastName"
                labelCol={{ style: { width: '100px', display: 'flex' } }}
                label="Last Name"
                rules={[{ required: true, message: "Please enter LastName" }]}
            >
                <Input placeholder="Enter FirstName" />
            </Form.Item>
            <Form.Item
                name="email"
                labelCol={{ style: { width: '100px', display: 'flex' } }}
                label="Email"
                rules={[{ required: true, message: "Please enter Email" }, { max: 100, message: 'Email must be at most 20 characters' }]}
            >
                <Input placeholder="Enter Email" />
            </Form.Item>
            <Form.Item
                name="phoneNumber"
                labelCol={{ style: { width: '100px', display: 'flex' } }}
                label="PhoneNumber"
                rules={[
                    { min: 10, max: 10, message: 'PhoneNumber must be exactly 20 characters' }]}
            >
                <Input placeholder="Enter PhoneNumber" />
            </Form.Item>
            <Form.Item
                name="address"
                labelCol={{ style: { width: '100px', display: 'flex' } }}
                label="Address"

            >
                <Input placeholder="Enter Address" />
            </Form.Item>
            <Form.Item>
                <Button type="primary" htmlType="submit">
                    {initialValues ? "Update" : "Add"}
                </Button>
            </Form.Item>
        </Form>
    );
};


const AddProductReturn = ({ onFinish, initialValues }) => {
    const [form] = Form.useForm();

    useEffect(() => {
        form.resetFields();
        if (initialValues) {
            form.setFieldsValue(initialValues);
        }
    }, [initialValues, form]);

    const handleSubmit = async () => {
        try {

            const values = await form.validateFields();
            values.returnDate = Date.now;
            onFinish(values);
            form.resetFields();
        } catch (error) {
            console.error("Validation failed:", error);
        }
    };

    return (
        <Form form={form} onFinish={handleSubmit}>
            <Form.Item
                name="productId"
                labelCol={{ style: { width: '100px', display: 'flex' } }}
                label="ProductID"
                
            >
                <Input placeholder="Enter ProductID" />
            </Form.Item>

            <Form.Item
                name="productName"
                labelCol={{ style: { width: '100px', display: 'flex' } }}
                label="ProductName"
                rules={[{ required: true, message: "Please enter ProductName" }, { max: 100, message: 'ProductName must be at most 20 characters' }]}
            >
                <Input placeholder="Enter ProductName" />
            </Form.Item>
            <Form.Item
                name="warranty"
                labelCol={{ style: { width: '100px', display: 'flex' } }}
                label="Warranty"
                rules={[{ required: true, message: "Please enter Warranty" }]}
            >
                <Input placeholder="Enter Warranty" />
            </Form.Item>

            <Row gutter={16}>
                <Col span={12}>
                    <Form.Item
                        name="weight"
                        labelCol={{ style: { width: '100px', display: 'flex' } }}
                        label="Weight"
                        rules={[{ required: true, message: "Please enter Weight" }]}
                    >
                        <InputNumber placeholder="Enter Weight" />
                    </Form.Item>
                </Col>
                <Col span={12}>
                    <Form.Item
                        name="price"
                        labelCol={{ style: { width: '100px', display: 'flex' } }}
                        label="Price"
                        rules={[{ required: true, message: "Please enter Price" }]}
                    >
                        <InputNumber placeholder="Enter Price" />
                    </Form.Item>
                </Col>
            </Row>
            <Row gutter={16}>
                <Col span={12}>
                    <Form.Item
                        name="quantity"
                        labelCol={{ style: { width: '100px', display: 'flex' } }}
                        label="Quantity"
                        rules={[{ required: true, message: "Please enter Quantity" }]}
                    >
                        <InputNumber placeholder="Enter Quantity" />
                    </Form.Item>
                </Col>
                <Col span={12}>
                    <Form.Item
                        name="categoryId"
                        labelCol={{ style: { width: '100px', display: 'flex' } }}
                        label="CategoryId"
                        rules={[{ required: true, message: "Please enter CategoryId" }]}
                    >
                        <InputNumber placeholder="Enter CategoryId" />
                    </Form.Item>
                </Col>
            </Row>
            <Row gutter={16}>
                <Col span={12}>
                    <Form.Item
                        name="stoneCost"
                        labelCol={{ style: { width: '100px', display: 'flex' } }}
                        label="StoneCost"
                        rules={[{ required: true, message: "Please enter StoneCost" }]}
                    >
                        <InputNumber placeholder="Enter StoneCost" />
                    </Form.Item>
                </Col>
                <Col span={12}>
                    <Form.Item
                        name="storeId"
                        labelCol={{ style: { width: '100px', display: 'flex' } }}
                        label="StoreId"
                        rules={[{ required: true, message: "Please enter StoreId" }]}
                    >
                        <InputNumber placeholder="Enter StoreId" />
                    </Form.Item>
                </Col>
            </Row>
            <Form.Item
                name="location"
                labelCol={{ style: { width: '100px', display: 'flex' } }}
                label="Location"
                rules={[{ required: true, message: "Please enter Location" }]}
            >
                <Input placeholder="Enter Location" />
            </Form.Item>
            <Form.Item
                name="image"
                labelCol={{ style: { width: '100px', display: 'flex' } }}
                label="Image"
                rules={[{ required: true, message: "Please enter Image" }]}
            >
                <Input placeholder="Enter Image" />
            </Form.Item>
            <Row gutter={16}>
                <Col span={12}>
                    <Form.Item
                        name="returnReason"
                        labelCol={{ style: { width: '100px', display: 'flex' } }}
                        label="ReturnReason"
                    >
                        <Input placeholder="Enter ReturnReason" />
                    </Form.Item>
                </Col>
                <Col span={12}>
                    <Form.Item
                        name="barcode"
                        labelCol={{ style: { width: '100px', display: 'flex' } }}
                        label="Barcode"
                        rules={[
                            { max: 50, message: 'Barcode cannot exceed 50 characters.' }]}
                    >
                        <Input placeholder="Enter Barcode" />
                    </Form.Item>
                </Col>
            </Row>



            <Row gutter={16}>
                <Col span={12}>
                    <Form.Item
                        name="manufacturingCost"
                        labelCol={{ style: { width: '100px', display: 'flex' } }}
                        label="Manufacturing"
                    >
                        <InputNumber placeholder="Enter ManufacturingCost" />
                    </Form.Item>
                </Col>
                <Col span={12}>
                    <Form.Item
                        name="isBuyback"
                        valuePropName="checked"
                        label="IsBuyback"
                        labelCol={{ style: { width: '80px', display: 'flex' } }}
                    >
                        <Checkbox></Checkbox>
                    </Form.Item>
                </Col>
            </Row>


            <Form.Item>
                <Button type="primary" htmlType="submit">
                    {"Add"}
                </Button>
            </Form.Item>
        </Form>
    );
};
export default Customer;

